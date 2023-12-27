using PlanetWars.Core.Contracts;
using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.Planets;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Planets.Entities;
using PlanetWars.Models.Weapons;
using PlanetWars.Repositories;
using PlanetWars.Repositories.Contracts;
using PlanetWars.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Xml.Linq;

namespace PlanetWars.Core
{
    public class Controller : IController
    {
        private PlanetRepository planets;
        public Controller()
        {
            planets = new PlanetRepository();
        }
        public string CreatePlanet(string name, double budget)
        {
           if (planets.FindByName(name) != null)
           {
                return string.Format(OutputMessages.ExistingPlanet, name);
           }
           planets.AddItem(new Planet(name, budget));
            return string.Format(OutputMessages.NewPlanet,name);

        }
        public string AddUnit(string unitTypeName, string planetName)
        {
            if (planets.FindByName(planetName) == null)
            {
                throw new InvalidOperationException ( string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }

            if (unitTypeName!= typeof(AnonymousImpactUnit).Name&&
                unitTypeName !=typeof(SpaceForces).Name &&
                unitTypeName != typeof(StormTroopers).Name)

            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.ItemNotAvailable,unitTypeName));
            }

            if (planets.FindByName(planetName).Army.Any(c => c.GetType().Name == unitTypeName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnitAlreadyAdded,unitTypeName,planetName));
            }


            if (unitTypeName == typeof(AnonymousImpactUnit).Name)
            {
                AnonymousImpactUnit anonymousImpactUnit = new AnonymousImpactUnit();
                planets.FindByName(planetName).Spend(anonymousImpactUnit.Cost);

                planets.FindByName(planetName).AddUnit(new AnonymousImpactUnit());

            }
            else if (unitTypeName == typeof(SpaceForces).Name)
            {
                SpaceForces spaceForces = new SpaceForces();
                planets.FindByName(planetName).Spend(spaceForces.Cost);
                planets.FindByName(planetName).AddUnit(spaceForces);
            }
            else if (unitTypeName == typeof(StormTroopers).Name)
            {
                StormTroopers stormTroopers = new StormTroopers();
                planets.FindByName(planetName).Spend(stormTroopers.Cost);
                planets.FindByName(planetName).AddUnit(stormTroopers);
            }

            return string.Format(OutputMessages.UnitAdded, unitTypeName, planetName);

        }

        public string AddWeapon(string planetName, string weaponTypeName, int destructionLevel)
        {
            if (planets.FindByName(planetName) == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }
            if (weaponTypeName != typeof(BioChemicalWeapon).Name &&
                weaponTypeName != typeof(NuclearWeapon).Name &&
                weaponTypeName != typeof(SpaceMissiles).Name)

            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.ItemNotAvailable, weaponTypeName));
            }
            if (planets.FindByName(planetName).Weapons.Any(w => w.GetType().Name == planetName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.WeaponAlreadyAdded, planetName, planetName));
            }

            if (weaponTypeName == typeof(BioChemicalWeapon).Name)
            {
                BioChemicalWeapon bioChemicalWeapon = new BioChemicalWeapon(destructionLevel);
                planets.FindByName(planetName).Spend(bioChemicalWeapon.Price);
                planets.FindByName(planetName).AddWeapon(new BioChemicalWeapon(destructionLevel));
            }
            else if (weaponTypeName == typeof(NuclearWeapon).Name)
            {
                NuclearWeapon nuclearWeapon = new NuclearWeapon(destructionLevel);
                planets.FindByName(planetName).Spend(nuclearWeapon.Price);
                planets.FindByName(planetName).AddWeapon(nuclearWeapon);
            }
            else if (weaponTypeName == typeof(SpaceMissiles).Name)
            {
                SpaceMissiles spaceMissiles = new SpaceMissiles(destructionLevel);
                planets.FindByName(planetName).Spend(spaceMissiles.Price);
                planets.FindByName(planetName).AddWeapon(spaceMissiles);
            }

            return string.Format(OutputMessages.UnitAdded, weaponTypeName, planetName);
        }


        public string SpecializeForces(string planetName)
        {
            if (planets.FindByName(planetName)==null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }

            if (planets.FindByName(planetName).Army.Count==0)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.NoUnitsFound));
            }
            planets.FindByName(planetName).TrainArmy();
            planets.FindByName(planetName).Spend(1.25);
            return string.Format(OutputMessages.ForcesUpgraded, planetName);
        }

        //public string SpaceCombat(string planetOne, string planetTwo)
        //{
        //   IPlanet planet1 = planets.FindByName(planetOne);
        //   IPlanet planet2 = planets.FindByName(planetTwo);
        //    if (planet1.MilitaryPower == planet2.MilitaryPower)
        //    {
        //        if (planet1.Weapons.Any(c=>c.GetType().Name == typeof(NuclearWeapon).Name) &&
        //            planet2.Weapons.Any(c => c.GetType().Name == typeof(NuclearWeapon).Name))
        //        {
        //            planet1.Spend(planet1.Budget / 2);
        //            planet2.Spend(planet2.Budget / 2);
        //            return OutputMessages.NoWinner;
        //        }
        //        else if (!planet1.Weapons.Any(c => c.GetType().Name == typeof(NuclearWeapon).Name) &&
        //           !planet2.Weapons.Any(c => c.GetType().Name == typeof(NuclearWeapon).Name))
        //        {
        //            planet1.Spend(planet1.Budget / 2);
        //            planet2.Spend(planet2.Budget / 2);
        //            return OutputMessages.NoWinner;
        //        }

        //    }
        //    else if (planet1.MilitaryPower > planet2.MilitaryPower)
        //    {
        //        planet1.Spend(planet1.Budget / 2);
        //        planet2.Spend(planet2.Budget / 2);

        //        planet1.Profit(planet2.Budget + planet2.MilitaryPower);
        //        planets.RemoveItem(planetTwo);
        //        return string.Format(OutputMessages.WinnigTheWar, planetOne, planetTwo);
        //    }
        //    else if (planet1.MilitaryPower < planet2.MilitaryPower)
        //    {
        //        planet1.Spend(planet1.Budget / 2);
        //        planet2.Spend(planet2.Budget / 2);

        //        planet2.Profit(planet1.Budget + planet2.MilitaryPower);
        //        planets.RemoveItem(planetOne);
        //        return string.Format(OutputMessages.WinnigTheWar, planetTwo, planetOne);
        //    }
        //    return "";
        //}
        public string SpaceCombat(string planetOne, string planetTwo)
        {
            IPlanet firstPlanet = planets.FindByName(planetOne);
            IPlanet secondPlanet = planets.FindByName(planetTwo);

            double firstPlanetHalfBudget = firstPlanet.Budget / 2;
            double secondPlanetHalfBudget = secondPlanet.Budget / 2;

            double firstCalculatedValue = firstPlanet.Army.Sum(x => x.Cost) +
                                            firstPlanet.Weapons.Sum(y => y.Price);

            double secondCalculatedValue = secondPlanet.Army.Sum(x => x.Cost) +
                                            secondPlanet.Weapons.Sum(y => y.Price);

            double firstPowerRatio = firstPlanet.MilitaryPower;
            double secondPowerRatio = secondPlanet.MilitaryPower;

            bool firstHasNuclear = firstPlanet.Weapons
                .Any(w => w.GetType().Name == nameof(NuclearWeapon));

            bool secondHasNuclear = secondPlanet.Weapons
                .Any(w => w.GetType().Name == nameof(NuclearWeapon));

            var firstNuclear = firstPlanet.Weapons
                .FirstOrDefault(w => w.GetType().Name == nameof(NuclearWeapon));
            var secondNuclear = secondPlanet.Weapons
                .FirstOrDefault(w => w.GetType().Name == nameof(NuclearWeapon));

            if (firstPowerRatio > secondPowerRatio)
            {
                firstPlanet.Spend(firstPlanetHalfBudget);
                firstPlanet.Profit(secondPlanetHalfBudget);
                firstPlanet.Profit(secondCalculatedValue);

                planets.RemoveItem(secondPlanet.Name);
                return string.Format(OutputMessages.WinnigTheWar, planetOne, planetTwo);
            }
            else if (firstPowerRatio < secondPowerRatio)
            {
                secondPlanet.Spend(secondPlanetHalfBudget);
                secondPlanet.Profit(firstPlanetHalfBudget);
                secondPlanet.Profit(firstCalculatedValue);

                planets.RemoveItem(firstPlanet.Name);
                return string.Format(OutputMessages.WinnigTheWar, planetTwo, planetOne);
            }
            else
            {
                if (firstNuclear != null && secondNuclear != null)
                {
                    //if (firstNuclear.DestructionLevel > secondNuclear.DestructionLevel)
                    //{
                    //    firstPlanet.Spend(firstPlanetHalfBudget);
                    //    firstPlanet.Profit(secondPlanetHalfBudget);
                    //    firstPlanet.Profit(secondCalculatedValue);

                    //    planets.RemoveItem(secondPlanet.Name);
                    //    return string.Format(OutputMessages.WinnigTheWar, planetOne, planetTwo);
                    //}
                    //else if (firstNuclear.DestructionLevel < secondNuclear.DestructionLevel)
                    //{
                    //    secondPlanet.Spend(secondPlanetHalfBudget);
                    //    secondPlanet.Profit(firstPlanetHalfBudget);
                    //    secondPlanet.Profit(firstCalculatedValue);

                    //    planets.RemoveItem(firstPlanet.Name);
                    //    return string.Format(OutputMessages.WinnigTheWar, planetTwo, planetOne);
                    //}
                    //else
                    //{
                    firstPlanet.Spend(firstPlanetHalfBudget);
                    secondPlanet.Spend(secondPlanetHalfBudget);
                    return string.Format(OutputMessages.NoWinner);
                    //}
                }
                else if (firstNuclear != null)
                {
                    firstPlanet.Spend(firstPlanetHalfBudget);
                    firstPlanet.Profit(secondPlanetHalfBudget);
                    firstPlanet.Profit(secondCalculatedValue);

                    planets.RemoveItem(secondPlanet.Name);
                    return string.Format(OutputMessages.WinnigTheWar, planetOne, planetTwo);
                }
                else if (secondNuclear != null)
                {
                    secondPlanet.Spend(secondPlanetHalfBudget);
                    secondPlanet.Profit(firstPlanetHalfBudget);
                    secondPlanet.Profit(firstCalculatedValue);

                    planets.RemoveItem(firstPlanet.Name);
                    return string.Format(OutputMessages.WinnigTheWar, planetTwo, planetOne);
                }
                else
                {
                    firstPlanet.Spend(firstPlanetHalfBudget);
                    secondPlanet.Spend(secondPlanetHalfBudget);
                    return string.Format(OutputMessages.NoWinner);
                }
            }
        }


        public string ForcesReport()
        {
            StringBuilder sb = new StringBuilder();



            sb.AppendLine("***UNIVERSE PLANET MILITARY REPORT***");
            foreach (var planet in planets.Models
                .OrderByDescending(c => c.MilitaryPower)
                .ThenBy(c => c.Name))
            {
                sb.AppendLine(planet.PlanetInfo());
            }

            return sb.ToString().TrimEnd();
        }




    }
 }


