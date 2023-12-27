using NUnit.Framework;
using System;
using System.ComponentModel;
using System.Linq;
using System.Numerics;

namespace PlanetWars.Tests
{
    public class Tests
    {
        [TestFixture]
        public class PlanetWarsTests
        {
            

        }
        [Test]
        public void TestWeapanConstructor()
        {
            Weapon weapon = new Weapon("Mech", 20, 5);

            Assert.AreEqual("Mech",weapon.Name);
            Assert.AreEqual(20,weapon.Price);
            Assert.AreEqual(5,weapon.DestructionLevel);

        }

        [TestCase(-1)]
        [TestCase(-10)]
        [TestCase(-5)]
        public void TestWeapanThrowExceptionPriceWhenLessZero(double price) 
        {
            ArgumentException exception=Assert.Throws< ArgumentException>(()=> new Weapon("Mech", price, 5));
            Assert.AreEqual("Price cannot be negative.", exception.Message);
        }

        [Test]
        public void TestWeapanIncreaseDestructionLevel()
        {
            Weapon weapon = new Weapon("Mech", 20, 5);

            Assert.AreEqual("Mech", weapon.Name);
            Assert.AreEqual(20, weapon.Price);
            Assert.AreEqual(5, weapon.DestructionLevel);
            weapon.IncreaseDestructionLevel();
            Assert.AreEqual(6, weapon.DestructionLevel);
            weapon.IncreaseDestructionLevel();
            Assert.AreEqual(7, weapon.DestructionLevel);


        }

        [Test]
        public void TestWeapanIsNuclear()
        {
            Weapon weapon = new Weapon("Mech", 20, 8);

            Assert.AreEqual("Mech", weapon.Name);
            Assert.AreEqual(20, weapon.Price);
            Assert.AreEqual(8, weapon.DestructionLevel);

            weapon.IncreaseDestructionLevel();
            Assert.AreEqual(9, weapon.DestructionLevel);
            Assert.AreEqual(false, weapon.IsNuclear);
            
            weapon.IncreaseDestructionLevel();
            Assert.AreEqual(10, weapon.DestructionLevel);
            Assert.AreEqual(true, weapon.IsNuclear);


        }
        [Test]
        public void TestPlanetConstructor()
        {
            Planet palnet = new Planet("Zemq",5);

            Assert.AreEqual("Zemq", palnet.Name);
            Assert.AreEqual(5, palnet.Budget);
            Assert.AreEqual(0, palnet.Weapons.Count);

        }

        [TestCase("")]
        [TestCase(null)]
        public void TestPlanetNameThrowExceptionWhenNullOrEmpty(string name)
        {
            ArgumentException exception=Assert.Throws< ArgumentException>(()=> new Planet(name, 5));

            Assert.AreEqual("Invalid planet Name",exception.Message);
            

        }

        [TestCase(-1)]
        [TestCase(-10)]
        [TestCase(-5)]
        public void TestPlanetBudgetThrowExceptionWhenLessZero(double budget)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() => new Planet("Zemq", budget));

