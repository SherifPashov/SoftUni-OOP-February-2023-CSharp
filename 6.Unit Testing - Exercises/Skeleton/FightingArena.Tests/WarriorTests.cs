namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class WarriorTests
    {
        private Warrior warrior;

        [SetUp]
        public void SetUp()
        {
            warrior=new Warrior("Pesho",15,35);
        }
        [TearDown]
        public void TearDown() 
        {
            warrior = null;
        }

        [Test]

        public void CorrectWarriorConstructor()
        {
            warrior = new Warrior("Pesho", 15, 35);

            Assert.AreEqual("Pesho", warrior.Name);
            Assert.AreEqual(15, warrior.Damage);
            Assert.AreEqual(35, warrior.HP);
        }

        [TestCase(null)]
        [TestCase("  ")]
        [TestCase(" ")]
        public void CreateWarriorThrowWhenNameNullOrWhiteSpace(string name)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => new Warrior(name, 15, 35));

            Assert.That(exception.Message, Is.EqualTo("Name should not be empty or whitespace!"));
        }

        [TestCase(0)]
        [TestCase(-2)]
        [TestCase(-3)]
        public void CreateWarriorThrowWhenDemageLessOrThatZero(int demage)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => new Warrior("Pesho", demage, 35));

            Assert.That(exception.Message, Is.EqualTo("Damage value should be positive!"));
        }

        
        [TestCase(-2)]
        [TestCase(-3)]
        public void CreateWarriorThrowWhenHpLessZero(int hp)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => new Warrior("Pesho", 15, hp));

            Assert.That(exception.Message, Is.EqualTo("HP should not be negative!"));
        }

        [Test]

        public void AttackShouldThrowHpLessThan30()
        {
            Warrior attacker = new Warrior("Gosho", 10, 10);
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(
                ()=> attacker.Attack(warrior));

            Assert.That(exception.Message, Is.EqualTo("Your HP is too low in order to attack other warriors!"));
        }
        [Test]
        public void DefenderShouldThrowHpLessThan30()
        {
            Warrior defender
                = new Warrior("Gosho", 10, 10);
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(
                () => warrior.Attack(defender));

            Assert.That(exception.Message, Is.EqualTo("Enemy HP must be greater than 30 in order to attack him!"));
        }

        [Test]

        public void AttackShouldThrowDemeegeLesEnemyHp()
        {
            Warrior defender = new Warrior("Gosho", 50, 100);
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(
                () => warrior.Attack(defender));

            Assert.That(exception.Message, Is.EqualTo("You are trying to attack too strong enemy"));
        }

        [Test]

        public void AttackShouldSucced() 
        {
            Warrior defender = new Warrior("Gosho", 15, 35);

            warrior.Attack(defender);

            Assert.AreEqual(20, warrior.HP);
            Assert.AreEqual(20, defender.HP);


        }
        [Test]
        public void AttackShouldKill()
        {
            Warrior attacker = new Warrior("Gosho", 36, 35);

            attacker.Attack(warrior);

            Assert.AreEqual(0, warrior.HP);
            Assert.AreEqual(20, attacker.HP);


        }
    }
}