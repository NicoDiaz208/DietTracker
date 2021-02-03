using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace DietTracker_Server.Classes.NutritionFacts
{
    class DailyProgressService
    {
        DailyProgressRepository nfr = new DailyProgressRepository();

        public string AddAchievements(NutritionFacts nutritionFacts)
        {
            var newnf = ConvertToBson(nutritionFacts);
            return nfr.AddNF(newnf);
        }

        public string DeleteAchievements(NutritionFacts nutritionFacts)
        {
            var newnf = ConvertToBson(nutritionFacts);
            return nfr.DeleteNF(newnf);
        }

        private BsonDocument ConvertToBson(NutritionFacts nutritionFacts)
        {
            var document = new BsonDocument
            {
                { "Calories", nutritionFacts.Calories },
                { "Protein", nutritionFacts.Protein },
                { "TotalCarbohydrates", nutritionFacts.TotalCarbohydrates },
                { "Sugar", nutritionFacts.Sugar },
                { "Fiber", nutritionFacts.Fiber },
                { "Fat", nutritionFacts.Fat }
            };
            return document;
        }
    }
}
