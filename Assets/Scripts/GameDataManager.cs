using System.Collections.Generic;
using Assets.Scripts.GameLogic;
using Assets.Scripts.GameLogic.DataModels;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Assets.Scripts
{

    public class GameDataManager :  Singleton<GameDataManager>
    {
        public Player PlayerData;
        public Player EnemyData;

        void Awake()
        {

            //инициализация Player'sов в DataLoadController
            DontDestroyOnLoad(gameObject);
            EventAggregator.DisableFairy += OnDisableFairy;
            EventAggregator.ActivateFairy += OnActivateFairy;
            EventAggregator.RemoveSpell += OnRemoveSpell;
            EventAggregator.AddSpell += OnAddSpell;
            EventAggregator.FairyAttack += OnFairyAttack;

            if (SceneManager.GetActiveScene().name == "Battlefield Scene")
            {
                InitializeBattleScene();
            }
        }

        void InitializeBattleScene()
        {
            //это временно?
            EnemyData = new Player();

            var activeFairies = new List<Fairy>
            {
                (Fairy)DataOfModels.Fairies["Abery"].Clone(),
                (Fairy)DataOfModels.Fairies["Sirael"].Clone(),
                (Fairy)DataOfModels.Fairies["Worgot"].Clone(),
                (Fairy)DataOfModels.Fairies["Manox"].Clone(),
                (Fairy)DataOfModels.Fairies["Beltaur"].Clone()
            };
            var allowFairies = new List<Fairy>();
            foreach (var fairy in DataOfModels.Fairies)
            {
                allowFairies.Add((Fairy)fairy.Value.Clone());
            }

            EnemyData.ActiveFairies = activeFairies;
            EnemyData.AllowFairies = allowFairies;
        }

        void Update()
        {

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

        public bool FairyActive(string name)
        {
            foreach (var fairy in PlayerData.ActiveFairies)
            {
                if (name == fairy.Name)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
