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


        public String AddSleep(Sleep user,string Database)
        {
            var database = db.GetDatabase(Database);
            var collection = database.GetCollection<Sleep>("Sleep");
            if (collection.Find(user.ToBsonDocument()) != null)
            {
                return "Exestiert bereits";
            }
            collection.InsertOne(user);

            return "Insert OK";
        }

        public String DeleteSleep(Sleep user, string Database)
        {
            var database = db.GetDatabase(Database);
            var collection = database.GetCollection<Sleep>("Sleep");
            if (collection.Find(user.ToBsonDocument()) == null)
            {
                return "Exestiert nicht";
            }
            collection.DeleteOne(user.ToBsonDocument());
            return "Delete OK";
        }
    }
}
