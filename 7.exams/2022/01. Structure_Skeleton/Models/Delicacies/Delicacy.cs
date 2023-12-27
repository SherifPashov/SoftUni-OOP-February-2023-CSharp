using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ChristmasPastryShop.Models.Delicacies
{
    public abstract class Delicacy : IDelicacy
    {
        private string name;
        private double price;
        public string Name {
            get => name;
            private set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.NameNullOrWhitespace);
                }
                name = value;
            }

        }

        public double Price { get; private set; }
        public Delicacy(string delicacyName, double price)
        {
            Name= delicacyName;
            Price = price;
        }

        public override string ToString()
        {
            return $"{Name} - {Price:f2} lv";
        }

    }
}
