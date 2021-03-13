using System;
using System.Collections.Generic;
using System.Text;

namespace DietTracker_Server.Classes.NutritionFacts
{
    public record NutritionFacts(double Calories, double Protein, double TotalCarbohydrates, double Sugar, double Fiber, double Fat);
    
}
