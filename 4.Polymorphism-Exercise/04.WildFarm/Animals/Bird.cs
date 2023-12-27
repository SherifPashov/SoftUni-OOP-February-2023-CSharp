
using WildFarm.Animals.Interfaces;

namespace WildFarm.Animals
{
    public class Bird :IBird
    {
        double IBird.WingSize => throw new NotImplementedException();
    }
}
