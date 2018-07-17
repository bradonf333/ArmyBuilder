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
            
        }

        public override string SoldierType()
        {
            return "Human";
        }
    }
}
