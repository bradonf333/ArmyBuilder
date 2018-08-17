namespace ArmyBuilder.Soldiers
{
    public class Lizardman : BaseSoldier
    {
        public Lizardman()
        {
            SoldierType = SoldierType.Lizardman;
            ApplyBonuses();
        }

        public sealed override void ApplyBonuses()
        {
            SoldierStats.AttackStrength += 5;
            SoldierStats.MagicResistance += 10;
        }
    }
}
