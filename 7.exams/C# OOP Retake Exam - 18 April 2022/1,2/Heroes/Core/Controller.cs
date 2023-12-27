using Heroes.Core.Contracts;
using Heroes.Models;
using Heroes.Models.Contracts;
using Heroes.Repositories;
using Heroes.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes.Core
{
    public class Controller : IController
    {
        private HeroRepository heros;
        private WeaponRepository weapons;
        private Map map;
        public Controller()
        {
            heros= new HeroRepository();
            weapons= new WeaponRepository();
            map= new Map();
        }
        public string CreateHero(string type, string name, int health, int armour)
        {
            if (heros.FindByName(name)!=null)
            {
                throw new InvalidOperationException(string.Format(OutputMessages.HeroAlreadyExist, name));
            }
            if (type != typeof(Knight).Name && type!= typeof(Barbarian).Name)
            {
                throw new InvalidOperationException(OutputMessages.HeroTypeIsInvalid);
            }

            if (type == typeof(Knight).Name)
            {
                Knight knight = new Knight(name, health, armour);
                heros.Add(knight);
                return string.Format(OutputMessages.SuccessfullyAddedKnight, name);
            }
            else if (type == typeof(Barbarian).Name)
            {
                Barbarian barbarian = new Barbarian(name, health, armour);
                heros.Add(barbarian);
                return string.Format(OutputMessages.SuccessfullyAddedBarbarian, name);

            }
            return "";
        }
        public string AddWeaponToHero(string weaponName, string heroName)
        {
            if (heros.FindByName(heroName)==null)
            {
                throw new InvalidOperationException(string.Format(OutputMessages.HeroDoesNotExist, heroName));
            }

            if (weapons.FindByName(weaponName)==null)
            {
                throw new InvalidOperationException(string.Format(OutputMessages.WeaponDoesNotExist, weaponName));
            }

            if (heros.FindByName(heroName).Weapon != null)
            {
                throw new InvalidOperationException(string.Format(OutputMessages.HeroAlreadyHasWeapon, heroName));
            }
            IWeapon weapon = weapons.FindByName(weaponName);
            heros.FindByName(heroName).AddWeapon(weapon);
            return string.Format(OutputMessages.WeaponAddedToHero,heroName, weapons.FindByName(weaponName).GetType().Name.ToLower());
        
        }


        public string CreateWeapon(string type, string name, int durability)
        {
            List<IWeapon> weapons1 = weapons.Models.ToList();
            if (weapons1.FirstOrDefault(c=>c.Name==name) != null)
            {
                throw new InvalidOperationException(string.Format(OutputMessages.WeaponAlreadyExists,name));
            }

            if (type!=typeof(Mace).Name && type != typeof(Claymore).Name)
            {
                throw new InvalidOperationException(string.Format(OutputMessages.WeaponTypeIsInvalid));
            }
            if (type == typeof(Mace).Name)
            {
                Mace mace = new Mace(name, durability);
                weapons.Add( mace );
            }
            else if (type == typeof(Claymore).Name)
            {
                Claymore claymore = new Claymore(name, durability);
                weapons.Add( claymore );
            }

            return string.Format(OutputMessages.WeaponAddedSuccessfully, type, name);
        }

        public string HeroReport()
        {
            StringBuilder sb = new StringBuilder();

            List<IHero> list = heros
                .Models
                .OrderBy(c=>c.Name.GetType().Name)
                .ThenByDescending(c=>c.Health)
                .ThenBy(c=>c.Name)
                .ToList();

            foreach (var hero in list)
            {
                sb.AppendLine($"{hero.Name.GetType().Name }: {hero.Name }");
                sb.AppendLine($"--Health: { hero.Health }");
                sb.AppendLine($"--Weapon: ");
                if (hero.Weapon!=null)
                {
                    sb.AppendLine($"{hero.Weapon.Name.GetType().Name}");
                }
                else
                {
                    sb.AppendLine($"Unarmed");
                }
            }
            return sb.ToString().TrimEnd();
        }

        public string StartBattle()
        => map.Fight(heros.Models.ToList());
    }
}
