﻿using System;
using System.Collections.Generic;
using MongoDB.Driver;
using System.Text;
using MongoDB.Bson;

namespace DietTracker_Server.Classes.Sleep
{
    class DailyProgressRepository
    {
        MongoClient db = new MongoClient("mongodb://localhost:27017");


        public String AddSleep(BsonDocument user)
        {
            var database = db.GetDatabase("TestDietTracker");
            var collection = database.GetCollection<BsonDocument>("Sleep");
            if (collection.Find(user) != null)
            {
                return "Exestiert bereits";
            }
            collection.InsertOne(user);

            return "Insert OK";
        }

        public String DeleteSleep(BsonDocument user)
        {
            var database = db.GetDatabase("TestDietTracker");
            var collection = database.GetCollection<BsonDocument>("Sleep");
            if (collection.Find(user) == null)
            {
                return "Exestiert nicht";
            }
            collection.DeleteOne(user);
            return "Delete OK";
        }
    }
}
