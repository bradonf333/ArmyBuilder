using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyBuilder.Soldiers
{
    public class Human : BaseSoldier
    {
        public Human()
        {
            SoldierType = SoldierType.Human;
        }

        public Human(ClassificationEnum classificationEnum)
        {
            SoldierType = SoldierType.Human;
            Classification = classificationEnum;
            HumanStatModifiers();
        }

        private void HumanStatModifiers()
        {
            if (Classification == ClassificationEnum.Cleric)
            {
                SoldierStats.AttackStrength += 3;
                SoldierStats.SorceryStrength += 3;
            }
        }
    }
}
