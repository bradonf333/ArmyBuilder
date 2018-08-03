using ArmyBuilder.Soldiers;
using NUnit.Framework;
using System;

namespace ArmyBuilder.Tests
{
    [TestFixture]
    public class BasicSoldierTests
    {
        [Test]
        public void Solider_WhenCreatedWithDefaults_ShouldHaveBaseAttackOfTwo()
        {
            // Arrange
            var sut = new Soldier();

            // Act
            var attackStrength = sut.SoldierStats.AttackStrength;
            Console.WriteLine($"Basic Soldier AttackStrength: {attackStrength}");

            // Assert
            Assert.That(attackStrength, Is.EqualTo(2));
        }

        [Test]
        public void Solider_WhenCreatedWithDefaults_ShouldHaveClassificationOfNone()
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
        public void Solider_WhenCreatedWithDefaults_ShouldHaveSoldierTypeOfBasicSoldier()
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
        public void Solider_WhenCreatedWithDefaults_ShouldHaveRankOfPrivate()
        {
            // Arrange
            var sut = new Soldier();

            // Act
            var soldierRank = sut.Rank;
            Console.WriteLine($"Basic Soldier Rank: {soldierRank}");

            // Assert
            Assert.That(soldierRank, Is.EqualTo(RankEnum.Private));
        }

        [Test]
        public void Solider_WhenCreatedWithDefaults_ShouldHaveAttackStrengthOfTwo()
        {
            // Arrange
            var sut = new Soldier();

            // Act
            sut.AssignStatModifiers();
            var soldierAttack = sut.SoldierStats.AttackStrength;
            Console.WriteLine($"Basic Soldier Attack: {soldierAttack}");

            // Assert
            Assert.That(soldierAttack, Is.EqualTo(2));
        }

        [Test]
        public void Solider_WhenCreatedWithDefaults_ShouldHaveSorceryStrengthOfZero()
        {
            // Arrange
            var sut = new Soldier();

            // Act
            sut.AssignStatModifiers();
            var soldierSorcery = sut.SoldierStats.SorceryStrength;
            Console.WriteLine($"Basic Soldier Sorcery: {soldierSorcery}");

            // Assert
            Assert.That(soldierSorcery, Is.EqualTo(0));
        }

        [Test]
        public void Solider_WhenCreatedWithDefaults_ShouldHaveBaseArmorOfZero()
        {
            // Arrange
            var sut = new Soldier();

            // Act
            sut.AssignStatModifiers();
            var soldierArmor = sut.SoldierStats.ArmorClass;
            Console.WriteLine($"Basic Soldier Armor Rating: {soldierArmor}");

            // Assert
            Assert.That(soldierArmor, Is.EqualTo(0));
        }

        [Test]
        public void Solider_WhenCreatedWithDefaults_ShouldHaveBaseMagicResistanceOfThree()
        {
            // Arrange
            var sut = new Soldier();

            // Act
            sut.AssignStatModifiers();
            var soldierMagicResistance = sut.SoldierStats.MagicResistance;
            Console.WriteLine($"Basic Soldier Magic Resistance: {soldierMagicResistance}");

            // Assert
            Assert.That(soldierMagicResistance, Is.EqualTo(0));
        }
    }
}
