namespace CarManager.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class CarManagerTests
    {
        private Car car;

        [SetUp]

        public void SetUp()
        {
            car = new Car("VW","Golf", 5.7 , 45);
        }

        [TearDown]
        public void TearDown()
        {
            car = null;

        }

        [Test]

        public void CreateCar()
        {
            car = new Car("VW", "Golf", 5.7, 45);
            Assert.AreEqual("VW", car.Make);
            Assert.AreEqual("Golf", car.Model);
            Assert.AreEqual(5.7, car.FuelConsumption);
            Assert.AreEqual(45, car.FuelCapacity);
            Assert.AreEqual(0, car.FuelAmount);
        }

        [TestCase(null)]
        [TestCase("")]

        public void CreateCarFailsIfMakeIsNullOrEmpty(string make)
        {
            ArgumentException exception = Assert.Throws< ArgumentException>(
                ()=>car = new Car(make, "Golf", 5.7, 45));

            Assert.That(exception.Message,
                Is.EqualTo("Make cannot be null or empty!"));

        }

        [TestCase(null)]
        [TestCase("")]

        public void CreateCarFailsIfModelIsNullOrEmpty(string model)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => car = new Car("VW", model, 5.7, 45));

            Assert.That(exception.Message,
                Is.EqualTo("Model cannot be null or empty!"));

        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-10)]

        public void CreateCarFailsIfFuelConsumptionLessOrEqualToZero(double fuelConsumption)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => car = new Car("VW", "Golf", fuelConsumption, 45));

            Assert.That(exception.Message,
                Is.EqualTo("Fuel consumption cannot be zero or negative!"));

        }

      
        [TestCase(-1)]
        [TestCase(-10)]

        public void CreateCarFailsIfFuelCapacityLessOrEqualToZero(double fuelCapacity)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => car = new Car("VW", "Golf", 5.7, fuelCapacity));

            Assert.That(exception.Message,
                Is.EqualTo("Fuel capacity cannot be zero or negative!"));

        }

        [Test]
        [TestCase(0)]
        [TestCase(-3)]

        public void RefuelShouldThrowLessThanOrEqualToZero(double litesrs)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(
                ()=> car.Refuel(litesrs));

            Assert.That(exception.Message, Is.EqualTo("Fuel amount cannot be zero or negative!"));
        }

        [Test]
        
        public void RefuelShouldBeEqualToFueAmmount()
        {
            car.Refuel(42);

            Assert.AreEqual(42, car.FuelAmount);
        }

        [Test]

        public void RefuelShouldBeEqualToToCamacity()
        {
            car.Refuel(48);

            Assert.AreEqual(45, car.FuelAmount);
        }

        [Test]

        public void DriveShouldThrowIfNotEnoghFuel()
        {
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(
                () => car.Drive(1));
            Assert.That(exception.Message, Is.EqualTo("You don't have enough fuel to drive!"));
        }

        [Test]
        public void DriveCorrectlyReturnFuelAmaunt() 
        {
            car.Refuel(10);
            car.Drive(100);

            Assert.AreEqual(4.3,car.FuelAmount);

        }

    }
}