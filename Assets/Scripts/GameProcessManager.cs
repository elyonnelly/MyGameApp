using System.Timers;
using Assets.Scripts.GameLogic;
using Assets.Scripts.GameLogic.DataModels;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Компонент, отвечающий за игровую логику по сути своей
    /// </summary>
    public class GameProcessManager : MonoBehaviour
    {

        private Player player;
        private Player enemy;
        private Timer timer;
        private string currentHero;
        private double allCurrentDamage;


        public delegate void CastEffect(Fairy attacker, Fairy victim, Spell spell);

        public static CastEffect[] Effects = new CastEffect[15];

        void Start()
        {
            player = GameDataManager.Instance.PlayerData;
            enemy = GameDataManager.Instance.EnemyData;
            timer = new Timer(180000); // 3 минуты
            timer.Elapsed += NewMove;
            timer.Enabled = true;

            currentHero = "player";

            EventAggregator.DisableFairy += OnDisableFairy;
            EventAggregator.ActivateFairy += OnActivateFairy;
            EventAggregator.RemoveSpell += OnRemoveSpell;
            EventAggregator.AddSpell += OnAddSpell;
            EventAggregator.FairyAttack += OnFairyAttack;
        }


        void NewMove(object source, ElapsedEventArgs e)
        {
            currentHero = currentHero == "player" ? "enemy" : "player";
            EventAggregator.OnStartMove(currentHero); //можно играть на одном устройстве,
                                                      //тогда будут поочередно разблокироваться разные части экрана
            if (currentHero == "enemy")
            {
                EnemyMove();
            }
        }

        private void EnemyMove()
        {

        }

        void OnFairyAttack(int forwardFairyNumber, int victimFairyNumber, string spellName, string victim)
        {

            if (victim == "Player")
            {
                var victimFairy = player.ActiveFairies[forwardFairyNumber];
                var forwardFairy = enemy.ActiveFairies[victimFairyNumber];
                var spell = DataOfModels.OffensiveSpells[spellName];

                forwardFairy.AttackFairy(victimFairy, (OffensiveSpell)spell);
            }

            if (victim == "Enemy")
            {
                var victimFairy = enemy.ActiveFairies[forwardFairyNumber];
                var forwardFairy = player.ActiveFairies[victimFairyNumber];
                var spell = DataOfModels.OffensiveSpells[spellName];

                forwardFairy.AttackFairy(victimFairy, (OffensiveSpell)spell);
            }

        }

        void OnRemoveSpell(int fairyPosition, int spellPosition, string name)
        {
            player.ActiveFairies[fairyPosition].Spells[spellPosition] = new Spell("Empty Slot");
        }

        void OnAddSpell(int fairyPosition, int spellPosition, string name)
        {
            player.ActiveFairies[fairyPosition].Spells[spellPosition] = (Spell)DataOfModels.Spells[name].Clone();
        }

        void OnDisableFairy(int position, string name)
        {
            player.ActiveFairies[position] = new Fairy();
        }

        void OnActivateFairy(int position, string name)
        {
            player.ActiveFairies[position] = (Fairy)(DataOfModels.Fairies[name]).Clone();
        }

        public void Effect0(Fairy attacker, Fairy victim, Spell spell)
        {
            victim.HealthPoint -= ((OffensiveSpell)spell).Damage * attacker.DamageCoefficient * victim.DamageReduction;
        }
        /// <summary>
        /// On critical hit: N damage points 
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="victim"></param>
        /// <param name="spell"></param>
        public void Effect1(Fairy attacker, Fairy victim, Spell spell)
        {
            victim.HealthPoint -= (spell.Characteristic + ((OffensiveSpell)spell).Damage) * attacker.DamageCoefficient * victim.DamageReduction;
        }
        /// <summary>
        /// On critical hit: N% more damage 
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="victim"></param>
        /// <param name="spell"></param>
        public void Effect2(Fairy attacker, Fairy victim, Spell spell)
        {
            victim.HealthPoint -= spell.Characteristic * ((OffensiveSpell)spell).Damage * attacker.DamageCoefficient * victim.DamageReduction;
        }

        /// <summary>
        /// On critical hit: Opponent's spells recharge N% slower
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="victim"></param>
        /// <param name="spell"></param>
        public void Effect3(Fairy attacker, Fairy victim, Spell spell)
        {
            victim.HealthPoint -= ((OffensiveSpell)spell).Damage * attacker.DamageCoefficient * victim.DamageReduction;
            victim.RateFactor = spell.Characteristic;
        }

        /// <summary>
        /// Glaciation
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="victim"></param>
        /// <param name="spell"></param>
        public void Effect4(Fairy attacker, Fairy victim, Spell spell)
        {
            victim.Wound = "Glaciation"; //минус все показатели вероятно.
            //вызов ивента на нарисовать спрайт?
            victim.HealthPoint -= ((OffensiveSpell)spell).Damage * attacker.DamageCoefficient * victim.DamageReduction;
            victim.RateFactor = 0.8;
            victim.DamageCoefficient = 0.8;
        }

        /// <summary>
        /// Blindness
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="victim"></param>
        /// <param name="spell"></param>
        public void Effect5(Fairy attacker, Fairy victim, Spell spell)
        {
            victim.Wound = "Blindness"; //минус все показатели вероятно.
            //вызов ивента на нарисовать спрайт?
            victim.HealthPoint -= ((OffensiveSpell)spell).Damage * attacker.DamageCoefficient * victim.DamageReduction;
            victim.RateFactor = 0;
            victim.DamageCoefficient = 0;
        }

        /// <summary>
        /// Burning
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="victim"></param>
        /// <param name="spell"></param>
        public void Effect6(Fairy attacker, Fairy victim, Spell spell)
        {
            victim.Wound = "Burning"; //минус все показатели вероятно.
            //вызов ивента на нарисовать спрайт?
            victim.HealthPoint -= ((OffensiveSpell)spell).Damage * attacker.DamageCoefficient * victim.DamageReduction;
            victim.RateFactor = 0;
            victim.DamageCoefficient = 0;
        }
        /// <summary>
        /// Temporary incapacity
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="victim"></param>
        /// <param name="spell"></param>
        public void Effect7(Fairy attacker, Fairy victim, Spell spell)
        {
            victim.HealthPoint -= ((OffensiveSpell)spell).Damage * attacker.DamageCoefficient * victim.DamageReduction;
            victim.RateFactor = 0;
            victim.DamageCoefficient = 0;
        }

        /// <summary>
        /// Loss of opportunity to critical hit
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="victim"></param>
        /// <param name="spell"></param>
        public void Effect8(Fairy attacker, Fairy victim, Spell spell)
        {
            victim.HealthPoint -= ((OffensiveSpell)spell).Damage * attacker.DamageCoefficient * victim.DamageReduction;
            victim.AbilityToCriticalHit = false;
        }

        /// <summary>
        /// Restores hit points
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="victim"></param>
        /// <param name="spell"></param>
        public void Effect9(Fairy attacker, Fairy victim, Spell spell)
        {
            attacker.HealthPoint += spell.Characteristic;
        }

        /// <summary>
        /// Restores life points in the amount of damage
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="victim"></param>
        /// <param name="spell"></param>
        public void Effect10(Fairy attacker, Fairy victim, Spell spell)
        {
            attacker.HealthPoint += allCurrentDamage;
        }

        /// <summary>
        /// critical strikes of the enemy are ineffectual
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="victim"></param>
        /// <param name="spell"></param>
        public void Effect11(Fairy attacker, Fairy victim, Spell spell)
        {
            attacker.AbilityToTakeCriticalHit = false;
        }

        /// <summary>
        /// increase spell casting speed
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="victim"></param>
        /// <param name="spell"></param>
        public void Effect12(Fairy attacker, Fairy victim, Spell spell)
        {
            attacker.RateFactor = spell.Characteristic;
        }

        /// <summary>
        /// N% additional damage on each hit
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="victim"></param>
        /// <param name="spell"></param>
        public void Effect13(Fairy attacker, Fairy victim, Spell spell)
        {
            attacker.DamageCoefficient = spell.Characteristic;
        }

        public void Effect14(Fairy attacker, Fairy victim, Spell spell)
        {
            attacker.DamageReduction = spell.Characteristic;
        }
    }
}
