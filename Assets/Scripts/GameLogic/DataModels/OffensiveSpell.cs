namespace Assets.Scripts.GameLogic.DataModels
{
    public class OffensiveSpell : Spell
    {
        public double Damage { get; }
        public int FireRate { get; }

        public OffensiveSpell(Element majorElement, int level, string name, int damage, int mana, int fireRate, Element minorElement = default) 
                            : base(majorElement, level, name, mana, minorElement)
        {
            Damage = damage;
            FireRate = fireRate;
        }
    }
}