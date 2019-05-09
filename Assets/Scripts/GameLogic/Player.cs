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

        }

        public Player()
        {
        }

        public List<Fairy> ActiveFairies { get; set; }
        public List<Fairy> AllowFairies { get; set; }
        public List<DefensiveSpell> AllowDefensiveSpells { get; set; }

        public List<OffensiveSpell> AllowOffensiveSpells { get; set; }
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


        
    }
}