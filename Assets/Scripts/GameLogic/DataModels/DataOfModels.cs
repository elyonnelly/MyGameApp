using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Assets.Scripts.GameLogic.DataModels
{
    public class DataOfModels
    {

        static DataOfModels()
        {
            //var jsonObject = JsonConvert.SerializeObject(Fairies.Keys);
            try
            {
                using (var reader = new StreamReader(@"fairies.json"))
                {
                    var fairiesJson = JsonConvert.DeserializeObject<Fairy[]>(reader.ReadLine());
                    foreach (var fairy in fairiesJson)
                    {
                        Fairies.Add(fairy.Name, fairy);
                    }
                }

                using (var reader = new StreamReader(@"Offensive spells.json"))
                {
                    OffensiveSpells = JsonConvert.DeserializeObject<Dictionary<string, OffensiveSpell>>(reader.ReadLine());
                    
                }

                using (var reader = new StreamReader(@"Defensive spells.json"))
                {
                    DefensiveSpells = JsonConvert.DeserializeObject<Dictionary<string, DefensiveSpell>>(reader.ReadLine());
                }
            }
            catch(Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }

        public static Dictionary<string, OffensiveSpell> Spells { get; set; }
    

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