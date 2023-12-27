using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Repositories
{
    public class UnitRepository : IRepository<IMilitaryUnit>
    {
        private List<IMilitaryUnit> models;
        public UnitRepository()
        {
            models = new List<IMilitaryUnit>();
        }
        public IReadOnlyCollection<IMilitaryUnit> Models => models.AsReadOnly();

        public void AddItem(IMilitaryUnit model)
        {
            models.Add(model);
        }

        public IMilitaryUnit FindByName(string name)
        =>models.FirstOrDefault(c=>c.GetType().Name == name);

        public bool RemoveItem(string name)
        {
            if (models.FirstOrDefault(c => c.GetType().Name == name) == null)
            {
                return false;
            }
            models.Remove(models.FirstOrDefault(c => c.GetType().Name == name));
            return true;
        }
    }
}
