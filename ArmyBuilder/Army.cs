using ArmyBuilder.Soldiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArmyBuilder.Enemies;
using ArmyBuilder.Input;
using ArmyBuilder.Output;

namespace ArmyBuilder
{
    public class Army
    {
        public List<ISoldier> Recruits { get; set; }
        public int GeneralCount { get; private set; }
        public int CaptainCount { get; private set; }
        public int SergeantCount { get; private set; }
        public int PrivateCount { get; private set; }
        public bool IsDefeated { get; set; }

        public int MaxGeneralCount
        {
            get { return _maxGeneralCount; }
            private set { _maxGeneralCount = 1; }
        }

        public int MaxCaptainCount
        {
            get { return _maxCaptainCount; }
            private set { _maxCaptainCount = 5; }
        }

        public int MaxSergeantCount
        {
            get { return _maxSergeantCount; }
            private set { _maxSergeantCount = PrivateCount / PrivatesNeededPerSergeant; }
        }

        private int _maxGeneralCount = 1;
        private int _maxCaptainCount = 5;
        private const int PrivatesNeededPerSergeant = 5;
        private readonly IWriter _writer;
        private readonly IReader _reader;

        // By default Allow 1 Sergeant
        private int _maxSergeantCount = 1;

        /// <summary>
        /// Pass in all the recruits needed to build the Army.
        /// Also need a reader and writer in order to communicate with the user.
        /// This will also Determine the recruits to ensure the Ranks are correct.
        /// If ranks are incorrect, some recruits Rank will be demoted!!
        /// </summary>
        /// <param name="recruits"></param>
        /// <param name="writer"></param>
        /// <param name="reader"></param>
        public Army(List<ISoldier> recruits, IWriter writer, IReader reader)
        {
            _writer = writer;
            _reader = reader;
            IsDefeated = false;
            Recruits = recruits;
            DetermineRanks();
        }

        /// <summary>
        /// Counts each Rank and if the Armies Rank Rules are broken, demote necessary recruits to fall in line with rules.
        /// </summary>
        public void DetermineRanks()
        {
            CountRanks();

            if (RankRulesBroken())
            {
                DemoteRecruits();
            }
        }

        /// <summary>
        /// Battle against an enemy. The Army will Attack first and Defend Second.
        /// </summary>
        /// <param name="enemy"></param>
        public void Battle(IEnemy enemy)
        {
            while (true)
            {
                if (!ReadyForBattle())
                {
                    continue;
                }

                DisplayBeginBattleMessage(enemy);

                while (!(enemy.IsDead || IsDefeated))
                {
                    Attack(enemy);

                    if (enemy.IsDead)
                    {
                        break;
                    }

                    Defend(enemy);

                    var soldierCount = Recruits.Count(r => !r.IsDead);

                    if (soldierCount <= 0)
                    {
                        break;
                    }

                    var soldierCasualties = Recruits.Count(r => r.IsDead);
                    var battleReport = BuildBattleReport(soldierCasualties, soldierCount, enemy.HitPoints);
                    _writer.WriteMessage(battleReport);

                    UserInputForNextRound();
                }

                if (enemy.IsDead)
                {
                    ArmyVictoriousMessage(enemy);
                }
                else
                {
                    ArmyDefeatedMessage(enemy);
                }

                break;
            }
        }

        /// <summary>
        /// Spent way too much time formatting the Messages.....
        /// </summary>
        /// <param name="enemy"></param>
        private void ArmyVictoriousMessage(IEnemy enemy)
        {
            _writer.CustomBG(Color.Green);
            _writer.CustomFG(Color.Black);
            _writer.WriteMessage($"{" ",-80}");
            _writer.WriteMessage($"{" ",-80}");
            _writer.WriteMessage($"{"-----------",15} {$"The Army has successfully defeated the {enemy.EnemyTypeToString()}",5} {"-----------",10}{" ",6}");
            _writer.WriteMessage($"{" ",-80}");
            _writer.WriteMessage($"{" ",-80}\n");
            _writer.Default();
        }

