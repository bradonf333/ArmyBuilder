using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace ArmyBuilder
{
    public class Monster : IEnemy
    {
        public int HitPoints { get; set; }
        public int PhysicalAttackDamage { get; set; }
        public int MagicalAttackDamage { get; set; }
        public AttackType AttackType { get; set; }
        public bool IsDead { get; set; }

        public Monster()
        {
            var numberGenerator = new Random();
            HitPoints = numberGenerator.Next(50, 100);
            PhysicalAttackDamage = numberGenerator.Next(1, 20);
            MagicalAttackDamage = numberGenerator.Next(1, 20);
        }

        public int Attack()
        {
            var numberGenerator = new Random();
            var attackTypeGenerator = numberGenerator.Next(0, 2);

            AttackType = attackTypeGenerator == 0 ? AttackType.Physical : AttackType.Magical;

            return AttackType == AttackType.Physical ? PhysicalAttackDamage : MagicalAttackDamage;

        }

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
