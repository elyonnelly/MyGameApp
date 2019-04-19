using System;

namespace Assets.Scripts.GameLogic.DataModels
{
    public class Spell : ICloneable
    {
        public string Name { get; }
        public Element[] Elements { get; }
        public int Mana { get; }
        public int Level { get; }

        public Spell(Element majorElement, int level, string name, int mana, Element minorElement = default)
        {
            Name = name;
            Elements = new[] { majorElement, minorElement == default ? majorElement : minorElement };
            Mana = mana;
            Level = level;
        }

        public Spell()
        {

        }

        public static Spell Default
        {
            get
            {
                return new Spell();
            }
        }

        public object Clone()
        {
            return new Spell(Elements[0], Level, Name, Mana, Elements.Length > 1 ? Elements[1] : Elements[0]);
        }
    }
}