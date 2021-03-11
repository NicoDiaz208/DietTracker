using System;
using System.Collections.Generic;
using MongoDB.Driver;
using System.Text;
using MongoDB.Bson;

namespace DietTracker_Server.Classes.User
{
    class UserRepository
    {
        MongoClient db;

        public UserRepository(string connectionString)
        {
            db = new MongoClient(connectionString);
        }


        public String AddUser(BsonDocument user,string Database)
        {
            var database = db.GetDatabase(Database);
            var collection = database.GetCollection<BsonDocument>("Users");
            if(collection.Find(user) != null)
            {
                return "Exestiert bereits";
            }
            collection.InsertOne(user);
            
            return "Insert OK";
        }

        public String DeleteUser(BsonDocument user, string Database)
        {
            var database = db.GetDatabase(Database);
            var collection = database.GetCollection<BsonDocument>("Users");
            if (collection.Find(user) == null)
            {
                return "Exestiert nicht";
            }
            collection.DeleteOne(user);
            return "Delete OK";
        }

        public String ReplaceUser(BsonDocument oldInfo,BsonDocument newInfo, string Database)
        {
            var database = db.GetDatabase(Database);
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
