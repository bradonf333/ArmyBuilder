using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyBuilder.Soldiers
{
    /// <summary>
    /// Soldier should probably be a base class which is used to create the specialized soldiers
    /// i.e. each race would be a specialization of Soldier
    /// </summary>
    public class Soldier
    {
        public string Name { get; set; }

        public string Race { get; set; }

        public ClassificationEnum Classification { get; set; }

        public RankEnum Rank { get; set; }

        public int HitPoints { get; set; }

        public DateTime Birthdate { get; set; }

        public int AttackStrength { get; set; }

        public int SorceryStrength { get; set; }

        public int MagicResistance { get; set; }

        public int ArmorClass { get; set; }

        public int BonusDamage { get; set; }

        public int BonusMagicDamage { get; set; }

        public int Defense { get; set; }

        public Soldier()
        {
            // All soldiers have a base AttackStrength of 2
            AttackStrength = 2;
            Classification = ClassificationEnum.None;
            Rank = RankEnum.Private;
        }

        public virtual string SoldierType()
        {
            return "Basic Soldier";
        }

        public void Attack()
        {

        }

        public void Defend()
        {

        }

        public Soldier AssignSoldierType()
        {
            Console.Clear();
            Console.WriteLine("What type of soldier do you want to create?");
            Console.WriteLine("Press L for Lizardman.");
            Console.WriteLine("Press H for Human.");
            Console.WriteLine("Press E for Elf.");
            Console.WriteLine("Press M for Minotaur.");

            var input = Console.ReadKey();

            if (input.Key == ConsoleKey.L)
            {
                return new Lizardman();
            }

            if (input.Key == ConsoleKey.H)
            {
                return new Human();
            }

            if (input.Key == ConsoleKey.E)
            {
                return new Elf();
            }

            if (input.Key == ConsoleKey.M)
            {
                return new Minotaur();
            }

            Console.Clear();

            return new Soldier();
        }
    }
}
