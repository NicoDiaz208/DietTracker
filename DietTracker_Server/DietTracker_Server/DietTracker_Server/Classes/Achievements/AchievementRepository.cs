using System;
using System.Collections.Generic;
using MongoDB.Driver;
using System.Text;
using MongoDB.Bson;

namespace DietTracker_Server.Classes.Achievements
{
    class AchievmentRepository
    {
        MongoClient db;

        public AchievmentRepository(string connectionString)
        {
            db = new MongoClient(connectionString);
        }

        public String AddAchievement(BsonDocument user)
        {
            var database = db.GetDatabase("TestDietTracker");
            var collection = database.GetCollection<BsonDocument>("Achievement");
            if (collection.Find(user) != null)
            {
                return "Exestiert bereits";
            }
            collection.InsertOne(user);

            return "Insert OK";
        }

        public String DeleteAchievement(BsonDocument user)
        {
            var database = db.GetDatabase("TestDietTracker");
            var collection = database.GetCollection<BsonDocument>("Achievement");
            if (collection.Find(user) == null)
            {
                return "Exestiert nicht";
            }
            collection.DeleteOne(user);
            return "Delete OK";
        }
    }
}
