using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace DietTracker_Server.Classes.Food
{
    class DailyProgressService
    {
        DailyProgressRepository fr = new DailyProgressRepository();

        public string AddFood(Food food)
        {
            var newfood = ConvertToBson(food);
            return fr.AddFood(newfood);
        }

        public string DeleteFood(Food food)
        {
            var newfood = ConvertToBson(food);
            return fr.DeleteFood(newfood);
        }

        public string ReplaceFood(Food oldFood, Food newFood)
        {
            var toReplace = ConvertToBson(oldFood);
            var replacement = ConvertToBson(newFood);
            return fr.ReplaceFood(toReplace, replacement);
        }


        private BsonDocument ConvertToBson(Food food)
        {
            var document = new BsonDocument
            {
                { "name", food.Name }
            };
            return document;
        }
    }
}
