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
            var monster = new Monster(); // New!! Dependency, since its in the Program class might be ok?

            var recruits = CreateRecruits(writer);
            var army = new Army(recruits, writer, reader); // New!! Dependency, since its in the Program class might be ok?

            army.Battle(monster);

            reader.ReadChar();
        }

        /// <summary>
        /// Send Army to battle the evil monster
        /// </summary>
        /// <param name="army"></param>
        private static void SendArmyToDoBattle(Army army)
        {
            var numberGenerator = new Random();
            var monsterHitPoints = numberGenerator.Next(50, 100);
            Console.WriteLine("\nThe foul monster sallies forth from its lair...");
            Thread.Sleep(2000);
            Console.WriteLine("\nSlime and flame eminate from the beast as it approaches...");
            Thread.Sleep(2000);
            Console.WriteLine("\nThe army attacks!");
            Thread.Sleep(2000);

            while (monsterHitPoints > 0)
            {
                //foreach (var recruit in army.Recruits)
                //{
                //    recruit.Attack();
                //}

                for (int i = 0; i < army.Recruits.Count; i++)
                {
                    if (army != null)
                    {
                        var attackStrength = Convert.ToInt32(army.Recruits[i].SoldierStats.AttackStrength);
                        var sorceryStrength = Convert.ToInt32(army.Recruits[i].SoldierStats.SorceryStrength);
                        if (attackStrength > sorceryStrength)
                        {
                            var damage = numberGenerator.Next(1, attackStrength);
                            damage += CheckForBonusAttackDamage(army.Recruits[i]);
                            monsterHitPoints -= damage;
                            Console.WriteLine($"\n{army.Recruits[i].Name} charges forth to attack and slashes at the monster for {damage} points of damage!");
                        }
                        else
                        {
                            var damage = numberGenerator.Next(1, sorceryStrength);
                            damage += CheckForBonusMagicDamage(army.Recruits[i]);
                            monsterHitPoints -= damage;
                            Console.WriteLine($"\n{army.Recruits[i].Name} pauses, chants a loud and ancient phrase, when suddenly lightning strikes the monster for {damage} points of damage!");
                        }
                    }
                }

                if (monsterHitPoints > 0)
                {
                    Console.WriteLine("Despite your best attacks, the monster still lives. It charges forth, and attacks!");

                    for (int i = 0; i < army.Recruits.Count; i++)
                    {
                        if (army != null)
                        {
                            var armorClass = army.Recruits[i].SoldierStats.ArmorResistance;
                            var magicResistance = army.Recruits[i].SoldierStats.MagicResistance;
                            var hitPoints = Convert.ToInt32(army.Recruits[i].SoldierStats.HitPoints);

                            if (i % 2 == 0)
                            {
                                var damage = numberGenerator.Next(1, 10);
                                damage -= armorClass;
                                damage -= CheckForAttackDefenseBonus(army.Recruits[i]);
                                if (damage >= hitPoints)
                                {
                                    Console.WriteLine($"{army.Recruits[i].Name} is hit by the monster with a mightly blow, and falls dead to the earth.\n");
                                    army.Recruits[i] = null;
                                }
                                else
                                {
                                    Console.WriteLine($"{army.Recruits[i].Name} is hit by the monster with a mightly blow, but recovers and is ready for another round of battle!\n");
                                }
                            }
                            else
                            {
                                var damage = numberGenerator.Next(1, 10);
                                damage -= magicResistance;
                                damage -= CheckForMagicDefenseBonus(army.Recruits[i]);
                                if (damage >= hitPoints)
                                {
                                    Console.WriteLine($"Flames spew forth from the tentacles of the beast and envelop {army.Recruits[i].Name} who falls dead to the earth.\n");
                                    army.Recruits[i] = null;
                                }
                                else
                                {
                                    Console.WriteLine($"{army.Recruits[i].Name} is hit by the magical flames, but recovers and is ready for another round of battle!\n");
                                }
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("\nThe monster is vanquished!!!!! All hail the king!");
                    break;
                }
                Console.WriteLine("Press any key to continue the battle...");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Check if the Soldier has a Magic Defense Bonus
        /// </summary>
        /// <param name="soldier">Pass in a Soldier Object</param>
        /// <returns>Magic Defense Bonus as an integer</returns>
        private static int CheckForMagicDefenseBonus(ISoldier soldier)
        {
            var bonusDefense = 0;

            // @@@ Moved to the Minotaur class! --DONE
            //var race = soldier.Race;

            // MINOTAURS TAKE 5 LESS MAGIC DAMAGE
            //if (race == "Minotaur")
            //{
            //    bonusDefense += 5;
            //}

            return bonusDefense;
        }

        /// <summary>
        /// Check if the Soldier has an Attack Defense Bonus
        /// </summary>
        /// <param name="soldier">Pass in a Soldier Object</param>
        /// <returns>Attack Defense Bonus as an integer</returns>
        private static int CheckForAttackDefenseBonus(ISoldier soldier)
        {
            var bonusDefense = 0;

            var classification = soldier.Classification;

            // @@@ - Moved to the Soldier Class --DONE
            // KNIGHTS TAKE 5 LESS ATTACK DAMAGE
            //if (classification == ClassificationEnum.Knight)
            //{
            //    bonusDefense += 5;
            //}

            return bonusDefense;
        }

        /// <summary>
        /// Check if the Soldier has a Magic Damage bonus
        /// </summary>
        /// <param name="soldier"></param>
        /// <returns></returns>
        private static int CheckForBonusMagicDamage(ISoldier soldier)
        {
            var bonusDamage = 0;
            var race = soldier.Race;
            var rank = soldier.Rank;
            var classification = soldier.Classification;

            // MINOTAUR GENERALS DO 15 EXTRA DAMAGE
            if (race == "Minotaur" && rank == Rank.General)
            {
                bonusDamage += 10;
            }

            // LIZARDMEN PRIVATES DO 1 EXTRA DAMAGE
            if (race == "Lizardman" && rank == Rank.Private)
            {
                bonusDamage += 1;
            }

            // HUMAN KNIGHTS DO 5 EXTRA DAMAGE
            if (race == "Human" && classification == Class.Knight)
            {
                bonusDamage += 5;
            }

            // IF AN ELF IS DOING A NON-MAGIC ATTACK, IT GETS A PENALTY OF -2
            if (race == "Elf")
            {
                bonusDamage -= 1;
            }

            return bonusDamage;
        }

        /// <summary>
        /// Check if the Soldier has an Attack Damage bonus
        /// </summary>
        /// <param name="soldier"></param>
        /// <returns></returns>
        private static int CheckForBonusAttackDamage(ISoldier soldier)
        {
            var bonusMagicDamage = 0;
            var race = soldier.Race;
            var classification = soldier.Classification;
            var rank = soldier.Rank;
            var birthdate = soldier.Birthdate;

            // ELF GENERALS DO 15 EXTRA MAGIC DAMAGE
            if (race == "Elf" && rank == Rank.General)
            {
                bonusMagicDamage += 15;
            }

            // LIZARDMEN CAPTAINS DO 10 EXTRA MAGIC DAMAGE
            if (race == "Lizardman" && rank == Rank.Captain)
            {
                bonusMagicDamage += 10;
            }

            // HUMAN WIZARDS DO 3 EXTRA DAMAGE
            if (race == "Human" && classification == Class.Wizard)
            {
                bonusMagicDamage += 3;
            }

            // ON THEIR BIRTHDAY, HUMANS DO MEGA DAMAGE!
            if (race == "Human" && birthdate.Date == DateTime.Today)
            {
                bonusMagicDamage += 100;
            }

            // IF A MINOTAUR IS DOING A NON-STRENGTH ATTACK, IT GETS A PENALTY OF -2
            if (race == "Minotaur")
            {
                bonusMagicDamage -= 1;
            }

            return bonusMagicDamage;
        }

        private static List<ISoldier> CreateRecruits(IWriter writer)
        {
            var soldierBuilder = new SoldierBuilder(writer);

            var createMoreSoldiers = true;
            var recruits = new List<ISoldier>();
            while (createMoreSoldiers)
            {
                var soldier = soldierBuilder.CreateSoldier();
                writer.WriteMessage(soldier.DisplayNewSoldier());
                recruits.Add(soldier);

                writer.WriteMessage("\nWould you like to create more soliders? Press Y to create more, or N to add soldiers to the army.");
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