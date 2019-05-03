using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Assets.Scripts.GameLogic.DataModels
{
    //[DataContract]
    public class Fairy : ICloneable, IEquatable<Fairy>
    {

        [JsonProperty("Name")]
        public string Name { private set; get; }

        [JsonProperty("Description")]
        public string Description { private set; get; }

        [JsonProperty("Element")]
        public Element Element { private set; get; }


        [JsonProperty("EvolvesTo")]
        public string EvolvesTo { private set; get; }

        [JsonProperty("LevelForEvolution")]
        public int LevelForEvolution { private set; get; }
        //[DataMember]
        public Spell[] Spells { set; get; }
        public int Level { set; get; }

        private int healthPoint;
        //[DataMember]
        public int HealthPoint
        {
            set
            {
                if (value <= 0)
                {
                    IsDead = true;
                    FairyDeath();
                    healthPoint = 0;
                }
                else
                {
                    healthPoint = value;
                }
            }
            get => healthPoint;
        }
        //[DataMember]
        public int Magic { set; get; }

        private int experiencePoints;
        //[DataMember]
        public int ExperiencePoints
        {
            //TODO организовать нормальное увеличение уровня
            set
            {
                if (value - experiencePoints > Level * 0.25)
                {
                    Level += 1;
                }

                experiencePoints = value;
            }
            get => experiencePoints;
        }
        public Fairy(string name, string description, Element element)
        {
            Name = name;
            HealthPoint = 30;
            Spells = new Spell[4];
            for (var i = 0; i < 4; i++)
            {
                Spells[i] = new Spell("Empty Slot");
            }
            Magic = 30;
            Description = description;
            Level = 1;
            Element = element;
            IsDead = false;
        }

        public Fairy(string name, string description, Element element, Spell[] spells)
        {
            Name = name;
            HealthPoint = 30;
            Spells = new Spell[4];
            for (var i = 0; i < 2; i++)
            {
                Spells[i] = spells[i];
            }
            for (var i = 2; i < 4; i++)
            {
                Spells[i] = new Spell("Empty Slot");
            }
            Magic = 30;
            Description = description;
            Level = 1;
            Element = element;
            IsDead = false;
        }

        public Fairy()
        {
            HealthPoint = 30;
            Spells = new Spell[4];
            for (var i = 0; i < 4; i++)
            {
                Spells[i] = new Spell("Empty Slot");
            }
            Magic = 30;
            Level = 1;
            IsDead = false;
            ExperiencePoints = 0;
        }

        public Fairy(FairyData fairy)
        {
            Name = fairy.Name;
            Description = fairy.Description;
            Element = fairy.Element;
            LevelForEvolution = fairy.LevelForEvolution;
            EvolvesTo = fairy.EvolvesTo;
        }

        
        public bool IsDead { set; get; }

        public string GetState()
        {
            return $"Magic: {Magic} HP: {HealthPoint}";
        }

        public virtual void AttackFairy(Fairy victim, OffensiveSpell spell)
        {
            //обработка на стихии должна быть
            victim.HealthPoint -= spell.Damage;
        }

        //TODO для других(не просто атакующих) заклинаний
        public virtual void CastSpell(Spell spell)
        {
        }

        public virtual void FairyDeath()
        {
        }
        public static Fairy Default => new PlayerFairy();
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((Fairy)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name != null ? Name.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (Description != null ? Description.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int)Element;
                return hashCode;
            }
        }
        public object Clone()
        {
            var fairy = new Fairy(Name, Description, Element)
            {
                Spells = new Spell[4],
                EvolvesTo = this.EvolvesTo,
                LevelForEvolution = this.LevelForEvolution
            };
            for (var i = 0; i < 4; i++)
            {
                fairy.Spells[i] = Spells[i];
            }

            return fairy;
        }

        public bool Equals(Fairy other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return string.Equals(Name, other.Name) && string.Equals(Description, other.Description) && Element == other.Element;
        }
    }
}