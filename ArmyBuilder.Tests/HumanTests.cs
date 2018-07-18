using System;

using ArmyBuilder.Soldiers;

using NUnit.Framework;

namespace ArmyBuilder.Tests
{
    [TestFixture]
    public class HumanTests
    {
        [Test]
        public void Human_WhenCreated_WithDefaults_ShouldHaveSoldierTypeOfHuman()
        {
            // Arrange
            var sut = new Human();

            // Act
            var soldierType = sut.SoldierType();
            Console.WriteLine($"Soldier Type: {soldierType}");

            // Assert
            Assert.That(soldierType, Is.EqualTo("Human"));
        }

        [Test]
        public void Human_WhenTypeOfIsEqualToCleric_ShouldGetBonusAttackToEqualFive()
        {
            // Arrange
            var sut = new Human();

            // Act
            sut.Classification = ClassificationEnum.Cleric;
            sut.AssignAttackBonus();
            var attackStrength = sut.AttackStrength;
        
            Console.WriteLine($"Attack Strength: {attackStrength}");

            // Assert
            Assert.That(attackStrength, Is.EqualTo(5));
        }
    }
}
