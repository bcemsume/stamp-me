using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using StampMe.Business.Abstract;
using StampMe.Common.CustomDTO;
using StampMe.DataAccess.Abstract;
using StampMe.Entities.Concrete;
using System.Linq;
using StampMe.Common.PasswordProtected;

namespace StampMe.Business.Concrete
{
    public class UserService : IUserService
    {
        IUserDal _userDal;
        IRestaurantDal _restaurantDal;
        IRewardDetailDal _rewardDetailDal;
        public UserService(IUserDal userDal, IRestaurantDal restaurantDal, IRewardDetailDal rewardDetailDal)
        {
            _userDal = userDal;
            _restaurantDal = restaurantDal;
            _rewardDetailDal = rewardDetailDal;
        }

        public async Task Add(UserDTO entity)
        {
            var usr = new User
            {
                BirthDay = entity.BirthDay,
                Email = entity.Email,
                FirstName = entity.FirstName,
                Gender = entity.Gender,
                Id = ObjectId.GenerateNewId(),
                LastName = entity.LastName,
                Password = PasswordHash.GetPasswordHash(entity.Password),
                Reward = new List<Reward>(),
                SocialToken = entity.SocialToken
            };

            await _userDal.AddAsync(usr);
        }

        public async Task DeleteAsync(UserDTO entity)
        {
            await _userDal.DeleteAsync(x => x.Id == new ObjectId(entity.Id));
        }

        public async Task<User> FirstOrDefaultAsync(Expression<Func<User, bool>> filter)
        {
            return await _userDal.GetAsync(filter);
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            var result = new List<UserDTO>();
            var lst = await _userDal.GetAllAsync();

            return lst.Select(x => new UserDTO {

            }).ToList();

        }

        public async Task UpdateAsync(UserDTO entity)
        {
            var usr =  await _userDal.GetAsync(x => x.Id == new ObjectId(entity.Id));

            if (usr == null)
                throw new Exception("Kullanıcı Bulunamadı..!!");

            await _userDal.UpdateAsync(x => x.Id == usr.Id, usr);
        }

        public async Task<IEnumerable<User>> WhereAsync(Expression<Func<User, bool>> filter)
        {
            return await _userDal.GetAllAsync(filter);
        }

        public async Task<UserDTO> Login(UserLoginDTO item)
        {
            var user = await _userDal.GetAsync(x => x.Email == item.UserName && x.Password == item.Password);

            if (user == null)
                throw new Exception("Kullanıcı Bulunamadı.!!");

            return new UserDTO
            {
                BirthDay = user.BirthDay,
                Email = user.Email,
                FirstName = user.FirstName,
                Gender = user.Gender,
                Id = user.Id.ToString(),
                LastName = user.LastName
            };

        }

        public async Task<IEnumerable<RewardDTO>> GetRewardList(object Id)
        {
            var lst = new List<RewardDTO>();
            var users = await _userDal.GetAsync(x => x.Id == new ObjectId((string)Id));
            var restList = await _restaurantDal.GetAllAsync();

            if (users == null)
                throw new Exception("Kullanıcı Bulunumadı..!!");


            foreach (var reward in users.Reward)
            {
                var rest = restList.FirstOrDefault(x => x.Promotion.Any(z => z.Id == reward.PromotionId));
                if (rest == null)
                    continue;

                lst.Add(new RewardDTO
                {
                    Claim = reward.ClaimCount,
                    isUsed = reward.isUsed,
                    RestId = rest.Id.ToString(),
                    RestName = rest.Name,
                    StampDate = reward.StampDate
                });
            }

            return lst;
        }

        public async Task AddRewardAsync(RewardItemDTO item)
        {
            var user = await _userDal.GetAsync(x => x.Id == new ObjectId(item.UserId));
            var rest = (await _restaurantDal.GetAllAsync()).FirstOrDefault(x => x.Promotion.Any(z => z.Id == new ObjectId(item.PromId)));

            if (rest == null)
                throw new Exception("Restaurant Bulunumadı..!!");

            var prom = rest.Promotion.FirstOrDefault(x => x.Id == new ObjectId(item.PromId));

            if (prom == null)
                throw new Exception("Promosyon Bulunumadı..!!");

            if (user == null)
                throw new Exception("Kullanıcı Bulunumadı..!!");




            if (user.Reward == null)
                user.Reward = new List<Reward>();

            var rew = user.Reward.FirstOrDefault(x => x.PromotionId == new ObjectId(item.PromId));

            if (rew == null)
            {
                rew = new Reward();
                rew.ClaimCount = 1;
                rew.Id = ObjectId.GenerateNewId();
                rew.isUsed = false;
                rew.PromotionId = new ObjectId(item.PromId);
                rew.StampDate = DateTime.Now;

                user.Reward.Add(rew);
            }
            else
            {
                if (prom.Claim == rew.ClaimCount)
                {
                    rew = new Reward();
                    rew.ClaimCount = 1;
                    rew.Id = ObjectId.GenerateNewId();
                    rew.isUsed = false;
                    rew.PromotionId = new ObjectId(item.PromId);
                    rew.StampDate = DateTime.Now;

                    user.Reward.Add(rew);
                }
                else
                {
                    rew.ClaimCount += 1;
                }
            }


            await _userDal.UpdateAsync(x => x.Id == new ObjectId(item.UserId), user);

            await _rewardDetailDal.AddAsync(new RewardDetail
            {
                ClaimCount = rew.ClaimCount,
                Id = ObjectId.GenerateNewId(),
                isUsed = false,
                PromotionId = new ObjectId(item.PromId),
                StampDate = DateTime.Now

            });

        }

        public async Task UserReward(UserRewardDTO item)
        {
            var user = await _userDal.GetAsync(x => x.Id == new ObjectId(item.UserId));

            if (user == null)
                throw new Exception("Kullanıcı Bulunamadı..!!");

            var reward = user.Reward.FirstOrDefault(x => x.Id == new ObjectId(item.RewardId));

            if (reward == null)
                throw new Exception("Ödül Bulunamadı..!!");

            reward.isUsed = true;

            await _userDal.UpdateAsync(x => x.Id == user.Id, user);

        }
    }
}
