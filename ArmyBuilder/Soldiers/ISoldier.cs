using System;

namespace ArmyBuilder.Soldiers
{
    public interface ISoldier
    {
        string Name { get; set; }
        DateTime Birthdate { get; set; }
        string Race { get; set; }
        Class Classification { get; set; }
        Rank Rank { get; set; }
        SoldierStats SoldierStats { get; set; }
        SoldierType SoldierType { get; set; }
        bool IsDead { get; set; }

        /// <summary>
        /// Returns the Soldiers Attack
        /// </summary>
        /// <returns></returns>
        int Attack();

        /// <summary>
        /// Defend against enemy attack.
        /// Need to know the AttackType and the Damage to Defend against.
        /// </summary>
        /// <param name="attackType"></param>
        /// <param name="attackDamage"></param>
        void Defend(AttackType attackType, int attackDamage);

        /// <summary>
        /// Display the Soldiers information
        /// </summary>
        /// <returns></returns>
        string DisplayNewSoldier();

        /// <summary>
        /// Assign the Base stats for the Soldier
        /// </summary>
        void AssignBaseStats();

        /// <summary>
        /// Handles all Modifiers that are shared across ANY Soldier.
        /// If there are modifiers 
        /// </summary>
        void AssignStatModifiers();
    }
}
