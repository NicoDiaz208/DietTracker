using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DietTracker_DataAccess
{
    public static class MongoCollectionExtensions
    {
        public static async Task DeleteById<T>(this IMongoCollection<T> collection, ObjectId id)
            where T : IHaveId
        {
            await collection.DeleteOneAsync(Builders<T>.Filter.Eq(a => a.Id, id));
        }

        public static async Task ReplaceById<T>(this IMongoCollection<T> collection, string id, T clas)
            where T : IHaveId
        {
           await collection.ReplaceOneAsync(Builders<T>.Filter.Eq(a => a.Id, ObjectId.Parse(id)), clas);
        }


        public static async Task<IEnumerable<T>> GetAll<T>(this IMongoCollection<T> collection)
        {
            var cursor = await collection.FindAsync<T>(Builders<T>.Filter.Empty);
            return await cursor.ToListAsync();
        }

        public static Task<T?> GetById<T>(this IMongoCollection<T> collection, string id)
            where T : IHaveId
            => GetById(collection, ObjectId.Parse(id));

        

        public static async Task<T?> GetById<T>(this IMongoCollection<T> collection, ObjectId id)
            where T: IHaveId
        {
            var cursor = await collection.FindAsync<T>(Builders<T>.Filter.Eq(x => x.Id, id));
            return await cursor.FirstOrDefaultAsync();
        }

        public static async Task<T?> GetByNameAndPassword<T>(this IMongoCollection<T> collection, string name,string password)
            where T : Login
        {
            var cursor = await collection.FindAsync<T>(Builders<T>.Filter.Eq(x => x.Password, password) & (Builders<T>.Filter.Eq(x => x.Username, name)));
            return await cursor.FirstOrDefaultAsync();
        }

        public static async Task<T?> GetUserByUsername<T>(this IMongoCollection<T> collection, string username)
            where T : User
        {
            var cursor = await collection.FindAsync<T>(Builders<T>.Filter.Eq(x => x.Name, username));
            return await cursor.FirstOrDefaultAsync();
        }

        public static async Task<T?> GetUserByEmail<T>(this IMongoCollection<T> collection, string email)
            where T : User
        {
            var cursor = await collection.FindAsync<T>(Builders<T>.Filter.Eq(x => x.Email, email));
            return await cursor.FirstOrDefaultAsync();
        }


    }
}
