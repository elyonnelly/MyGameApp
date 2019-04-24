namespace Assets.Scripts
{
    public class EventAggregator
    {
        // часть событий происходит в UI части, часть в GameLogic

        public delegate void ChangeActiveFairy(int position, string name);

        public delegate void ChangeActiveSpell(int fairyPosition, int spellPosition, string name);

        public delegate void PlayerWin();

        public static event ChangeActiveFairy DisableFairy;
        public static event ChangeActiveFairy ActivateFairy;

        public static event ChangeActiveSpell RemoveSpell;
        public static event ChangeActiveSpell AddSpell;

        public static event PlayerWin PlayerWon;

        public static void PublishFairyDeactivation(int position, string name)
        {
            DisableFairy?.Invoke(position, name);
        }

        public static void PublishFairyActivation(int position, string name)
        {
            ActivateFairy?.Invoke(position, name);
        }

        public static void PublishRemovalSpell(int fairyPosition, int spellPosition, string name)
        {
            RemoveSpell?.Invoke(fairyPosition, spellPosition, name);
        }

        public static void PublishAddingSpell(int fairyPosition, int spellPosition, string name)
        {
            AddSpell?.Invoke(fairyPosition, spellPosition, name);
        }

        public static void OnPlayerWon()
        {
            PlayerWon?.Invoke();
        }
    }
}