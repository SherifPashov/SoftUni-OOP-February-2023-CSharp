using _04.BorderControl.Interface;
using _04.BorderControl.Model;
using System.Globalization;

List<IIdentifiable> society = new();

string comand;

while((comand =Console.ReadLine()) != "End")
{
    string[] info = comand
        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
        .ToArray();

    if (info.Length==3)
    {
        string name = info[0];
        int age = int.Parse(info[1]);
        string id = info[2];
        society.Add(new Citizen(name, age, id));
    }
    else if (info.Length == 2)
    {
        string model = info[0];
        string id = info[1];
        society.Add(new Robot(model, id));
    }
}

string lastNumberFakeID = Console.ReadLine();

foreach (var item in society)
{
    if (item.Id.EndsWith(lastNumberFakeID))
    {
        Console.WriteLine(item.Id);
    }
}
