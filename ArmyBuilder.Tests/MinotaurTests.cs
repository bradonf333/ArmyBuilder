using System;

using ArmyBuilder.Soldiers;

using NUnit.Framework;

namespace ArmyBuilder.Tests
{
    [TestFixture]
    public class MinotaurTests
    {
        [Test]
        public void Minotaur_WhenCreated_WithDefaults_ShouldGetBonusAttackStrength_ToEqualTwelve()
        {
            // Arrange
            var sut = new Minotaur();

            // Act
            var attackStrenth = sut.SoldierStats.AttackStrength;

            // Assert
            Assert.That(attackStrenth, Is.EqualTo(12));
        }

        [Test]
        public void Minotaur_WhenCreated_WithDefaults_ShouldGetDefenseOfFive()
        {
            // Arrange
            var sut = new Minotaur();

            // Act
            var defense = sut.SoldierStats.Defense;

            // Assert
            Assert.That(defense, Is.EqualTo(5));
        }

        [Test]
        public void Minotaur_WhenCreated_WithDefaults_ShouldHaveSoldierTypeOfMinotaur()
        {
            // Arrange
            var sut = new Minotaur();

            // Act
            var soldierType = sut.SoldierType;
            Console.WriteLine($"Soldier Type: {soldierType}");

            // Assert
            Assert.That(soldierType, Is.EqualTo("Minotaur"));
        }
    }
}
