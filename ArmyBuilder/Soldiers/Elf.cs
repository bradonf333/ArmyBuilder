using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyBuilder.Soldiers
{
    public class Elf : BaseSoldier, ISpecificBonus
    {
        public Elf()
        {
            SoldierType = SoldierType.Elf;
            SoldierStats.SorceryStrength += 10;
        }

        public void ApplySpecificBonuses()
        {
            
        }
    }
}
