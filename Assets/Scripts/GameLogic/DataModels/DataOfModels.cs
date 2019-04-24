using System.Collections.Generic;

namespace Assets.Scripts.GameLogic.DataModels
{
    public class DataOfModels
    {
        //public string[] NamesOfFairies = {"Worgot", "Rasrow", "Sirael", "Manox","Dracwin", "Beltaur"};

        //TODO нормально загрузить информацию о типах из файла и переместить это в какое-то логичное место
        public static Dictionary<string, Fairy> Fairies = new Dictionary<string, Fairy>()
        {
            
            {
                "Abery", new Fairy("Abery", "This mushroomhead must be one of the weirdest creatures in Zanzarah, and is easy to find among the other mushrooms and toadstools. " +
                                            "Once this being reaches a certain age and level of training, " +
                                            "it becomes a particularly dangerous specimen. ",
                                   Element.Nature)
            },
            {
                "Worgot", new Fairy("Worgot", "As larvae, Worgots depend on symbiosis with their flying companions, " +
                                              "which they cannot completely steer yet. This strange pair can be found in the bark of trees, " +
                                              "in blossoming plants and in swamps. ",
                                    Element.Nature)
            },
            {
                "Rasrow", new Fairy("Rasrow", "Creatures of chaos, like Rasrow, are native to the Shadow Realm. " +
                                              "Their sinister powers spread terror among the mythical creatures. " +
                                              "Only very few fairies can prevail in a fight against one of these.",
                                    Element.Chaos)
            },
            {
                "Sirael", new Fairy("Sirael", "Air beings like this have their entries to the world of Zanzarah in steam clouds and inside birds' nests. " +
                                              "They are very careless, which is why they are often sighted by mortals.",
                                    Element.Air)
            },
            {
                "Sillia", new Fairy("Sillia",
                                    "Nature DataOfModels, like Sillia, get their energy from the living things around them. " +
                                    "These beings hate being fenced in, therefore you'll only find them out of doors, under trees, " +
                                    "in bushes or in the undergrowth. ",
                                    Element.Nature)
            },
            {
                "Tadana", new Fairy("Tadana",
                                    "In the wide spaces of the oceans and seas, but also in the lakes and rivers in Zanzarah, " +
                                    "Water DataOfModels can be found. Tadana is a very young water being, who is often underestimated by her opponents. ",
                                    Element.Water)
            },
            {
                "Vesbat", new Fairy("Vesbat", "Stones and rock build the entrances to the world of Zanzarah. " +
                                              "Gritting its teeth and using its excellent sense of smell, the Vesbat approaches its opponent. " +
                                              "It requires training in both stamina and strength. ",
                                    Element.Stone)
            },
            {
                "Manox", new Fairy("Manox", "These beings of darkness are the epitome of evil. They are hard to catch and hardly ever get on with any other species. " +
                                            "These very strong beings can only be held back with Crystal Spheres. ",
                                   Element.Dark)
            },
            {
                "Dracwin", new Fairy("Dracwin", "Dragons are adherents of the element of fire, and a special kind of flying companion. " +
                                                "Dracwin can be trained to become a Flagwin using an Elemental Stone of Fire.",
                                    Element.Fire)
            },
            {
                "Beltaur", new Fairy("Beltaur", "This grey-red companion is plump and appears clumsy and awkward in its movements. " +
                                                "However, it possesses psi powers few others have, though these are not yet fully developed.",
                                     Element.Psi)
            }

        };

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

    }
}