using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    public class Cake : Dessert
    {
        public const double Grams = 250;
        public const double Calories = 1000;
        public const decimal CakePrice = 5m;
        public Cake(string name) : base(name, CakePrice, Grams, Calories)
        {
        }
    }
}
