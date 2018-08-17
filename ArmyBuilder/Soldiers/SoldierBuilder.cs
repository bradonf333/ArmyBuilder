using System;
using ArmyBuilder.Writers;

namespace ArmyBuilder.Soldiers
{
    public class SoldierBuilder
    {
        private readonly IWriter _writer;

        public SoldierBuilder(IWriter writer)
        {
            _writer = writer;
        }

        public ISoldier CreateSoldier()
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
            _writer.ClearMessage();

            return soldier;
        }

        private BaseSoldier BuildSoldier(SoldierType soldierType)
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

        private SoldierType PickSoldierType()
        {
            _writer.ClearMessage();

            var soldierTypes = Enum.GetNames(typeof(SoldierType));
            _writer.WriteMessage("What type of soldier do you want to create?");

            DisplaySoldierTypes(soldierTypes);

            var input = Console.ReadKey();
            _writer.WriteMessage(input.Key.ToString());
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

        private void DisplaySoldierTypes(string[] soldierTypes)
        {
            for (int i = 0; i < soldierTypes.Length; i++)
            {
                _writer.WriteMessage($"{i} : {soldierTypes[i]}");
            }
        }

        public DateTime PickSoldierBirthdate()
        {
            _writer.ClearMessage();
            _writer.WriteMessage($"\nWhat is your soldier's birthdate? Use format MM/DD/YYYY.");
            var birthdate = Console.ReadLine();
            return string.IsNullOrWhiteSpace(birthdate) ? DateTime.Now : DateTime.Parse(birthdate);
        }

        public int PickSoldierHitPoints()
        {
            _writer.ClearMessage();
            _writer.WriteMessage($"\nHow tough is your soldier? Pick a number between 1 (weak) and 10 (strong).");
            return Convert.ToInt32(Console.ReadLine());
        }

        public string PickSoldierName()
        {
            _writer.ClearMessage();
            _writer.WriteMessage("\nWhat is the name of the solider you want to create?");
            return Console.ReadLine();
        }

        public Rank PickSoldierRank()
        {
            _writer.ClearMessage();
            _writer.WriteMessage($"\nNext, pick a rank for your soldier!");

            var canContinueWithoutRank = false;
            while (!canContinueWithoutRank)
            {
                _writer.WriteMessage("Press P for Private.");
                _writer.WriteMessage("Press S for Sergeant.");
                _writer.WriteMessage("Press C for Captain.");
                _writer.WriteMessage("Press G for General.");

                var input = Console.ReadKey();
                if (input.Key == ConsoleKey.P)
                {
                    return Rank.Private;
                }

                if (input.Key == ConsoleKey.S)
                {
                    return Rank.Sergeant;
                }

                if (input.Key == ConsoleKey.C)
                {
                    return Rank.Captain;
                }

                if (input.Key == ConsoleKey.G)
                {
                    return Rank.General;
                }
            }

            return Rank.Private;
        }

        public Class PickSoldierClassification()
        {
            _writer.ClearMessage();
            _writer.WriteMessage($"\nNext, pick a class for your soldier!");
            var canContinueWithoutClassification = false;
            while (!canContinueWithoutClassification)
            {
                _writer.WriteMessage("Press K for Knight.");
                _writer.WriteMessage("Press W for Wizard.");
                _writer.WriteMessage("Press C for Cleric.");
                _writer.WriteMessage("Press T for Thief.");

                var input = Console.ReadKey();
                if (input.Key == ConsoleKey.K)
                {
                    return Class.Knight;
                }

                if (input.Key == ConsoleKey.W)
                {
                    return Class.Wizard;
                }

                if (input.Key == ConsoleKey.C)
                {
                    return Class.Cleric;
                }

                if (input.Key == ConsoleKey.T)
                {
                    return Class.Thief;
                }
            }

            return Class.None;
        }
    }
}
