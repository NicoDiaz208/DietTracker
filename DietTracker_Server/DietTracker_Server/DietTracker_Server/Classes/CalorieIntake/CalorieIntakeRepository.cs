using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DietTracker_Server.Classes.CalorieIntake
{
    class CalorieIntakeRepository
    {
        MongoClient db;

        public CalorieIntakeRepository(string connectionString)
        {
            db = new MongoClient(connectionString);
        }


        public String AddCalorie(CalorieIntake user,string Database)
        {
            var database = db.GetDatabase(Database);
            var collection = database.GetCollection<CalorieIntake>("CalorieIntake");
            if (collection.Find(user.ToBsonDocument()) != null)
            {
                return "Exestiert bereits";
            }
            collection.InsertOne(user);

            return "Insert OK";
        }

        public String DeleteCalorie(CalorieIntake user, string Database)
        {
            var database = db.GetDatabase(Database);
            var collection = database.GetCollection<CalorieIntake>("CalorieIntake");
            if (collection.Find(user.ToBsonDocument()) == null)
            {
                return "Exestiert nicht";
            }
            collection.DeleteOne(user.ToBsonDocument());
            return "Delete OK";
        }
    }
}
