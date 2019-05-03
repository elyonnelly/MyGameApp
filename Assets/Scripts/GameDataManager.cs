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

            EventAggregator.DisableFairy += OnDisableFairy;
            EventAggregator.ActivateFairy += OnActivateFairy;
            EventAggregator.RemoveSpell += OnRemoveSpell;
            EventAggregator.AddSpell += OnAddSpell;
            EventAggregator.FairyAttack += OnFairyAttack;

        }


        void Initialize()
        {
            try
            {
                using (var reader = new StreamReader(@"player.json"))
                {
                    PlayerData = JsonConvert.DeserializeObject<Player>(reader.ReadLine());
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
        
        void OnFairyAttack(int forwardFairyNumber, int victimFairyNumber, string spellName, string victim)
        {
            if (victim == "Player")
            {
                var victimFairy = PlayerData.ActiveFairies[forwardFairyNumber];
                var forwardFairy = EnemyData.ActiveFairies[victimFairyNumber];
                var spell = DataOfModels.Spells[spellName];

                forwardFairy.AttackFairy(victimFairy, (OffensiveSpell) spell);
            }

            if (victim == "Enemy")
            {
                var victimFairy = EnemyData.ActiveFairies[forwardFairyNumber];
                var forwardFairy = PlayerData.ActiveFairies[victimFairyNumber];
                var spell = DataOfModels.Spells[spellName];

                forwardFairy.AttackFairy(victimFairy, (OffensiveSpell)spell);
                Debug.Log(victimFairy.Name + victimFairy.GetState());
            }

        }

        void OnRemoveSpell(int fairyPosition, int spellPosition, string name)
        {
            PlayerData.ActiveFairies[fairyPosition].Spells[spellPosition] = new Spell("Empty Slot");
        }

        void OnAddSpell(int fairyPosition, int spellPosition, string name)
        {
            PlayerData.ActiveFairies[fairyPosition].Spells[spellPosition] = (Spell)DataOfModels.Spells[name].Clone();
        }

        void OnDisableFairy(int position, string name)
        {
            PlayerData.ActiveFairies[position] = new Fairy();
        }

        void OnActivateFairy(int position, string name)
        {
            PlayerData.ActiveFairies[position] = (Fairy)(DataOfModels.Fairies[name]).Clone();
        }

    }
}
