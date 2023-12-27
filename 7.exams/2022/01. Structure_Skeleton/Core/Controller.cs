//using ChristmasPastryShop.Core.Contracts;
//using ChristmasPastryShop.Models.Booths;
//using ChristmasPastryShop.Models.Booths.Contracts;
//using ChristmasPastryShop.Models.Cocktails;
//using ChristmasPastryShop.Models.Delicacies;
//using ChristmasPastryShop.Repositories;
//using ChristmasPastryShop.Utilities.Messages;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace ChristmasPastryShop.Core
//{
//    public class Controller : IController
//    {
//        private DelicacyRepository delicacyRepository;
//        private CocktailRepository cocktailRepository;
//        private BoothRepository boothRepository;
//        public Controller()
//        {
//            delicacyRepository= new DelicacyRepository();
//            cocktailRepository= new CocktailRepository();
//            boothRepository= new BoothRepository();
//        }
//        public string AddBooth(int capacity)
//        {
//            boothRepository.AddModel(new Booth(boothRepository.Models.Count+1,capacity));
//            return string.Format(OutputMessages.NewBoothAdded, boothRepository.Models.Count+1, capacity);
//        }

//        public string AddDelicacy(int boothId, string delicacyTypeName, string delicacyName)
//        {
//            switch (delicacyTypeName)
//            {
//                case "Gingerbread":
//                    Gingerbread gingerbread = new Gingerbread(delicacyName);
//                    if (delicacyRepository.Models.FirstOrDefault(s=>s.Name ==delicacyName) != null)
//                    {
//                        return string.Format(OutputMessages.DelicacyAlreadyAdded, delicacyName);

//                    }
//                    else
//                    {
//                        delicacyRepository.AddModel(gingerbread);
//                    }
//                    break;
//                case "Stolen":
//                    Stolen stolen = new Stolen(delicacyName);
//                    if (delicacyRepository.Models.FirstOrDefault(s => s.Name == delicacyName) != null)
//                    {
//                        return string.Format(OutputMessages.DelicacyAlreadyAdded, delicacyName);

//                    }
//                    else
//                    {
//                        delicacyRepository.AddModel(stolen);

//                    }
//                    break;

//                default:
//                    return string.Format(OutputMessages.InvalidDelicacyType, delicacyTypeName);
//                    break;
//            }
//            return string.Format(OutputMessages.NewDelicacyAdded,delicacyTypeName,delicacyName);

//        }

//        public string AddCocktail(int boothId, string cocktailTypeName, string cocktailName, string size)
//        {
//            if (cocktailTypeName != "Hibernation"&& cocktailTypeName != "MulledWine")
//            {
//                return string.Format(OutputMessages.InvalidCocktailType, cocktailTypeName);
//            }

//            if (size != "Small" && size != "Middle" && size != "Large")
//            {
//                return string.Format(OutputMessages.InvalidCocktailSize, size);
//            }

//            if (cocktailRepository.Models.FirstOrDefault(c=>c.Name == cocktailName && c.Size==size)!=null)
//            {
//                return string.Format(OutputMessages.CocktailAlreadyAdded, size, cocktailName);
//            }

//            if (cocktailName == "Hibernation")
//            {
//                Hibernation hibernation = new Hibernation(cocktailName, size);
//                cocktailRepository.AddModel(hibernation);

//            }
//            else if (cocktailName == "MulledWine")
//            {
//                MulledWine mulledWine = new MulledWine(cocktailName, size);
//                cocktailRepository.AddModel(mulledWine);

//            }
//            return String.Format(OutputMessages.NewCocktailAdded, size, cocktailName, cocktailTypeName);
//        }

//        public string ReserveBooth(int countOfPeople)
//        {
//            IBooth booth = boothRepository
//                 .Models
//                 .Where(b => b.IsReserved == false)
//                 .Where(b=> b.Capacity >= countOfPeople)
//                 .OrderBy(b => b.Capacity)
//                 .ThenByDescending(b => b.BoothId)
//                 .FirstOrDefault();
//            if (booth == null)
//            {
//                return string.Format(OutputMessages.NoAvailableBooth, countOfPeople);
//            }
//            boothRepository.Models.First(c => c == booth).ChangeStatus();

//            return string.Format(OutputMessages.BoothReservedSuccessfully, booth.BoothId, countOfPeople);    

//        }
//        public string TryOrder(int boothId, string order)
//        {
//            string[] ordersinfo = order.Split('/').ToArray();

