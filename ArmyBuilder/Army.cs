using ArmyBuilder.Soldiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArmyBuilder.Input;
using ArmyBuilder.Writers;

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
        /// Pass in all the recruits and the type of writer to display details about the army to the user.
        /// This will also Determine the recruits to ensure the Ranks are correct.
        /// If ranks are incorrect, some recruits Rank will be demoted!!
        /// </summary>
        /// <param name="recruits"></param>
        /// <param name="writer"></param>
        public Army(List<ISoldier> recruits, IWriter writer, IReader reader)
        {
            IsDefeated = false;
            Recruits = recruits;
            DetermineRanks();
            _writer = writer;
            _reader = reader;
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
            if (BeginBattle())
            {
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
                    var soldierCasualties = Recruits.Count(r => r.IsDead);

                    _writer.WriteMessage(BuildBattleReport(soldierCasualties, soldierCount, enemy.HitPoints));
                }

                _writer.WriteMessage(enemy.IsDead
                    ? $"The Army has successfully defeated the {enemy.EnemyTypeToString()}!"
                    : $"The Army has been defeated by the {enemy.EnemyTypeToString()}!");
            }
        }

        private bool BeginBattle()
        {
            _writer.WriteMessage("Are you ready to begin the battle Y for Yes N for N?:\n");
            return _reader.ReadChar() == 'Y';
        }

        /// <summary>
        /// Displays a message at the beginning of the battle.
        /// </summary>
        /// <param name="enemy"></param>
        private void DisplayBeginBattleMessage(IEnemy enemy)
        {
            var messageBuilder = new StringBuilder();
            messageBuilder.Append("\nThe army approaches a dark and dreary cavern.......");
            messageBuilder.Append($"\nA foul {enemy.EnemyTypeToString()} sallies forth from its lair...");
            messageBuilder.Append("\nSlime and flame eminate from the beast as it approaches...");

            _writer.WriteMessage(messageBuilder.ToString());
        }

        /// <summary>
        /// Each Recruit Attacks an enemy
        /// </summary>
        /// <param name="enemy"></param>
        private void Attack(IEnemy enemy)
        {
            _writer.WriteMessage("\nThe Army begins its Attack!!");

            foreach (var soldier in Recruits)
            {
                if (enemy.HitPoints > 0)
                {
                    var attackDamage = soldier.Attack();
                    _writer.WriteMessage($"{soldier.Name} charges forth to attack and slashes at the monster for {attackDamage} points damage!\n");
                    //{army.Recruits[i].Name} pauses, chants a loud and ancient phrase, when suddenly lightning strikes the monster for {damage} points of damage!");
                    enemy.Defend(attackDamage);
                }
                else
                {
                    soldier.IsDead = true;
                    break;
                }
            }
        }

        /// <summary>
        /// Army Defends against the Enemy
        /// </summary>
        /// <param name="enemy"></param>
        public void Defend(IEnemy enemy)
        {
            var liveSoldiers = Recruits.Where(r => !r.IsDead);

            _writer.WriteMessage("The Monster attacks each recruit in the army!!");
            foreach (var soldier in liveSoldiers)
            {
                var attackDamage = enemy.Attack();

                soldier.Defend(enemy.AttackType, attackDamage);
                _writer.WriteMessage($"{soldier.Name} has been hit with a {enemy.AttackType} attack for {attackDamage} damage!");
            }
        }

        /// <summary>
        /// Generate Battle reports: Casulties, Remaining Soldiers and Monster HitPoints
        /// </summary>
        /// <param name="soldierCasualties"></param>
        /// <param name="soldierCount"></param>
        /// <param name="monsterHitPoints"></param>
        /// <returns></returns>
        private string BuildBattleReport(int soldierCasualties, int soldierCount, int monsterHitPoints)
        {
            var messageBuilder = new StringBuilder();
            messageBuilder.Append("Your Army has defended against the Monster. Here is the battle report:\n");
            messageBuilder.Append($"Casualties: {soldierCasualties}\nRemaining Soldiers: {soldierCount}\n");
            messageBuilder.Append($"Monster HitPoints: {monsterHitPoints}\n");
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
