using Assets.Scripts.GameLogic;
using Assets.Scripts.GameLogic.DataModels;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameProcessManager : MonoBehaviour
    {

        private Player PlayerData;
        private Player EnemyData;
        void Start()
        {
            PlayerData = GameDataManager.Instance.PlayerData;
            EnemyData = GameDataManager.Instance.EnemyData;

            EventAggregator.DisableFairy += OnDisableFairy;
            EventAggregator.ActivateFairy += OnActivateFairy;
            EventAggregator.RemoveSpell += OnRemoveSpell;
            EventAggregator.AddSpell += OnAddSpell;
            EventAggregator.FairyAttack += OnFairyAttack;
        }


        void OnFairyAttack(int forwardFairyNumber, int victimFairyNumber, string spellName, string victim)
        {
            Debug.Log(spellName);

            if (victim == "Player")
            {
                var victimFairy = PlayerData.ActiveFairies[forwardFairyNumber];
                var forwardFairy = EnemyData.ActiveFairies[victimFairyNumber];
                var spell = DataOfModels.Spells[spellName];

                forwardFairy.AttackFairy(victimFairy, (OffensiveSpell)spell);
            }

            if (victim == "Enemy")
            {
                var victimFairy = EnemyData.ActiveFairies[forwardFairyNumber];
                var forwardFairy = PlayerData.ActiveFairies[victimFairyNumber];
                var spell = DataOfModels.Spells[spellName];

                forwardFairy.AttackFairy(victimFairy, (OffensiveSpell)spell);
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
