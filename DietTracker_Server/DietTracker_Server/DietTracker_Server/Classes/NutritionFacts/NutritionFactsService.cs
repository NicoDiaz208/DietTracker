using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace DietTracker_Server.Classes.NutritionFacts
{
    class DailyProgressService
    {
        DailyProgressRepository nfr = new DailyProgressRepository();

        public string AddNF(NutritionFacts nutritionFacts)
        {
            var newnf = ConvertToBson(nutritionFacts);
            return nfr.AddNF(newnf);
        }

        public string DeleteNFs(NutritionFacts nutritionFacts)
        {
            var newnf = ConvertToBson(nutritionFacts);
            return nfr.DeleteNF(newnf);
        }

        public String ReplaceNF(NutritionFacts oldNF,NutritionFacts newNF)
        {
            var toReplace = ConvertToBson(oldNF);
            var replacement = ConvertToBson(newNF);
            return nfr.ReplaceNF(toReplace, replacement);
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
