using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace DietTracker_Server.Classes.Food
{
    class DailyProgressService
    {
        DailyProgressRepository fr = new DailyProgressRepository();

        public string AddAchievements(Food food)
        {
            var newfood = ConvertToBson(food);
            return fr.AddFood(newfood);
        }

        public string DeleteAchievements(Food food)
        {
            var newfood = ConvertToBson(food);
            return fr.DeleteFood(newfood);
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
