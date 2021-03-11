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


        public String AddUser(User user,string Database)
        {
            var database = db.GetDatabase(Database);
            var collection = database.GetCollection<User>("Users");
            if(collection.Find(user.ToBsonDocument()) != null)
            {
                return "Exestiert bereits";
            }
            collection.InsertOne(user);
            
            return "Insert OK";
        }

        public String DeleteUser(User user, string Database)
        {
            var database = db.GetDatabase(Database);
            var collection = database.GetCollection<User>("Users");
            if (collection.Find(user.ToBsonDocument()) == null)
            {
                return "Exestiert nicht";
            }
            collection.DeleteOne(user.ToBsonDocument());
            return "Delete OK";
        }

        public String ReplaceUser(User oldInfo, User newInfo, string Database)
        {
            var database = db.GetDatabase(Database);
            var collection = database.GetCollection<User>("Users");
            if (collection.Find(oldInfo.ToBsonDocument()) == null)
            {
                return "Exestiert nicht";
            }
            collection.ReplaceOne(oldInfo.ToBsonDocument(), newInfo);
            return "Replace OK";
        }
    }
}
