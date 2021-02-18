﻿using System;
using System.Collections.Generic;
using MongoDB.Driver;
using System.Text;
using MongoDB.Bson;

namespace DietTracker_Server.Classes.Recipe
{
    class DailyProgressRepository
    {
        MongoClient db = new MongoClient("mongodb://localhost:27017");


        public String AddRecipe(BsonDocument recipe)
        {
            var database = db.GetDatabase("TestDietTracker");
            var collection = database.GetCollection<BsonDocument>("Recipe");
            if (collection.Find(recipe) != null)
            {
                return "Exestiert bereits";
            }
            collection.InsertOne(recipe);

            return "Insert OK";
        }

        public String DeleteRecipe(BsonDocument recipe)
        {
            var database = db.GetDatabase("TestDietTracker");
            var collection = database.GetCollection<BsonDocument>("Recipe");
            if (collection.Find(recipe) == null)
            {
                return "Exestiert nicht";
            }
            collection.DeleteOne(recipe);
            return "Delete OK";
        }

        public String ReplaceRecipe(BsonDocument oldRecipe, BsonDocument newRecipe)
        {
            var database = db.GetDatabase("TestDietTracker");
            var collection = database.GetCollection<BsonDocument>("Recipe");
            if (collection.Find(oldRecipe) == null)
            {
                return "Exestiert nicht";
            }
            collection.ReplaceOne(oldRecipe, newRecipe);
            return "Delete OK";
        }
    }
}
