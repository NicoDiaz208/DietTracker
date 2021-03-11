using System;
using System.Collections.Generic;
using MongoDB.Driver;
using System.Text;
using MongoDB.Bson;

namespace DietTracker_Server.Classes.WaterIntake
{
    class DailyProgressRepository
    {
        MongoClient db;

        public DailyProgressRepository(string connectionString)
        {
            db = new MongoClient(connectionString);
        }


        public String AddWaterIntake(BsonDocument user,string Database)
        {
            var database = db.GetDatabase(Database);
            var collection = database.GetCollection<BsonDocument>("WaterIntake");
            if (collection.Find(user) != null)
            {
                return "Exestiert bereits";
            }
            collection.InsertOne(user);

            return "Insert OK";
        }

        public String DeleteWaterIntake(BsonDocument user,string Database)
        {
            var database = db.GetDatabase(Database);
            var collection = database.GetCollection<BsonDocument>("WaterIntake");
            if (collection.Find(user) == null)
            {
                return "Exestiert nicht";
            }
            collection.DeleteOne(user);
            return "Delete OK";
        }
    }
}
