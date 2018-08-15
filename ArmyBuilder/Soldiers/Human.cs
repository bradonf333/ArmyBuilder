using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyBuilder.Soldiers
{
    public class Human : BaseSoldier, ISpecificBonus
    {
        /// <summary>
        /// Default Constructor leaves Classification as the default: BasicSoldier which doesn't give many stat modifiers.
        /// </summary>
        public Human()
        {
            SoldierType = SoldierType.Human;
            ApplySpecificBonuses();
        }

        /// <summary>
        /// Pass in the Classification which affects the Stat Modifiers
        /// </summary>
        /// <param name="classificationEnum"></param>
        public Human(ClassificationEnum classificationEnum)
        {
            SoldierType = SoldierType.Human;
            Classification = classificationEnum;
            ApplySpecificBonuses();
        }

        public void ApplySpecificBonuses()
        {
            if (Classification == ClassificationEnum.Cleric)
            {
                SoldierStats.AttackStrength += 3;
                SoldierStats.SorceryStrength += 3;
            }
        }
    }
}
