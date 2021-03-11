using System;
using System.Collections.Generic;
using MongoDB.Driver;
using System.Text;
using MongoDB.Bson;

namespace DietTracker_Server.Classes.Sleep
{
    class SleepRepository
    {
        MongoClient db;

        public SleepRepository(string connectionString)
        {
            db = new MongoClient(connectionString);
        }


        public String AddSleep(BsonDocument user)
        {
            var database = db.GetDatabase("TestDietTracker");
            var collection = database.GetCollection<BsonDocument>("Sleep");
            if (collection.Find(user) != null)
            {
                return "Exestiert bereits";
            }
            collection.InsertOne(user);

            return "Insert OK";
        }

        public String DeleteSleep(BsonDocument user)
        {
            var database = db.GetDatabase("TestDietTracker");
            var collection = database.GetCollection<BsonDocument>("Sleep");
            if (collection.Find(user) == null)
            {
                return "Exestiert nicht";
            }
            collection.DeleteOne(user);
            return "Delete OK";
        }
    }
}
