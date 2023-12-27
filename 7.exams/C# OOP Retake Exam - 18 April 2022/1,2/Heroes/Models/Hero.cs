using Heroes.Models.Contracts;
using Heroes.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Models
{
    public abstract class Hero : IHero
    {
        private string name;
        private int healt;
        private int armour;
        private IWeapon weapon;
        public Hero(string name, int health, int armour)
        {
            Name = name;
            Health = health;
            Armour= armour;
            
        }
        public string Name
        {
            get =>name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.HeroNameNull);
                }
                name = value;
            }
        }

        public int Health
        {
            get =>healt;
            private set
            {
                if (value<0)
                {
                    throw new ArgumentException(ExceptionMessages.HeroHealthBelowZero);
                }
                healt = value;
            }
        }

        public int Armour
        {
            get =>armour;
            private set
            {
                if (value<0)
                {
                    throw new ArgumentException(ExceptionMessages.HeroArmourBelowZero);
                }
                armour = value;
            }
        }

        public IWeapon Weapon
        {
            get =>weapon;
            private set
            {
                if (value ==null)
                {
                    throw new ArgumentException(ExceptionMessages.WeaponNull);
                }
                weapon = value;
            }
        }

        public bool IsAlive => this.healt>0;

        public void AddWeapon(IWeapon weapon)
        {
           Weapon = weapon;
        }

        public void TakeDamage(int points)
        {
            if (armour-points >= 0)
            {
                Armour -= points;
            }
            else if (Armour-points<0)
            {
                points -= armour;
                armour= 0;

                if (healt-points > 0)
                {
                    Health -= points;
                }
                else if (healt-points<=0)
                {
                    Health = 0;
                }
            }
        }
    }
}
