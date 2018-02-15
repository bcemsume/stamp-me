using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using StampMe.Business.Abstract;
using StampMe.Common.CustomDTO;
using StampMe.Entities.Concrete;

namespace StampMe.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [EnableCors("MyPolicy")]
    public class UserController : Controller
    {
        IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<User> Get(object id)
        {
            return await _userService.FirstOrDefaultAsync(x => x.Id == (ObjectId)id);
        }

        // POST api/values
        [HttpPost]
        public async Task AddAsync([FromBody]UserDTO item)
        {
            await _userService.Add(item);
        }

        // PUT api/values/5
        [HttpPost]
        public async Task UpdateAsync([FromBody]UserDTO item)
        {
            await _userService.UpdateAsync(item);
        }

        // DELETE api/values/5
        [HttpDelete("DeleteAsync")]
        public async Task DeleteAsync(UserDTO item)
        {
            await _userService.DeleteAsync(item);
        }

        [HttpPost]
        public async Task<UserDTO> Login([FromBody] UserLoginDTO item)
        {
            return await _userService.Login(item);
        }

        [HttpGet]
        public async Task<IEnumerable<RewardDTO>> GetRewardList(string Id)
        {
            return await _userService.GetRewardList(Id);
        }


    }
}
