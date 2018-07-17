using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyBuilder.Soldiers
{
    public class Minotaur : Soldier
    {
        public Minotaur()
        {
            AttackStrength += 10;
            //BonusDamage += 5;
            BonusMagicDamage -= 1;
            Defense += 5;
        }

        public override string SoldierType()
        {
            return "Minotaur";
        }
    }
}