            Assert.AreEqual("Budget cannot drop below Zero!", exception.Message);


        }

        [Test]
        public void TestPlanetAddWeapon()
        {
            Planet planet = new Planet("Zemq", 5);
            Weapon weapon = new Weapon("Mech", 20, 8);

            planet.AddWeapon(new Weapon("Knife", 20, 8));

            Assert.AreEqual(1, planet.Weapons.Count);
            planet.AddWeapon(weapon);
            Assert.AreEqual(2, planet.Weapons.Count);


        }
        [Test]
        public void TestPlanetAddWeaponThrowWhenWeapanAllredyAdded()
        {
            Planet planet = new Planet("Zemq", 5);
            Weapon weapon = new Weapon("Mech", 20, 8);

            planet.AddWeapon(weapon);

            Assert.AreEqual(1, planet.Weapons.Count);
            InvalidOperationException exception=Assert.Throws< InvalidOperationException>(()=> planet.AddWeapon(weapon));
            Assert.AreEqual($"There is already a Mech weapon.", exception.Message);


        }

        [Test]
        public void TestPlanetMilitaryPowerRatio()
        {
            Planet planet = new Planet("Zemq", 5);
            Weapon weapon = new Weapon("Mech", 20, 8);

            planet.AddWeapon(new Weapon("Knife", 20, 8));
            planet.AddWeapon(weapon);

            Assert.AreEqual(16.0d, planet.MilitaryPowerRatio);
            


        }
        [Test]
        public void TestPlanetProfit()
        {
            Planet planet = new Planet("Zemq", 5);
            planet.Profit(50);

            Assert.AreEqual(55.0d, planet.Budget);

        }

        [Test]
        public void TestPlanetSpendFunds()
        {
            Planet planet = new Planet("Zemq", 50);
            planet.SpendFunds(5);

            Assert.AreEqual(45.0d, planet.Budget);

        }
        [Test]
        public void TestPlanetSpendFundsThrowExceptionWhenBudgetLesAmaunt()
        {
            Planet planet = new Planet("Zemq", 5);


            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => planet.SpendFunds(50));
            Assert.AreEqual(exception.Message, "Not enough funds to finalize the deal.");
        }

        [Test]
        public void TestPlanetRemoveWeapon()
        {
            Planet planet = new Planet("Zemq", 5);
            Weapon weapon = new Weapon("Mech", 20, 8);

            planet.AddWeapon(new Weapon("Knife", 20, 8));
            planet.AddWeapon(weapon);

            Assert.AreEqual(2, planet.Weapons.Count);
            planet.RemoveWeapon("Mech");
            Assert.AreEqual(1, planet.Weapons.Count);



        }

        [Test]
        public void TestPlanetUpgradeWeapon()
        {
            Planet planet = new Planet("Zemq", 5);
            Weapon weapon = new Weapon("Mech", 20, 8);

            planet.AddWeapon(new Weapon("Knife", 20, 8));
            planet.AddWeapon(weapon);
            planet.UpgradeWeapon("Mech");
            

        }
        [Test]
        public void TestPlanetUpgradeWeaponThrowException()
        {
            Planet planet = new Planet("Zemq", 5);
            Weapon weapon = new Weapon("Mech", 20, 8);

            planet.AddWeapon(new Weapon("Knife", 20, 8));
            planet.AddWeapon(weapon);
            InvalidOperationException exception=Assert.Throws< InvalidOperationException>(()=> planet.UpgradeWeapon("Noj"));

            Assert.AreEqual(exception.Message, $"Noj does not exist in the weapon repository of Zemq");

        }

        [Test]
        public void TestPlanetDestructOpponent()
        {
            Planet planet = new Planet("Zemq", 5);
            Planet oponent = new Planet("Ti", 5);
            Weapon weapon = new Weapon("Mech", 20, 8);

            planet.AddWeapon(new Weapon("Knife", 20, 8));
            planet.AddWeapon(weapon);

            oponent.AddWeapon(new Weapon("Knife", 20, 8));
            Assert.AreEqual($"Ti is destructed!", planet.DestructOpponent(oponent));

        }

        [Test]
        public void TestPlanetDestructOpponentThrowException()
        {
            Planet oponent = new Planet("Zemq", 5);
            Planet planet = new Planet("Ti", 5);
            Weapon weapon = new Weapon("Mech", 20, 8);

            oponent.AddWeapon(new Weapon("Knife", 20, 8));
            oponent.AddWeapon(weapon);
            planet.AddWeapon(weapon);

            planet.AddWeapon(new Weapon("Knife", 20, 8));
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(()=> oponent.DestructOpponent(planet));

            Assert.AreEqual("Ti is too strong to declare war to!", exception.Message);

        }


        [Test]
        public void TestPlanetUpgradeWeapon2()
        {
            Planet planet = new Planet("Zemq", 5);
            Weapon weapon = new Weapon("Mech", 20, 8);

            
            planet.AddWeapon(weapon);
            planet.UpgradeWeapon("Mech");

            Weapon[] weapon1 = planet.Weapons.ToArray(); 
            
                Assert.AreEqual(9, weapon1[0].DestructionLevel);
           

        }

    }
}
