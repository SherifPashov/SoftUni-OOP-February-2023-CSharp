using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02.VehiclesExtension
{
    public class Bus : IVehicle
    {
        private const double FuelConsumptionConditioners = 1.4;
        public Bus(double fuelQuantity, double fuelConsumptionLitersPerKm, double tankCapacity)
        {
            FuelQuantity = fuelQuantity;
            FuelConsumptionLitersPerKm = fuelConsumptionLitersPerKm;
            TankCapacity = tankCapacity;
            if (FuelQuantity>TankCapacity)
            {
                FuelQuantity = 0;
            }
        }

        public double FuelQuantity { get; private set; }

        public double FuelConsumptionLitersPerKm { get; private set; }

        public double TankCapacity { get; private set; }
        public string Driving(double distance)
        {
            if (distance * (FuelConsumptionLitersPerKm + FuelConsumptionConditioners) > FuelQuantity)
            {
                return $"Bus needs refueling";
            }
            FuelQuantity -= (FuelConsumptionLitersPerKm + FuelConsumptionConditioners) * distance;
            return $"Bus travelled {distance} km";
        }
        public string DriveEmpty(double distance) 
        {
            if (distance * FuelConsumptionLitersPerKm > FuelQuantity)
            {
                return $"Bus needs refueling";
            }
            FuelQuantity -= FuelConsumptionLitersPerKm * distance;
            return $"Bus travelled {distance} km";
        }

        public void Refueling(double literFuel)
        {
            if (literFuel<=0)
            {
                Console.WriteLine("Fuel must be a positive number");
                return;
            }

            if (TankCapacity<literFuel+FuelQuantity)
            {
                Console.WriteLine($"Cannot fit {literFuel} fuel in the tank");
                return;
            }
            FuelQuantity += literFuel;

        }
        public override string ToString()
        {
            return $"Bus: {FuelQuantity:f2}";
        }
    }
}
