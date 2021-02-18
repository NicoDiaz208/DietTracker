using System;
using System.Collections.Generic;
using MongoDB.Driver;
using System.Text;
using MongoDB.Bson;

namespace DietTracker_Server.Classes.DailyProgress
{
    class DailyProgressRepository
    {
        MongoClient db = new MongoClient("mongodb://localhost:27017");


        public String AddDailyProgress(BsonDocument dailyP)
        {
            var database = db.GetDatabase("TestDietTracker");
            var collection = database.GetCollection<BsonDocument>("DailyProgress");
            if (collection.Find(dailyP) != null)
            {
                return "Exestiert bereits";
            }
            collection.InsertOne(dailyP);

            return "Insert OK";
        }

        public String DeleteDailyProgress(BsonDocument dailyP)
        {
            var database = db.GetDatabase("TestDietTracker");
            var collection = database.GetCollection<BsonDocument>("DailyProgress");
            if (collection.Find(dailyP) == null)
            {
                return "Exestiert nicht";
            }
            collection.DeleteOne(dailyP);
            return "Delete OK";
        }

        public String ReplaceDailyProgress(BsonDocument oldDP,BsonDocument newDP)
        {
            var database = db.GetDatabase("TestDietTracker");
            var collection = database.GetCollection<BsonDocument>("DailyProgress");
            if (collection.Find(oldDP) == null)
            {
                return "Exestiert nicht";
            }
            collection.ReplaceOne(oldDP, newDP);
            return "Replace OK";
        }
    }
}
