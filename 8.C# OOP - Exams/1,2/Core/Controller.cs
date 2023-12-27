using RobotService.Core.Contracts;
using RobotService.Models;
using RobotService.Models.Contracts;
using RobotService.Repositories;
using RobotService.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Core
{
    public class Controller : IController
    {
        private SupplementRepository supplements;
        private RobotRepository robots;
        public Controller()
        {
            supplements= new SupplementRepository();
            robots= new RobotRepository();
        }
        public string CreateRobot(string model, string typeName)
        {
            if (typeName !=typeof(DomesticAssistant).Name &&
                typeName !=typeof(IndustrialAssistant).Name)
            {
                return string.Format(OutputMessages.RobotCannotBeCreated, typeName);
            }
            IRobot robot;
            if (typeName == typeof(DomesticAssistant).Name)
            {
                robot = new DomesticAssistant(model);
                robots.AddNew(robot);
            }
            else
            {
                robot = new  IndustrialAssistant(model);
                robots.AddNew(robot);
            }
            return string.Format(OutputMessages.RobotCreatedSuccessfully, typeName,model);
        }

        public string CreateSupplement(string typeName)
        {
            if (typeName!=typeof(SpecializedArm).Name&&
                typeName != typeof(LaserRadar).Name)
            {
                return string.Format(OutputMessages.SupplementCannotBeCreated, typeName);
            }
            ISupplement supplement;
            if (typeName==typeof(SpecializedArm).Name)
            {
                supplement = new SpecializedArm();
            }
            else
            {
                supplement= new LaserRadar();
            }
            supplements.AddNew(supplement);
            return string.Format(OutputMessages.SupplementCreatedSuccessfully, typeName);
        }

        public string UpgradeRobot(string model, string supplementTypeName)
        {
            ISupplement supplement=supplements.Models().FirstOrDefault(m=>m.GetType().Name==supplementTypeName);
            int intervace = supplement.InterfaceStandard;
            List<IRobot> supRobots = robots.Models()
                .Where(s=>s.Model==model)
                .Where(c=>c.InterfaceStandards.Contains(intervace)==false).ToList();

            if (supRobots.Count==0)
            {
                return string.Format(OutputMessages.AllModelsUpgraded, model);
            }
            robots.Models()
                .Where(s => s.Model == model)
                .Where(c => c.InterfaceStandards.Contains(intervace) == false).FirstOrDefault().InstallSupplement(supplement);
            supplements.RemoveByName(supplementTypeName);

            return string.Format(OutputMessages.UpgradeSuccessful,model,supplementTypeName); 

        }
        public string PerformService(string serviceName, int intefaceStandard, int totalPowerNeeded)
        {
            List<IRobot> listRobots = robots.Models().Where(r=>r.InterfaceStandards.Contains(intefaceStandard)).ToList();
            if (listRobots.Count==0)
            {
                return string.Format(OutputMessages.UnableToPerform, intefaceStandard);
            }
            listRobots = listRobots.OrderByDescending(c=>c.BatteryLevel).ToList();
            int baterySum = listRobots.Sum(r=>r.BatteryLevel);

            if (baterySum< totalPowerNeeded)
            {
                return string.Format(OutputMessages.MorePowerNeeded, serviceName, totalPowerNeeded - baterySum);
            }
            else
            {
                int count=0;
                while (totalPowerNeeded>0)
                {
                    foreach (var bot in listRobots)
                    {
                        if (bot.BatteryLevel>totalPowerNeeded)
                        {
                            bot.ExecuteService(totalPowerNeeded);
                            count++;
                            return string.Format(OutputMessages.PerformedSuccessfully, serviceName, count);

                        } 
                        else
                        {
                            totalPowerNeeded -= bot.BatteryLevel;
                            bot.ExecuteService(bot.BatteryLevel);
                            count++;
                        }
                    }
                    
                }
                return string.Format(OutputMessages.PerformedSuccessfully, serviceName, count);
            }
        }
        public string RobotRecovery(string model, int minutes)
        {
            int count = 0;
            List<IRobot> listRobots = robots.Models().Where(c => c.BatteryLevel <= c.BatteryCapacity * 0.5).ToList();
            foreach (var robo in listRobots)
            {
                robo.Eating(minutes);
                count++;
            }
            return string.Format(OutputMessages.RobotsFed, count);
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            List<IRobot> listRobots = robots.Models().OrderByDescending(c => c.BatteryLevel).ToList();
            foreach (var robot in listRobots)
            {
                sb.AppendLine(robot.ToString());
            }
            return sb.ToString().TrimEnd();
        }


    }
}
