

using _05.BirthdayCelebrations.Interface;
using _05.BirthdayCelebrations.Model;
using System.Globalization;

List<IIdentifiable> society = new();
List<IBirthdate> societyBirthdate =new List<IBirthdate>();


string comand;

while((comand=Console.ReadLine())!="End")
{
    string[] info = comand
        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
        .ToArray();

    string type = info[0];

    if (type == "Citizen")
    {
        string name = info[1];
        int age = int.Parse(info[2]);
        string id = info[3];
        string birthdate = info[4];

        society.Add(new Citizen(name, age, id, birthdate));
        societyBirthdate.Add(new Citizen(name,age, id, birthdate));
    }
    else if (type == "Pet")
    {
        string name = info[1];
        string birthdate = info[2];
        societyBirthdate.Add(new Pet(name, birthdate));
    }
    else if (type == "Robot")
    {
        string model = info[1];
        string id = info[2];
        society.Add(new Robot(model, id));
    }
}
string year = Console.ReadLine();

foreach (var item in societyBirthdate)
{
    if (item.Birthdate.EndsWith(year))
    {
        Console.WriteLine(item.Birthdate);
    }
}