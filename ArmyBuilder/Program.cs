using System;
using System.Collections.Generic;
using System.Threading;

using ArmyBuilder.Soldiers;

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
            var recruits = CreateRecruits();

            var army = MarshallArmy(recruits);

            SendArmyToDoBattle(army);

            Console.ReadKey();
        }

        /// <summary>
        /// Send Army to battle the evil monster
        /// </summary>
        /// <param name="army"></param>
        private static void SendArmyToDoBattle(List<Soldier> army)
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
                for (int i = 0; i < army.Count; i++)
                {
                    if (army[i] != null)
                    {
                        var attackStrength = Convert.ToInt32(army[i].AttackStrength);
                        var sorceryStrength = Convert.ToInt32(army[i].SorceryStrength);
                        if (attackStrength > sorceryStrength)
                        {
                            var damage = numberGenerator.Next(1, attackStrength);
                            damage += CheckForBonusAttackDamage(army[i]);
                            monsterHitPoints -= damage;
                            Console.WriteLine($"\n{army[i].Name} charges forth to attack and slashes at the monster for {damage} points of damage!");
                        }
                        else
                        {
                            var damage = numberGenerator.Next(1, sorceryStrength);
                            damage += CheckForBonusMagicDamage(army[i]);
                            monsterHitPoints -= damage;
                            Console.WriteLine($"\n{army[i].Name} pauses, chants a loud and ancient phrase, when suddenly lightning strikes the monster for {damage} points of damage!");
                        }
                    }
                }

                if (monsterHitPoints > 0)
                {
                    Console.WriteLine("Despite your best attacks, the monster still lives. It charges forth, and attacks!");

                    for (int i = 0; i < army.Count; i++)
                    {
                        if (army[i] != null)
                        {
                            var armorClass = army[i].ArmorClass;
                            var magicResistance = army[i].MagicResistance;
                            var hitPoints = Convert.ToInt32(army[i].HitPoints);

                            if (i % 2 == 0)
                            {
                                var damage = numberGenerator.Next(1, 10);
                                damage -= armorClass;
                                damage -= CheckForAttackDefenseBonus(army[i]);
                                if (damage >= hitPoints)
                                {
                                    Console.WriteLine($"{army[i].Name} is hit by the monster with a mightly blow, and falls dead to the earth.\n");
                                    army[i] = null;
                                }
                                else
                                {
                                    Console.WriteLine($"{army[i].Name} is hit by the monster with a mightly blow, but recovers and is ready for another round of battle!\n");
                                }
                            }
                            else
                            {
                                var damage = numberGenerator.Next(1, 10);
                                damage -= magicResistance;
                                damage -= CheckForMagicDefenseBonus(army[i]);
                                if (damage >= hitPoints)
                                {
                                    Console.WriteLine($"Flames spew forth from the tentacles of the beast and envelop {army[i].Name} who falls dead to the earth.\n");
                                    army[i] = null;
                                }
                                else
                                {
                                    Console.WriteLine($"{army[i].Name} is hit by the magical flames, but recovers and is ready for another round of battle!\n");
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
        private static int CheckForMagicDefenseBonus(Soldier soldier)
        {
            var bonusDefense = 0;

            var race = soldier.Race;

            // MINOTAURS TAKE 5 LESS MAGIC DAMAGE
            if (race == "Minotaur")
            {
                bonusDefense += 5;
            }

            return bonusDefense;
        }

        /// <summary>
        /// Check if the Soldier has an Attack Defense Bonus
        /// </summary>
        /// <param name="soldier">Pass in a Soldier Object</param>
        /// <returns>Attack Defense Bonus as an integer</returns>
        private static int CheckForAttackDefenseBonus(Soldier soldier)
        {
            var bonusDefense = 0;

            var classification = soldier.Classification;

            // KNIGHTS TAKE 5 LESS ATTACK DAMAGE
            if (classification == ClassificationEnum.Knight)
            {
                bonusDefense += 5;
            }

            return bonusDefense;
        }

        /// <summary>
        /// Check if the Soldier has a Magic Damage bonus
        /// </summary>
        /// <param name="soldier"></param>
        /// <returns></returns>
        private static int CheckForBonusMagicDamage(Soldier soldier)
        {
            var bonusDamage = 0;
            var race = soldier.Race;
            var rank = soldier.Rank;
            var classification = soldier.Classification;

            // MINOTAUR GENERALS DO 15 EXTRA DAMAGE
            if (race == "Minotaur" && rank == RankEnum.General)
            {
                bonusDamage += 10;
            }

            // LIZARDMEN PRIVATES DO 1 EXTRA DAMAGE
            if (race == "Lizardman" && rank == RankEnum.Private)
            {
                bonusDamage += 1;
            }

            // HUMAN KNIGHTS DO 5 EXTRA DAMAGE
            if (race == "Human" && classification == ClassificationEnum.Knight)
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
        private static int CheckForBonusAttackDamage(Soldier soldier)
        {
            var bonusMagicDamage = 0;
            var race = soldier.Race;
            var classification = soldier.Classification;
            var rank = soldier.Rank;
            var birthdate = soldier.Birthdate;

            // ELF GENERALS DO 15 EXTRA MAGIC DAMAGE
            if (race == "Elf" && rank == RankEnum.General)
            {
                bonusMagicDamage += 15;
            }

            // LIZARDMEN CAPTAINS DO 10 EXTRA MAGIC DAMAGE
            if (race == "Lizardman" && rank == RankEnum.Captain)
            {
                bonusMagicDamage += 10;
            }

            // HUMAN WIZARDS DO 3 EXTRA DAMAGE
            if (race == "Human" && classification == ClassificationEnum.Wizard)
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

        private static List<Soldier> MarshallArmy(IEnumerable<Soldier> recruits)
        {
            var army = new List<Soldier>();
            var maxGeneralCount = 1;
            var maxCaptainCount = 5;

            var generalCount = 0;
            var captainCount = 0;
            var sergeantCount = 0;
            var privateCount = 0;

            foreach (var recruit in recruits)
            {
                if (recruit.Rank == RankEnum.General)
                {
                    if (generalCount == 0)
                    {
                        army.Add(recruit);
                        generalCount = 1;
                        Console.WriteLine($"\n{recruit.Name} has been added to the army!");
                    }
                    else
                    {
                        Console.WriteLine("You can't have more than one General!");
                    }
                }

                if (recruit.Rank == RankEnum.Captain)
                {
                    if (captainCount != 5)
                    {
                        army.Add(recruit);
                        captainCount++;
                        Console.WriteLine($"\n{recruit.Name} has been added to the army!");
                    }
                    else
                    {
                        Console.WriteLine("\nYou can't have more than five Captains!");
                    }
                }

                if (recruit.Rank == RankEnum.Sergeant)
                {
                    var canAddSergeant = (privateCount / 5) < sergeantCount;
                    if (canAddSergeant)
                    {
                        army.Add(recruit);
                        Console.WriteLine($"\n{recruit.Name} has been added to the army!");
                        sergeantCount++;
                    }
                    else
                    {
                        Console.WriteLine("\nYou can't have more one Sergeant for every five privates!");
                    }
                }

                if (recruit.Rank == RankEnum.Private)
                {
                    army.Add(recruit);
                    Console.WriteLine($"\n{recruit.Name} has been added to the army!");
                    privateCount++;
                }
            }

            return army;
        }

        private static List<Soldier> CreateRecruits()
        {
            var createMoreSoldiers = true;
            var recruits = new List<Soldier>();
            while (createMoreSoldiers)
            {
                var soldier = CreateSoldier();
                recruits.Add(soldier);

                Console.WriteLine("\nWould you like to create more soliders? Press Y to create more, or N to add soldiers to the army.");
                if (Console.ReadKey().Key == ConsoleKey.N)
                {
                    break;
                }
            }

            return recruits;
        }

        private static Soldier CreateSoldier()
        {
            // Create the specific soldier type
            var soldier = new Soldier();
            soldier = soldier.AssignSoldierType();

            /*
             * QUESTION:
             * Better to put this stuff in the Soldier class?
             */

            // GET NAME
            Console.Clear();
            Console.WriteLine("\nWhat is the name of the solider you want to create?");
            soldier.Name = Console.ReadLine();
            Console.Clear();

            // PICK A CLASSIFICATION
            Console.WriteLine($"\nNext, pick a class for {soldier.Name}!");
            soldier.Classification = PickClassification();
            Console.Clear();

            // PICK A RANK
            Console.WriteLine($"\nNext, pick a rank for {soldier.Name}!");
            soldier.Rank = PickRank();
            Console.Clear();

            // HIT POINTS
            Console.WriteLine($"\nHow tough is {soldier.Name}? Pick a number between 1 (weak) and 10 (strong).");
            soldier.HitPoints = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            // BIRTHDAY
            Console.WriteLine($"\nWhat is {soldier.Name}'s birthdate? Use format MM/DD/YYYY.");
            var birthdate = Console.ReadLine();
            soldier.Birthdate = string.IsNullOrWhiteSpace(birthdate) ? DateTime.Now : DateTime.Parse(birthdate);

            // AttackBonus
            soldier.AssignAttackBonus();

            // Sorcery Bonus
            soldier.AssignSorceryStrengthBonus();

            // Armor Bonus
            soldier.AssignArmorClass();

            //AssignAttackStrength(soldier);
            //AssignSorceryStrength(soldier);
            //AssignArmorClass(soldier);
            AssignMagicResistance(soldier);

            Console.WriteLine($"\n{soldier.Name} the {soldier.SoldierType()} is a {soldier.Rank} {soldier.Classification} ready to join the army!");

            return soldier;
        }

        private static void AssignAttackStrength(Soldier soldier)
        {
            var attackStrength = 2;

            // KNIGHTS ARE STROOOOONG!
            //if (soldier.Classification == ClassificationEnum.Knight)
            //{
            //    attackStrength += 10;
            //}

            /*
             * QUESTION:
             * This is a good example to put the modifiers on the Soldier class:
             * Can put the attack strength modifier if a Cleric in the Human class.
             */

            // HUMAN CLERICS DO PUSHUPS AND ARE strongish?!
            if (soldier.Race == "Human" && soldier.Classification == ClassificationEnum.Cleric)
            {
                attackStrength += 3;
            }

            soldier.AttackStrength = attackStrength;
        }

        /*
         * QUESTION:
         * Maybe put the Classification logic in the specified soldiers class.
         * i.e. Minotaur checks its class for the wizard type and adds sorcery strength if true
         *
         *
         */
        private static void AssignSorceryStrength(Soldier soldier)
        {
            var sorceryStrength = 0;

            // ELVES ARE MAGICAL!!
            if (soldier.Race == "Elf")
            {
                sorceryStrength += 10;
            }

            // WIZARDS ARE MAGICAL!!
            if (soldier.Classification == ClassificationEnum.Wizard)
            {
                sorceryStrength += 10;
            }

            // HUMAN CLERICS READ BOOKS AND ARE magical?!
            if (soldier.Race == "Human" && soldier.Classification == ClassificationEnum.Cleric)
            {
                sorceryStrength += 3;
            }

            soldier.SorceryStrength = sorceryStrength;
        }

        private static void AssignArmorClass(Soldier soldier)
        {
            var armorClass = 0;

            // THIEVES ARE DODGY!!
            if (soldier.Classification == ClassificationEnum.Thief)
            {
                armorClass += 10;
            }

            // KNIGHTS ARE BEEFY!!
            if (soldier.Classification == ClassificationEnum.Knight)
            {
                armorClass += 10;
            }

            // HUMAN CLERICS HIDE BEHIND TOUGH GUYS IN A FIGHT!
            if (soldier.Race == "Human" && soldier.Classification == ClassificationEnum.Cleric)
            {
                armorClass += 3;
            }

            soldier.ArmorClass = armorClass;
        }

        private static void AssignMagicResistance(Soldier soldier)
        {
            var magicResistance = 3;

            // LIZARDMEN ONLY BELIEVE IN SCIENCE!
            if (soldier.Race == "Lizardman")
            {
                magicResistance += 10;
            }

            soldier.MagicResistance = magicResistance;
        }

        private static RankEnum PickRank()
        {
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

        private static ClassificationEnum PickClassification()
        {
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