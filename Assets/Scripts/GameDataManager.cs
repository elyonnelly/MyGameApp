using System;
using System.Collections.Generic;
using System.IO;
using Assets.Scripts.GameLogic;
using Assets.Scripts.GameLogic.DataModels;
using Newtonsoft.Json;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts
{
    public class GameDataManager : MonoBehaviour
    {
        public static GameDataManager Instance;

        private readonly Random randomizer = new Random();
        public Player PlayerData { set; get; }
        public Player EnemyData { set; get; }

        private void Awake()
        {
            Instance?.Destroy();
            Instance = this;

            DontDestroyOnLoad(gameObject);

            Initialize();
        }

        private void Destroy()
        {
            Destroy(gameObject);
        }

        private void Initialize()
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

            EnemyData = new Player {ActiveFairies = new List<Fairy>()};
            var fairies = new string[DataOfModels.Fairies.Count];
            DataOfModels.Fairies.Keys.CopyTo(fairies, 0);
            var middleLevel = 0;

            for (var i = 0; i < 5; i++)
            {
                middleLevel += PlayerData.ActiveFairies[i].Level;
            }

            middleLevel /= 5;

            for (var i = 0; i < 5; i++)
            {
                EnemyData.ActiveFairies.Add((Fairy) DataOfModels.Fairies[fairies[randomizer.Next(fairies.Length)]].Clone());
                var fairy = EnemyData.ActiveFairies[i];
                fairy.Level = randomizer.Next(Math.Max(1, middleLevel - 2), middleLevel + 2);
                fairy.Spells = new string[4];
                for (var j = 0; j < 4; j++)
                {
                    fairy.Spells[j] = "Empty Slot";
                }

                foreach (var spell in DataOfModels.OffensiveSpells.Values)
                {
                    if (spell.MajorElement != fairy.Element || !(Math.Ceiling(fairy.Level / 6.0) >= spell.Level))
                    {
                        continue;
                    }

                    if (fairy.Spells[0] == "Empty Slot")
                    {
                        fairy.Spells[0] = spell.Name;
                    }
                    else
                    {
                        fairy.Spells[2] = spell.Name;
                    }
                }

                foreach (var spell in DataOfModels.DefensiveSpells.Values)
                {
                    if (spell.MajorElement != fairy.Element || !(Math.Ceiling(fairy.Level / 6.0) >= spell.Level))
                    {
                        continue;
                    }

                    if (fairy.Spells[1] == "Empty Slot")
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