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


        public String AddWaterIntake(WaterIntake user,string Database)
        {
            var database = db.GetDatabase(Database);
            var collection = database.GetCollection<WaterIntake>("WaterIntake");
            if (collection.Find(user.ToBsonDocument()) != null)
            {
                return "Exestiert bereits";
            }
            collection.InsertOne(user);

            return "Insert OK";
        }

        public String DeleteWaterIntake(WaterIntake user,string Database)
        {
            var database = db.GetDatabase(Database);
            var collection = database.GetCollection<WaterIntake>("WaterIntake");
            if (collection.Find(user.ToBsonDocument()) == null)
            {
                return "Exestiert nicht";
            }
            collection.DeleteOne(user.ToBsonDocument());
            return "Delete OK";
        }
    }
}
