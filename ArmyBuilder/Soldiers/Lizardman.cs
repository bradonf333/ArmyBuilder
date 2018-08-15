namespace ArmyBuilder.Soldiers
{
    public class Lizardman : BaseSoldier, ISpecificBonus
    {
        public Lizardman()
        {
            SoldierType = SoldierType.Lizardman;
            ApplySpecificBonuses();
        }

        public void ApplySpecificBonuses()
        {
            SoldierStats.AttackStrength += 5;
            SoldierStats.MagicResistance += 10;
        }
    }
}
