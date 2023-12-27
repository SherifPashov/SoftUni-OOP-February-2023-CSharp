using _03.Raiding.Model.Intervace;

namespace Raiding.Factories.Interfaces;

public interface IHeroFactory
{
    IHero Create(string type, string name);
}