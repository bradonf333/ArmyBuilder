using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyBuilder.Soldiers
{
    /// <summary>
    /// All soldiers need a classification to be created successfully. If this is not provided,
    /// a basic soldier will be created instead which has no classification.
    /// </summary>
    public class BaseSoldier : ISoldier
    {
        public BaseSoldier()
        {
            SoldierType = SoldierType.BasicSoldier;
            SoldierStats = new SoldierStats();
            AssignBaseStats();

            Classification = ClassificationEnum.None;
            Rank = RankEnum.Private;
        }

        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public string Race { get; set; }
        public ClassificationEnum Classification { get; set; }
        public RankEnum Rank { get; set; }
        public SoldierStats SoldierStats { get; set; }
        public SoldierType SoldierType { get; set; }

        public void AssignBaseStats()
        {
            // All soldiers have base stats
            SoldierStats.AttackStrength = 2;
            SoldierStats.SorceryStrength = 0;
            SoldierStats.ArmorClass = 0;
            SoldierStats.MagicResistance = 3;
        }

        public void Attack()
        {
            throw new NotImplementedException();
        }

        public void Defend()
        {
            throw new NotImplementedException();
        }

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
            if (Classification == ClassificationEnum.Thief 
                || Classification == ClassificationEnum.Knight)
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
