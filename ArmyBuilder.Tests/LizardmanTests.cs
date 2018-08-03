

using System;

using ArmyBuilder.Soldiers;

using NUnit.Framework;

namespace ArmyBuilder.Tests
{
    [TestFixture]
    public class LizardmanTests
    {

        [Test]
        public void Lizardman_WhenCreated_WithDefaults_ShouldGetBonusAttackStrength_ToEqualSeven()
        {
            // Arrange
            var sut = new Lizardman();

            // Act
            var attackStrength = sut.SoldierStats.AttackStrength;

            // Assert
            Assert.That(attackStrength, Is.EqualTo(7));
        }

        [Test]
        public void Lizardman_WhenCreated_WithDefaults_ShouldGetBonusMagicResistance_ToEqualTen()
        {
            // Arrange
            var sut = new Lizardman();

            // Act
            var magicResistance = sut.SoldierStats.MagicResistance;

            // Assert
            Assert.That(magicResistance, Is.EqualTo(13));
        }

        [Test]
        public void Lizradman_WhenCreated_WithDefaults_ShouldHaveSoldierTypeOfLizardman()
        {
            // Arrange
            var sut = new Lizardman();

            // Act
            var soldierType = sut.SoldierType();
            Console.WriteLine($"Soldier Type: {soldierType}");

            // Assert
            Assert.That(soldierType, Is.EqualTo("Lizardman"));
        }
    }
}
