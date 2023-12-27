

using _03.ShoppingSpree;
using ShoppingSpree.Models;

List<Person> peoples= new List<Person>();
List<Product> products= new List<Product>();


try
{
    string[] allPeople = Console.ReadLine()
        .Split(";",StringSplitOptions.RemoveEmptyEntries)
        .ToArray();

    for (int i = 0; i < allPeople.Length; i++)
    {
        string[] infoPerson = allPeople[i]
            .Split("=",StringSplitOptions.RemoveEmptyEntries)
            .ToArray();

        string name = infoPerson[0];
        decimal money = decimal.Parse(infoPerson[1]);

        
        peoples.Add(new Person(name, money));
    }


    string[] allProduct = Console.ReadLine()
        .Split(";",StringSplitOptions.RemoveEmptyEntries)
        .ToArray();


    for (int i = 0; i < allProduct.Length; i++)
    {
        string[] infoProduct = allProduct[i].Split("=").ToArray();

        string name = infoProduct[0];
        decimal cost = decimal.Parse(infoProduct[1]);

        products.Add(new Product(name, cost));
    }
}
catch (ArgumentException ex)
{

    Console.WriteLine(ex.Message);
    return;
}



string comand;

while ((comand = Console.ReadLine()) != "END")
{
    string[] info = comand
        .Split(" ",StringSplitOptions.RemoveEmptyEntries)
        .ToArray();

    string namePerson = info[0];
    string buyProduct = info[1];

    Person person = peoples.FirstOrDefault(p => p.Name == namePerson);
    Product product = products.FirstOrDefault(p => p.Name == buyProduct);

    if (person is not null && product is not null)
    {
        Console.WriteLine(person.Add(product));
    }
}
Console.WriteLine(string.Join(Environment.NewLine, peoples));
