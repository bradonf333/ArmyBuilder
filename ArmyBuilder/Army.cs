using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArmyBuilder.Soldiers;

namespace ArmyBuilder
{
    public class Army
    {
        public List<ISoldier> Recruits { get; set; }
        public int GeneralCount { get; private set; }
        public int CaptainCount { get; private set; }
        public int SergeantCount { get; private set; }
        public int PrivateCount { get; private set; }

        private int _maxGeneralCount = 1;
        private int _maxCaptainCount = 5;
        private const int PrivatesNeededPerSergeant = 5;

        // By default Allow 1 Sergeant
        private int _maxSergeantCount = 1;

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

        /// <summary>
        /// Pass in all the recruits. This will also Determine the recruits to ensre the Ranks are correct.
        /// If they are not, some recruits Rank will be demoted.
        /// </summary>
        /// <param name="recruits"></param>
        public Army(List<ISoldier> recruits)
        {
            Recruits = recruits;
            DetermineRanks();
        }

        public void DetermineRanks()
        {
            CountRanks();

            if (RankRulesBroken())
            {
                DemoteRecruits();
            }
        }

        public void Battle(Monster monster)
        {
            Attack(monster);
            Defend(monster);

            var soldierCount = Recruits.Count(r => !r.IsDead);
            var soldierCasualties = Recruits.Count(r => r.IsDead);
            DisplayBattleReports(soldierCount, soldierCasualties, monster.HitPoints);
        }

        private void Attack(Monster monster)
        {
            foreach (var soldier in Recruits)
            {
                if (monster.HitPoints > 0)
                {
                    monster.Defend(soldier.Attack());
                }
                else
                {
                    break;
                }
            }
        }

        public void Defend(Monster monster)
        {
            var liveSoldiers = Recruits.Where(r => !r.IsDead);

            foreach (var soldier in liveSoldiers)
            {
                soldier.Defend(monster.AttackType, monster.Attack());
            }
        }

        private string DisplayBattleReports(int soldierCasualties, int soldierCount, int monsterHitPoints)
        {
            var messageBuilder = new StringBuilder();
            messageBuilder.Append("Your Army has defended against the Monster. Here is the battle report:");
            messageBuilder.Append($"Casualties: {soldierCasualties}\nRemaining Soldiers: {soldierCount}");
            messageBuilder.Append($"Monster HitPoints: {monsterHitPoints}");
            return messageBuilder.ToString();
        }

        private bool RankRulesBroken()
        {
            return GeneralCount > _maxGeneralCount
                   || CaptainCount > _maxCaptainCount
                   || SergeantCount > MaxSergeantCount;
        }

        private void CountRanks()
        {
            GeneralCount = Recruits.Count(s => s.Rank == RankEnum.General);
            CaptainCount = Recruits.Count(s => s.Rank == RankEnum.Captain);
            SergeantCount = Recruits.Count(s => s.Rank == RankEnum.Sergeant);
            PrivateCount = Recruits.Count(s => s.Rank == RankEnum.Private);
            MaxSergeantCount = PrivateCount / PrivatesNeededPerSergeant;
        }

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

        private void DemoteNecessaryCaptainsToPrivates()
        {
            Console.WriteLine($"You can't have more than {MaxCaptainCount} Captains!");

            for (int i = CaptainCount; i > MaxCaptainCount; i--)
            {
                var captainToDemote = Recruits.First(s => s.Rank == RankEnum.Captain);
                captainToDemote.Rank = RankEnum.Private;
                Console.WriteLine($"{captainToDemote.Name} has been demoted from {RankEnum.Captain} to {RankEnum.Private}");
            }
        }

        private void DemoteNecessaryGeneralsToCaptains()
        {
            Console.WriteLine($"You can't have more than {MaxGeneralCount} General!");

            for (int i = GeneralCount; i > MaxGeneralCount; i--)
            {
                var generalToDemote = Recruits.First(s => s.Rank == RankEnum.General);
                generalToDemote.Rank = RankEnum.Captain;
                Console.WriteLine($"{generalToDemote.Name} has been demoted from {RankEnum.General} to {RankEnum.Captain}");
            }
        }

        private void DemoteNecessarySergeantsToPrivates()
        {
            Console.WriteLine($"You can't have more than {MaxSergeantCount} Sergeant(s)!");

            for (int i = SergeantCount; i > MaxSergeantCount; i--)
            {
                var sergeantToDemote = Recruits.First(s => s.Rank == RankEnum.Sergeant);
                sergeantToDemote.Rank = RankEnum.Private;
                Console.WriteLine($"{sergeantToDemote.Name} has been demoted from {RankEnum.Sergeant} to {RankEnum.Private}");
            }
        }
    }
}
