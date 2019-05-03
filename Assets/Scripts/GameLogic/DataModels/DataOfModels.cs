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

            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }


        public static Dictionary<string, Spell> Spells = new Dictionary<string, Spell>()
        {
            {
                "Small Spirit", new OffensiveSpell(Element.Nature, 1, "Small Spirit", 1, 2, 5, Element.Water)
            },
            {
                "Insanity", new OffensiveSpell(Element.Stone, 2, "Insanity", 2, 2, 2, Element.Fire)
            },
            {
                "Telekinesis", new DefensiveSpell(Element.Nature, 1, "Telekinesis", 0, Element.Water)
            },
            {
                "Quake of Power", new DefensiveSpell(Element.Stone, 2, "Quake of Power", 2, Element.Fire)
            },
            {
                "Chaos Lightning", new OffensiveSpell(Element.Psi, 1, "Chaos Lightning", 3, 2, 2, Element.Chaos)
            },
            {
                "Spirit of Chaos", new OffensiveSpell(Element.Air, 2, "Spirit of Chaos", 4, 1, 1, Element.Dark)
            },
            {
                "Dance of Chaos", new DefensiveSpell(Element.Psi, 1, "Dance of Chaos", 5, Element.Chaos)
            },
            {
                "Confused Spirit", new DefensiveSpell(Element.Air, 2, "Confused Spirit", 0, Element.Dark)
            },

        };

        //public static Dictionary<string, Fairy> Fairies = new Dictionary<string, Fairy>();
        //TODO нормально загрузить информацию о типах из файла и переместить это в какое-то логичное место
        public static Dictionary<string, Fairy> Fairies = new Dictionary<string, Fairy>();



    }
}