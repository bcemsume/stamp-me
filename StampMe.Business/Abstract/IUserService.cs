using System;
using System.Collections.Generic;
using StampMe.Entities.Concrete;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace StampMe.Business.Abstract
{

    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task Add(User entity);
        Task DeleteAsync(User entity);
        Task UpdateAsync(User entity);
        Task<User> FirstOrDefaultAsync(Expression<Func<User, bool>> filter);
        Task<IEnumerable<User>> WhereAsync(Expression<Func<User, bool>> filter);
    }

}
