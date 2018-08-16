namespace ArmyBuilder.Soldiers
{
    public class Lizardman : BaseSoldier
    {
        public Lizardman()
        {
            SoldierType = SoldierType.Lizardman;
            ApplyBonuses();
        }

        public override void ApplyBonuses()
        {
            SoldierStats.AttackStrength += 5;
            SoldierStats.MagicResistance += 10;
        }
    }
}
