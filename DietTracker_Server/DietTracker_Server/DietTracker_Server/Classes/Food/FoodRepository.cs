using System;
using System.Collections.Generic;
using MongoDB.Driver;
using System.Text;
using MongoDB.Bson;

namespace DietTracker_Server.Classes.Food
{
    class FoodRepository
    {
        MongoClient db;

        public FoodRepository(string connectionString)
        {
            db = new MongoClient(connectionString);
        }
        public String AddFood(Food food,string Database)
        {
            var database = db.GetDatabase(Database);
            var collection = database.GetCollection<Food>("Food");
            if (collection.Find(food.ToBsonDocument()) != null)
            {
                return "Exestiert bereits";
            }
            collection.InsertOne(food);

            return "Insert OK";
        }

        public String DeleteFood(Food food, string Database)
        {
            var database = db.GetDatabase(Database);
            var collection = database.GetCollection<Food>("Food");
            if (collection.Find(food.ToBsonDocument()) == null)
            {
                return "Exestiert nicht";
            }
            collection.DeleteOne(food.ToBsonDocument());
            return "Delete OK";
        }

        public String ReplaceFood(Food oldFood, Food newFood, string Database)
        {
            var database = db.GetDatabase(Database);
            var collection = database.GetCollection<Food>("Food");
            if (collection.Find(oldFood.ToBsonDocument()) == null)
            {
                return "Exestiert nicht";
            }
            collection.ReplaceOne(oldFood.ToBsonDocument(), newFood);
            return "Altes Nahrungsmittel wurde geändert";
        }

    }
}
