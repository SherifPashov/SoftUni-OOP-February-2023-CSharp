//using PlanetWars.Models.MilitaryUnits;
//using PlanetWars.Models.MilitaryUnits.Contracts;
//using PlanetWars.Models.Planets.Contracts;
//using PlanetWars.Models.Weapons;
//using PlanetWars.Models.Weapons.Contracts;
//using PlanetWars.Utilities.Messages;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace PlanetWars.Models.Planets
//{
//    public class Planet : IPlanet
//    {
//        private string name;
//        private double budget;
//        private double militaryPower;
//        private List<IMilitaryUnit> army;
//        private List<IWeapon> weapons;
//        public Planet(string name, double budget)
//        {
//            Name = name;
//            Budget = budget;
//            army = new List<IMilitaryUnit>();
//            weapons= new List<IWeapon>();
//        }
//        public string Name
//        {
//            get => name;
//            private set
//            {
//                if (string.IsNullOrWhiteSpace(value)) 
//                {
//                    throw new ArgumentException(ExceptionMessages.InvalidPlanetName);
//                }
//                name = value;
//            }
//        }

//        public double Budget
//        {
//            get => budget;
//            private set
//            {
//                if (0 > value)
//                {
//                    throw new ArgumentException(ExceptionMessages.InvalidBudgetAmount);
//                }
//                budget = value;
//            }
//        }

//        public double MilitaryPower
//        {
//            get => militaryPower;
//            private set
//            {
//                double amaunt=0;
//                foreach (var arm in army)
//                {
//                    amaunt += arm.EnduranceLevel;
//                }

//                foreach (var weapon in weapons)
//                {
//                    amaunt += weapon.DestructionLevel;
//                }
//                if (army.Any(a => a.GetType().Name == typeof(AnonymousImpactUnit).Name))
//                {
//                    amaunt = amaunt * 1.3;
//                }
//                else if (weapons.Any(a => a.GetType().Name == typeof(NuclearWeapon).Name))
//                {
//                    amaunt = amaunt * 1.45;
//                }

//                militaryPower= Math.Round( amaunt,3);
//            }
//        }

//        public IReadOnlyCollection<IMilitaryUnit> Army => army.AsReadOnly();

//        public IReadOnlyCollection<IWeapon> Weapons => weapons.AsReadOnly();

//        public void AddUnit(IMilitaryUnit unit)
//        {
//            army.Add(unit);
//        }

//        public void AddWeapon(IWeapon weapon)
//        {
//            weapons.Add(weapon);
//        }

//        public void TrainArmy()
//        {
//            foreach (var arm in army)
//            {
//                arm.IncreaseEndurance();
//            }
//        }
//        public void Spend(double amount)
//        {
//            if (Budget-amount<0)
//            {
//                throw new AggregateException(ExceptionMessages.UnsufficientBudget);
//            }
//            Budget -= amount;
//        }


//        public void Profit(double amount)
//        {
//            Budget += amount;
//        }
//        public string PlanetInfo()
//        {
//            StringBuilder sb = new StringBuilder();
//            sb.AppendLine($"Planet: {this.Name}");
//            sb.AppendLine($"--Budget: {this.Budget} billion QUID");
//            sb.Append($"--Forces: ");

//            if (this.Army.Count == 0)
//            {
//                sb.AppendLine("No units");
//            }
//            else
//            {
//                var units = new Queue<string>();

//                foreach (var item in this.Army)
//                {
//                    units.Enqueue(item.GetType().Name);
//                }

//                sb.AppendLine(string.Join(", ", units));
//            }

//            sb.Append($"--Combat equipment: ");

//            if (this.Weapons.Count == 0)
//            {
//                sb.AppendLine("No weapons");
//            }
//            else
//            {
//                var equipment = new Queue<string>();

//                foreach (var item in this.Weapons)
//                {
//                    equipment.Enqueue(item.GetType().Name);
//                }

//                sb.AppendLine(string.Join(", ", equipment));
//            }
//            sb.AppendLine($"--Military Power: {this.MilitaryPower}");

//            return sb.ToString().Trim();
//        }



//    }
//}


using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories;
using PlanetWars.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Models.Planets.Entities
{
    public class Planet : IPlanet
    {
        private string name;
        private double budget;
        private UnitRepository units;
        private WeaponRepository weapons;

        public Planet(string name, double budget)
        {
            Name = name;
            Budget = budget;
            this.units = new UnitRepository();
            this.weapons = new WeaponRepository();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidPlanetName));
                }
                name = value;
            }
        }

        public double Budget
        {
            get => budget;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidBudgetAmount));
                }
                budget = value;
            }
        }

        public double MilitaryPower => Math.Round(this.CalculateMilitaryPower(), 3);

        public IReadOnlyCollection<IMilitaryUnit> Army => this.units.Models;

        public IReadOnlyCollection<IWeapon> Weapons => this.weapons.Models;

        public void AddUnit(IMilitaryUnit unit)
        {
            this.units.AddItem(unit);
        }

        public void AddWeapon(IWeapon weapon)
        {
            this.weapons.AddItem(weapon);
        }

        public string PlanetInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Planet: {this.Name}");
            sb.AppendLine($"--Budget: {this.Budget} billion QUID");
            sb.Append($"--Forces: ");

            if (this.Army.Count == 0)
            {
                sb.AppendLine("No units");
            }
            else
            {
                var units = new Queue<string>();

                foreach (var item in this.Army)
                {
                    units.Enqueue(item.GetType().Name);
                }

                sb.AppendLine(string.Join(", ", units));
            }

            sb.Append($"--Combat equipment: ");

            if (this.Weapons.Count == 0)
            {
                sb.AppendLine("No weapons");
            }
            else
            {
                var equipment = new Queue<string>();

                foreach (var item in this.Weapons)
                {
                    equipment.Enqueue(item.GetType().Name);
                }

                sb.AppendLine(string.Join(", ", equipment));
            }
            sb.AppendLine($"--Military Power: {this.MilitaryPower}");

            return sb.ToString().Trim();
        }

        public void Profit(double amount)
        {
            this.Budget += amount;
        }

        public void Spend(double amount)
        {
            if (amount > this.Budget)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnsufficientBudget));
            }
            this.Budget -= amount;
        }

        public void TrainArmy()
        {
            foreach (var unit in this.Army)
            {
                unit.IncreaseEndurance();
            }
        }

        private double CalculateMilitaryPower()
        {
            double result = this.units.Models.Sum(x => x.EnduranceLevel) + this.weapons.Models.Sum(x => x.DestructionLevel);

            if (this.units.Models.Any(x => x.GetType().Name == nameof(AnonymousImpactUnit)))
            {
                result *= 1.3;
            }
            if (this.weapons.Models.Any(x => x.GetType().Name == nameof(NuclearWeapon)))
            {
                result *= 1.45;
            }

            return result;
        }
    }
}
