using System;
using System.Collections.Generic;
using System.Text;

namespace DietTracker_Server.Classes.NutritionFacts
{
    class NutritionFacts
    {
        public double Calories { get; set; }
        public double Protein { get; set; }
        public double TotalCarbohydrates { get; set; }
        public double Sugar { get; set; }
        public double Fiber { get; set; }
        public double Fat { get; set; }

        public NutritionFacts(double calories, double protein, double totalCarbohydrates, double sugar, double fiber, double fat)
        {
            Calories = calories;
            Protein = protein;
            TotalCarbohydrates = totalCarbohydrates;
            Sugar = sugar;
            Fiber = fiber;
            Fat = fat;
        }
    }
}
