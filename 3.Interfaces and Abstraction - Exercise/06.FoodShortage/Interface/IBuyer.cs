using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShortage.Interface
{
    public interface IBuyer2 
    {
        string Name { get; }
        int BuyFood();
        int Food { get; }
    }
}
