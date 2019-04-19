using Assets.Scripts.GameLogic.DataModels;

namespace Assets.Scripts
{
    public class EventAggregator
    {
        // часть событий происходит в UI части, часть в GameLogic
        public delegate void AttackToFairy(Fairy attack, Fairy victim, OffensiveSpell spell);

        public static event AttackToFairy PlayerAttack;
        public static event AttackToFairy EnemyAttack;

        public delegate void ChangeFairy(int position, Fairy newFairy);

        public static event ChangeFairy AddActiveFairy;
        public static event ChangeFairy RemoveActiveFairy;

        public delegate void ChangeSpell(int fairyPosition, int spellPosition, Spell newSpell);

        public static event ChangeSpell AddActiveSpell;
        public static event ChangeSpell RemoveActiveSpell;

        public delegate void ChangeAciveFairy(string nameFairy);

        public static event ChangeAciveFairy FreedomFairy;

        public static void PublishFreedomFairy(string name)
        {
            FreedomFairy?.Invoke(name);

        }

        public delegate void PlayerWin();

        public static event PlayerWin PlayerWon;

        public static void OnPlayerWon()
        {
            PlayerWon?.Invoke();
        }
    }
}