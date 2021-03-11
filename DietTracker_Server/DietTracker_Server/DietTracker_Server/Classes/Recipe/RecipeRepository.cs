using System;
using System.Collections.Generic;
using MongoDB.Driver;
using System.Text;
using MongoDB.Bson;

namespace DietTracker_Server.Classes.Recipe
{
    class RecipeRepository
    {
        MongoClient db;

        public RecipeRepository(string connectionString)
        {
            db = new MongoClient(connectionString);
        }


        public String AddRecipe(Recipe recipe,string Database)
        {
            var database = db.GetDatabase(Database);
            var collection = database.GetCollection<Recipe>("Recipe");
            if (collection.Find(recipe.ToBsonDocument()) != null)
            {
                return "Exestiert bereits";
            }
            collection.InsertOne(recipe);

            return "Insert OK";
        }

        public String DeleteRecipe(Recipe recipe, string Database)
        {
            var database = db.GetDatabase(Database);
            var collection = database.GetCollection<Recipe>("Recipe");
            if (collection.Find(recipe.ToBsonDocument()) == null)
            {
                return "Exestiert nicht";
            }
            collection.DeleteOne(recipe.ToBsonDocument());
            return "Delete OK";
        }

        public String ReplaceRecipe(Recipe oldRecipe, Recipe newRecipe, string Database)
        {
            var database = db.GetDatabase(Database);
            var collection = database.GetCollection<Recipe>("Recipe");
            if (collection.Find(oldRecipe.ToBsonDocument()) == null)
            {
                return "Exestiert nicht";
            }
            collection.ReplaceOne(oldRecipe.ToBsonDocument(), newRecipe);
            return "Replace OK";
        }
    }
}
