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
            SoldierStats.BonusMagicDamage -= 1;
            SoldierStats.Defense += 5;
        }

        // How to handle these modifiers? Maybe how Human is doing it?
        public override void AssignStatModifiers()
        {
            if (Classification == ClassificationEnum.Knight)
            {

            }
        }

        public override string SoldierType()
        {
            return "Minotaur";
        }
    }
}
