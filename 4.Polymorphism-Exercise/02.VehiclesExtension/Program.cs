



using _02.VehiclesExtension;

string[] carInfo = Console.ReadLine()
    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    .ToArray();

double fuelQuantityCar = double.Parse(carInfo[1]);
double literPerKmCar = double.Parse(carInfo[2]);
double tankCapacityCar = double.Parse(carInfo[3]);

Car car = new Car(fuelQuantityCar, literPerKmCar, tankCapacityCar);


string[] truckInfo = Console.ReadLine()
    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    .ToArray();
double fuelQuantityTruck = double.Parse(truckInfo[1]);
double literPerKmTruck = double.Parse(truckInfo[2]);
double tankCapacityTruck = double.Parse(truckInfo[3]);

Truck truck = new Truck(fuelQuantityTruck, literPerKmTruck, tankCapacityTruck);

string[] busInfo = Console.ReadLine()
    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    .ToArray();
double fuelQuantityBus = double.Parse(busInfo[1]);
double literPerKmBus = double.Parse(busInfo[2]);
double tankCapacityBus = double.Parse(busInfo[3]);

Bus bus = new Bus(fuelQuantityBus, literPerKmBus, tankCapacityBus);

int numberComand = int.Parse(Console.ReadLine());
for (int i = 0; i < numberComand; i++)
{
    string[] comandInfo = Console.ReadLine()
        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
        .ToArray();
    string comand = comandInfo[0];
    string vehicleType = comandInfo[1];

    if (comand == "Drive")
    {
        double distance = double.Parse(comandInfo[2]);
        if (vehicleType == "Car")
        {
            Console.WriteLine(car.Driving(distance));
        }
        else if (vehicleType == "Truck")
        {
            Console.WriteLine(truck.Driving(distance));
        }
        else if (vehicleType == "Bus")
        {
            Console.WriteLine(bus.Driving(distance));
        }
    }
    else if (comand == "Refuel")
    {
        double liter = double.Parse(comandInfo[2]);
        if (vehicleType == "Car")
        {
            car.Refueling(liter);
        }
        else if (vehicleType == "Truck")
        {
            truck.Refueling(liter);
        }
        else if (vehicleType == "Bus")
        {
            bus.Refueling(liter);
        }
    }
    else if (comand =="DriveEmpty")
    {
        double distance = double.Parse(comandInfo[2]);
        Console.WriteLine( bus.DriveEmpty(distance));
    }
    

    

}

Console.WriteLine(car);
Console.WriteLine(truck);
Console.WriteLine(bus);

