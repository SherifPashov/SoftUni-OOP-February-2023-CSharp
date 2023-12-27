

using FoodShortage.Interface;

namespace FoodShortage.Model
{
    public class Pet : IPet, IBuyer
    {
        public Pet(string name, string birthdate)
        {
            Name = name;
            Birthdate = birthdate;
        }

        public string Name { get; private set; }

        public string Birthdate { get; private set; }
    }
}
