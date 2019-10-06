using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace MovieQuestion.Server.Mongo
{
    public class MongoContext
    {
        private readonly IMongoDatabase _database;

        public MongoContext(string connectionString)
        {
            var mongoUrl = new MongoUrl(connectionString);
            var client = new MongoClient(mongoUrl);
            _database = client.GetDatabase(mongoUrl.DatabaseName);
        }

        /// <summary>
        /// Gets collection with name being equal to <see cref="T"/>
        /// </summary>
        /// <typeparam name="T">The type of collection's items.</typeparam>
        /// <returns></returns>
        public IMongoCollection<T> GetCollection<T>() => _database.GetCollection<T>(typeof(T).Name);

        /// <summary>
        /// Gets collection with given name.
        /// </summary>
        /// <typeparam name="T">The type of collection's items.</typeparam>
        /// <param name="name">Name of the collection.</param>
        /// <returns></returns>
        public IMongoCollection<T> GetCollection<T>(string name) => _database.GetCollection<T>(name);

        public async Task DropCollectionAsync<T>()
        {
            await _database.DropCollectionAsync(typeof(T).Name);
        }

        public async Task DropCollectionAsync(string name)
        {
            await _database.DropCollectionAsync(name);
        }
    }
}
