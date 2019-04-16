namespace Assets.Scripts.GameLogic.DataModels
{
    public class Spell
    {
        public int Element { get; }
        public int MagicForCast { get; }
        public int Damage { get; }

        public static Spell Default
        {
            get
            {
                return new Spell();
            }
        }
    }
}