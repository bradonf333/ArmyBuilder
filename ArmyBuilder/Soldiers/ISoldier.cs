using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyBuilder.Soldiers
{
    public interface ISoldier
    {
        string Name { get; set; }

        DateTime Birthdate { get; set; }

        string Race { get; set; }

        ClassificationEnum Classification { get; set; }

        RankEnum Rank { get; set; }

        SoldierStats SoldierStats { get; set; }
        SoldierType SoldierType { get; set; }

        void AssignBaseStats();

        void Attack();

        void Defend();

        /// <summary>
        /// Handles all Modifiers that are shared across ANY Soldier.
        /// If there are modifiers 
        /// </summary>
        void AssignStatModifiers();
    }
}
