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


        public String AddDailyProgress(DailyProgress dailyP,string Database)
        {
            var database = db.GetDatabase(Database);
            var collection = database.GetCollection<DailyProgress>("DailyProgress");
            if (collection.Find(dailyP.ToBsonDocument()) != null)
            {
                return "Exestiert bereits";
            }
            collection.InsertOne(dailyP);

            return "Insert OK";
        }

        public String DeleteDailyProgress(DailyProgress dailyP, string Database)
        {
            var database = db.GetDatabase(Database);
            var collection = database.GetCollection<DailyProgress>("DailyProgress");
            if (collection.Find(dailyP.ToBsonDocument()) == null)
            {
                return "Exestiert nicht";
            }
            collection.DeleteOne(dailyP.ToBsonDocument());
            return "Delete OK";
        }

        public String ReplaceDailyProgress(DailyProgress oldDP, DailyProgress newDP, string Database)
        {
            var database = db.GetDatabase(Database);
            var collection = database.GetCollection<DailyProgress>("DailyProgress");
            if (collection.Find(oldDP.ToBsonDocument()) == null)
            {
                return "Exestiert nicht";
            }
            collection.ReplaceOne(oldDP.ToBsonDocument(), newDP.ToBsonDocument());
            return "Replace OK";
        }
    }
}
