﻿using MongoDB.Bson;
using MongoDB.Driver;
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
    }
}