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
            var recruits = AddFiveMinotaurPrivates();
            recruits.Add(AddMinotaurGeneral());
            recruits.Add(AddMinotaurGeneral());
            recruits.Add(AddMinotaurGeneral());
            recruits.Add(AddMinotaurGeneral());

            // Act
            var sut = new Army(recruits);
            var actualGeneralCount = sut.GeneralCount;

            // Assert
            Assert.That(actualGeneralCount, Is.EqualTo(expectedGeneralCount));
        }

        [Test]
        public void Army_WhenTenPrivatesAdded_CanHaveUpTo_TwoSergeants()
        {
            // Arrange
            const int expectedSergeantCount = 2;
            var recruits = AddFiveMinotaurPrivates();
            recruits.AddRange(AddFiveMinotaurPrivates());

            // Act
            var sut = new Army(recruits);
            var actualMaxSergeantCount = sut.MaxSergeantCount;

            // Assert
            Assert.That(actualMaxSergeantCount, Is.EqualTo(expectedSergeantCount));
        }

        [Test]
        public void Army_WhenTenPrivatesAdded_WithThreeSergeants_OneSergeantShouldBeDemoted_ToPrivate()
        {
            // Arrange
            const int expectedSergeantCount = 2;
            var recruits = AddFiveMinotaurPrivates();
            recruits.AddRange(AddFiveMinotaurPrivates());
            recruits.Add(CreateMinotaur(ClassificationEnum.Knight, RankEnum.Sergeant));
            recruits.Add(CreateMinotaur(ClassificationEnum.Knight, RankEnum.Sergeant));
            recruits.Add(CreateMinotaur(ClassificationEnum.Knight, RankEnum.Sergeant));

            // Act
            var sut = new Army(recruits);
            var actualSergeantCount = sut.SergeantCount;

            // Assert
            Assert.That(actualSergeantCount, Is.EqualTo(expectedSergeantCount));
        }

        [Test]
        public void Army_WhenNoGeneralsAdded_ShouldFunctionAsNormal()
        {
            // Arrange
            const int expectedGeneralCount = 0;
            var recruits = AddFiveMinotaurPrivates();
            recruits.AddRange(AddFiveMinotaurPrivates());
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
            var recruits = AddFiveMinotaurPrivates();
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

        private List<ISoldier> AddFiveMinotaurPrivates()
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
