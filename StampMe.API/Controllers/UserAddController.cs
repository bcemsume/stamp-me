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
    // [Authorize]
    [EnableCors("MyPolicy")]
    public class UserAddController : Controller
    {
        IUserService _userService;
        public UserAddController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task AddAsync([FromBody]UserDTO item)
        {
            await _userService.Add(item);
        }
    }
}
