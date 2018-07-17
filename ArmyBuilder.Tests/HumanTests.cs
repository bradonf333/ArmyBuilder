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
    }
}
