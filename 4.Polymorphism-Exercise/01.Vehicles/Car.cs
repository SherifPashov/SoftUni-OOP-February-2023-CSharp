using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace _01.Vehicles
{
    public class Car : IVehicle
    {
        private const double FuelConsumptionConditioners = 0.9;
        public Car(double fuelQuantity, double fuelConsumptionLitersPerKm)
        {
            FuelQuantity = fuelQuantity;
            FuelConsumptionLitersPerKm = fuelConsumptionLitersPerKm + FuelConsumptionConditioners;
        }

        public double FuelQuantity { get;private set; }

        public double FuelConsumptionLitersPerKm { get; private set; }

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
            FuelQuantity+= literFuel;
            
        }
        public override string ToString()
        {
            return $"Car: {FuelQuantity:f2}";
        }
    }
}
