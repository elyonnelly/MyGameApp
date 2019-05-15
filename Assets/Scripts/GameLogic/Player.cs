using System.Collections.Generic;
using Assets.Scripts.GameLogic.DataModels;

namespace Assets.Scripts.GameLogic
{
    public class Player
    {
        public string Name { get; set; }
        
        public Player()
        {
            ActiveFairies = new List<Fairy>();
            AllowFairies = new List<Fairy>();
            AllowDefensiveSpells = new List<string>();
            AllowOffensiveSpells = new List<string>();
        }

        public List<Fairy> ActiveFairies { get; set; }
        public List<Fairy> AllowFairies { get; set; }
        public List<string> AllowDefensiveSpells { get; set; }

        public List<string> AllowOffensiveSpells { get; set; }

    }
}