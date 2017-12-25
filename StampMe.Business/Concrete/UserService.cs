using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using StampMe.Business.Abstract;
using StampMe.DataAccess.Abstract;
using StampMe.Entities.Concrete;

namespace StampMe.Business.Concrete
{
    public class UserService : IUserService
    {
        IUserDal _userDal;

        public UserService(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public async Task Add(User entity)
        {
            await _userDal.AddAsync(entity);
        }

        public async Task DeleteAsync(User entity)
        {
            await _userDal.DeleteAsync(x=> x.Id == entity.Id);
        }

        public async Task<User> FirstOrDefaultAsync(Expression<Func<User, bool>> filter)
        {
            return await _userDal.GetAsync(filter);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userDal.GetAllAsync();
        }

        public async Task UpdateAsync(User entity)
        {
            await _userDal.UpdateAsync(x => x.Id == entity.Id, entity);
        }

        public async Task<IEnumerable<User>> WhereAsync(Expression<Func<User, bool>> filter)
        {
            return await _userDal.GetAllAsync(filter);
        }
    }
}
