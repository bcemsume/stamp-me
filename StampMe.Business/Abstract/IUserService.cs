using System;
using System.Collections.Generic;
using StampMe.Entities.Concrete;
using System.Threading.Tasks;
using System.Linq.Expressions;
using StampMe.Common.CustomDTO;

namespace StampMe.Business.Abstract
{

    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllAsync();
        Task Add(UserDTO entity);
        Task DeleteAsync(UserDTO entity);
        Task UpdateAsync(UserDTO entity);
        Task<User> FirstOrDefaultAsync(Expression<Func<User, bool>> filter);
        Task<IEnumerable<User>> WhereAsync(Expression<Func<User, bool>> filter);
        Task<UserDTO> Login(UserLoginDTO item);

        Task<IEnumerable<RewardDTO>> GetRewardList(object Id);
        Task<IEnumerable<RewardDTO>> GetRewardListByRestaurant(object Id, object RestId);

        Task AddRewardAsync(RewardItemDTO item);

        Task UserReward(UserRewardDTO item);
    }

}
