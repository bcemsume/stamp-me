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

        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await _userService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<User> Get(object id)
        {
            return await _userService.FirstOrDefaultAsync(x => x.Id == (ObjectId)id);
        }

        // POST api/values
        [HttpPost]
        public async Task AddAsync([FromBody]User item)
        {
            await _userService.Add(item);
        }

        // PUT api/values/5
        [HttpPost]
        public async Task UpdateAsync(string id, [FromBody]User item)
        {
            item.Id = new ObjectId(id);
            await _userService.UpdateAsync(item);
        }

        // DELETE api/values/5
        [HttpDelete("DeleteAsync")]
        public async Task DeleteAsync(string id, User item)
        {
            item.Id = new ObjectId(id);
            await _userService.DeleteAsync(item);
        }

        [HttpPost]
        public async Task<UserDTO> Login([FromBody] UserLoginDTO item)
        {
            return await _userService.Login(item);
        }

    }
}
