using _03.Raiding.Model.Intervace;
using _03.Raiding.Model;
using Raiding.Factories.Interfaces;


namespace VehiclesExtension.Factories;

public class HeroFactory : IHeroFactory
{
    public IHero Create(string type, string name)
    {
        switch (type)
        {
            case "Druid":
                return new Druid(name);
            case "Paladin":
                return new Paladin(name);
            case "Rogue":
                return new Rogue(name);
            case "Warrior":
                return new Warrior(name);
            default:
                throw new ArgumentException("Invalid hero!");
        }
    }
}