using RobotService.Models.Contracts;
using RobotService.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Models
{
    public abstract class Robot : IRobot
    {
        private string model;
        private int batteryCapacity;
        private int batteryLevel;
        private List<int> interfaceStandards;

        public Robot(string model, int batteryCapacity, int conversionCapacityIndex)
        {
            Model = model;
            BatteryCapacity = batteryCapacity;
            batteryLevel = batteryCapacity;
            ConvertionCapacityIndex = conversionCapacityIndex;

            interfaceStandards = new List<int>();
        }
        public string Model
        { 
            get { return model; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value)) 
                {
                    throw new ArithmeticException(ExceptionMessages.ModelNullOrWhitespace);
                }
                model = value;
            }
        }

        public int BatteryCapacity
        {
            get { return batteryCapacity; }
            private set
            {
                if (value<0)
                {
                    throw new ArithmeticException(ExceptionMessages.BatteryCapacityBelowZero);
                }
                batteryCapacity = value;
            }
        }

        public int BatteryLevel
        {
            get { return batteryLevel; }
            private set
            {
                if (value >= batteryCapacity)
                {
                    batteryLevel = batteryCapacity;
                }
                batteryLevel = BatteryCapacity;
            }
        }

        public int ConvertionCapacityIndex { get;private set; }
        

        public IReadOnlyCollection<int> InterfaceStandards => interfaceStandards.AsReadOnly();

        public void Eating(int minutes)
        {
            for (int i = 1; i <= minutes; i++)
            {
                batteryLevel += ConvertionCapacityIndex;
                if (BatteryLevel>=BatteryCapacity)
                {
                    BatteryLevel = BatteryCapacity;
                    return;
                }
            }
            
        }
        public void InstallSupplement(ISupplement supplement)
        {
            interfaceStandards.Add(supplement.InterfaceStandard);
            BatteryCapacity -= supplement.BatteryUsage;
        }

        public bool ExecuteService(int consumedEnergy)
        {
            if (BatteryLevel>=consumedEnergy)
            {
                BatteryLevel -= consumedEnergy;
                return true;
            }
            else 
            {
                return false;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{Model.GetType().Name} {Model}:");
            sb.AppendLine($"--Maximum battery capacity: {BatteryCapacity}");
            sb.AppendLine($"--Current battery level: {BatteryLevel}");
            if (interfaceStandards.Count==0)
            {
                sb.AppendLine("--Supplements installed: none");
            }
            else
            {
                sb.Append($"--Supplements installed: ");
                foreach (var inter in interfaceStandards)
                {
                    sb.Append($"{inter.ToString()} ");
                }
            }
            return sb.ToString();
        }

    }
}
