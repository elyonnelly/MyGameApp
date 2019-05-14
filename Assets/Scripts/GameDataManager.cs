using System;
using System.Collections.Generic;
using System.IO;
using Assets.Scripts.GameLogic;
using Assets.Scripts.GameLogic.DataModels;
using Newtonsoft.Json;
using UnityEngine;

namespace Assets.Scripts
{

    public class GameDataManager : MonoBehaviour
    {
        public static GameDataManager Instance;
        public Player PlayerData;
        public Player EnemyData;
        private Guid guid = Guid.NewGuid();
        private System.Random randomizer = new System.Random();

        private void Awake()
        {
            Instance?.Destroy();
            Instance = this;

            DontDestroyOnLoad(gameObject);

            Initialize();
        }

        void Destroy()
        {
            Destroy(gameObject);
        }

        void Initialize()
        {
            try
            {
                if (File.Exists(Application.persistentDataPath + "/player.json"))
                {
                    using (var reader = new StreamReader(Application.persistentDataPath + "/player.json"))
                    {
                        PlayerData = JsonConvert.DeserializeObject<Player>(reader.ReadLine());
                    }
                }
                else
                {
                    var player = Resources.Load<TextAsset>("player");
                    PlayerData = JsonConvert.DeserializeObject<Player>(player.text);

                    var file = File.Create(Application.persistentDataPath + "player.json");
                    using (var writer = new StreamWriter(Application.persistentDataPath + "/player.json"))
                    {
                        writer.Write(JsonConvert.SerializeObject(PlayerData));
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }

            EnemyData = new Player();
            EnemyData.ActiveFairies = new System.Collections.Generic.List<Fairy>();
            var fairies = new string[DataOfModels.Fairies.Count];
            DataOfModels.Fairies.Keys.CopyTo(fairies, 0);
            var middleLevel = 0;

            for (int i = 0; i < 5; i++)
            {
                middleLevel += PlayerData.ActiveFairies[i].Level;
            }
            middleLevel /= 5;

            for (int i = 0; i < 5; i++)
            {
                EnemyData.ActiveFairies.Add((Fairy)DataOfModels.Fairies[fairies[randomizer.Next(73)]].Clone());
                var fairy = EnemyData.ActiveFairies[i];
                fairy.Level = randomizer.Next(middleLevel - 2, middleLevel + 1);
                fairy.Spells = new string[4];
                for (int j = 0; j < 4; j++)
                {
                    fairy.Spells[j] = "Empty Slot";
                }
                foreach (var spell in DataOfModels.OffensiveSpells.Values)
                {
                      
                    if ((spell.MajorElement == fairy.Element || spell.MinorElement == fairy.Element) && fairy.Level / 4.0 <= spell.Level)
                    {
                        if (fairy.Spells[0] == "Empty Slot")
                        {
                            fairy.Spells[0] = spell.Name;
                        }
                        else
                        {
                            fairy.Spells[2] = spell.Name;
                        }
                    }
                }

                foreach (var spell in DataOfModels.DefensiveSpells.Values)
                {
                    if ((spell.MajorElement == fairy.Element || spell.MinorElement == fairy.Element) && fairy.Level / 4.0 <= spell.Level)
                    {
                        if (fairy.Spells[1] == null)
                        {
                            fairy.Spells[1] = spell.Name;
                        }
                        else
                        {
                            fairy.Spells[3] = spell.Name;
                        }
                    }
                }
            }

        }


    }
}
