

using _01.Vehicles;
using System.Net.Http.Headers;

string[] carInfo = Console.ReadLine()
    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    .ToArray();

double fuelQuantityCar = double.Parse(carInfo[1]);
double literPerKmCar = double.Parse(carInfo[2]);

Car car = new Car(fuelQuantityCar,literPerKmCar);


string[] truckInfo = Console.ReadLine()
    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    .ToArray();
double fuelQuantityTruck = double.Parse(truckInfo[1]);
double literPerKmTruck = double.Parse(truckInfo[2]);

Truck truck = new Truck(fuelQuantityTruck, literPerKmTruck);

int numberComand = int.Parse(Console.ReadLine());
for (int i = 0; i < numberComand; i++)
{
    string[] comandInfo = Console.ReadLine()
        .Split(" ",StringSplitOptions.RemoveEmptyEntries)
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
    }
    else if (comand=="Refuel")
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
    }
}

Console.WriteLine(car);
Console.WriteLine(truck);

