using System;

using ArmyBuilder.Soldiers;

using NUnit.Framework;

namespace ArmyBuilder.Tests
{
    [TestFixture]
    public class HumanTests
    {
        [Test]
        public void Human_WhenCreated_WithDefaults_ShouldHaveSoldierClassificationOfNone()
        {
            // Arrange
            var sut = new Human();

            // Act
            var soldierClass = sut.Classification;
            Console.WriteLine($"Soldier Classification: {soldierClass}");

            // Assert
            Assert.That(soldierClass, Is.EqualTo(Class.None));
        }

        [Test]
        public void Human_WhenCreated_WithDefaults_ShouldHaveSoldierTypeOfHuman()
        {
            // Arrange
            var sut = new Human();

            // Act
            var soldierType = sut.SoldierType;
            Console.WriteLine($"Soldier Type: {soldierType}");

            // Assert
            Assert.That(soldierType, Is.EqualTo(SoldierType.Human));
        }

        [Test]
        public void Human_WhenTypeOfIsEqualToCleric_ShouldGetBonusAttackToEqualFive()
        {
            // Arrange
            var sut = new Human(Class.Cleric);

            // Act
            sut.AssignStatModifiers();
            var attackStrength = sut.SoldierStats.AttackStrength;
        
            Console.WriteLine($"Attack Strength: {attackStrength}");

            // Assert
            Assert.That(attackStrength, Is.EqualTo(5));
        }
    }
}
