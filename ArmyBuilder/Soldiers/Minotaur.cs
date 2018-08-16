using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyBuilder.Soldiers
{
    public class Minotaur : BaseSoldier
    {
        public Minotaur()
        {
            SoldierType = SoldierType.Minotaur;
            ApplyBonuses();
        }

        public override void ApplyBonuses()
        {
            SoldierStats.Defense += 5;
            SoldierStats.AttackStrength += 10;
            SoldierStats.BonusMagicDamage -= 1;

            if (Rank == Rank.General)
            {
                SoldierStats.BonusDamage += 10;
            }

            if (Classification == Class.Knight)
            {

            }
        }
    }
}
