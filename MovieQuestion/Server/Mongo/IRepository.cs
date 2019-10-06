using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using MovieQuestion.Shared.Models;

namespace MovieQuestion.Server.Mongo
{
    public interface IRepository<T> where T :IEntity
    {
        Task AddAsync(T item);
        Task AddManyAsync(IEnumerable<T> items);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IAsyncCursor<T>> GetAsyncCursorAsync();
        Task<T> FirstAsync(Expression<Func<T, bool>> func);
        Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> func);
        Task RemoveAll();
        Task DropAsync();
        Task Remove(T entity);
    }

}
