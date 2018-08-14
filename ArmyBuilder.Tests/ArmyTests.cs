using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArmyBuilder.Soldiers;
using NUnit.Framework;

namespace ArmyBuilder.Tests
{
    [TestFixture]
    public class ArmyTests
    {
        [Test]
        public void Army_WhenManyGeneralsAdded_CannotHaveMoreThan_OneGeneral()
        {
            // Arrange
            const int expectedGeneralCount = 1;
            var recruits = AddManyMinotaurPrivates();
            recruits.Add(AddMinotaurGeneral());
            recruits.Add(AddMinotaurGeneral());
            recruits.Add(AddMinotaurGeneral());
            recruits.Add(AddMinotaurGeneral());
            var sut = new Army(recruits);

            // Act
            sut.DetermineRanks();
            var actualGeneralCount = sut.GeneralCount;

            // Assert
            Assert.That(actualGeneralCount, Is.EqualTo(expectedGeneralCount));
        }

        [Test]
        public void Army_WhenNoGeneralsAdded_ShouldFunctionAsNormal()
        {
            // Arrange
            const int expectedGeneralCount = 0;
            var recruits = AddManyMinotaurPrivates();
            recruits.AddRange(AddManyMinotaurPrivates());
            var sut = new Army(recruits);

            // Act
            sut.DetermineRanks();
            var actualGeneralCount = sut.GeneralCount;

            // Assert
            Assert.That(actualGeneralCount, Is.EqualTo(expectedGeneralCount));
        }

        [Test]
        public void Army_WhenManyGeneralsDemotedToCaptainsOverCaptainLimit_CaptainsDemotedToPrivates()
        {
            // Arrange
            const int expectedCaptainCount = 5;
            var recruits = AddManyMinotaurPrivates();
            recruits.Add(CreateMinotaur(ClassificationEnum.Wizard, RankEnum.Captain));
            recruits.Add(CreateMinotaur(ClassificationEnum.Wizard, RankEnum.Captain));
            recruits.Add(CreateMinotaur(ClassificationEnum.Wizard, RankEnum.Captain));
            recruits.Add(CreateMinotaur(ClassificationEnum.Wizard, RankEnum.Captain));
            recruits.Add(AddMinotaurGeneral());
            recruits.Add(AddMinotaurGeneral());
            recruits.Add(AddMinotaurGeneral());
            var sut = new Army(recruits);

            // Act
            sut.DetermineRanks();
            var actualCaptainCount = sut.CaptainCount;

            // Assert
            Assert.That(actualCaptainCount, Is.EqualTo(expectedCaptainCount));
        }

        private List<ISoldier> AddManyMinotaurPrivates()
        {
            var recruits = new List<ISoldier>();
            recruits.Add(CreateMinotaur(ClassificationEnum.Knight, RankEnum.Private));
            recruits.Add(CreateMinotaur(ClassificationEnum.Thief, RankEnum.Private));
            recruits.Add(CreateMinotaur(ClassificationEnum.Cleric, RankEnum.Private));
            recruits.Add(CreateMinotaur(ClassificationEnum.Wizard, RankEnum.Private));
            recruits.Add(CreateMinotaur(ClassificationEnum.Knight, RankEnum.Private));

            return recruits;
        }

        private ISoldier CreateMinotaur(ClassificationEnum classificationEnum, RankEnum rank)
        {
            var rand = new Random();

            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            var birthday = start.AddDays(rand.Next(range));
            return new Minotaur()
            {
                Name = "Bradon",
                Birthdate = birthday,
                Classification = classificationEnum,
                Rank = rank,
                SoldierType = SoldierType.Minotaur
            };
        }

        private ISoldier AddMinotaurGeneral()
        {
            var rand = new Random();

            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            var birthday = start.AddDays(rand.Next(range));
            return new Minotaur()
            {
                Name = "Bradon",
                Birthdate = birthday,
                Classification = ClassificationEnum.Knight,
                Rank = RankEnum.General,
                SoldierType = SoldierType.Minotaur
            };
        }
    }
}
