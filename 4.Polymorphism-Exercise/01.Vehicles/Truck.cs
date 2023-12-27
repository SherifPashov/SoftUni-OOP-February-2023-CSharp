using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01.Vehicles
{
    public class Truck : IVehicle
    {
        private const double FuelConsumptionConditioners = 1.6;
        public Truck(double fuelQuantity, double fuelConsumptionLitersPerKm)
        {
            FuelQuantity = fuelQuantity;
            FuelConsumptionLitersPerKm = fuelConsumptionLitersPerKm + FuelConsumptionConditioners;
        }

        public double FuelQuantity { get; private set; }

        public double FuelConsumptionLitersPerKm { get; private set; }

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
            FuelQuantity += literFuel*0.95;
            
        }
        public override string ToString()
        {
            return $"Truck: {FuelQuantity:f2}";
        }
    }
}
