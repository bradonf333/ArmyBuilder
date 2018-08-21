namespace ArmyBuilder.Enemies
{
    /// <summary>
    /// All the necessary things needed to be classified as an enemy!
    /// </summary>
    public interface IEnemy
    {
        int HitPoints { get; set; }
        int PhysicalAttackDamage { get; set; }
        int MagicalAttackDamage { get; set; }
        AttackType AttackType { get; set; }
        bool IsDead { get; set; }

        int Attack();
        void Defend(int damage);
        
        /// <summary>
        /// Easily readable way to display the Type of the Enemy
        /// </summary>
        /// <returns>Enemies type as a string</returns>
        string EnemyTypeToString();
    }
}
