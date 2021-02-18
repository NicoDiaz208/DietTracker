using System;
using System.Collections.Generic;
using MongoDB.Driver;
using System.Text;
using MongoDB.Bson;

namespace DietTracker_Server.Classes.User
{
    class UserRepository
    {
        //Könnte Probleme machen
        MongoClient db = new MongoClient("mongodb://localhost:27017");
        

        public String AddUser(BsonDocument user)
        {
            var database = db.GetDatabase("TestDietTracker");
            var collection = database.GetCollection<BsonDocument>("Users");
            if(collection.Find(user) != null)
            {
                return "Exestiert bereits";
            }
            collection.InsertOne(user);
            
            return "Insert OK";
        }

        public String DeleteUser(BsonDocument user)
        {
            var database = db.GetDatabase("TestDietTracker");
            var collection = database.GetCollection<BsonDocument>("Users");
            if (collection.Find(user) == null)
            {
                return "Exestiert nicht";
            }
            collection.DeleteOne(user);
            return "Delete OK";
        }

        public String ReplaceUser(BsonDocument oldInfo,BsonDocument newInfo)
        {
            var database = db.GetDatabase("TestDietTracker");
            var collection = database.GetCollection<BsonDocument>("Users");
            if (collection.Find(oldInfo) == null)
            {
                return "Exestiert nicht";
            }
            collection.ReplaceOne(oldInfo, newInfo);
            return "Replace OK";
        }
    }
}
