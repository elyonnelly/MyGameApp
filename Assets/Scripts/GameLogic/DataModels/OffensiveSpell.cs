using Newtonsoft.Json;

namespace Assets.Scripts.GameLogic.DataModels
{
    public class OffensiveSpell : Spell
    {
        [JsonProperty("Damage")]
        public double Damage {  set; get; }

        [JsonProperty("FireRate")]
        public int FireRate {  set; get; }

        public OffensiveSpell(Element majorElement, int level, string name, int damage, int mana, int fireRate, Element minorElement = default) 
                            : base(majorElement, level, name, mana, minorElement)
        {
            Damage = damage;
            FireRate = fireRate;
        }
    }
}