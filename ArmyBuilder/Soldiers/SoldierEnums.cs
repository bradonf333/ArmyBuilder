using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyBuilder.Soldiers
{
    /// <summary>
    /// Cool types of Soldier Classes you can pick from!!
    /// </summary>
    public enum Class
    {
        Knight, Wizard, Cleric, Thief, None
    }

    /// <summary>
    /// The main building block of the Soldier. What type do you wanna pick??
    /// </summary>
    public enum SoldierType
    {
        BasicSoldier, Lizardman, Human, Elf, Minotaur
    }

    /// <summary>
    /// Your soldiers rank! Generals are the strongest if you ask me, but are rare and limited son!
    /// </summary>
    public enum Rank
    {
        Private, Sergeant, Captain, General
    }
}
