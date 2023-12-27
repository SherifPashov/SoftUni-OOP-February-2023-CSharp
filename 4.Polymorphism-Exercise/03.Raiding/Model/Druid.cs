using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.Raiding.Model
{
    public class Druid : Hero
    {
        private const int DefoultPower = 80;
        public Druid(string name) : base(name, DefoultPower)
        {
        }

        public override string CastAbility()
        => $"{this.GetType().Name} - {Name} healed for {Power}";
            
    }
}
