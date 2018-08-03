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
            SoldierStats.SorceryStrength += 10;
        }

        public override string SoldierType()
        {
            return "Elf";
        }

    }
}
