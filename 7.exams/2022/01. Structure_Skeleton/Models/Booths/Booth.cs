using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Repositories;
using ChristmasPastryShop.Repositories.Contracts;
using ChristmasPastryShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChristmasPastryShop.Models.Booths
{
    public class Booth : IBooth
    {
        private int boothId;
        private int capacity;
        private double currentBill;
        private double turnover;
        bool isReserved;


        private readonly IRepository<IDelicacy> delicacies;
        private readonly IRepository<ICocktail> cocktails;
        public int BoothId { get;private set; }

        public int Capacity { get => capacity;
            private set
            {
                if (value <=0)
                {
                    throw new ArgumentException(ExceptionMessages.CapacityLessThanOne);
                }
                capacity = value;
            }
        }

        public IRepository<IDelicacy> DelicacyMenu => this.delicacies;

        public IRepository<ICocktail> CocktailMenu => this.cocktails;

        public double CurrentBill => currentBill;

        public double Turnover=> turnover;
            
        

        public bool IsReserved { get => isReserved;
            private set
            {
                isReserved = value;
            }
        }
        public Booth(int boothId, int capacity)
        {
            BoothId= boothId;
            Capacity= capacity;
            this.delicacies = new DelicacyRepository();
            this.cocktails = new CocktailRepository();
            isReserved = false;
        }
        

        public void ChangeStatus()
        {
            if (isReserved)
            {
                isReserved = false;
            }
            else
            {
                isReserved = true;
            }
        }

        public void Charge()
        {
            turnover += currentBill;
            currentBill = 0;
           
        }

        public void UpdateCurrentBill(double amount)
        {
            currentBill += amount;
        }
        public override string ToString()
        {
            StringBuilder se = new StringBuilder();

            se.AppendLine($"Booth: {boothId}");
            se.AppendLine($"Capacity: {capacity}");
            se.AppendLine($"Turnover: {turnover:f2} lv");
            se.AppendLine($"-Cocktail menu:");

            foreach ( var cocktail in CocktailMenu.Models)
            {
                se.AppendLine(cocktail.ToString());
            }

            foreach (var delicacy in DelicacyMenu.Models)
            {
                se.AppendLine(delicacy.ToString());
            }
            return se.ToString().TrimEnd();
        }
    }
}
