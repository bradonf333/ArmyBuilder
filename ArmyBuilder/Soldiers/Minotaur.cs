using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyBuilder.Soldiers
{
    public class Minotaur : BaseSoldier, ISpecificBonus
    {
        public Minotaur()
        {
            SoldierType = SoldierType.Minotaur;
            ApplySpecificBonuses();
        }

        public void ApplySpecificBonuses()
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
