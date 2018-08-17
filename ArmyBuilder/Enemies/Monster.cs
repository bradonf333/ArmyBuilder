using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ArmyBuilder.Writers;

namespace ArmyBuilder
{
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
            var attackTypeGenerator = numberGenerator.Next(0, 2);

            AttackType = attackTypeGenerator == 0 ? AttackType.Physical : AttackType.Magical;

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
    }
}
