using System;
using Assets.Scripts.GUI;
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

        [JsonProperty("HitPoints")]
        public int HitPoints { private set; get; }

        //[DataMember]
        public string[] Spells { set; get; }
        public int level;

        public int Level
        {
            set
            {
                if (level == LevelForEvolution)
                {
                    Name = EvolvesTo;
                    //Description = DataOfModels.Fairies[EvolvesTo].Name;
                }

                level = value;
            }
            get => level;
        }

        private double healthPoint;
        //[DataMember]
        public double HealthPoint
        {
            set
            {
                if (value <= 0)
                {
                    IsDead = true;
                    //FairyDeath();
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

        private int levelCoefficient;

        public int LevelCoefficient
        {
            get
            {
                if (Level < 10)
                {
                    levelCoefficient = 3;
                }
                if (Level >= 10 && Level < 25)
                {
                    levelCoefficient = 5;
                }
                if (Level >= 25 && Level < 45)
                {
                    levelCoefficient = 10;
                }

                if (Level >= 45)
                {
                    levelCoefficient = 15;
                }

                return levelCoefficient;
            }
        }

        private int experiencePoints;
        //[DataMember]
        public int ExperiencePoints
        {
            //TODO организовать нормальное увеличение уровня
            set
            {
                if (value - experiencePoints > Level * 0.25)
                {
                    Level += (value - experiencePoints) / 5;
                }

                experiencePoints = value;
            }
            get => experiencePoints;
        }
        public Fairy(string name, string description, Element element) : this()
        {
            HealthPoint = 11 + (HitPoints) * 8 + Level * (HitPoints * 2);
            Name = name;
            Description = description;
            Element = element;
        }


        public Fairy()
        {
            HealthPoint = 11 + (HitPoints) * 8 + Level * (HitPoints * 2); ;
            Spells = new string[4];
            for (var i = 0; i < 4; i++)
            {
                Spells[i] = "Empty Slot";
            }
            Magic = 15;
            Level = 1;
            IsDead = false;
            ExperiencePoints = 0;
            RateFactor = 1;
            DamageCoefficient = 1;
            AbilityToCriticalHit = true;
            AbilityToTakeCriticalHit = true;
            DamageReduction = 1;
        }
        public bool IsDead { set; get; }
        public string Wound { set; get; }
        public double RateFactor { get; set; }
        public double DamageCoefficient { get; set; }
        public bool AbilityToCriticalHit {get;set;}
        public bool AbilityToTakeCriticalHit { get; set; }
        public double DamageReduction { get; set; }
        public string GetState()
        {
            return $"HP: {HealthPoint} Lvl: {Level}";
        }

        public void Attack(Fairy victim, OffensiveSpell spell)
        {
            if (victim.AbilityToTakeCriticalHit && AbilityToCriticalHit)
            {
                Effects.SetOfEffects[Math.Min((int)spell.Effect, 7)](this, victim, spell);
            }
            else
            {
                Effects.SetOfEffects[0](this, victim, spell);
            }

            victim.AbilityToTakeCriticalHit = true;
            AbilityToCriticalHit = true;
        }
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
                HealthPoint = this.HitPoints * 8 + 11,
                Spells = new string[4],
                EvolvesTo = this.EvolvesTo,
                LevelForEvolution = this.LevelForEvolution,
                HitPoints = this.HitPoints
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