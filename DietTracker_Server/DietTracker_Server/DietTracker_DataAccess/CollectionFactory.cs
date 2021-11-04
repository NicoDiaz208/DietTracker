using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace DietTracker_DataAccess
{
    public class CollectionFactory
    {
        private readonly MongoClient client;
        private readonly string dbName;

        public CollectionFactory(MongoClient client, IConfiguration configuration)
        {
            this.client = client;
            dbName = configuration["MongoDbName"];
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            var db = client.GetDatabase(dbName);
            return db.GetCollection<T>(typeof(T).Name);
        }

        public IMongoDatabase GetDatabase()
        {
            var db = client.GetDatabase(dbName);
            return db;
        }
    }
}
