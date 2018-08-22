namespace ArmyBuilder.Soldiers
{
    /// <summary>
    /// Lizardman are fairly strong but pretty resistant to Magic.
    /// </summary>
    public class Lizardman : BaseSoldier
    {
        public Lizardman()
        {
            SoldierType = SoldierType.Lizardman;
            ApplyBonuses();
            AssignAttackType();
        }

        public sealed override void ApplyBonuses()
        {
            SoldierStats.AttackStrength += 5;
            SoldierStats.MagicResistance += 10;
        }
    }
}
