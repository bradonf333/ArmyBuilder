using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyBuilder.Soldiers
{
    public class Elf : Soldier
    {
        public Elf()
        {
            /*
             * QUESTION:
             * Elves always get a SorceryStrengthBonus of 10. Should I do this by default,
             * or keep in the AssignSorceryStrengthBonus method?
             */

            SorceryStrength += 10;
        }

        public override string SoldierType()
        {
            return "Elf";
        }

    }
}
