using System;

namespace Assets.Scripts.GameLogic.DataModels
{
    public class Fairy : ICloneable, IEquatable<Fairy>
    {
        private int healthPoint;

        private int score;

        public Fairy(string name, string description, Element element)
        {
            Name = name;
            HealthPoint = 30;
            Spells = new Spell[4];
            Magic = 30;
            Description = description;
            Level = 1;
            Element = element;
            IsDead = false;
        }

        public Fairy()
        {
        }

        public static Fairy Default => new PlayerFairy();

        public string Name { get; }

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

        public Spell[] Spells { set; get; }

        public int Magic { set; get; }

        public string Description { get; }

        public int Level { set; get; }

        public int Score
        {
            //TODO организовать нормальное увеличение уровня
            set
            {
                if (value - score > Level * 0.25)
                {
                    Level += 1;
                }

                score = value;
            }
            get => score;
        }

        public Element Element { get; }
        public bool IsDead { set; get; }

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

            return Equals((Fairy) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name != null ? Name.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (Description != null ? Description.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int) Element;
                return hashCode;
            }
        }

        public virtual void AttackFairy(Fairy victim, OffensiveSpell spell)
        {
        }

        //TODO для других(не просто атакующих) заклинаний
        public virtual void CastSpell(Spell spell)
        {
        }

        public virtual void FairyDeath()
        {
        }

        public object Clone() => new Fairy(Name, Description, Element);

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