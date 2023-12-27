using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Repositories
{
    public class WeaponRepository : IRepository<IWeapon>
    {
        private List<IWeapon> models;
        public WeaponRepository()
        {
            models= new List<IWeapon>();
        }
        public IReadOnlyCollection<IWeapon> Models =>models.AsReadOnly();

        public void AddItem(IWeapon model)
        {
           models.Add(model);
        }

        public IWeapon FindByName(string name)
        => models.FirstOrDefault(n=>n.GetType().Name == name);

        public bool RemoveItem(string name)
        {
            if (models.FirstOrDefault(c => c.GetType().Name == name)==null)
            {
                return false;
            }
            models.Remove(models.FirstOrDefault(c => c.GetType().Name == name));
            return true;
        }
    }
}
