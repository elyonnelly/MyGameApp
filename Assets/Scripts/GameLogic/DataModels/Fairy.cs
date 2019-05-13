using System;
using Newtonsoft.Json;
using UnityEngine;

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
        public int Level { set; get; }

        private double healthPoint;
        //[DataMember]
        public double HealthPoint
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

        private int levelCoefficient;

        public int LevelCoefficient
        {
            get
            {
                if (Level < 10)
                {
                    levelCoefficient = 1;
                }
                if (Level >= 10 && Level < 25)
                {
                    levelCoefficient = 2;
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
                    Level += 1;
                }

                experiencePoints = value;
            }
            get => experiencePoints;
        }
        public Fairy(string name, string description, Element element) : this()
        {
            HealthPoint = 11 + (HitPoints) * 8;
            Name = name;
            Description = description;
            Element = element;
        }


        public Fairy()
        {
            HealthPoint = 11 + (HitPoints) * 8;
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
            DamageReduction = 0;
        }


        public bool IsDead { set; get; }

        public string GetState()
        {
            return $"HP: {HealthPoint} Lvl: {Level}";
        }

        public virtual void Attack(Fairy victim, OffensiveSpell spell)
        {

            var effectiveness = DataOfModels.TableOfEffectiveness[(int)Element, (int)victim.Element];

            //просто атака заклинания
            var damage = effectiveness == -1 ? spell.Damage * levelCoefficient - 0.8 * spell.Damage * levelCoefficient :
                effectiveness == 1 ? spell.Damage * levelCoefficient + 0.8 * spell.Damage * levelCoefficient : spell.Damage * levelCoefficient;


            victim.HealthPoint -= Math.Max((int)damage, 1);

        }


        //TODO для других(не просто атакующих) заклинаний
        public virtual void CastSpell(Spell spell)
        {
        }

        public virtual void FairyDeath()
        {
        }

        public string Wound { set; get; }

        public double RateFactor { get; set; }
        public double DamageCoefficient { get; set; }
        public bool AbilityToCriticalHit
        {
            get;
            set;
        }
        public bool AbilityToTakeCriticalHit { get; set; }
        public double DamageReduction { get; set; }

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