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

        public Army()
        {
            Recruits = new List<ISoldier>();
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

            if (GeneralCount > 1 || CaptainCount > 5)
            {
                DemoteRecruits();
            }
        }

        private void CountRanks()
        {
            GeneralCount = Recruits.Count(s => s.Rank == RankEnum.General);
            CaptainCount = Recruits.Count(s => s.Rank == RankEnum.Captain);
            SergeantCount = Recruits.Count(s => s.Rank == RankEnum.Sergeant);
            PrivateCount = Recruits.Count(s => s.Rank == RankEnum.Private);
        }

        private void DemoteRecruits()
        {
            if (GeneralCount > MaxGeneralCount)
            {
                for (int i = GeneralCount; i > MaxGeneralCount; i--)
                {
                    var generalToDemote = Recruits.First(s => s.Rank == RankEnum.General);
                    generalToDemote.Rank = RankEnum.Captain; 
                }
            }
            
            CountRanks();

            if (CaptainCount > MaxCaptainCount)
            {
                for (int i = CaptainCount; i > MaxCaptainCount; i--)
                {
                    var captainToDemote = Recruits.First(s => s.Rank == RankEnum.Captain);
                    captainToDemote.Rank = RankEnum.Private;
                }
            }

            CountRanks();
        }

        private void DemoteCaptains()
        {
            for (int i = 0; i < CaptainCount - 1; i++)
            {
                var captainToDemote = Recruits.First(s => s.Rank == RankEnum.Captain);
                captainToDemote.Rank = RankEnum.Private;
            }

            DetermineRanks();
        }


    }
}
