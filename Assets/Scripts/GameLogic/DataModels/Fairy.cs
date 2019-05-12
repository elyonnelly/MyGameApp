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

        //[JsonProperty("HitPoints")]
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
            Name = name;
            Description = description;
            Element = element;
        }


        public Fairy()
        {
            HealthPoint = 30;
            Spells = new string[4];
            for (var i = 0; i < 4; i++)
            {
                Spells[i] = "Empty Slot";
            }
            Magic = 30;
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
            return $"HP: {HealthPoint}";
        }

        public virtual void AttackFairy(Fairy victim, OffensiveSpell spell)
        {
            //обработка на стихии

            var effectiveness = DataOfModels.TableOfEffectiveness[(int)Element, (int)victim.Element];

            var damage = effectiveness == -1 ? spell.Damage * 10 - 0.8 * spell.Damage :
                            effectiveness == 1 ? spell.Damage * 10 + 0.8 * spell.Damage : spell.Damage * 10;
            victim.HealthPoint -= (int)damage;

            Debug.Log(victim.HealthPoint);


        }


        //TODO для других(не просто атакующих) заклинаний
        public virtual void CastSpell(Spell spell)
        {
        }

        public virtual void FairyDeath()
        {
        }

        public string Wound { set; get; }

        public double RateFactor;
        public double DamageCoefficient;
        public bool AbilityToCriticalHit;
        public bool AbilityToTakeCriticalHit;
        public double DamageReduction;

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
                Spells = new string[4],
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