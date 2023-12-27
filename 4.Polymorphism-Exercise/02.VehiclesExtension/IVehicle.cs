using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02.VehiclesExtension
{
    public interface IVehicle
    {
        public double FuelQuantity { get; }

        public double FuelConsumptionLitersPerKm { get; }
        public double TankCapacity { get; }

        public string Driving(double kmDrive);
        public void Refueling(double literGas);
    }
}
