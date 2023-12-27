using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PlanetWars.Repositories
{
    public class PlanetRepository : IRepository<IPlanet>
    {
        private List<IPlanet> models; public PlanetRepository()
        {
            models= new List<IPlanet>();
        }
        public IReadOnlyCollection<IPlanet> Models => models.AsReadOnly();

        public void AddItem(IPlanet model)
        {
            models.Add(model);
        }

        public IPlanet FindByName(string name)
        =>models.FirstOrDefault(p=>p.Name == name);

        public bool RemoveItem(string name)
        {
            if (models.FirstOrDefault(c => c.Name == name) == null)
            {
                return false;
            }
            models.Remove(models.FirstOrDefault(c => c.Name == name));
            return true;
        }
    }
}
