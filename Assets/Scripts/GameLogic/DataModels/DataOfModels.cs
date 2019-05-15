using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Assets.Scripts.GameLogic.DataModels
{
    public class DataOfModels
    {

        static DataOfModels()
        {
            try
            {

                var fairies = Resources.Load<TextAsset>("fairies");
                var fairiesJson = JsonConvert.DeserializeObject<Fairy[]>(fairies.text);
                foreach (var fairy in fairiesJson)
                {
                    Fairies.Add(fairy.Name, fairy);
                }

                var offensiveSpells = Resources.Load<TextAsset>("Offensive spells");
                    OffensiveSpells = JsonConvert.DeserializeObject<Dictionary<string, OffensiveSpell>>(offensiveSpells.text);
                
                var defensiveSpells = Resources.Load<TextAsset>("Defensive spells");
                DefensiveSpells = JsonConvert.DeserializeObject<Dictionary<string, DefensiveSpell>>(defensiveSpells.text);
                
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }
        
        public static Dictionary<string, OffensiveSpell> OffensiveSpells = new Dictionary<string, OffensiveSpell>();

        public static Dictionary<string, DefensiveSpell> DefensiveSpells = new Dictionary<string, DefensiveSpell>();


        public static Dictionary<string, Fairy> Fairies = new Dictionary<string, Fairy>();

        public static int[,] TableOfEffectiveness =
        {
            {0,0,0,0,0,1,0,-1,-1,-1,1,-1},
            {0,0,1,0,-1,1,-1,-1,-1,-1,1,1},
            { 0,-1,0,0,-1,0,0,-1,1,1,-1,1},
            {0,0,0,0,0,-1,1,-1,1,1,1,-1},
            {0,0,1,0,0,1,-1,-1,0,0,0,1},
            {-1,-1,0,1,-1,0,1,1,0,-1,-1,-1},
            {0,1,0,-1,1,1-1,0,0,0,-1,0,0},
            {1,1,1,-1,1,-1,-1,0,-1,0,0,1},
            {1,1,-1,-1,0,0,0,1,0,-1,1,-1},
            {0,1,-1,-1,0,1,1,0,1,0,0,0},
            {-1,-1,1,-1,0,1,0,0,-1,0,0,0},
            {1,-1,-1,1,-1,1,0,-1,1,0,0,0}
        };



    }
}