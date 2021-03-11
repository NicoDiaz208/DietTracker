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


        public String AddNF(NutritionFacts nf,string Database)
        {
            var database = db.GetDatabase(Database);
            var collection = database.GetCollection<NutritionFacts>("NutritionFacts");
            if (collection.Find(nf.ToBsonDocument()) != null)
            {
                return "Exestiert bereits";
            }
            collection.InsertOne(nf);

            return "Insert OK";
        }

        public String DeleteNF(NutritionFacts nf, string Database)
        {
            var database = db.GetDatabase(Database);
            var collection = database.GetCollection<NutritionFacts>("NutritionFacts");
            if (collection.Find(nf.ToBsonDocument()) == null)
            {
                return "Exestiert nicht";
            }
            collection.DeleteOne(nf.ToBsonDocument());
            return "Delete OK";
        }

        public String ReplaceNF(NutritionFacts oldNF, NutritionFacts newNF, string Database)
        {
            var database = db.GetDatabase(Database);
            var collection = database.GetCollection<NutritionFacts>("NutritionFacts");
            if (collection.Find(oldNF.ToBsonDocument()) == null)
            {
                return "Exestiert nicht";
            }
            collection.ReplaceOne(oldNF.ToBsonDocument(), newNF);
            return "Replace OK";
        }
    }
}
