using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace DietTracker_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        public class MongoCRUD
        {
            private IMongoDatabase db;

            public MongoCRUD(string database)
            {
                var client = new MongoClient("mongodb://localhost:27017");
                db = client.GetDatabase(database);
            }
            public void insertRecord<T>(string table, T record)
            {
                var collection = db.GetCollection<T>(table);
                collection.InsertOne(record);
            }
        }
    }
}
