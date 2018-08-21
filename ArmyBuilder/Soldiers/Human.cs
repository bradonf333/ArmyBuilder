namespace ArmyBuilder.Soldiers
{
    /// <summary>
    /// Humans are fairly cool and get a little bit of a bonus.
    /// </summary>
    public class Human : BaseSoldier
    {
        /// <summary>
        /// Default Constructor leaves Classification as the default: BasicSoldier which doesn't give many stat modifiers.
        /// </summary>
        public Human()
        {
            SoldierType = SoldierType.Human;
            ApplyBonuses();
        }

        /// <summary>
        /// Pass in the Class which affects the Stat Modifiers
        /// </summary>
        /// <param name="classEnum"></param>
        public Human(Class classEnum)
        {
            SoldierType = SoldierType.Human;
            Classification = classEnum;
            ApplyBonuses();
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public sealed override void ApplyBonuses()
        {
            if (Classification == Class.Cleric)
            {
                SoldierStats.AttackStrength += 3;
                SoldierStats.SorceryStrength += 3;
            }
        }
    }
}
