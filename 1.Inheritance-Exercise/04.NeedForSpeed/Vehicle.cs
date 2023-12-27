﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeedForSpeed
{
    public abstract class Vehicle
    {
        private const double DefaultFuelConsumption = 1.25;

        protected Vehicle( int horsePower, double fuel)
        {
            Fuel = fuel;
            HorsePower = horsePower;
        }

        public virtual double FuelConsumption => DefaultFuelConsumption;
        public double Fuel { get; set; }
        public int HorsePower { get; set; }

        public virtual void Drive(double kilometers)=>Fuel -= kilometers * FuelConsumption;
        

    }
}