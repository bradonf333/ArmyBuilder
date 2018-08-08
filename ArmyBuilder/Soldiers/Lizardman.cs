using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyBuilder.Soldiers
{
    public class Lizardman : BaseSoldier
    {
        public Lizardman()
        {
            SoldierType = SoldierType.Lizardman;
            SoldierStats.AttackStrength += 5;
            SoldierStats.MagicResistance += 10;
        }
    }
}
