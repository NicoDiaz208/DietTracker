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
        public String AddFood(BsonDocument food)
        {
            var database = db.GetDatabase("TestDietTracker");
            var collection = database.GetCollection<BsonDocument>("Food");
            if (collection.Find(food) != null)
            {
                return "Exestiert bereits";
            }
            collection.InsertOne(food);

            return "Insert OK";
        }

        public String DeleteFood(BsonDocument food)
        {
            var database = db.GetDatabase("TestDietTracker");
            var collection = database.GetCollection<BsonDocument>("Food");
            if (collection.Find(food) == null)
            {
                return "Exestiert nicht";
            }
            collection.DeleteOne(food);
            return "Delete OK";
        }

        public String ReplaceFood(BsonDocument oldFood, BsonDocument newFood)
        {
            var database = db.GetDatabase("TestDietTracker");
            var collection = database.GetCollection<BsonDocument>("Food");
            if (collection.Find(oldFood) == null)
            {
                return "Exestiert nicht";
            }
            collection.ReplaceOne(oldFood, newFood);
            return "Altes Nahrungsmittel wurde geändert";
        }

    }
}