//            string itemTypeName = ordersinfo[0];
//            string itemName = ordersinfo[1];
//            int countOfTheOrdered = int.Parse(ordersinfo[2]);
//            string size;
//            double price = 0;

//            switch (itemTypeName)
//            {
//                case "Gingerbread":
//                    Gingerbread gingerbread = new Gingerbread(itemName);
//                    if (delicacyRepository.Models.FirstOrDefault(d=>d.Name==itemName)!=null)
//                    {
//                        return string.Format(OutputMessages.NotRecognizedItemName, itemTypeName, itemName);
//                    }
//                    break;
//                case "Stolen":
//                    Stolen stolen = new Stolen(itemName);
//                    if (delicacyRepository.Models.FirstOrDefault(d => d.Name == itemName) != null)
//                    {
//                        return string.Format(OutputMessages.NotRecognizedItemName, itemTypeName, itemName);
//                    }

//                    break;
//                case "MulledWine":

//                    size = ordersinfo[3];
//                    MulledWine mulledWine = new MulledWine(itemName, size);

//                    if (cocktailRepository.Models.FirstOrDefault(d => d.Name == itemName) != null)
//                    {
//                        return string.Format(OutputMessages.NotRecognizedItemName, itemTypeName, itemName);
//                    }
//                    else if (cocktailRepository.Models.FirstOrDefault(d => d.Name == itemName && d.Size==size)==null) 

//                    {
//                        return string.Format(OutputMessages.NotRecognizedItemName, size, itemName);
//                    }
//                    break;
//                case "Hibernation":
//                    size = ordersinfo[3];
//                    Hibernation hibernation = new Hibernation(itemName,size);
//                    if (cocktailRepository.Models.FirstOrDefault(d => d.Name == itemName) != null)
//                    {
//                        return string.Format(OutputMessages.NotRecognizedItemName, itemTypeName, itemName);
//                    }
//                    else if (cocktailRepository.Models.FirstOrDefault(d => d.Name == itemName && d.Size == size) == null)

//                    {
//                        return string.Format(OutputMessages.NotRecognizedItemName, size, itemName);
//                    }

//                    break;
//                default:
//                    return string.Format(OutputMessages.NotRecognizedType, itemTypeName);
//                    break;
//            }
//            return "";

//        }


//        public string BoothReport(int boothId)
//        {
//            throw new NotImplementedException();
//        }

//        public string LeaveBooth(int boothId)
//        {
//            throw new NotImplementedException();
//        }




//    }
//}
using ChristmasPastryShop.Core.Contracts;
using ChristmasPastryShop.Models.Booths.Contracts;

using ChristmasPastryShop.Models.Delicacies.Contracts;

using ChristmasPastryShop.Repositories.Contracts;

using ChristmasPastryShop.Utilities.Messages;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using ChristmasPastryShop.Models.Cocktails.Contracts;
using System.Drawing;
using ChristmasPastryShop.Models.Booths;
using ChristmasPastryShop.Models.Cocktails;
using ChristmasPastryShop.Models.Delicacies;
using ChristmasPastryShop.Repositories;

namespace ChristmasPastryShop.Core
{
    public class Controller : IController
    {
        private IRepository<IBooth> booths;

        public Controller()
        {
            this.booths = new BoothRepository();
        }
        public string AddBooth(int capacity)
        {
            int boothId = this.booths.Models.Count + 1;

            Booth booth = new Booth(boothId, capacity);
            this.booths.AddModel(booth);

            return string
                .Format(OutputMessages.NewBoothAdded, boothId, capacity);
        }

        public string AddCocktail(int boothId, string cocktailTypeName, string cocktailName, string size)
        {
            if (cocktailTypeName != nameof(MulledWine) && cocktailTypeName != nameof(Hibernation))
            {
                return string
                    .Format(OutputMessages.InvalidCocktailType, cocktailTypeName);
            }

            if (size != "Small" && size != "Middle" && size != "Large")
            {
                return string.Format(OutputMessages.InvalidCocktailSize, size);
            }

            if (this.booths.Models.Any(b => b.CocktailMenu.Models.Any(cm => cm.Name == cocktailName && cm.Size == size)))
            {
                return string
                    .Format(OutputMessages.CocktailAlreadyAdded, size, cocktailName);
            }

            ICocktail cocktail;
            if (cocktailTypeName == nameof(MulledWine))
            {
                cocktail = new MulledWine(cocktailName, size);
            }
            else
            {
                cocktail = new Hibernation(cocktailName, size);
            }

            IBooth booth = this.booths.Models.FirstOrDefault(b => b.BoothId == boothId);
            booth.CocktailMenu.AddModel(cocktail);

            return string.Format(OutputMessages.NewCocktailAdded, size, cocktailName, cocktailTypeName);
        }

