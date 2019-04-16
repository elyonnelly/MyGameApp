using System;
using System.Collections.Generic;
using Assets.Scripts.GameLogic.DataModels;

namespace Assets.Scripts.GameLogic
{
    public class Player
    {
        public Player(List<Fairy> activeFairies, List<Fairy> allowFairies, int currentStage)
        {
            ActiveFairies = activeFairies;
            AllowFairies = allowFairies;
            CurrentStage = currentStage;

            EventAggregator.AddActiveFairy += AddActiveFairy;
            EventAggregator.RemoveActiveFairy += RemoveActiveFairy;
            EventAggregator.AddActiveSpell += AddActiveSpell;
            EventAggregator.RemoveActiveSpell += RemoveActiveSpell;
            EventAggregator.PlayerAttack += Attack;
        }

        public Player()
        {
        }

        public List<Fairy> ActiveFairies { get; set; }
        public List<Fairy> AllowFairies { get; set; }
        public int CurrentStage { set; get; }

        public void AddActiveFairy(int position, Fairy newFairy)
        {
            ActiveFairies[position] = newFairy;
        }

        public void RemoveActiveFairy(int position, Fairy newFairy)
        {
            ActiveFairies[position] = Fairy.Default;
        }

        public void AddActiveSpell(int fairyPosition, int spellPosition, Spell spell)
        {
            ActiveFairies[fairyPosition].Spells[spellPosition] = spell;
        }

        public void RemoveActiveSpell(int fairyPosition, int spellPosition, Spell spell)
        {
            ActiveFairies[fairyPosition].Spells[spellPosition] = Spell.Default;
        }

        public void Attack(Fairy attack, Fairy victim, Spell spell)
        {
            attack.AttackFairy(victim, spell);
        }

        
    }
}