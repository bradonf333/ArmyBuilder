using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyBuilder.Soldiers
{
    public class SoldierBuilder
    {
        public static ISoldier CreateSoldier()
        {
            var soldierName = PickSoldierName();
            var hitPoints = PickSoldierHitPoints(); // Probably get rid of this in future and have it decided by the other info
            var soldierBirthday = PickSoldierBirthdate();
            var soldierClass = PickSoldierClassification();
            var soldierRank = PickSoldierRank();
            var soldierType = PickSoldierType();

            var soldier = BuildSoldier(soldierType);
            soldier.Classification = soldierClass;
            soldier.Rank = soldierRank;
            soldier.Name = soldierName;
            soldier.Birthdate = soldierBirthday;
            soldier.SoldierStats.HitPoints = hitPoints;

            soldier.AssignStatModifiers();
            Console.Clear();

            return soldier;
        }

        

        private static BaseSoldier BuildSoldier(SoldierType soldierType)
        {
            if (soldierType == SoldierType.Lizardman)
            {
                return new Lizardman();
            }
            if (soldierType == SoldierType.Human)
            {
                return new Human();
            }
            if (soldierType == SoldierType.Elf)
            {
                return new Elf();
            }
            if (soldierType == SoldierType.Minotaur)
            {
                return new Minotaur();
            }

            return new BaseSoldier();
        }

        private static SoldierType PickSoldierType()
        {
            Console.Clear();

            var soldierTypes = Enum.GetNames(typeof(SoldierType));
            Console.WriteLine("What type of soldier do you want to create?");

            WriteSoldierTypes(soldierTypes);

            var input = Console.ReadKey();
            Console.WriteLine(input.Key);
            switch (input.Key)
            {
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    return SoldierType.Lizardman;
                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    return SoldierType.Human;
                case ConsoleKey.NumPad3:
                case ConsoleKey.D3:
                    return SoldierType.Elf;
                case ConsoleKey.NumPad4:
                case ConsoleKey.D4:
                    return SoldierType.Minotaur;
                default:
                    return SoldierType.BasicSoldier;
            }
        }

        private static void WriteSoldierTypes(string[] soldierTypes)
        {
            for (int i = 0; i < soldierTypes.Length; i++)
            {
                Console.WriteLine($"{i} : {soldierTypes[i]}");
            }
        }

        public static DateTime PickSoldierBirthdate()
        {
            Console.Clear();
            Console.WriteLine($"\nWhat is your soldier's birthdate? Use format MM/DD/YYYY.");
            var birthdate = Console.ReadLine();
            return String.IsNullOrWhiteSpace(birthdate) ? DateTime.Now : DateTime.Parse(birthdate);
        }

        public static int PickSoldierHitPoints()
        {
            Console.Clear();
            Console.WriteLine($"\nHow tough is your soldier? Pick a number between 1 (weak) and 10 (strong).");
            return Convert.ToInt32(Console.ReadLine());
        }

        public static string PickSoldierName()
        {
            Console.Clear();
            Console.WriteLine("\nWhat is the name of the solider you want to create?");
            return Console.ReadLine();
        }

        public static RankEnum PickSoldierRank()
        {
            Console.Clear();
            Console.WriteLine($"\nNext, pick a rank for your soldier!");

            var canContinueWithoutRank = false;
            while (!canContinueWithoutRank)
            {
                Console.WriteLine("Press P for Private.");
                Console.WriteLine("Press S for Sergeant.");
                Console.WriteLine("Press C for Captain.");
                Console.WriteLine("Press G for General.");

                var input = Console.ReadKey();
                if (input.Key == ConsoleKey.P)
                {
                    return RankEnum.Private;
                }

                if (input.Key == ConsoleKey.S)
                {
                    return RankEnum.Sergeant;
                }

                if (input.Key == ConsoleKey.C)
                {
                    return RankEnum.Captain;
                }

                if (input.Key == ConsoleKey.G)
                {
                    return RankEnum.General;
                }
            }

            return RankEnum.Private;
        }

        public static ClassificationEnum PickSoldierClassification()
        {
            Console.Clear();
            Console.WriteLine($"\nNext, pick a class for your soldier!");
            var canContinueWithoutClassification = false;
            while (!canContinueWithoutClassification)
            {
                Console.WriteLine("Press K for Knight.");
                Console.WriteLine("Press W for Wizard.");
                Console.WriteLine("Press C for Cleric.");
                Console.WriteLine("Press T for Thief.");

                var input = Console.ReadKey();
                if (input.Key == ConsoleKey.K)
                {
                    return ClassificationEnum.Knight;
                }

                if (input.Key == ConsoleKey.W)
                {
                    return ClassificationEnum.Wizard;
                }

                if (input.Key == ConsoleKey.C)
                {
                    return ClassificationEnum.Cleric;
                }

                if (input.Key == ConsoleKey.T)
                {
                    return ClassificationEnum.Thief;
                }
            }

            return ClassificationEnum.None;
        }
    }
}
