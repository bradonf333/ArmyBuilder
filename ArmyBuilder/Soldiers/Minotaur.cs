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
            SoldierStats.AttackStrength += 10;
            // BonusDamage += 5;
            SoldierStats.BonusMagicDamage -= 1;
            SoldierStats.Defense += 5;
        }

        public override string SoldierType()
        {
            return "Minotaur";
        }
    }
}
