
using _03.Telephony;

StationaryPhone stationaryPhone = new StationaryPhone();
Smartphone smartphone= new Smartphone();

string[] numbersPhone = Console.ReadLine()
    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    .ToArray();

string[] urlsSite = Console.ReadLine()
    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    .ToArray();


    foreach (var numberPhone in numbersPhone)
    {
        try
        {
            if (numberPhone.Length == 10)
            {
                Console.WriteLine(smartphone.Call(numberPhone));
            }
            else if (numberPhone.Length == 7)
            {
                Console.WriteLine(stationaryPhone.Call(numberPhone));
            }
        }
        catch (ArgumentException ex)
        {

            Console.WriteLine(ex.Message);
        }

    }

    foreach (var urlSite in urlsSite)
    {
        try
        {
            Console.WriteLine(smartphone.Browsing(urlSite));
        }
        catch (ArgumentException ex)
        {
    
            Console.WriteLine(ex.Message);
        }
    }



