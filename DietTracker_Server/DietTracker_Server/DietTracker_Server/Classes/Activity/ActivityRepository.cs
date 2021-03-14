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


        public String AddActivity(Activity user,string Database)
        {
            var database = db.GetDatabase(Database);
            var collection = database.GetCollection<Activity>("Acivity");
            if (collection.Find(user.ToBsonDocument()) != null)
            {
                return "Exestiert bereits";
            }
            collection.InsertOne(user);

            return "Insert OK";
        }

        public String DeletedActivity(Activity user, string Database)
        {
            var database = db.GetDatabase(Database);
            var collection = database.GetCollection<Activity>("Acivity");
            if (collection.Find(user.ToBsonDocument()) == null)
            {
                return "Exestiert nicht";
            }
            collection.DeleteOne(user.ToBsonDocument());
            return "Delete OK";
        }
    }
}
