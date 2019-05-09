using System;
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
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
            
            DontDestroyOnLoad(gameObject);

            Initialize();

        }


        void Initialize()
        {
            try
            {
                using (var reader = new StreamReader(@"player.json"))
                {
                    PlayerData = JsonConvert.DeserializeObject<Player>(reader.ReadLine());
                    PlayerData.AllowDefensiveSpells = new System.Collections.Generic.List<DefensiveSpell>();
                    PlayerData.AllowOffensiveSpells = new System.Collections.Generic.List<OffensiveSpell>();
                    foreach (var spell in DataOfModels.DefensiveSpells.Values)
                    {
                        PlayerData.AllowDefensiveSpells.Add(spell);
                    }

                    foreach (var spell in DataOfModels.OffensiveSpells.Values)
                    {
                        PlayerData.AllowOffensiveSpells.Add(spell);
                    }
                }
                using (var reader = new StreamReader(@"enemy.json"))
                {
                    EnemyData = JsonConvert.DeserializeObject<Player>(reader.ReadLine());
                }

            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }
        
    }
}
