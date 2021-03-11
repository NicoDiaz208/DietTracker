using System;
using System.Collections.Generic;
using MongoDB.Driver;
using System.Text;
using MongoDB.Bson;

namespace DietTracker_Server.Classes.NutritionFacts
{
    class NutritionFactRepository
    {
        MongoClient db;

        public NutritionFactRepository(string connectionString)
        {
            db = new MongoClient(connectionString);
        }


        public String AddNF(BsonDocument nf,string Database)
        {
            var database = db.GetDatabase(Database);
            var collection = database.GetCollection<BsonDocument>("NutritionFacts");
            if (collection.Find(nf) != null)
            {
                return "Exestiert bereits";
            }
            collection.InsertOne(nf);

            return "Insert OK";
        }

        public String DeleteNF(BsonDocument nf, string Database)
        {
            var database = db.GetDatabase(Database);
            var collection = database.GetCollection<BsonDocument>("NutritionFacts");
            if (collection.Find(nf) == null)
            {
                return "Exestiert nicht";
            }
            collection.DeleteOne(nf);
            return "Delete OK";
        }

        public String ReplaceNF(BsonDocument oldNF,BsonDocument newNF, string Database)
        {
            var database = db.GetDatabase(Database);
            var collection = database.GetCollection<BsonDocument>("NutritionFacts");
            if (collection.Find(oldNF) == null)
            {
                return "Exestiert nicht";
            }
            collection.ReplaceOne(oldNF, newNF);
            return "Replace OK";
        }
    }
}
