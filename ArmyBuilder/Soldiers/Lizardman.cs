using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyBuilder.Soldiers
{
    public class Lizardman : Soldier
    {
        public Lizardman()
        {
            AttackStrength += 5;
            MagicResistance += 10;
        }

        public override string SoldierType()
        {
            return "Lizardman";
        }
    }
}
