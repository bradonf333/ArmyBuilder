using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyBuilder.Soldiers
{
    public class Lizardman : Soldier
    {
        public Lizardman()
        {
            AttackStrength += 5;

            /*
             * QUESTION:
             * Lizardmen always get a MagicResistance of 10. Should I do this by default,
             * or keep in the AssignSorceryStrengthBonus method?
             *
             * I think I like this option better. IF/WHEN a new child Soldier is added, we add them
             * as their own class and give them their own bonuses.
             * The base Soldier class doesnt worry about any child inheriting from it.
             *
             * Just need to figure out how to handle the Classification modifiers as well...
             */
            MagicResistance += 10;
        }

        public override string SoldierType()
        {
            return "Lizardman";
        }
    }
}
