using Heroes.Models.Contracts;
using Heroes.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes.Models
{
    public class Map : IMap
    {
        public string Fight(ICollection<IHero> players)
        {
            List<Knight> knights = new List<Knight>();
                
            List<Barbarian> barbarians = new List<Barbarian>();

            foreach (var playar in players)
            {
                if (playar as Knight == null)
                {
                    barbarians.Add((Barbarian)playar);
                }
                else
                {
                    knights.Add((Knight)playar);
                }
            }

            while (true)
            {
                

                foreach (var knight in knights.Where(b => b.Health > 0))
                {
                    int points = knight.Weapon.DoDamage();
                    foreach (var barbarian in barbarians.Where(k => k.Health > 0))
                    {
                        barbarian.TakeDamage(points);
                    }

                }

                if (barbarians.All(b => b.Health == 0)) 
                {
                    return string.Format(OutputMessages.MapFightKnightsWin, knights.Where(k => k.IsAlive==false).Count());
                }

                foreach (var barbarian in barbarians.Where(b => b.Health > 0))
                {
                    foreach (var knight in knights.Where(k => k.Health > 0))
                    {
                        barbarian.Weapon.DoDamage();
                    }

                }
                if (knights.All(b => b.Health == 0)) ;
                {
                    return string.Format(OutputMessages.MapFigthBarbariansWin, barbarians.Where(b => b.IsAlive==false).Count());
                }
            }

           
        }
    }
}
