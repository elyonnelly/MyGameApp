using System.Collections.Generic;
using Assets.Scripts.GameLogic.DataModels;

namespace Assets.Scripts.GameLogic
{
    public class Player
    {
        public string Name { get; set; }
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
        public List<string> AllowDefensiveSpells { get; set; }

        public List<string> AllowOffensiveSpells { get; set; }
        public int CurrentStage { set; get; }

    }
}