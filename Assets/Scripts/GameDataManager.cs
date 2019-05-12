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
            /*PlayerData = new Player();
            PlayerData.ActiveFairies = new System.Collections.Generic.List<Fairy>
            {
                (Fairy) DataOfModels.Fairies["Sillia"].Clone()
            };
            PlayerData.AllowFairies = new System.Collections.Generic.List<Fairy>
            {
                (Fairy) DataOfModels.Fairies["Sillia"].Clone()
            };
            PlayerData.AllowOffensiveSpells = new System.Collections.Generic.List<OffensiveSpell>();
            PlayerData.AllowDefensiveSpells = new System.Collections.Generic.List<DefensiveSpell>();


            EnemyData = new Player();
            EnemyData.ActiveFairies = new System.Collections.Generic.List<Fairy>
            {
                (Fairy) DataOfModels.Fairies["Sillia"].Clone()
            };
            EnemyData.AllowFairies = new System.Collections.Generic.List<Fairy>
            {
                (Fairy) DataOfModels.Fairies["Sillia"].Clone()
            };
            EnemyData.AllowOffensiveSpells = new System.Collections.Generic.List<OffensiveSpell>();
            EnemyData.AllowDefensiveSpells = new System.Collections.Generic.List<DefensiveSpell>();*/

            try
            {
                using (var reader = new StreamReader(@"player.json"))
                {
                    PlayerData = JsonConvert.DeserializeObject<Player>(reader.ReadLine());
                    /*PlayerData.AllowOffensiveSpells = new System.Collections.Generic.List<string>();
                    foreach (var spell in DataOfModels.OffensiveSpells.Values)
                    {
                        PlayerData.AllowOffensiveSpells.Add(spell.Name);
                    }
                    PlayerData.AllowDefensiveSpells = new System.Collections.Generic.List<string>();
                    foreach (var spell in DataOfModels.DefensiveSpells.Values)
                    {
                        PlayerData.AllowDefensiveSpells.Add(spell.Name);
                    }*/

                }
                using (var reader = new StreamReader(@"enemy.json"))
                {
                    EnemyData = JsonConvert.DeserializeObject<Player>(reader.ReadLine());
                    /*EnemyData.AllowOffensiveSpells = new System.Collections.Generic.List<string>();
                    foreach (var spell in DataOfModels.OffensiveSpells.Values)
                    {
                        EnemyData.AllowOffensiveSpells.Add(spell.Name);
                    }
                    EnemyData.AllowDefensiveSpells = new System.Collections.Generic.List<string>();
                    foreach (var spell in DataOfModels.DefensiveSpells.Values)
                    {
                        EnemyData.AllowDefensiveSpells.Add(spell.Name);
                    }*/
                }

            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }
        
    }
}
