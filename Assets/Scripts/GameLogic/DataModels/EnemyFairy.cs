namespace Assets.Scripts.GameLogic.DataModels
{
    public class EnemyFairy : Fairy
    {
        public override void Attack(Fairy victim, OffensiveSpell spell)
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

        public EnemyFairy(string name, string description, Element element) : base(name, description, element)
        {
        }
    }
}