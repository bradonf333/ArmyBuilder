namespace ArmyBuilder.Soldiers
{
    /// <summary>
    /// Minotaur are the strongest. Level these guys up to a general and you got a 1 man army. Hint... Hint...
    /// </summary>
    public class Minotaur : BaseSoldier
    {
        public Minotaur()
        {
            SoldierType = SoldierType.Minotaur;
            ApplyBonuses();
        }

        public sealed override void ApplyBonuses()
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
