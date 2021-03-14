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

        public string AddAchievement(Achievement user,string Database)
        {
            var database = db.GetDatabase(Database);
            var collection = database.GetCollection<Achievement>("Achievement");
           
                collection.InsertOne(user);
                return "Insert OK";
            


        }

        public string DeleteAchievement(Achievement user, string Database)
        {
            var database = db.GetDatabase(Database);
            var collection = database.GetCollection<Achievement>("Achievement");
            if (collection.Find(user.ToBsonDocument()) == null)
            {
                return "Exestiert nicht";
            }
            collection.DeleteOne(user.ToBsonDocument());
            return "Delete OK";
        }
    }
}
