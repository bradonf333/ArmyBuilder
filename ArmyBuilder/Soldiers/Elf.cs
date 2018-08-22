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
            ApplyBonuses();
            AssignAttackType();
        }

        public sealed override void ApplyBonuses()
        {
            SoldierStats.SorceryStrength += 10;
        }
    }
}
