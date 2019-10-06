using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using MovieQuestion.Shared.Models;

namespace MovieQuestion.Server.Mongo
{
    public class Repository<T> : IRepository<T> where T : IEntity
    {
        private readonly MongoContext _context;
        protected readonly IMongoCollection<T> Collection;
        private string _collectionName;
        public Repository(MongoContext context)
        {
            _context = context;

            Collection = context.GetCollection<T>(typeof(T).Name);
            _collectionName = typeof(T).Name;
        }

        /// <summary>
        /// Adds items.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task AddAsync(T item)
        {
            await Collection.InsertOneAsync(item);
        }

        /// <summary>
        /// Adds list of items.
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public async Task AddManyAsync(IEnumerable<T> items)
        {
            await Collection.InsertManyAsync(items);
        }

        /// <summary>
        /// Gets all items.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Collection.Find(_ => true).ToListAsync();
        }

        /// <summary>
        /// Gets cursor for all items.
        /// </summary>
        /// <returns></returns>
        public async Task<IAsyncCursor<T>> GetAsyncCursorAsync()
        {
            return await Collection.Find(_ => true).ToCursorAsync();
        }

        /// <summary>
        /// Gets first item.
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public async Task<T> FirstAsync(Expression<Func<T, bool>> func)
        {
            return await Collection.Find(func).FirstOrDefaultAsync();
        }


        public async Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> func)
        {
            return Collection.AsQueryable().Where(func).ToList();
        }

        /// <summary>
        /// Removes all items.
        /// </summary>
        /// <returns></returns>
        public async Task RemoveAll()
        {
            await Collection.DeleteManyAsync(arg => true);
        }

        /// <summary>
        /// Drops collection.
        /// </summary>
        /// <returns></returns>
        public async Task DropAsync()
        {
            await _context.DropCollectionAsync(_collectionName);
        }

        public async Task Remove(T entity)
        {
            Collection.DeleteOne(arg => arg.ObjectId == entity.ObjectId);
        }
    }

}
