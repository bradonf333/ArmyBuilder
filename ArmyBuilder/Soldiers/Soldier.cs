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

        public ClassificationEnum Classification { get; set; }

        public RankEnum Rank { get; set; }

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
            // All soldiers have a base stats
            AttackStrength = 2;
            Classification = ClassificationEnum.None;
            Rank = RankEnum.Private;
            SorceryStrength = 0;
            ArmorClass = 0;
            MagicResistance = 3;
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

        public void AssignAttackBonus()
        {
            var soldierType = this.GetType();

            if (Classification == ClassificationEnum.Knight)
            {
                AttackStrength += 10;
            }

            if (soldierType == typeof(Human) && Classification == ClassificationEnum.Cleric)
            {
                AttackStrength += 3;
            }
        }

        public void AssignSorceryStrengthBonus()
        {
            var soldierType = this.GetType();

            // ELVES ARE MAGICAL!!
            // Doing this by default in the Elf Class now.
            //if (soldierType == typeof(Elf))
            //{
            //    SorceryStrength += 10;
            //}

            // WIZARDS ARE MAGICAL!!
            if (Classification == ClassificationEnum.Wizard)
            {
                SorceryStrength += 10;
            }

            // HUMAN CLERICS READ BOOKS AND ARE MAGICAL?!
            if (soldierType == typeof(Human) && Classification == ClassificationEnum.Cleric)
            {
                SorceryStrength += 3;
            }
        }

        public void AssignArmorClass()
        {
            var soldierType = this.GetType();

            // KNIGHTS ARE BEEFY!! THIEVES ARE DODGY!!
            if (Classification == ClassificationEnum.Thief || Classification == ClassificationEnum.Knight)
            {
                ArmorClass += 10;
            }

            // HUMAN CLERICS READ BOOKS AND ARE MAGICAL?!
            if (soldierType == typeof(Human) && Classification == ClassificationEnum.Cleric)
            {
                ArmorClass += 3;
            }
        }

        public void AssignMagicResistanceBonus()
        {
            throw new NotImplementedException();
        }
    }
}
