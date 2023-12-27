using EDriveRent.Core.Contracts;
using EDriveRent.Models;
using EDriveRent.Models.Contracts;
using EDriveRent.Repositories;
using EDriveRent.Repositories.Contracts;
using EDriveRent.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Core
{
    public class Controller : IController
    {
        private UserRepository users;
        private VehicleRepository vehicles;
        private RouteRepository routes;
        public Controller()
        {
            users = new UserRepository();
            vehicles = new VehicleRepository();
            routes = new RouteRepository();
        }

        public string RegisterUser(string firstName, string lastName, string drivingLicenseNumber)
        {
            if (users.FindById(drivingLicenseNumber)!=null)
            {
                return string.Format(OutputMessages.UserWithSameLicenseAlreadyAdded,drivingLicenseNumber);
            }

            User user = new User(firstName, lastName, drivingLicenseNumber);
            users.AddModel(user);

            return string.Format(OutputMessages.UserSuccessfullyAdded, firstName, lastName, drivingLicenseNumber);
        }

        public string UploadVehicle(string vehicleType, string brand, string model, string licensePlateNumber)
        {
            if (vehicleType != typeof(PassengerCar).Name
                && vehicleType !=typeof(CargoVan).Name)
            {
                return string.Format(OutputMessages.VehicleTypeNotAccessible,vehicleType);
            }

            if (vehicles.FindById(licensePlateNumber)!=null)
            {
                return string.Format(OutputMessages.LicensePlateExists, licensePlateNumber);
            }
            IVehicle vehicle;
            if (typeof(CargoVan).Name == vehicleType)
            {
                vehicle = new CargoVan(brand, model, licensePlateNumber);
            }
            else 
            {
                vehicle = new PassengerCar(brand, model, licensePlateNumber);
            }
            vehicles.AddModel(vehicle);
            return string.Format(OutputMessages.VehicleAddedSuccessfully,brand,model,licensePlateNumber);
        }

        public string AllowRoute(string startPoint, string endPoint, double length)
        {

            if (routes.GetAll()
                .Where(c=>c.IsLocked==false)
                .FirstOrDefault(c=>c.StartPoint == startPoint &&
                c.EndPoint==endPoint &&
                c.Length == length)!=null)
            {
               return string.Format(OutputMessages.RouteExisting,startPoint,endPoint,length);
            }
            else if (routes.GetAll()
                 .Where(c => c.IsLocked == false)
                .FirstOrDefault(c=>c.StartPoint == startPoint &&
                c.EndPoint == endPoint&&
                c.Length<length)!=null)
            {
                return string.Format(OutputMessages.RouteIsTooLong, startPoint, endPoint);

            }
            else 
            {

                IRoute route = new Route(startPoint, endPoint, length, routes.GetAll().Count + 1);


                routes.AddModel(route);

                if (routes.GetAll()
                    .Where(c => c.IsLocked == false)
                    .FirstOrDefault(c => c.StartPoint == startPoint &&
                    c.EndPoint == endPoint &&
                    c.Length > length) != null)
                {
                   IRoute r= routes.GetAll()
                                     .Where(c => c.IsLocked == false)
                                    .FirstOrDefault(c => c.StartPoint == startPoint &&
                                    c.EndPoint == endPoint &&
                                    c.Length > length);
                    if (r!=null)
                    {
                        r.LockRoute() ;
                    }
                }
                return string.Format(OutputMessages.NewRouteAdded, startPoint, endPoint,length);

            }
            
        }

        public string MakeTrip(string drivingLicenseNumber, string licensePlateNumber, string routeId, bool isAccidentHappened)
        {
            IUser user = users.FindById(drivingLicenseNumber);
            IVehicle vehicle = vehicles.FindById(licensePlateNumber);
            IRoute route= routes.FindById(routeId);

            if (user.IsBlocked == true)
            {
                return string.Format(OutputMessages.UserBlocked, drivingLicenseNumber);
            }

            if (vehicle.IsDamaged == true)
            {
              
                return string.Format(OutputMessages.VehicleDamaged, licensePlateNumber);
            }

            if (route.IsLocked == true)
            {
                return string.Format(OutputMessages.RouteLocked, routeId);
            }

            vehicle.Drive(route.Length);
            if (isAccidentHappened)
            {
                vehicles.FindById(licensePlateNumber).ChangeStatus();
                user.DecreaseRating();
            }
            else
            {
                user.IncreaseRating();
                
            }
            return vehicle.ToString();
        }

        

        public string RepairVehicles(int count)
        {
            int countOfRepairedVehicles = 0;

            List<IVehicle> listVehicles= vehicles.GetAll()
                .Where(c=>c.IsDamaged==true)
                .OrderBy(b=>b.Brand)
                .ThenBy(v=>v.Model)
                .ToList();

            if (listVehicles.Count >= count)
            {
                for (int i = 0; i < count; i++)
                {
                    listVehicles[i].Recharge();
                    listVehicles[i].ChangeStatus();
                    countOfRepairedVehicles++;
                }
                return string.Format(OutputMessages.RepairedVehicles, count);

            }
            else if (listVehicles.Count<count)
            {
                for (int i = 0; i < listVehicles.Count; i++)
                {
                    listVehicles[i].Recharge();
                    listVehicles[i].ChangeStatus();
                    countOfRepairedVehicles++;
                }
            }
            return string.Format(OutputMessages.RepairedVehicles, listVehicles.Count);
        }

        

        public string UsersReport()
        {
            StringBuilder sb = new StringBuilder();

            List<IUser> listUsers = users.GetAll()
                .OrderByDescending(c => c.Rating)
                .ThenBy(c=>c.LastName)
                .ThenBy(c=>c.FirstName)
                .ToList();
            sb.AppendLine("*** E-Drive-Rent ***");
            foreach (var user in listUsers)
            {
                sb.AppendLine(user.ToString());
            }
            return sb.ToString().TrimEnd();
        }
    }
}
