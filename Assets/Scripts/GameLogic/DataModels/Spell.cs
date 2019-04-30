using System;
using System.Runtime.Serialization;

namespace Assets.Scripts.GameLogic.DataModels
{
    [DataContract]
    public class Spell : ICloneable
    {
        [DataMember]
        public string Name { private set; get; }

        [DataMember]
        public Element MajorElement { private set; get; }

        [DataMember]
        public Element MinorElement { private set; get; }

        [DataMember]
        public int Mana { private set; get; }

        [DataMember]
        public int Level { private set; get; }

        public Spell(Element majorElement, int level, string name, int mana, Element minorElement)
        {
            Name = name;
            MajorElement = majorElement;
            MinorElement = minorElement;
            Mana = mana;
            Level = level;
        }

        public Spell(string name)
        {
            Name = name;
        }

        public static Spell Default => new Spell("Empty Slot");

        public object Clone()
        {
            return new Spell(MajorElement, Level, Name, Mana, MinorElement);
        }
    }
}