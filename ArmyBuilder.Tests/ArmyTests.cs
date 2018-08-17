using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArmyBuilder.Soldiers;
using ArmyBuilder.Writers;
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
            var consoleWriter = new ConsoleWriter();
            const int expectedGeneralCount = 1;
            var recruits = AddFiveMinotaurPrivates();
            recruits.Add(AddMinotaurGeneral());
            recruits.Add(AddMinotaurGeneral());
            recruits.Add(AddMinotaurGeneral());
            recruits.Add(AddMinotaurGeneral());

            // Act
            var sut = new Army(recruits, consoleWriter);
            var actualGeneralCount = sut.GeneralCount;

            // Assert
            Assert.That(actualGeneralCount, Is.EqualTo(expectedGeneralCount));
        }

        [Test]
        public void Army_WhenTenPrivatesAdded_CanHaveUpTo_TwoSergeants()
        {
            // Arrange
            var consoleWriter = new ConsoleWriter();
            const int expectedSergeantCount = 2;
            var recruits = AddFiveMinotaurPrivates();
            recruits.AddRange(AddFiveMinotaurPrivates());

            // Act
            var sut = new Army(recruits, consoleWriter);
            var actualMaxSergeantCount = sut.MaxSergeantCount;

            // Assert
            Assert.That(actualMaxSergeantCount, Is.EqualTo(expectedSergeantCount));
        }

        [Test]
        public void Army_WhenTenPrivatesAdded_WithThreeSergeants_OneSergeantShouldBeDemoted_ToPrivate()
        {
            // Arrange
            var consoleWriter = new ConsoleWriter();
            const int expectedSergeantCount = 2;
            var recruits = AddFiveMinotaurPrivates();
            recruits.AddRange(AddFiveMinotaurPrivates());
            recruits.Add(CreateMinotaur(Class.Knight, Rank.Sergeant));
            recruits.Add(CreateMinotaur(Class.Knight, Rank.Sergeant));
            recruits.Add(CreateMinotaur(Class.Knight, Rank.Sergeant));

            // Act
            var sut = new Army(recruits, consoleWriter);
            var actualSergeantCount = sut.SergeantCount;

            // Assert
            Assert.That(actualSergeantCount, Is.EqualTo(expectedSergeantCount));
        }

        [Test]
        public void Army_WhenNoGeneralsAdded_ShouldFunctionAsNormal()
        {
            // Arrange
            var consoleWriter = new ConsoleWriter();
            const int expectedGeneralCount = 0;
            var recruits = AddFiveMinotaurPrivates();
            recruits.AddRange(AddFiveMinotaurPrivates());
            var sut = new Army(recruits, consoleWriter);

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
            var consoleWriter = new ConsoleWriter();
            const int expectedCaptainCount = 5;
            var recruits = AddFiveMinotaurPrivates();
            recruits.Add(CreateMinotaur(Class.Wizard, Rank.Captain));
            recruits.Add(CreateMinotaur(Class.Wizard, Rank.Captain));
            recruits.Add(CreateMinotaur(Class.Wizard, Rank.Captain));
            recruits.Add(CreateMinotaur(Class.Wizard, Rank.Captain));
            recruits.Add(AddMinotaurGeneral());
            recruits.Add(AddMinotaurGeneral());
            recruits.Add(AddMinotaurGeneral());
            var sut = new Army(recruits, consoleWriter);

            // Act
            sut.DetermineRanks();
            var actualCaptainCount = sut.CaptainCount;

            // Assert
            Assert.That(actualCaptainCount, Is.EqualTo(expectedCaptainCount));
        }

        private List<ISoldier> AddFiveMinotaurPrivates()
        {
            var recruits = new List<ISoldier>();
            recruits.Add(CreateMinotaur(Class.Knight, Rank.Private));
            recruits.Add(CreateMinotaur(Class.Thief, Rank.Private));
            recruits.Add(CreateMinotaur(Class.Cleric, Rank.Private));
            recruits.Add(CreateMinotaur(Class.Wizard, Rank.Private));
            recruits.Add(CreateMinotaur(Class.Knight, Rank.Private));

            return recruits;
        }

        private ISoldier CreateMinotaur(Class classificationEnum, Rank rank)
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
                Classification = Class.Knight,
                Rank = Rank.General,
                SoldierType = SoldierType.Minotaur
            };
        }
    }
}
