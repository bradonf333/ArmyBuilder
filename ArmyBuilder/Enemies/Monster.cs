﻿using System;

namespace ArmyBuilder.Enemies
{
    /// <summary>
    /// Monsters are cool!!!
    /// </summary>
    public class Monster : IEnemy
    {
        public int HitPoints { get; set; }
        public int PhysicalAttackDamage { get; set; }
        public int MagicalAttackDamage { get; set; }
        public AttackType AttackType { get; set; }
        public bool IsDead { get; set; }

        /// <summary>
        /// Monster is created with a Physical and Magical Attack Damage.
        /// These numbers are based on a random number within a given range. 
        /// </summary>
        public Monster()
        {
            var numberGenerator = new Random();
            HitPoints = numberGenerator.Next(50, 100);
            PhysicalAttackDamage = numberGenerator.Next(1, 20);
            MagicalAttackDamage = numberGenerator.Next(1, 20);
        }

        /// <summary>
        /// Monster attacks with the specified AttackType.
        /// The AttackType is determined randomly.
        /// </summary>
        /// <returns>Attack Damage as an integer</returns>
        public int Attack()
        {
            var numberGenerator = new Random();
            var attackTypeGenerator = numberGenerator.Next(0, 5000);

            AttackType = attackTypeGenerator % 2 == 0 ? AttackType.Physical : AttackType.Magical;

            var attackDamage = AttackType == AttackType.Physical ? PhysicalAttackDamage : MagicalAttackDamage;

            return attackDamage;
        }

        /// <inheritdoc />
        /// <summary>
        /// Easily readable way to display the Type of the Enemy
        /// </summary>
        /// <returns>Enemies type as a string</returns>
        public string EnemyTypeToString()
        {
            return "Monster";
        }

        /// <summary>
        /// Reduce the Monsters hit points by the given damage amount.
        /// </summary>
        /// <param name="damage"></param>
        public void Defend(int damage)
        {
            HitPoints -= damage;
            if (HitPoints <= 0)
            {
                IsDead = true;
            }
        }

        public string AttackMessage()
        {
            var attackMessage = AttackType == AttackType.Physical
                ? "The Monster turns its ugly head and unleashes a mighty blow!\n"
                : $"Flames spew forth from the tentacles of the beast and envelop the foe.\n";

            return attackMessage;
        }
    }
}
