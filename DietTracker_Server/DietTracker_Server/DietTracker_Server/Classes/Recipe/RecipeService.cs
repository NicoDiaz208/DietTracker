using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace DietTracker_Server.Classes.Recipe
{
    class DailyProgressService
    {
        DailyProgressRepository rr = new DailyProgressRepository();

        public string AddAchievements(Recipe recipe)
        {
            var newrecipe = ConvertToBson(recipe);
            return rr.AddRecipe(newrecipe);
        }

        public string DeleteAchievements(Recipe recipe)
        {
            var newrecipe = ConvertToBson(recipe);
            return rr.DeleteRecipe(newrecipe);
        }

        private BsonDocument ConvertToBson(Recipe recipe)
        {
            var document = new BsonDocument
            {
                { "Name", recipe.Name },
                { "PrepareTime", recipe.PrepareTime },
                { "Difficulty", recipe.Difficulty },
                { "Kategorie", recipe.Kategorie }
            };
            return document;
        }
    }
}
