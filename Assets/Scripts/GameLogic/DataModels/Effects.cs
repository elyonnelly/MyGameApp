namespace Assets.Scripts.GameLogic.DataModels
{
    public class Effects
    {
        public static CastEffect[] SetOfEffects = new CastEffect[]
        {
            Effect0, Effect1, Effect2, Effect3, Effect4, Effect5, Effect6, Effect7, Effect8, Effect9, Effect10, Effect11, Effect12, Effect13, Effect14
        };
    
        public delegate void CastEffect(Fairy attacker, Fairy victim, Spell spell);


        private static double EfficiencyCoefficient(Fairy attacker, Fairy victim)
        {
            if (DataOfModels.TableOfEffectiveness[(int)attacker.Element, (int)victim.Element] == 1)
            {
                return 1.8;
            }
            if (DataOfModels.TableOfEffectiveness[(int)attacker.Element, (int)victim.Element] == -1)
            {
                return 0.2;
            }

            return 1;
        }
        public static void Effect0(Fairy attacker, Fairy victim, Spell spell)
        {
            victim.HealthPoint -= ((OffensiveSpell) spell).Damage * attacker.DamageCoefficient * victim.DamageReduction
                                  * EfficiencyCoefficient(attacker, victim);
        }

        /// <summary>
        ///     On critical hit: N damage points
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="victim"></param>
        /// <param name="spell"></param>
        public static void Effect1(Fairy attacker, Fairy victim, Spell spell)
        {
            victim.HealthPoint -= (spell.Characteristic + ((OffensiveSpell) spell).Damage) * attacker.DamageCoefficient * victim.DamageReduction
                                  * EfficiencyCoefficient(attacker, victim);
        }

        /// <summary>
        ///     On critical hit: N% more damage
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="victim"></param>
        /// <param name="spell"></param>
        public static void Effect2(Fairy attacker, Fairy victim, Spell spell)
        {
            victim.HealthPoint -= spell.Characteristic * ((OffensiveSpell) spell).Damage * attacker.DamageCoefficient * victim.DamageReduction
                                  * EfficiencyCoefficient(attacker, victim);
        }

        /// <summary>
        ///     On critical hit: Opponent's spells recharge N% slower
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="victim"></param>
        /// <param name="spell"></param>
        public static void Effect3(Fairy attacker, Fairy victim, Spell spell)
        {
            victim.HealthPoint -= ((OffensiveSpell) spell).Damage * attacker.DamageCoefficient * victim.DamageReduction
                                  * EfficiencyCoefficient(attacker, victim);
                //victim.RateFactor = spell.Characteristic;
        }

        /// <summary>
        ///     Glaciation
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="victim"></param>
        /// <param name="spell"></param>
        public static void Effect4(Fairy attacker, Fairy victim, Spell spell)
        {
            victim.Wound = "Glaciation";
            victim.HealthPoint -= ((OffensiveSpell) spell).Damage * attacker.DamageCoefficient * victim.DamageReduction
                                  * EfficiencyCoefficient(attacker, victim);
            victim.RateFactor = 0.8;
            victim.DamageCoefficient = 0.8;
        }

        /// <summary>
        ///     Blindness
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="victim"></param>
        /// <param name="spell"></param>
        public static void Effect5(Fairy attacker, Fairy victim, Spell spell)
        {
            victim.Wound = "Blindness";
            victim.HealthPoint -= ((OffensiveSpell) spell).Damage * attacker.DamageCoefficient * victim.DamageReduction
                                  * EfficiencyCoefficient(attacker, victim);
            victim.RateFactor = 0;
            victim.DamageCoefficient = 0;
        }

        /// <summary>
        ///     Burning
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="victim"></param>
        /// <param name="spell"></param>
        public static void Effect6(Fairy attacker, Fairy victim, Spell spell)
        {
            victim.Wound = "Burning"; 
            victim.HealthPoint -= ((OffensiveSpell) spell).Damage * attacker.DamageCoefficient * victim.DamageReduction
                                  * EfficiencyCoefficient(attacker, victim);
            victim.RateFactor = 0;
            victim.DamageCoefficient = 0;
        }

        /// <summary>
        ///     Temporary incapacity
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="victim"></param>
        /// <param name="spell"></param>
        public static void Effect7(Fairy attacker, Fairy victim, Spell spell)
        {
            victim.HealthPoint -= ((OffensiveSpell) spell).Damage * attacker.DamageCoefficient * victim.DamageReduction
                                  * EfficiencyCoefficient(attacker, victim);
            victim.RateFactor = 0;
            victim.DamageCoefficient = 0;
        }

        /// <summary>
        ///     Loss of opportunity to critical hit
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="victim"></param>
        /// <param name="spell"></param>
        public static void Effect8(Fairy attacker, Fairy victim, Spell spell)
        {
            victim.HealthPoint -= ((OffensiveSpell) spell).Damage * attacker.DamageCoefficient * victim.DamageReduction
                                  * EfficiencyCoefficient(attacker, victim);
            victim.AbilityToCriticalHit = false;
        }

        /// <summary>
        ///     Restores hit points
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="victim"></param>
        /// <param name="spell"></param>
        public static void Effect9(Fairy attacker, Fairy victim, Spell spell)
        {
            attacker.HealthPoint += spell.Characteristic;
        }

        /// <summary>
        ///     Restores life points in the amount of damage
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="victim"></param>
        /// <param name="spell"></param>
        public static void Effect10(Fairy attacker, Fairy victim, Spell spell)
        {
            attacker.HealthPoint += ((OffensiveSpell) spell).Damage;
        }

        /// <summary>
        ///     critical strikes of the enemy are ineffectual
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="victim"></param>
        /// <param name="spell"></param>
        public static void Effect11(Fairy attacker, Fairy victim, Spell spell)
        {
            attacker.AbilityToTakeCriticalHit = false;
        }

        /// <summary>
        ///     increase spell casting speed
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="victim"></param>
        /// <param name="spell"></param>
        public static void Effect12(Fairy attacker, Fairy victim, Spell spell)
        {
            attacker.DamageCoefficient = spell.Characteristic;
        }

        /// <summary>
        ///     N% additional damage on each hit
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="victim"></param>
        /// <param name="spell"></param>
        public static void Effect13(Fairy attacker, Fairy victim, Spell spell)
        {
            attacker.DamageCoefficient = spell.Characteristic;
        }

        public static void Effect14(Fairy attacker, Fairy victim, Spell spell)
        {
            attacker.DamageReduction = spell.Characteristic;
        }
    }
}