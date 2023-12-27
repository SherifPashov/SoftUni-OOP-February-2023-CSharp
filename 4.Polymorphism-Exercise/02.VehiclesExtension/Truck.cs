using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02.VehiclesExtension
{
    public class Truck : IVehicle
    {
        private const double FuelConsumptionConditioners = 1.6;
        public Truck(double fuelQuantity, double fuelConsumptionLitersPerKm, double tankCapacity)
        {
            FuelQuantity = fuelQuantity;
            FuelConsumptionLitersPerKm = fuelConsumptionLitersPerKm + FuelConsumptionConditioners;
            TankCapacity = tankCapacity;
            if (fuelQuantity>TankCapacity)
            {
                FuelQuantity = 0;
            }
        }

        public double FuelQuantity { get; private set; }

        public double FuelConsumptionLitersPerKm { get; private set; }

        public double TankCapacity { get; private set; }

        public string Driving(double distance)
        {
            if (distance * FuelConsumptionLitersPerKm > FuelQuantity)
            {
                return $"Truck needs refueling";
            }
            FuelQuantity -= FuelConsumptionLitersPerKm * distance;
            return $"Truck travelled {distance} km";
        }

        public void Refueling(double literFuel)
        {
            if (literFuel<=0)
            {
                Console.WriteLine("Fuel must be a positive number");
                return;
            }
            if (TankCapacity < literFuel + FuelQuantity)
            {
                Console.WriteLine($"Cannot fit {literFuel} fuel in the tank");
                return;
            }
            FuelQuantity += literFuel*0.95;
            
        }
        public override string ToString()
        {
            return $"Truck: {FuelQuantity:f2}";
        }
    }
}