        /// <summary>
        /// Spent way too much time formatting the Messages.....
        /// </summary>
        /// <param name="enemy"></param>
        private void ArmyDefeatedMessage(IEnemy enemy)
        {
            _writer.Alert();
            _writer.WriteMessage($"{" ",-80}");
            _writer.WriteMessage($"{" ",-80}");
            _writer.WriteMessage($"{"-----------",20} {$"The Army has been defeated by the {enemy.EnemyTypeToString()}",5} {"-----------",10}{" ",6}");
            _writer.WriteMessage($"{" ",-80}");
            _writer.WriteMessage($"{" ",-80}\n");
            _writer.Default();
        }

        /// <summary>
        /// Pause before the next round of battle.
        /// </summary>
        private void UserInputForNextRound()
        {
            _writer.WriteMessage("Press any key to begin the next round of battle!");
            _reader.ReadChar();
        }

        /// <summary>
        /// Determines if you are ready to battle.
        /// </summary>
        /// <returns></returns>
        private bool ReadyForBattle()
        {
            _writer.WriteMessage("\n");
            _writer.WriteMessage("Are you ready to begin the battle?\nPress Y to begin or N if you are not ready:");
            return char.ToUpper(_reader.ReadChar()).Equals('Y');
        }

        /// <summary>
        /// Displays a message at the beginning of the battle.
        /// </summary>
        /// <param name="enemy"></param>
        private void DisplayBeginBattleMessage(IEnemy enemy)
        {
            _writer.ClearMessage();

            var messageBuilder = new StringBuilder();
            messageBuilder.Append("\nThe army approaches a dark and dreary cavern.......");
            messageBuilder.Append($"\nA foul {enemy.EnemyTypeToString()} sallies forth from its lair...");

            /*
             * todo:
             * This part might be different for different enemies.
             * Maybe create a method on the enemy that says BeginBattleMessage or something?
             */
            messageBuilder.Append("\nSlime and flame eminate from the beast as it approaches...");

            _writer.WriteMessage(messageBuilder.ToString());
        }

        /// <summary>
        /// Each Recruit Attacks an enemy
        /// </summary>
        /// <param name="enemy"></param>
        private void Attack(IEnemy enemy)
        {
            _writer.Information();
            _writer.WriteMessage($"\n{" ",-80}");
            _writer.WriteMessage($"{"-----------",22} {"The Army begins its Attack!!",5} {"-----------",10}{" ",17}");
            _writer.WriteMessage($"{" ",-80}\n");
            _writer.Default();

            foreach (var soldier in Recruits.Where(r => !r.IsDead))
            {
                if (enemy.HitPoints > 0)
                {
                    var attackDamage = soldier.Attack();
                    _writer.WriteMessage(soldier.AttackMessage());
                    
                    // Enemy Defends the Attack
                    enemy.Defend(attackDamage);
                }
                else
                {
                    enemy.IsDead = true;
                    break;
                }
            }
        }

        /// <summary>
        /// Army Defends against the Enemy
        /// </summary>
        /// <param name="enemy"></param>
        private void Defend(IEnemy enemy)
        {
            var liveSoldiers = Recruits.Where(r => !r.IsDead);

            DisplayDefendMessage();

            foreach (var soldier in liveSoldiers)
            {
                var attackDamage = enemy.Attack();
                _writer.WriteMessage(enemy.AttackMessage());
                soldier.Defend(enemy.AttackType, attackDamage);

                // Wonder about putting this inside Defend, but then defend would need to return a string which might be weird?
                _writer.WriteMessage(soldier.DefendMessage(enemy.AttackType, attackDamage));
            }
        }

