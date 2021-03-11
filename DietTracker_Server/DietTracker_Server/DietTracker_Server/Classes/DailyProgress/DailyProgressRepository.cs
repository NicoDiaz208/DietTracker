using System;
using System.Collections.Generic;
using MongoDB.Driver;
using System.Text;
using MongoDB.Bson;

namespace DietTracker_Server.Classes.DailyProgress
{
    class DailyProgressRepository
    {
        MongoClient db;

        public DailyProgressRepository(string connectionString)
        {
            db = new MongoClient(connectionString);
        }


        public String AddDailyProgress(BsonDocument dailyP,string Database)
        {
            var database = db.GetDatabase(Database);
            var collection = database.GetCollection<BsonDocument>("DailyProgress");
            if (collection.Find(dailyP) != null)
            {
                return "Exestiert bereits";
            }
            collection.InsertOne(dailyP);

            return "Insert OK";
        }

        public String DeleteDailyProgress(BsonDocument dailyP, string Database)
        {
            var database = db.GetDatabase(Database);
            var collection = database.GetCollection<BsonDocument>("DailyProgress");
            if (collection.Find(dailyP) == null)
            {
                return "Exestiert nicht";
            }
            collection.DeleteOne(dailyP);
            return "Delete OK";
        }

        public String ReplaceDailyProgress(BsonDocument oldDP,BsonDocument newDP, string Database)
        {
            var database = db.GetDatabase(Database);
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
