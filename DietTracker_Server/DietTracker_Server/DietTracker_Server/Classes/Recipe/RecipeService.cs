using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace DietTracker_Server.Classes.Recipe
{
    class DailyProgressService
    {
        DailyProgressRepository rr = new DailyProgressRepository();

        public String AddRecipe(Recipe recipe)
        {
            var newrecipe = ConvertToBson(recipe);
            return rr.AddRecipe(newrecipe);
        }

        public String DeleteRecipe(Recipe recipe)
        {
            var newrecipe = ConvertToBson(recipe);
            return rr.DeleteRecipe(newrecipe);
        }

        public String ReplaceRecipe(Recipe oldRecipe,Recipe newRecipe)
        {
            var toReplace = ConvertToBson(oldRecipe);
            var replacement = ConvertToBson(newRecipe);
            return rr.ReplaceRecipe(toReplace, replacement);
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
