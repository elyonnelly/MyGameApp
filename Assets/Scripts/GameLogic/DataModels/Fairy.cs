using System.Collections.Generic;

namespace Assets.Scripts.GameLogic.DataModels
{
    public abstract class Fairy
    {
        protected Fairy()
        {

        }
        protected Fairy(string name, int healthPoint, Spell[] spells, int magic, string description, int level, Element element)
        {
            Name = name;
            HealthPoint = healthPoint;
            Spells = spells;
            Magic = magic;
            Description = description;
            Level = level;
            Element = element;
            IsDead = false;
        }

        public string Name { get; }

        private int healthPoint;
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
            get { return healthPoint; }
        }

        public Spell[] Spells { set; get; }

        public int Magic { set; get; }

        public string Description { get; }

        public int Level { set; get; }

        private int score;
        public int Score {
            //TODO организовать нормальное увеличение уровня
            set
            {
                if (value - score > Level * 0.25)
                {
                    Level += 1;
                }

                score = value;
            }
            get { return score; }
        }

        public Element Element { get; }
        public  bool IsDead { set; get; }

        public static Fairy Default
        {
            get
            {
                return new PlayerFairy();
            }
        }

        public abstract void AttackFairy(Fairy victim, Spell spell);

        //TODO для других(не просто атакующих) заклинаний
        public abstract void CastSpell(Spell spell);

        public abstract void FairyDeath();

        public override bool Equals(object obj)
        {
            var fairy = obj as Fairy;
            return fairy != null &&
                   Name == fairy.Name &&
                   healthPoint == fairy.healthPoint &&
                   HealthPoint == fairy.HealthPoint &&
                   EqualityComparer<Spell[]>.Default.Equals(Spells, fairy.Spells) &&
                   Magic == fairy.Magic &&
                   Description == fairy.Description &&
                   Level == fairy.Level &&
                   Element == fairy.Element;
        }

        public override int GetHashCode()
        {
            var hashCode = -887168581;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + healthPoint.GetHashCode();
            hashCode = hashCode * -1521134295 + HealthPoint.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Spell[]>.Default.GetHashCode(Spells);
            hashCode = hashCode * -1521134295 + Magic.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            hashCode = hashCode * -1521134295 + Level.GetHashCode();
            hashCode = hashCode * -1521134295 + Element.GetHashCode();
            return hashCode;
        }
    }
}