using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01.Vehicles
{
    public interface IVehicle
    {
        public double FuelQuantity { get; }

        public double FuelConsumptionLitersPerKm { get; }

        public string Driving(double kmDrive);
        public void Refueling(double literGas);
    }
}
