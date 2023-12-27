using Heroes.Models.Contracts;
using Heroes.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Models
{
    public abstract class Weapon : IWeapon
    {
        private string name;
        private int durability;
        private int damage;

        public Weapon(string name, int durability, int demage)
        {
            Name = name;
            Durability = durability;
            Demage = demage;

        }
        public int Demage { get; private set; }
        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.WeaponTypeNull);
                }
                name = value; 
            }

        }

        public int Durability
        {
            get { return durability; }
            private set
            {
                if (value<0)
                {
                    throw new ArgumentException(ExceptionMessages.DurabilityBelowZero);
                }
                durability= value;
            }
        }

        public int DoDamage()
        {
            if (name.GetType().Name=="Mec")
            {

            }
                durability -= 1;

            
                if (this.durability==0)
                {
                    return 0;
                }


            return damage;
        }
    }
}
