using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using DietTracker_Server.Classes.Achievement;

namespace DietTracker_Server
{
    class MongoCRUD
    {
        private IMongoDatabase db;

        public MongoCRUD(string database)
        {
            var client = new MongoClient("mongodb+srv://ameldz:Eldin2010@diettracker.ijgzi.mongodb.net/test");
            
            db = client.GetDatabase(database);
        }
        public void insertRecord(string table, Achievement record)
        {
            var collection = db.GetCollection<Achievement>(table);
            collection.InsertOne(record);
        }
    }
}