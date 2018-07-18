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
    public class BasicSoldierTests
    {
        [Test]
        public void Solider_WhenCreatedWithDefaults_ReturnsBaseAttackOfTwo()
        {
            // Arrange
            var sut = new Soldier();

            // Act
            var attackStrength = sut.AttackStrength;
            Console.WriteLine($"Basic Soldier AttackStrength: {attackStrength}");

            // Assert
            Assert.That(attackStrength, Is.EqualTo(2));
        }

        [Test]
        public void Solider_WhenCreatedWithDefaults_ReturnsClassificationOfNone()
        {
            // Arrange
            var sut = new Soldier();

            // Act
            var classification = sut.Classification;
            Console.WriteLine($"Basic Soldier Classification: {classification}");

            // Assert
            Assert.That(classification, Is.EqualTo(ClassificationEnum.None));
        }

        [Test]
        public void Solider_WhenCreatedWithDefaults_ReturnsSoldierTypeOfBasicSoldier()
        {
            // Arrange
            var sut = new Soldier();

            // Act
            var soldierType = sut.SoldierType();
            Console.WriteLine($"Basic Soldier Type: {soldierType}");

            // Assert
            Assert.That(soldierType, Is.EqualTo("Basic Soldier"));
        }

        [Test]
        public void Solider_WhenCreatedWithDefaults_ReturnsRankOfPrivate()
        {
            // Arrange
            var sut = new Soldier();

            // Act
            var soldierRank = sut.Rank;
            Console.WriteLine($"Basic Soldier Rank: {soldierRank}");

            // Assert
            Assert.That(soldierRank, Is.EqualTo(RankEnum.Private));
        }
    }
}
