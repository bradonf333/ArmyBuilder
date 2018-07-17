using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ArmyBuilderConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var recruits = CreateRecruits();

            var army = MarshallArmy(recruits);

            SendArmyToDoBattle(army);

            Console.ReadKey();
        }

        private static void SendArmyToDoBattle(List<string[]> army)
        {
            var numberGenerator = new Random();
            var monsterHitPoints = numberGenerator.Next(50, 100);
            Console.WriteLine("The foul monster sallies forth from its lair...");
            Thread.Sleep(2000);
            Console.WriteLine("Slime and flame eminate from the beast as it approaches...");
            Thread.Sleep(2000);
            Console.WriteLine("The army attacks!");
            Thread.Sleep(2000);

            while (monsterHitPoints > 0)
            {
                for (int i = 0; i < army.Count; i++)
                {
                    if (army[i] != null)
                    {
                        var attackStrength = Convert.ToInt32(army[i][6]);
                        var sorceryStrength = Convert.ToInt32(army[i][7]);
                        if (attackStrength > sorceryStrength)
                        {
                            var damage = numberGenerator.Next(1, attackStrength);
                            damage += CheckForBonusAttackDamage(army[i]);
                            monsterHitPoints -= damage;
                            Console.WriteLine($"{army[i][0]} charges forth to attack and slashes at the monster for {damage} points of damage!");
                        }
                        else
                        {
                            var damage = numberGenerator.Next(1, sorceryStrength);
                            damage += CheckForBonusMagicDamage(army[i]);
                            monsterHitPoints -= damage;
                            Console.WriteLine($"{army[i][0]} pauses, chants a loud and ancient phrase, when suddenly lightning strikes the monster for {damage} points of damage!");
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
                            var armorClass = Convert.ToInt32(army[i][8]);
                            var magicResistance = Convert.ToInt32(army[i][8]);
                            var hitPoints = Convert.ToInt32(army[i][4]);

                            if (i % 2 == 0)
                            {
                                var damage = numberGenerator.Next(1, 10);
                                damage -= armorClass;
                                damage -= CheckForAttackDefenseBonus(army[i]);
                                if (damage >= hitPoints)
                                {
                                    Console.WriteLine($"{army[i][0]} is hit by the monster with a mightly blow, and falls dead to the earth.");
                                    army[i] = null;
                                }
                                else
                                {
                                    Console.WriteLine($"{army[i][0]} is hit by the monster with a mightly blow, but recovers and is ready for another round of battle!");
                                }
                            }
                            else
                            {
                                var damage = numberGenerator.Next(1, 10);
                                damage -= magicResistance;
                                damage -= CheckForMagicDefenseBonus(army[i]);
                                if (damage >= hitPoints)
                                {
                                    Console.WriteLine($"Flames spew forth from the tentacles of the beast and envelop {army[i][0]} who falls dead to the earth.");
                                    army[i] = null;
                                }
                                else
                                {
                                    Console.WriteLine($"{army[i][0]} is hit by the magical flames, but recovers and is ready for another round of battle!");
                                }
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("The monster is vanquished! All hail the king!");
                    break;
                }
                Console.WriteLine("Press any key to continue the battle...");
                Console.ReadKey();
            }
        }

        private static int CheckForMagicDefenseBonus(string[] soldier)
        {
            var bonusDefense = 0;

            var race = soldier[1];
            // MINOTAURS TAKE 5 LESS MAGIC DAMAGE
            if (race == "Minotaur")
            {
                bonusDefense += 5;
            }

            return bonusDefense;
        }

        private static int CheckForAttackDefenseBonus(string[] soldier)
        {
            var bonusDefense = 0;

            var classification = soldier[2];
            // KNIGHTS TAKE 5 LESS ATTACK DAMAGE
            if (classification == "Knight")
            {
                bonusDefense += 5;
            }

            return bonusDefense;
        }

        private static int CheckForBonusMagicDamage(string[] soldier)
        {
            var bonusDamage = 0;
            var race = soldier[1];
            var rank = soldier[3];

            // MINOTAUR GENERALS DO 15 EXTRA DAMAGE
            if (race == "Minotaur" && rank == "General")
            {
                bonusDamage += 10;
            }

            // LIZARDMEN PRIVATES DO 1 EXTRA DAMAGE
            if (race == "Lizardman" && rank == "Private")
            {
                bonusDamage += 1;
            }

            // HUMAN KNIGHTS DO 5 EXTRA DAMAGE
            if (race == "Human" && rank == "Knight")
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

        private static int CheckForBonusAttackDamage(string[] soldier)
        {
            var bonusMagicDamage = 0;
            var race = soldier[1];
            var classification = soldier[2];
            var rank = soldier[3];
            var birthdate = Convert.ToDateTime(soldier[5]);

            // ELF GENERALS DO 15 EXTRA MAGIC DAMAGE
            if (race == "Elf" && rank == "General")
            {
                bonusMagicDamage += 15;
            }

            // LIZARDMEN CAPTAINS DO 10 EXTRA MAGIC DAMAGE
            if (race == "Lizardman" && rank == "Captain")
            {
                bonusMagicDamage += 10;
            }

            // HUMAN WIZARDS DO 3 EXTRA DAMAGE
            if (race == "Human" && classification == "Wizard")
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

        private static List<string[]> MarshallArmy(List<string[]> recruits)
        {
            var army = new List<string[]>();
            var maxGeneralCount = 1;
            var maxCaptainCount = 5;

            var generalCount = 0;
            var captainCount = 0;
            var sergeantCount = 0;
            var privateCount = 0;

            foreach (var recruit in recruits)
            {
                if (recruit[3] == "General")
                {
                    if (generalCount == 0)
                    {
                        army.Add(recruit);
                        generalCount = 1;
                        Console.WriteLine($"{recruit[0]} has been added to the army!");
                    }
                    else
                    {
                        Console.WriteLine("You can't have more than one General!");
                    }
                }

                if (recruit[3] == "Captain")
                {
                    if (captainCount != 5)
                    {
                        army.Add(recruit);
                        captainCount++;
                        Console.WriteLine($"{recruit[0]} has been added to the army!");
                    }
                    else
                    {
                        Console.WriteLine("You can't have more than five Captains!");
                    }
                }

                if (recruit[3] == "Sergeant")
                {
                    var canAddSergeant = (privateCount / 5) < sergeantCount;
                    if (canAddSergeant)
                    {
                        army.Add(recruit);
                        Console.WriteLine($"{recruit[0]} has been added to the army!");
                        sergeantCount++;
                    }
                    else
                    {
                        Console.WriteLine("You can't have more one Sergeant for every five privates!");
                    }
                }

                if (recruit[3] == "Private")
                {
                    army.Add(recruit);
                    Console.WriteLine($"{recruit[0]} has been added to the army!");
                    privateCount++;
                }
            }

            return army;
        }

        private static List<string[]> CreateRecruits()
        {
            var createMoreSoldiers = true;
            var recruits = new List<string[]>();
            while (createMoreSoldiers)
            {
                var soldier = CreateSoldier();
                recruits.Add(soldier);

                Console.WriteLine("Would you like to create more soliders? Press Y to create more, or N to add soldiers to the army.");
                if (Console.ReadKey().Key == ConsoleKey.N)
                {
                    break;
                }
            }

            return recruits;
        }

        private static string[] CreateSoldier()
        {
            var soldier = new string[10];
            Console.WriteLine("What kind of soldier would you like to create?");

            // GET NAME
            Console.WriteLine("What is the name of the solider you want to create?");
            soldier[0] = Console.ReadLine();
            Console.Clear();

            // PICK A RACE
            Console.WriteLine($"Next, pick a race for {soldier[0]}!");
            soldier[1] = PickRace();
            Console.Clear();

            // PICK A CLASSIFICATION
            Console.WriteLine($"Next, pick a class for {soldier[0]}!");
            soldier[2] = PickClassification();
            Console.Clear();

            // PICK A RANK
            Console.WriteLine($"Next, pick a rank for {soldier[0]}!");
            soldier[3] = PickRank();
            Console.Clear();

            // HIT POINTS
            Console.WriteLine($"How tough is {soldier[0]}? Pick a number between 1 (weak) and 10 (strong).");
            soldier[4] = Console.ReadLine();

            // BIRTHDAY
            Console.WriteLine($"What is {soldier[0]}'s birthdate? Use format MM/DD/YYYY.");
            soldier[5] = Console.ReadLine();

            AssignAttackStrength(soldier);
            AssignSorceryStrength(soldier);
            AssignArmorClass(soldier);
            AssignMagicResistance(soldier);

            Console.WriteLine($"{soldier[0]} the {soldier[1]} is a {soldier[3]} {soldier[2]} ready to join the army!");

            return soldier;
        }

        private static void AssignAttackStrength(string[] soldier)
        {
            var attackStrength = 2;

            // MINOTAURS ARE STROOOONG!
            if (soldier[1] == "Minotaur")
            {
                attackStrength += 10;
            }

            // LIZARDMEN ARE strongish!
            if (soldier[1] == "Lizardman")
            {
                attackStrength += 5;
            }

            // KNIGHTS ARE STROOOOONG!
            if (soldier[2] == "Knight")
            {
                attackStrength += 10;
            }

            // HUMAN CLERICS DO PUSHUPS AND ARE strongish?!
            if (soldier[1] == "Human" && soldier[2] == "Cleric")
            {
                attackStrength += 3;
            }

            soldier[6] = attackStrength.ToString();
        }

        private static void AssignSorceryStrength(string[] soldier)
        {
            var sorceryStrength = 0;

            // ELVES ARE MAGICAL!!
            if (soldier[1] == "Elf")
            {
                sorceryStrength += 10;
            }

            // WIZARDS ARE MAGICAL!!
            if (soldier[2] == "Wizard")
            {
                sorceryStrength += 10;
            }

            // HUMAN CLERICS READ BOOKS AND ARE magical?!
            if (soldier[1] == "Human" && soldier[2] == "Cleric")
            {
                sorceryStrength += 3;
            }

            soldier[7] = sorceryStrength.ToString();
        }

        private static void AssignArmorClass(string[] soldier)
        {
            var armorClass = 0;

            // THIEVES ARE DODGY!!
            if (soldier[2] == "Thief")
            {
                armorClass += 10;
            }

            // KNIGHTS ARE BEEFY!!
            if (soldier[2] == "Knight")
            {
                armorClass += 10;
            }

            // HUMAN CLERICS HIDE BEHIND TOUGH GUYS IN A FIGHT!
            if (soldier[1] == "Human" && soldier[2] == "Cleric")
            {
                armorClass += 3;
            }

            soldier[8] = armorClass.ToString();
        }

        private static void AssignMagicResistance(string[] soldier)
        {
            var magicResistance = 3;

            // LIZARDMEN ONLY BELIEVE IN SCIENCE!
            if (soldier[1] == "Lizardman")
            {
                magicResistance += 10;
            }

            soldier[9] = magicResistance.ToString();
        }

        private static string PickRank()
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
                    return "Private";
                }

                if (input.Key == ConsoleKey.S)
                {
                    return "Sergeant";
                }

                if (input.Key == ConsoleKey.C)
                {
                    return "Captain";
                }

                if (input.Key == ConsoleKey.G)
                {
                    return "General";
                }
            }

            return "";
        }

        private static string PickClassification()
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
                    return "Knight";
                }

                if (input.Key == ConsoleKey.W)
                {
                    return "Wizard";
                }

                if (input.Key == ConsoleKey.C)
                {
                    return "Cleric";
                }

                if (input.Key == ConsoleKey.T)
                {
                    return "Thief";
                }
            }

            return "";
        }

        private static string PickRace()
        {
            var canContinueWithoutRace = false;
            while (!canContinueWithoutRace)
            {
                Console.WriteLine("Press L for Lizardman.");
                Console.WriteLine("Press H for Human.");
                Console.WriteLine("Press E for Elf.");
                Console.WriteLine("Press M for Minotaur.");

                var input = Console.ReadKey();
                if (input.Key == ConsoleKey.L)
                {
                    return "Lizardman";
                }

                if (input.Key == ConsoleKey.H)
                {
                    return "Human";
                }

                if (input.Key == ConsoleKey.E)
                {
                    return "Elf";
                }

                if (input.Key == ConsoleKey.M)
                {
                    return "Minotaur";
                }
            }

            return "";
        }
    }
}