﻿using System;
using System.Text;
using ArmyBuilder.Writers;

namespace ArmyBuilder.Soldiers
{
    /// <summary>
    /// All soldiers need a classification to be created successfully. If this is not provided,
    /// a basic soldier will be created instead which has no classification.
    /// </summary>
    public class BaseSoldier : ISoldier, IBonusRules
    {
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public string Race { get; set; }
        public Class Classification { get; set; }
        public Rank Rank { get; set; }
        public SoldierStats SoldierStats { get; set; }
        public SoldierType SoldierType { get; set; }
        public bool IsDead { get; set; }

        private const int StatModifier = 16;

        /// <summary>
        /// Default Soldier if no SoldierType is specified at creation.
        /// </summary>
        public BaseSoldier()
        {
            SoldierType = SoldierType.BasicSoldier;
            SoldierStats = new SoldierStats();
            AssignBaseStats();

            Classification = Class.None;
            Rank = Rank.Private;
        }

        public void AssignBaseStats()
        {
            // All soldiers have base stats
            SoldierStats.AttackStrength = 2;
            SoldierStats.SorceryStrength = 0;
            SoldierStats.ArmorResistance = 0;
            SoldierStats.MagicResistance = 3;
        }

        /// <summary>
        /// Return a string to display the Soldiers Info and Stats
        /// </summary>
        /// <returns></returns>
        public string DisplayNewSoldier()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"{"Your Soldier has been created! Here is a summary of your new soldier:",-15}\n");
            stringBuilder.Append($"{"Name:",-15} {Name,5}\n");
            stringBuilder.Append($"{"Birthdate:",-15} {Birthdate,5}\n");
            stringBuilder.Append($"{"Race:",-15} {SoldierType,5}\n");
            stringBuilder.Append($"{"Class:",-15} {Classification,5}\n");
            stringBuilder.Append($"{"Rank:",-15} {Rank,5}\n");
            stringBuilder.Append($"{"-----------",-10} {"Stats",5} {"-----------",10}\n");
            stringBuilder.Append($"{"AttackStrength:",-15} {SoldierStats.AttackStrength,7}\n");
            stringBuilder.Append($"{"Defense:",-15} {SoldierStats.Defense,7}\n");
            stringBuilder.Append($"{"ArmorClass:",-15} {SoldierStats.ArmorResistance,7}\n");
            stringBuilder.Append($"{"HitPoints:",-15} {SoldierStats.HitPoints,7}\n");
            stringBuilder.Append($"{"MagicResistance:",-15} {SoldierStats.MagicResistance,6}\n");
            stringBuilder.Append($"{"BonusDamage:",-15} {SoldierStats.BonusDamage,7}\n");
            stringBuilder.Append($"{"SorceryStrength:",-15} {SoldierStats.SorceryStrength,6}\n");
            stringBuilder.Append($"{"BonusMagicDamage:",-15} {SoldierStats.BonusMagicDamage,5}\n");

            return stringBuilder.ToString();
        }

        /// <inheritdoc />
        /// <summary>
        /// Determines which Type of Attack the Soldier uses based on whichever is highest.
        /// </summary>
        /// <returns>Attack as an integer</returns>
        public int Attack()
        {
            return SoldierStats.AttackStrength > SoldierStats.SorceryStrength
                ? SoldierStats.AttackStrength
                : SoldierStats.SorceryStrength;
        }

        /// <inheritdoc />
        /// <summary>
        /// Defend an incoming Attack. Can defend from a Physical or Magical Attack based on the AttackType.
        /// </summary>
        /// <param name="attackType"></param>
        /// <param name="attackDamage"></param>
        public void Defend(AttackType attackType, int attackDamage)
        {
            var damageAfterModifier = Convert.ToInt32(attackDamage * DefenseBonusModifier(attackType));

            SoldierStats.HitPoints -= damageAfterModifier;

            if (SoldierStats.HitPoints <= 0)
            {
                IsDead = true;
            }
        }

        private double DefenseBonusModifier(AttackType attackType)
        {
            var totalDefense = 0;
            if (attackType == AttackType.Physical)
            {
                totalDefense = (SoldierStats.Defense + SoldierStats.ArmorResistance);

            }
            else
            {
                totalDefense = SoldierStats.MagicResistance;
            }

            var modifier = (10 - (totalDefense * .02 * StatModifier)) * .1;

            return modifier;
        }

        /// <inheritdoc />
        /// <summary>
        /// Assigns the Bonuses that are shared among all Soldiers.
        /// Any child of soldier that has specific bonsuses handle those bonuses in the Constructor.
        /// </summary>
        public void AssignStatModifiers()
        {
            AssignAttackBonus();
            AssignSorceryStrengthBonus();
            AssignArmorClass();
            AssignMagicResistanceBonus();
            AssignDefenseBonus();
        }

        private void AssignDefenseBonus()
        {
            // KNIGHTS TAKE 5 LESS ATTACK DAMAGE
            if (Classification == Class.Knight)
            {
                SoldierStats.Defense += 5;
            }
        }

        private void AssignAttackBonus()
        {
            if (Classification == Class.Knight)
            {
                SoldierStats.AttackStrength += 10;
            }
        }

        private void AssignSorceryStrengthBonus()
        {
            // WIZARDS ARE MAGICAL!!
            if (Classification == Class.Wizard)
            {
                SoldierStats.SorceryStrength += 10;
            }
        }

        private void AssignArmorClass()
        {
            // KNIGHTS ARE BEEFY!! THIEVES ARE DODGY!!
            if (Classification == Class.Thief
                || Classification == Class.Knight)
            {
                SoldierStats.ArmorResistance += 10;
            }
        }

        private void AssignMagicResistanceBonus()
        {
            SoldierStats.MagicResistance = 0;
        }

        public virtual void ApplyBonuses()
        {
        }
    }
}
