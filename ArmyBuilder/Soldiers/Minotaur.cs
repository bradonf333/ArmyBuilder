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
            MinotaurStatModifiers();
        }

        // How to handle these modifiers? Maybe how Human is doing it?
        private void MinotaurStatModifiers()
        {
            SoldierStats.Defense += 5;
            SoldierStats.AttackStrength += 10;
            SoldierStats.BonusMagicDamage -= 1;

            if (Rank == RankEnum.General)
            {
                SoldierStats.BonusDamage += 10;
            }

            if (Classification == ClassificationEnum.Knight)
            {

            }
        }
    }
}
