using ArmyBuilder.Soldiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public string BattleReport { get; set; }

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

        // By default Allow 1 Sergeant
        private int _maxSergeantCount = 1;

        /// <summary>
        /// Pass in all the recruits. This will also Determine the recruits to ensre the Ranks are correct.
        /// If they are not, some recruits Rank will be demoted.
        /// </summary>
        /// <param name="recruits"></param>
        public Army(List<ISoldier> recruits)
        {
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
        public string Battle(IEnemy enemy)
        {
            while (!(enemy.IsDead || IsDefeated))
            {
                Attack(enemy);
                Defend(enemy);

                var soldierCount = Recruits.Count(r => !r.IsDead);
                var soldierCasualties = Recruits.Count(r => r.IsDead);

                Console.WriteLine(BuildBattleReport(soldierCasualties, soldierCount, enemy.HitPoints));
            }

            if(enemy.IsDead)
            {
                return "The Army has successfully defeated the Enemy!";
            }
            else
            {
                return "The Army has been defeated by the Enemy!";
            }
            
        }

        /// <summary>
        /// Each Recruit Attacks an enemy
        /// </summary>
        /// <param name="enemy"></param>
        private void Attack(IEnemy enemy)
        {
            foreach (var soldier in Recruits)
            {
                if (enemy.HitPoints > 0)
                {
                    enemy.Defend(soldier.Attack());
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

            foreach (var soldier in liveSoldiers)
            {
                soldier.Defend(enemy.AttackType, enemy.Attack());
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
            messageBuilder.Append($"Monster HitPoints: {monsterHitPoints}");
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
            Console.WriteLine($"You can't have more than {MaxGeneralCount} General!");

            for (int i = GeneralCount; i > MaxGeneralCount; i--)
            {
                var generalToDemote = Recruits.First(s => s.Rank == Rank.General);
                generalToDemote.Rank = Rank.Captain;
                Console.WriteLine($"{generalToDemote.Name} has been demoted from {Rank.General} to {Rank.Captain}");
            }
        }

        /// <summary>
        /// Demote Captains to Privates
        /// </summary>
        private void DemoteNecessaryCaptainsToPrivates()
        {
            Console.WriteLine($"You can't have more than {MaxCaptainCount} Captains!");

            for (int i = CaptainCount; i > MaxCaptainCount; i--)
            {
                var captainToDemote = Recruits.First(s => s.Rank == Rank.Captain);
                captainToDemote.Rank = Rank.Private;
                Console.WriteLine($"{captainToDemote.Name} has been demoted from {Rank.Captain} to {Rank.Private}");
            }
        }

        /// <summary>
        /// Demote Sergeants to Privates
        /// </summary>
        private void DemoteNecessarySergeantsToPrivates()
        {
            Console.WriteLine($"You can't have more than {MaxSergeantCount} Sergeant(s)!");

            for (int i = SergeantCount; i > MaxSergeantCount; i--)
            {
                var sergeantToDemote = Recruits.First(s => s.Rank == Rank.Sergeant);
                sergeantToDemote.Rank = Rank.Private;
                Console.WriteLine($"{sergeantToDemote.Name} has been demoted from {Rank.Sergeant} to {Rank.Private}");
            }
        }
    }
}
