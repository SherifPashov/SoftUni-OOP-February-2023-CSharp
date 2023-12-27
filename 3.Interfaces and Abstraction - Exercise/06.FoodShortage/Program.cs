



using _05.BirthdayCelebrations.Model;
using FoodShortage.Interface;
using FoodShortage.Model;

internal class Program
{
    private static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine());

        List<IBuyer2> buyer2s = new List<IBuyer2>();

        for (int i = 0; i < n; i++)
        {
            string[] infoBuyer = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            string name = infoBuyer[0];
            int age = int.Parse(infoBuyer[1]);
            if (infoBuyer.Length == 4)
            {
                string id = infoBuyer[2];
                string birdthdate = infoBuyer[3];
                buyer2s.Add(new Citizen(name, age, id, birdthdate));
            }
            else if (infoBuyer.Length == 3)
            {
                string group = infoBuyer[2];
                buyer2s.Add(new Rebel(name, age, group));
            }
        }


        int buyFoodSum = 0;
        string comand;
        while ((comand = Console.ReadLine()) != "End")
        {
            string nameBuyer = comand;
            IBuyer2 buyer = buyer2s.FirstOrDefault(x => x.Name == comand);
            if (buyer != null)
            {
                int currentFood= buyer.BuyFood();
                buyFoodSum += currentFood;
            }

        }
        Console.WriteLine(buyFoodSum);
    }
}