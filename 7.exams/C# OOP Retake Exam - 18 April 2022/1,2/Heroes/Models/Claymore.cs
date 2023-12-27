using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Models
{
    public class Claymore : Weapon
    {
        public Claymore(string name, int durability) : base(name, durability,20)
        {
        }
    }
}
