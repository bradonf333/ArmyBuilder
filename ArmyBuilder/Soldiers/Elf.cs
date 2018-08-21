namespace ArmyBuilder.Soldiers
{
    public class Elf : BaseSoldier
    {
        /// <summary>
        /// Elfs are Cool and Magical
        /// </summary>
        public Elf()
        {
            SoldierType = SoldierType.Elf;
            SoldierStats.SorceryStrength += 10;
        }
    }
}
