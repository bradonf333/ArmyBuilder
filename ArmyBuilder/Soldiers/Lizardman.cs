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
            SoldierStats.AttackStrength += 5;

            /*
             * QUESTION:
             * Just need to figure out how to handle the Classification modifiers as well...
             */
            SoldierStats.MagicResistance += 10;
        }

        public override string SoldierType()
        {
            return "Lizardman";
        }
    }
}
