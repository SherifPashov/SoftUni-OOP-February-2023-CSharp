using _04.PizzaCalories;

try
{
    //missing name should throw custom exception - not using RemoveEmptyEntries
    string[] pizaInfo = Console.ReadLine().Split();
    string[] doughInfo = Console.ReadLine().Split();

    string pizzaName = pizaInfo[1];

    Dough dough = new(doughInfo[1], doughInfo[2], double.Parse(doughInfo[3]));

    Pizza pizza = new(pizzaName, dough);

    while (true)
    {
        string toppingInfo = Console.ReadLine();

        if (toppingInfo == "END")
        {
            break;
        }

        string[] toppIngItem = toppingInfo.Split();

        Topping topping = new(toppIngItem[1], double.Parse(toppIngItem[2]));

        pizza.AddTopping(topping);
    }

    Console.WriteLine(pizza);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}