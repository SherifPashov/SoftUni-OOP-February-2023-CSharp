using NUnit.Framework;

namespace VehicleGarage.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void VehicleConstructor()
        {
            Vehicle vehicle = new Vehicle("Az", "qk", "123", 4);
            Assert.AreEqual("Az", vehicle.Brand);
            Assert.AreEqual("qk", vehicle.Model);
            Assert.AreEqual("123", vehicle.LicensePlateNumber);
            Assert.AreEqual(100, vehicle.BatteryLevel);
            Assert.AreEqual(false, vehicle.IsDamaged);
        }

        [Test]
        public void GarageConstructor()
        {
            Garage garage = new Garage(3);
            Assert.AreEqual(3, garage.Capacity);
            Assert.AreEqual(0, garage.Vehicles.Count);

        }

        [Test]
        public void GarageAddVehicleCorrect()
        {
            Garage garage = new Garage(3);
            Vehicle vehicle = new Vehicle("Az", "qk", "123", 4);
            Assert.AreEqual(true,garage.AddVehicle(vehicle));
            Assert.AreEqual(1, garage.Vehicles.Count);

            Assert.AreEqual("Az", garage.Vehicles[0].Brand);
            Assert.AreEqual("qk", garage.Vehicles[0].Model);
            Assert.AreEqual("123", garage.Vehicles[0].LicensePlateNumber);
            Assert.AreEqual(100, garage.Vehicles[0].BatteryLevel);
            Assert.AreEqual(false, garage.Vehicles[0].IsDamaged);
        }
        [Test]
        public void GarageAddVehicleUnCorrect()
        {
            Garage garage = new Garage(2);
            Vehicle vehicle = new Vehicle("Az", "qk", "123", 4);
           

            Assert.AreEqual(true, garage.AddVehicle(vehicle));
            Assert.AreEqual(1, garage.Vehicles.Count);

            Assert.AreEqual("Az", garage.Vehicles[0].Brand);
            Assert.AreEqual("qk", garage.Vehicles[0].Model);
            Assert.AreEqual("123", garage.Vehicles[0].LicensePlateNumber);
            Assert.AreEqual(100, garage.Vehicles[0].BatteryLevel);
            Assert.AreEqual(false, garage.Vehicles[0].IsDamaged);

            Assert.AreEqual(false, garage.AddVehicle(vehicle));
            Assert.AreEqual(1, garage.Vehicles.Count);

            Assert.AreEqual("Az", garage.Vehicles[0].Brand);
            Assert.AreEqual("qk", garage.Vehicles[0].Model);
            Assert.AreEqual("123", garage.Vehicles[0].LicensePlateNumber);
            Assert.AreEqual(100, garage.Vehicles[0].BatteryLevel);
            Assert.AreEqual(false, garage.Vehicles[0].IsDamaged);

            Vehicle vehicle1 = new Vehicle("Az", "qk", "12", 4);
            Assert.AreEqual(true, garage.AddVehicle(vehicle1));
            Assert.AreEqual(2, garage.Vehicles.Count);

            Vehicle vehicleDemo = new Vehicle("Az", "qk", "1234", 4);
            Assert.AreEqual(false, garage.AddVehicle(vehicleDemo));
            Assert.AreEqual(2, garage.Vehicles.Count);
        }

        [Test]
        public void GarageChargeVehicles()
        {
            Garage garage = new Garage(3);
            Vehicle vehicle = new Vehicle("Az", "qk", "123", 4);
            Vehicle vehicle2 = new Vehicle("Ti", "qk", "1234", 4);
            Vehicle vehicle3 = new Vehicle("Ti", "qk", "12", 4);
            vehicle.BatteryLevel = 60;
            vehicle2.BatteryLevel = 80;
            vehicle3.BatteryLevel = 70;

            Assert.AreEqual(true, garage.AddVehicle(vehicle));
            Assert.AreEqual(1, garage.Vehicles.Count);

            

            Assert.AreEqual(true, garage.AddVehicle(vehicle2));
            Assert.AreEqual(2, garage.Vehicles.Count);

            Assert.AreEqual(true, garage.AddVehicle(vehicle3));
            Assert.AreEqual(3, garage.Vehicles.Count);

            Assert.AreEqual(2, garage.ChargeVehicles(70));
            Assert.AreEqual(garage.Vehicles[0].BatteryLevel, 100);
            Assert.AreEqual(garage.Vehicles[1].BatteryLevel, 80);
            Assert.AreEqual(garage.Vehicles[2].BatteryLevel, 100);
        }

        [Test]
        public void GarageDriveVehicleUnCorrect()
        {
            Garage garage = new Garage(3);
            Vehicle vehicle = new Vehicle("Az", "qk", "123", 4);

            Assert.AreEqual(true, garage.AddVehicle(vehicle));
            Assert.AreEqual(1, garage.Vehicles.Count);

            garage.DriveVehicle("123", 101, false);

            Assert.AreEqual("Az", garage.Vehicles[0].Brand);
            Assert.AreEqual("qk", garage.Vehicles[0].Model);
            Assert.AreEqual("123", garage.Vehicles[0].LicensePlateNumber);
            Assert.AreEqual(100, garage.Vehicles[0].BatteryLevel);
            Assert.AreEqual(false, garage.Vehicles[0].IsDamaged);

            garage.Vehicles[0].IsDamaged = true;

            garage.DriveVehicle("123", 100, false);

            Assert.AreEqual("Az", garage.Vehicles[0].Brand);
            Assert.AreEqual("qk", garage.Vehicles[0].Model);
            Assert.AreEqual("123", garage.Vehicles[0].LicensePlateNumber);
            Assert.AreEqual(100, garage.Vehicles[0].BatteryLevel);
            Assert.AreEqual(true, garage.Vehicles[0].IsDamaged);

            garage.Vehicles[0].IsDamaged = false;

            garage.Vehicles[0].BatteryLevel = 20;
            garage.DriveVehicle("123", 31, false);


            Assert.AreEqual("Az", garage.Vehicles[0].Brand);
            Assert.AreEqual("qk", garage.Vehicles[0].Model);
            Assert.AreEqual("123", garage.Vehicles[0].LicensePlateNumber);
            Assert.AreEqual(20, garage.Vehicles[0].BatteryLevel);
            Assert.AreEqual(false, garage.Vehicles[0].IsDamaged);


            garage.Vehicles[0].BatteryLevel = 100;


            garage.DriveVehicle("123", 31, false);

            Assert.AreEqual("Az", garage.Vehicles[0].Brand);
            Assert.AreEqual("qk", garage.Vehicles[0].Model);
            Assert.AreEqual("123", garage.Vehicles[0].LicensePlateNumber);
            Assert.AreEqual(69, garage.Vehicles[0].BatteryLevel);
            Assert.AreEqual(false, garage.Vehicles[0].IsDamaged);

            garage.DriveVehicle("123", 31, true);

            Assert.AreEqual("Az", garage.Vehicles[0].Brand);
            Assert.AreEqual("qk", garage.Vehicles[0].Model);
            Assert.AreEqual("123", garage.Vehicles[0].LicensePlateNumber);
            Assert.AreEqual(38, garage.Vehicles[0].BatteryLevel);
            Assert.AreEqual(true, garage.Vehicles[0].IsDamaged);

            garage.DriveVehicle("123", 31, true);
            Assert.AreEqual("Az", garage.Vehicles[0].Brand);
            Assert.AreEqual("qk", garage.Vehicles[0].Model);
            Assert.AreEqual("123", garage.Vehicles[0].LicensePlateNumber);
            Assert.AreEqual(38, garage.Vehicles[0].BatteryLevel);
            Assert.AreEqual(true, garage.Vehicles[0].IsDamaged);

        }

        [Test]
        public void GarageRepairVehicles()
        {
            Garage garage = new Garage(3);
            Vehicle vehicle1 = new Vehicle("Az", "qk", "123", 4);
            Vehicle vehicle2 = new Vehicle("Az", "qk", "1", 4);
            

            Assert.AreEqual(true, garage.AddVehicle(vehicle1));
            Assert.AreEqual(1, garage.Vehicles.Count);
            Assert.AreEqual(true, garage.AddVehicle(vehicle2));
            Assert.AreEqual(2, garage.Vehicles.Count);

            Assert.AreEqual("Vehicles repaired: 0", garage.RepairVehicles());

            garage.Vehicles[1].IsDamaged = true;

            Assert.AreEqual("Vehicles repaired: 1", garage.RepairVehicles());
            Assert.AreEqual(garage.Vehicles[1].IsDamaged, false);
        }
    }
}