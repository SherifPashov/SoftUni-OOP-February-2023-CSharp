using EDriveRent.Models.Contracts;
using EDriveRent.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Models
{
    public abstract class Vehicle : IVehicle
    {
        private string brand;
        private string model;
        private string licensePlateNumber;
        private int batteryLevel;

        public Vehicle(string brand, string model, double maxMileage, string licensePlateNumber)
        {
            Brand = brand;
            Model = model;
            MaxMileage = maxMileage;
            LicensePlateNumber = licensePlateNumber;
            BatteryLevel = 100;
            IsDamaged = false;
        }
        
        public string Brand
        {
            get =>brand; 
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.BrandNull);
                }
                brand = value;
            }
        }

        public string Model
        {
            get => model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.ModelNull);
                }
                model = value;
            }
        }

        public double MaxMileage
        {
            get;private set;
        }


        public string LicensePlateNumber
        {
            get => licensePlateNumber;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.LicenceNumberRequired);
                }
                licensePlateNumber = value;
            }
        }

        public int BatteryLevel
        {
            get => batteryLevel;
            private set
            {
                if (value > 100)
                {
                    batteryLevel = 100;
                }
                batteryLevel = value;
            }
        }


        public void Drive(double mileage)
        {
            int percent =(int) (MaxMileage / mileage);
            BatteryLevel -= percent;
            if (this.MaxMileage==180) 
            {
                BatteryLevel -= 5;
            }
        }

        public bool IsDamaged {get; private set;}

        public void ChangeStatus()
        {
            if (IsDamaged)
            {
                IsDamaged = false;
            }
            else 
            { 
                IsDamaged = true;
            }
        }

        

        public void Recharge()
        {
            BatteryLevel = 100;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{Brand} {Model} License plate: {LicensePlateNumber} Battery: {BatteryLevel}% Status: ");
            if (IsDamaged)
            {
                sb.AppendLine("damaged");
            }
            else
            {
                sb.AppendLine("OK");
            }
            return sb.ToString().TrimEnd();
        }
    }
}
