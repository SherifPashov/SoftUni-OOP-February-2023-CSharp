﻿using _03.ShoppingSpree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSpree.Models
{
    public class Person
    {
        private string name;
        private decimal money;
        private List<Product> products;

        public Person(string name, decimal money)
        {
            Name = name;
            Money = money;
            products = new List<Product>();
        }

        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be empty");
                }
                name = value;
            }
        }
        public decimal Money
        {
            get => money;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Money cannot be negative");
                }
                money = value;
            }
        }
        public string Add(Product product)
        {
            if (Money < product.Cost)
            {
                return $"{Name} can't afford {product}";
            }
            products.Add(product);

            Money -= product.Cost;

            return $"{Name} bought {product}";
        }




        public override string ToString()
        {
            if (products.Count == 0)
            {
                return $"{Name} - Nothing bought";
            }
            return $"{Name} - {string.Join(", ",products)}";
        }
    }
}