        public string AddDelicacy(int boothId, string delicacyTypeName, string delicacyName)
        {
            if (delicacyTypeName != nameof(Gingerbread) && delicacyTypeName != nameof(Stolen))
            {
                return string
                    .Format(ExceptionMessages.InvalidDelicacyType, delicacyTypeName);
            }

            if (this.booths.Models.Any(b => b.DelicacyMenu.Models.Any(dm => dm.Name == delicacyName)))
            {
                return string
                    .Format(OutputMessages.DelicacyAlreadyAdded, delicacyName);
            }

            IDelicacy delicacy;
            if (delicacyTypeName == nameof(Gingerbread))
            {
                delicacy = new Gingerbread(delicacyName);
            }
            else
            {
                delicacy = new Stolen(delicacyName);
            }

            IBooth booth = this.booths.Models.FirstOrDefault(b => b.BoothId == boothId);
            booth.DelicacyMenu.AddModel(delicacy);

            return string.Format(OutputMessages.NewDelicacyAdded, delicacyTypeName, delicacyName);
        }

        public string BoothReport(int boothId)
        {
            return this.booths.Models.FirstOrDefault(b => b.BoothId == boothId).ToString().TrimEnd();
        }

        public string LeaveBooth(int boothId)
        {
            IBooth booth = this.booths.Models.FirstOrDefault(b => b.BoothId == boothId);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Bill {booth.CurrentBill:f2} lv");

            booth.Charge();
            booth.ChangeStatus();

            sb.AppendLine($"Booth {boothId} is now available!");

            return sb.ToString().TrimEnd();
        }

        public string ReserveBooth(int countOfPeople)
        {
            var booth = this.booths.Models
                .Where(b => b.IsReserved == false && b.Capacity >= countOfPeople)
                .OrderBy(b => b.Capacity)
                .ThenByDescending(b => b.BoothId)
                .FirstOrDefault();

            if (booth == null)
            {
                return string.Format(OutputMessages.NoAvailableBooth, countOfPeople);
            }

            booth.ChangeStatus();
            return string.Format(OutputMessages.BoothReservedSuccessfully, booth.BoothId, countOfPeople);
        }

        public string TryOrder(int boothId, string order)
        {
            IBooth booth = this.booths.Models.FirstOrDefault(b => b.BoothId == boothId);

            string[] orderArray = order.Split('/');

            bool isCocktail = false;

            string itemTypeName = orderArray[0];

            if (itemTypeName != nameof(MulledWine) &&
                itemTypeName != nameof(Hibernation) &&
                itemTypeName != nameof(Gingerbread) &&
                itemTypeName != nameof(Stolen))
            {
                return string.Format(OutputMessages.NotRecognizedType, itemTypeName);
            }

            string itemName = orderArray[1];

            if (!booth.CocktailMenu.Models.Any(m => m.Name == itemName) &&
                !booth.DelicacyMenu.Models.Any(m => m.Name == itemName))
            {
                return string.Format(OutputMessages.NotRecognizedItemName, itemTypeName, itemName);
            }

            int pieces = int.Parse(orderArray[2]);



            if (itemTypeName == nameof(MulledWine) || itemTypeName == nameof(Hibernation))
            {
                isCocktail = true;
            }

            if (isCocktail)
            {
                string size = orderArray[3];

                ICocktail desiredCocktail = booth
                    .CocktailMenu.Models
                    .FirstOrDefault(m => m.GetType().Name == itemTypeName && m.Name == itemName && m.Size == size);

                if (desiredCocktail == null)
                {
                    return string.Format(OutputMessages.CocktailStillNotAdded, size, itemName);
                }

                booth.UpdateCurrentBill(desiredCocktail.Price * pieces);
                return string.Format(OutputMessages.SuccessfullyOrdered, boothId, pieces, itemName);
            }
            else
            {
                IDelicacy desiredDelicacy = booth
                .DelicacyMenu.Models
                    .FirstOrDefault(m => m.GetType().Name == itemTypeName && m.Name == itemName);

                if (desiredDelicacy == null)
                {
                    return string.Format(OutputMessages.DelicacyStillNotAdded, itemName);
                }

                booth.UpdateCurrentBill(desiredDelicacy.Price * pieces);
                return string.Format(OutputMessages.SuccessfullyOrdered, boothId, pieces, itemName);
            }
        }
    }
}

