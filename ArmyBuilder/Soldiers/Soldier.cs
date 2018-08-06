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

        public DateTime Birthdate { get; set; }

        public string Race { get; set; }

        public ClassificationEnum Classification { get; set; }

        public RankEnum Rank { get; set; }

        public SoldierStats SoldierStats { get; set; }

        public Soldier()
        {
            SoldierStats = new SoldierStats();
            AssignBaseStats();

            Classification = ClassificationEnum.None;
            Rank = RankEnum.Private;
        }

        private void AssignBaseStats()
        {
            // All soldiers have base stats
            SoldierStats.AttackStrength = 2;
            SoldierStats.SorceryStrength = 0;
            SoldierStats.ArmorClass = 0;
            SoldierStats.MagicResistance = 3;
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

        /// <summary>
        /// Handles all Modifiers that are shared across ANY Soldier.
        /// If there are modifiers 
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
            if (Classification == ClassificationEnum.Knight)
            {
                SoldierStats.Defense += 5;
            }
        }

        private void AssignAttackBonus()
        {
            if (Classification == ClassificationEnum.Knight)
            {
                SoldierStats.AttackStrength += 10;
            }
        }

        private void AssignSorceryStrengthBonus()
        {
            var soldierType = GetType();

            // WIZARDS ARE MAGICAL!!
            if (Classification == ClassificationEnum.Wizard)
            {
                SoldierStats.SorceryStrength += 10;
            }
        }

        private void AssignArmorClass()
        {
            var soldierType = this.GetType();

            // KNIGHTS ARE BEEFY!! THIEVES ARE DODGY!!
            if (Classification == ClassificationEnum.Thief || Classification == ClassificationEnum.Knight)
            {
                SoldierStats.ArmorClass += 10;
            }
        }

        private void AssignMagicResistanceBonus()
        {
            SoldierStats.MagicResistance = 0;
        }
    }
}
