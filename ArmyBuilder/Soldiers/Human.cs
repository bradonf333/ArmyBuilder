using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyBuilder.Soldiers
{
    public class Human : Soldier
    {
        public Human()
        {
            HumanStatModifiers();
        }

        public override string SoldierType()
        {
            return "Human";
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
