using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ArmyBuilder.Enemies;
using ArmyBuilder.Input;
using ArmyBuilder.Output;
using ArmyBuilder.Soldiers;
using ArmyBuilder.Writers;

namespace ArmyBuilder
{
    /// <summary>
    /// ArmyBuilder app is a console application which lets you create an army of Soldiers.
    /// When the Soldier has been added to the army they become a recruit.
    /// When the army is full you can send the army to battle the evil monster.
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            var writer = new ConsoleWriter();
            var reader = new ConsoleReader();
            var monster = new Monster();

            var recruits = CreateRecruits(writer);
            var army = new Army(recruits, writer, reader);

            army.Battle(monster);
            
            writer.WriteMessage("Press any key to continue");
            reader.ReadChar();
        }
        
        /// <summary>
        /// Create a bunch of recruits!
        /// </summary>
        /// <param name="writer"></param>
        /// <returns></returns>
        private static List<ISoldier> CreateRecruits(IWriter writer)
        {
            var soldierBuilder = new SoldierBuilder(writer);

            var recruits = new List<ISoldier>();
            while (true)
            {
                var soldier = soldierBuilder.CreateSoldier();
                writer.WriteMessage(soldier.DisplayNewSoldier());
                recruits.Add(soldier);

                writer.WriteMessage("\nWould you like to create more soliders?" +
                                    "\nPress Y to create more, or N to stop add created soldiers to the army.");
                if (Console.ReadKey().Key == ConsoleKey.N)
                {
                    break;
                }
            }

            writer.ClearMessage();
            return recruits;
        }
    }
}