﻿using System;
using System.Collections.Generic;
using MongoDB.Driver;
using System.Text;
using MongoDB.Bson;

namespace DietTracker_Server.Classes.Food
{
    class DailyProgressRepository
    {
        MongoClient db = new MongoClient("mongodb://localhost:27017");
        public String AddFood(BsonDocument food)
        {
            var database = db.GetDatabase("TestDietTracker");
            var collection = database.GetCollection<BsonDocument>("Food");
            if (collection.Find(food) != null)
            {
                return "Exestiert bereits";
            }
            collection.InsertOne(food);

            return "Insert OK";
        }

        public String DeleteFood(BsonDocument food)
        {
            var database = db.GetDatabase("TestDietTracker");
            var collection = database.GetCollection<BsonDocument>("Food");
            if (collection.Find(food) == null)
            {
                return "Exestiert nicht";
            }
            collection.DeleteOne(food);
            return "Delete OK";
        }

        public string ReplaceFood(BsonDocument oldFood, BsonDocument newFood)
        {
            var database = db.GetDatabase("TestDietTracker");
            var collection = database.GetCollection<BsonDocument>("Food");
            if (collection.Find(oldFood) == null)
            {
                return "Exestiert nicht";
            }
            collection.ReplaceOne(oldFood, newFood);
            return "Altes Nahrungsmittel wurde geändert";
        }

    }
}
