using NUnit.Framework;
using System;

namespace RepairShop.Tests
{
    public class Tests
    {
        public class RepairsShopTests
        {
            [Test] 
            public void CarConstrucktor()
            {
                Car car = new Car("Vw", 5);

                Assert.AreEqual("Vw", car.CarModel);
                Assert.AreEqual(5, car.NumberOfIssues);
                Assert.AreEqual(false, car.IsFixed);

                Car car1 = new Car("Vw", 0);
                Assert.AreEqual("Vw", car1.CarModel);
                Assert.AreEqual(0, car1.NumberOfIssues);
                Assert.AreEqual(true, car1.IsFixed);
            }


            [Test]
            public void GarageConstrucktor()
            {
                Garage garage = new Garage("Az", 5);
                Assert.AreEqual("Az",garage.Name );
                Assert.AreEqual(5, garage.MechanicsAvailable);
                Assert.AreEqual(0, garage.CarsInGarage);
            }

            [TestCase(null)]
            [TestCase("")]
            public void GarageNameThrowExceptionWhenNullOrEmpty(string name)
            {
                ArgumentNullException exception=Assert.Throws< ArgumentNullException>(()=> new Garage(name, 5));

                Assert.AreEqual(exception.Message, "Invalid garage name. (Parameter 'value')");

            }

            [TestCase(0)]
            [TestCase(-10)]
            public void GarageMechanicsAvailableThrowExceptionWhenLessone(int mechanicsAvailable)
            {
                ArgumentException exception = Assert.Throws<ArgumentException>(() => new Garage("Az", mechanicsAvailable));

                Assert.AreEqual(exception.Message, "At least one mechanic must work in the garage.");

            }

            [Test]
            public void GarageAddCar()
            {
                Garage garage = new Garage("Az", 5);

                Assert.AreEqual(0,garage.CarsInGarage);

                garage.AddCar(new Car("Audi", 3));

                Assert.AreEqual(1,garage.CarsInGarage);
            }

            [Test]
            public void GarageAddCarThrowExceptionWhenZeroAvailble()
            {
                Garage garage = new Garage("Az", 1);

                Assert.AreEqual(0, garage.CarsInGarage);

                garage.AddCar(new Car("Audi", 3));
                InvalidOperationException exception = Assert.Throws< InvalidOperationException>(()=> garage.AddCar(new Car("Audi A3", 3)));

                Assert.AreEqual(exception.Message, "No mechanic available.");
            }

            [Test]
            public void GarageCarFix()
            {
                Garage garage = new Garage("Az", 5);
                garage.AddCar(new Car("Audi", 3));
                Car car = garage.FixCar("Audi");

                Assert.AreEqual("Audi", car.CarModel);
                Assert.AreEqual(0, car.NumberOfIssues);
                Assert.AreEqual(true, car.IsFixed);

            }

            [Test]
            public void GarageCarFixThrowExceptionWhenNoCarExist()
            {
                Garage garage = new Garage("Az", 5);
                garage.AddCar(new Car("Audi", 3));
                InvalidOperationException exception=Assert.Throws< InvalidOperationException>(()=> garage.FixCar("Vw"));

                Assert.AreEqual(exception.Message, "The car Vw doesn't exist.");

            }
            [Test]
            public void GarageRemoveFixedCar()
            {
                Garage garage = new Garage("Az", 5);
                garage.AddCar(new Car("Audi", 3));
                garage.AddCar(new Car("VW", 4));

                Car car = garage.FixCar("Audi");

                Assert.AreEqual(2,garage.CarsInGarage);

                Assert.AreEqual("Audi", car.CarModel);
                Assert.AreEqual(0, car.NumberOfIssues);
                Assert.AreEqual(true, car.IsFixed);

                Assert.AreEqual(1,garage.RemoveFixedCar());
                Assert.AreEqual(1,garage.CarsInGarage);

            }

            [Test]
            public void GarageRemoveFixedCarThrowExceptironWhenNoHaveFixsetCar()
            {
                Garage garage = new Garage("Az", 5);
                garage.AddCar(new Car("Audi", 3));
                garage.AddCar(new Car("VW", 4));
                InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => garage.RemoveFixedCar());

                Assert.AreEqual(exception.Message, "No fixed cars available.");
            }

            [Test]
            public void GarageReport()
            {
                Garage garage = new Garage("Az", 5);
                garage.AddCar(new Car("Audi", 3));
                garage.AddCar(new Car("VW", 4));
                

                Assert.AreEqual(garage.Report(), $"There are 2 which are not fixed: Audi, VW.");

                garage.FixCar("VW");
                Assert.AreEqual(garage.Report(), $"There are 1 which are not fixed: Audi.");
            }
        }
    }
}