using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.Raiding.Model
{
    public class Paladin : Hero
    {
        private const int DefoultPower = 100;
        public Paladin(string name) : base(name, DefoultPower)
        {
        }

        public override string CastAbility()
        => $"{this.GetType().Name} - {Name} healed for {Power}";

    }
}
