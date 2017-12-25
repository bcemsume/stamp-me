using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using StampMe.Core.Entities;

namespace StampMe.Core.DataAccess
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        Task<T> GetAsync(Expression<Func<T, bool>> filter = null);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);
        Task DeleteAsync(Expression<Func<T, bool>> filter);
        Task UpdateAsync(Expression<Func<T, bool>> filter, T entity);
        Task AddAsync(T entity);
        IQueryable<T> AsQueryable();
        IQueryable<T> Where(Expression<Func<T, bool>> filter);

    }
}