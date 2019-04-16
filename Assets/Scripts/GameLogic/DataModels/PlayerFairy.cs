using System;

namespace Assets.Scripts.GameLogic.DataModels
{
    public class PlayerFairy : Fairy
    {
        public override void AttackFairy(Fairy victim, Spell spell)
        {
            //TODO добавить обработку коэфициента в зависимости от стихии феи
            Magic -= spell.MagicForCast;
            victim.HealthPoint -= spell.Damage;
        }

        public override void CastSpell(Spell spell)
        {
            throw new NotImplementedException();
        }

        public override void FairyDeath()
        {
            throw new NotImplementedException();
        }

        public void ChangeSpell()
        {
            throw new System.NotImplementedException();
        }

        public PlayerFairy(string name, int healthPoint, Spell[] spells, int magic, string description, int level, Element element) : base(name, healthPoint, spells, magic, description, level, element)
        {
        }

        public PlayerFairy()
        {
        }
    }
}