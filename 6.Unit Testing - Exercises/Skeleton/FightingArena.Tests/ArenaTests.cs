namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class ArenaTests
    {
        private Arena arena;
        [SetUp]
        public void SetUp()
        {
            arena = new Arena();
        }

        [TearDown]
        public void TearDown()
        {
            arena = null;
        }

        [Test]

        public void ArenaShouldBeVoidOnCreate()
        {
            arena = new Arena();
            Assert.AreEqual(0, arena.Count);
        }

        [Test]
        public void EnrollShouldAddWarrior()
        {
            arena.Enroll(new Warrior("Ivan", 5, 12));

            Assert.AreEqual(1, arena.Count);
        }

        [Test]
        public void EnrollShouldThrowWarriorNameIsnoUnique()
        {
            arena.Enroll(new Warrior("Ivan", 5, 12));

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(
                () => arena.Enroll(new Warrior("Ivan", 5, 12)));

            Assert.That(exception.Message, Is.EqualTo("Warrior is already enrolled for the fights!"));
        }

        [TestCase("Ivan","Misho")]
        [TestCase("Misho", "Ivan")]

        public void FightShouldThrowIfOneWarriorsIsMissing(string attacer,string defender)
        {
            arena.Enroll(new Warrior("Ivan", 5, 12));

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(
                () => arena.Fight(attacer, defender));

            Assert.That(exception.Message, Is.EqualTo("There is no fighter with name Misho enrolled for the fights!"));

        }
        [Test]
        public void TestFight()
        {
            Warrior attacker = new Warrior("Pesho", 15, 35);
            Warrior defender = new Warrior("Gosho", 15, 45);
            arena.Enroll(attacker);
            arena.Enroll(defender);

            arena.Fight(attacker.Name, defender.Name);

            Assert.AreEqual(20, attacker.HP);
            Assert.AreEqual(30, defender.HP);
        }

        

    }
}
