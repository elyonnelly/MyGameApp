using System;

namespace Assets.Scripts.GameLogic.DataModels
{
    public class Spell : ICloneable
    {
        public string Name { get; }
        public Element MajorElement { get; }
        public Element MinorElement { get; }
        public int Mana { get; }
        public int Level { get; }

        public Spell(Element majorElement, int level, string name, int mana, Element minorElement)
        {
            Name = name;
            MajorElement = majorElement;
            MinorElement = minorElement;
            Mana = mana;
            Level = level;
        }

        public Spell()
        {

        }

        public static Spell Default => new Spell();

        public object Clone()
        {
            return new Spell(MajorElement, Level, Name, Mana, MinorElement);
        }
    }
}