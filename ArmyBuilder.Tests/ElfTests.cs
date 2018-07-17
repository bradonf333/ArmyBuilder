﻿

using System;

using ArmyBuilder.Soldiers;

using NUnit.Framework;
using NUnit.Framework.Internal;

namespace ArmyBuilder.Tests
{
    [TestFixture]
    public class ElfTests
    {

        [Test]
        public void Elf_WhenCreated_WithDefaults_ShouldGetBonusSorceryStrengthOfTen()
        {
            // Arrange
            var sut = new Elf();

            // Act
            var sorceryStrength = sut.SorceryStrength;

            // Assert
            Assert.That(sorceryStrength, Is.EqualTo(10));
        }

        [Test]
        public void Elf_WhenCreated_WithDefaults_ShouldHaveSoldierTypeOfElf()
        {
            // Arrange
            var sut = new Elf();

            // Act
            var soldierType = sut.SoldierType();
            Console.WriteLine($"Soldier Type: {soldierType}");

            // Assert
            Assert.That(soldierType, Is.EqualTo("Elf"));
        }
    }
}
