using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.PizzaCalories
{
    public class Dough
    {
		private const double BaseCaloriesPerGram = 2;

		private string flourType;
        private string bakingTechnique;
        private double weight;

		private  Dictionary<string, double> flourTypeModifiers;
		private Dictionary<string, double> bakinTechniqueModifiers;

        public Dough(string flourType, string bakingTechnique, double weight)
        {
            flourTypeModifiers =
            new Dictionary<string, double> { { "white", 1.5 }, { "wholegrain", 1.0 } };

            bakinTechniqueModifiers =
                new Dictionary<string, double> { { "crispy", 0.9 }, { "chewy", 1.1 }, { "homemade", 1.0 } };

            FlourType = flourType;
            BakingTechnique = bakingTechnique;
            Weight = weight;
        }
        public double Weight
        {
            get
            {
                return weight;
            }
            set
            {
                if (value < 0 || value > 200)
                {
                    throw new ArgumentException("Dough weight should be in the range [1..200].");
                }
                weight = value;
            }
        }

        public string FlourType
        {
			get
            {
                return flourType; 
            }
			set {
                if (!flourTypeModifiers.ContainsKey(value.ToLower()))
                {
                    throw new ArgumentException("Invalid type of dough.");
                }
                flourType = value.ToLower(); 
            }
		}

		public string BakingTechnique
        {
			get
            {
                return bakingTechnique;
            }
			set
            {
                if (!bakinTechniqueModifiers.ContainsKey(value.ToLower()))
                {
                    throw new ArgumentException("Invalid type of dough.");
                }
                bakingTechnique = value.ToLower();
            }
		}

		

        public double Calories
        {
            get
            {
                double flourTypeModifierInfo = bakinTechniqueModifiers[BakingTechnique];
                double techniqueModifierInfo = flourTypeModifiers[FlourType];

                return BaseCaloriesPerGram * weight * flourTypeModifierInfo * techniqueModifierInfo;
            }
        }

    }
}
