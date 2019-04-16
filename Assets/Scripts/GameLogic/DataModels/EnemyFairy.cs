namespace Assets.Scripts.GameLogic.DataModels
{
    public class EnemyFairy : Fairy
    {
        public override void AttackFairy(Fairy victim, Spell spell)
        {
            throw new System.NotImplementedException();
        }

        public override void CastSpell(Spell spell)
        {
            throw new System.NotImplementedException();
        }

        public override void FairyDeath()
        {
            throw new System.NotImplementedException();
        }

        public EnemyFairy(string name, int healthPoint, Spell[] spells, int magic, string description, int level, Element element) : base(name, healthPoint, spells, magic, description, level, element)
        {
        }
    }
}