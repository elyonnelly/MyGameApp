using Assets.Scripts.GameLogic.DataModels;

namespace Assets.Scripts
{
    public class Attack
    {
        public int Forward { get; set; }
        public int Victim { get; set; }
        public Spell Spell { get; set; }

        public bool WasChoice { get; set; }

        public Attack(int forward, int victim, Spell spell)
        {
            Forward = forward;
            Victim = victim;
            Spell = spell;
            WasChoice = true;
        }

        public Attack()
        {
            WasChoice = false;
        }
    }
}