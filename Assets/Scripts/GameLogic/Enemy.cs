using System.Collections.Generic;
using Assets.Scripts.GameLogic.DataModels;

namespace Assets.Scripts.GameLogic
{
    public class Enemy
    {
        public List<Fairy> ActiveFairies { get; set; }

        public void Attack(Fairy attack, Fairy victim, Spell spell)
        {
            attack.AttackFairy(victim, spell);
        }
    }
}