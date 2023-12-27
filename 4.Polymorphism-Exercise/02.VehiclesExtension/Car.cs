using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace _02.VehiclesExtension
{
    public class Car : IVehicle
    {
        private const double FuelConsumptionConditioners = 0.9;
        public Car(double fuelQuantity, double fuelConsumptionLitersPerKm, double tankCapacity)
        {
            FuelQuantity = fuelQuantity;
            FuelConsumptionLitersPerKm = fuelConsumptionLitersPerKm + FuelConsumptionConditioners;
            TankCapacity = tankCapacity;
            if (FuelQuantity>TankCapacity)
            {
                FuelQuantity = 0;
            }
        }

        public double FuelQuantity { get;private set; }

        public double FuelConsumptionLitersPerKm { get; private set; }

        public double TankCapacity { get; private set; }

        public string Driving(double distance)
        {
            if (distance*FuelConsumptionLitersPerKm>FuelQuantity)
            {
                return $"Car needs refueling";
            }
            FuelQuantity -= FuelConsumptionLitersPerKm * distance;
            return $"Car travelled {distance} km";
        }

        public void Refueling(double literFuel)
        {
            if (literFuel <= 0)
            {
                Console.WriteLine("Fuel must be a positive number");
                return;
            }

            if (TankCapacity < literFuel + FuelQuantity)
            {
                Console.WriteLine($"Cannot fit {literFuel} fuel in the tank");
                return;
            }
            FuelQuantity += literFuel;
            
        }
        public override string ToString()
        {
            return $"Car: {FuelQuantity:f2}";
        }
    }
}
