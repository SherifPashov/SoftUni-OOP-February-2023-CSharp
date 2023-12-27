using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ChristmasPastryShop.Models.Cocktails
{
    public abstract class Cocktail : ICocktail
    {
        private string name;
        
        
        public string Name { get => name;
            private set
            { 
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.NameNullOrWhitespace);
                }
                name = value;
            }
        }

        public string Size { get; private set; }

        public double Price { get; private set; }

        public override string ToString()
        {
            return $"{Name} ({Size}) - {Price:f2} lv";
        }

        public Cocktail(string cocktailName, string size, double price)
        {
            Name= cocktailName;
            Size = size;
            Price = price;

        }
    }
}
