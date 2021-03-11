using System;
using System.Collections.Generic;
using MongoDB.Driver;
using System.Text;
using MongoDB.Bson;

namespace DietTracker_Server.Classes.Achievement
{
    class AchievmentRepository
    {
        MongoClient db;

        public AchievmentRepository(string connectionString)
        {
            db = new MongoClient(connectionString);
        }

        public String AddAchievement(Achievement user,string Database)
        {
            var database = db.GetDatabase(Database);
            var collection = database.GetCollection<Achievement>("Achievement");
            if (collection.Find(user) != null)
            {
                return "Exestiert bereits";
            }
            collection.InsertOne(user);

            return "Insert OK";
        }

        public String DeleteAchievement(BsonDocument user, string Database)
        {
            var database = db.GetDatabase(Database);
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
