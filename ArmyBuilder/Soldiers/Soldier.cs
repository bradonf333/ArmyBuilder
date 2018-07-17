using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyBuilder.Soldiers
{
    /// <summary>
    /// Soldier should probably be a base class which is used to create the specialized soldiers
    /// i.e. each race would be a specialization of Soldier
    /// </summary>
    public class Soldier
    {
        public string Name { get; set; }

        public string Race { get; set; }

        public ClassificationEnums Classification { get; set; }

        public string Rank { get; set; }

        public int HitPoints { get; set; }

        public DateTime Birthdate { get; set; }

        public int AttackStrength { get; set; }

        public int SorceryStrength { get; set; }

        public int MagicResistance { get; set; }

        public int ArmorClass { get; set; }

        public int BonusDamage { get; set; }

        public int BonusMagicDamage { get; set; }

        public int Defense { get; set; }

        public Soldier()
        {
            // All soldiers have a base AttackStrength of 2
            AttackStrength = 2;
        }

        public virtual string SoldierType()
        {
            return "Basic Soldier";
        }

        public void Attack()
        {

        }

        public void Defend()
        {

        }
    }
}
