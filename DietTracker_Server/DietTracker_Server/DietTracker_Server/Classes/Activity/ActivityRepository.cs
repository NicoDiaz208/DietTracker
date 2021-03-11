using System;
using System.Collections.Generic;
using MongoDB.Driver;
using System.Text;
using MongoDB.Bson;

namespace DietTracker_Server.Classes.Activity
{
    class ActivityRepository
    {

        MongoClient db;

        public ActivityRepository(string connectionString)
        {
            db = new MongoClient(connectionString);
        }


        public String AddActivity(BsonDocument user)
        {
            var database = db.GetDatabase("TestDietTracker");
            var collection = database.GetCollection<BsonDocument>("Acivity");
            if (collection.Find(user) != null)
            {
                return "Exestiert bereits";
            }
            collection.InsertOne(user);

            return "Insert OK";
        }

        public String DeletedActivity(BsonDocument user)
        {
            var database = db.GetDatabase("TestDietTracker");
            var collection = database.GetCollection<BsonDocument>("Acivity");
            if (collection.Find(user) == null)
            {
                return "Exestiert nicht";
            }
            collection.DeleteOne(user);
            return "Delete OK";
        }
    }
}
