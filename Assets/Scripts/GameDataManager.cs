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
        private Guid guid = Guid.NewGuid();

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
            /*PlayerData = new Player();
            PlayerData.ActiveFairies = new System.Collections.Generic.List<Fairy>
            {
                (Fairy) DataOfModels.Fairies["Sillia"].Clone(),

                (Fairy) DataOfModels.Fairies["Corgot"].Clone(),
                (Fairy) DataOfModels.Fairies["Sirael"].Clone(),

                (Fairy) DataOfModels.Fairies["Tadana"].Clone(),

                (Fairy) DataOfModels.Fairies["Worgot"].Clone()

            };

            PlayerData.AllowFairies = new System.Collections.Generic.List<Fairy>();
            foreach (var fairy in DataOfModels.Fairies.Values)
            {
                PlayerData.AllowFairies.Add((Fairy)fairy.Clone());
            }


            EnemyData = new Player();
            EnemyData.ActiveFairies = new System.Collections.Generic.List<Fairy>
            {
                (Fairy) DataOfModels.Fairies["Sillia"].Clone()
            };
            EnemyData.AllowFairies = new System.Collections.Generic.List<Fairy>
            {
                (Fairy) DataOfModels.Fairies["Sillia"].Clone()
            };


            PlayerData.AllowOffensiveSpells = new System.Collections.Generic.List<string>();
            EnemyData.AllowOffensiveSpells = new System.Collections.Generic.List<string>();
            foreach (var spell in DataOfModels.OffensiveSpells.Values)
            {
                PlayerData.AllowOffensiveSpells.Add(spell.Name);
                EnemyData.AllowOffensiveSpells.Add(spell.Name);
            }
            PlayerData.AllowDefensiveSpells = new System.Collections.Generic.List<string>();
            EnemyData.AllowDefensiveSpells = new System.Collections.Generic.List<string>();
            foreach (var spell in DataOfModels.DefensiveSpells.Values)
            {
                PlayerData.AllowDefensiveSpells.Add(spell.Name);
                EnemyData.AllowDefensiveSpells.Add(spell.Name);
            }*/

            try
            {
                using (var reader = new StreamReader(@"player.json"))
                {
                    PlayerData = JsonConvert.DeserializeObject<Player>(reader.ReadLine());
                    /*foreach (var fairy in PlayerData.ActiveFairies)
                    {
                        fairy.HealthPoint = fairy.HitPoints * 8 + 11;
                    }
                    foreach (var fairy in PlayerData.AllowFairies)
                    {
                        fairy.HealthPoint = fairy.HitPoints * 8 + 11;
                    }
                    PlayerData.AllowOffensiveSpells = new System.Collections.Generic.List<string>();
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
                    /*foreach (var fairy in EnemyData.ActiveFairies)
                    {
                        fairy.HealthPoint = fairy.HitPoints * 8 + 11;
                    }
                    foreach (var fairy in EnemyData.AllowFairies)
                    {
                        fairy.HealthPoint = fairy.HitPoints * 8 + 11;
                    }
                    EnemyData.AllowOffensiveSpells = new System.Collections.Generic.List<string>();
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