        private void DisplayDefendMessage()
        {
            _writer.CustomBG(Color.Cyan);
            _writer.CustomFG(Color.Black);
            _writer.WriteMessage($"{" ",-80}");
            _writer.WriteMessage($"{"-----------",20} {"It's now time for the Army to Defend!",5} {"-----------",10}{" ",10}");
            _writer.WriteMessage($"{" ",-80}\n");
            _writer.Default();
        }

        /// <summary>
        /// Generate Battle reports: Casualties, Remaining Soldiers and Monster HitPoints
        /// </summary>
        /// <param name="soldierCasualties"></param>
        /// <param name="soldierCount"></param>
        /// <param name="enemyHitPoints"></param>
        /// <returns></returns>
        private static string BuildBattleReport(int soldierCasualties, int soldierCount, int enemyHitPoints)
        {
            var messageBuilder = new StringBuilder();
            messageBuilder.Append("Your Army has defended against the Monster. Here is the battle report:\n");
            messageBuilder.Append($"Casualties: {soldierCasualties}\nRemaining Soldiers: {soldierCount}\n");
            messageBuilder.Append($"Enemy HitPoints: {enemyHitPoints}\n");
            return messageBuilder.ToString();
        }

        /// <summary>
        /// Determines if the Armies Rules on how many of each Rank are allowed have been broken.
        /// </summary>
        /// <returns></returns>
        private bool RankRulesBroken()
        {
            return GeneralCount > _maxGeneralCount
                   || CaptainCount > _maxCaptainCount
                   || SergeantCount > MaxSergeantCount;
        }

        /// <summary>
        /// Count all the Ranks
        /// </summary>
        private void CountRanks()
        {
            GeneralCount = Recruits.Count(s => s.Rank == Rank.General);
            CaptainCount = Recruits.Count(s => s.Rank == Rank.Captain);
            SergeantCount = Recruits.Count(s => s.Rank == Rank.Sergeant);
            PrivateCount = Recruits.Count(s => s.Rank == Rank.Private);
            MaxSergeantCount = PrivateCount / PrivatesNeededPerSergeant;
        }

        /// <summary>
        /// Demote Recruits Ranks to fall in line with the Armies Rank Rules
        /// </summary>
        private void DemoteRecruits()
        {
            if (GeneralCount > MaxGeneralCount)
            {
                DemoteNecessaryGeneralsToCaptains();
                CountRanks();
            }

            if (CaptainCount > MaxCaptainCount)
            {
                DemoteNecessaryCaptainsToPrivates();
                CountRanks();
            }

            if (SergeantCount > MaxSergeantCount)
            {
                DemoteNecessarySergeantsToPrivates();
                CountRanks();
            }

            DisplayArmyRecruitRanks();
        }

        private void DisplayArmyRecruitRanks()
        {
            _writer.WriteMessage("Updated Army Ranks");
            _writer.WriteMessage($"Sergeants: {SergeantCount, 5}");
            _writer.WriteMessage($"Generals: {GeneralCount, 5}");
            _writer.WriteMessage($"Captains: {CaptainCount, 5}");
            _writer.WriteMessage($"Privates: {PrivateCount, 5}");
        }

        /// <summary>
        /// Demote Generals to Captains
        /// </summary>
        private void DemoteNecessaryGeneralsToCaptains()
        {
            _writer.WriteMessage($"You can't have more than {MaxGeneralCount} General!");

            for (int i = GeneralCount; i > MaxGeneralCount; i--)
            {
                var generalToDemote = Recruits.First(s => s.Rank == Rank.General);
                generalToDemote.Rank = Rank.Captain;
                _writer.WriteMessage($"{generalToDemote.Name} has been demoted from {Rank.General} to {Rank.Captain}");
            }
        }

        /// <summary>
        /// Demote Captains to Privates
        /// </summary>
        private void DemoteNecessaryCaptainsToPrivates()
        {
            _writer.WriteMessage($"You can't have more than {MaxCaptainCount} Captains!");

            for (int i = CaptainCount; i > MaxCaptainCount; i--)
            {
                var captainToDemote = Recruits.First(s => s.Rank == Rank.Captain);
                captainToDemote.Rank = Rank.Private;
                _writer.WriteMessage($"{captainToDemote.Name} has been demoted from {Rank.Captain} to {Rank.Private}");
            }
        }

        /// <summary>
        /// Demote Sergeants to Privates
        /// </summary>
        private void DemoteNecessarySergeantsToPrivates()
        {
            _writer.WriteMessage($"You can't have more than {MaxSergeantCount} Sergeant(s)!");

            for (int i = SergeantCount; i > MaxSergeantCount; i--)
            {
                var sergeantToDemote = Recruits.First(s => s.Rank == Rank.Sergeant);
                sergeantToDemote.Rank = Rank.Private;
                _writer.WriteMessage($"{sergeantToDemote.Name} has been demoted from {Rank.Sergeant} to {Rank.Private}");
            }
        }
    }
}
