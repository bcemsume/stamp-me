using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StampMe.Business.Abstract;
using StampMe.Common.CustomDTO;

namespace StampMe.API.Controllers
{
    [Route("token")]
    [AllowAnonymous]
    public class TokenController : Controller
    {
        IUserService _userService;
        public TokenController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]UserLoginDTO inputModel)
        {
            var user = await _userService.Login(inputModel);
            if (user == null)
                return Unauthorized();

            var token = new JwtTokenBuilder()
                .AddSecurityKey(JwtSecurityKey.Create("huppa-secret-key"))
                .AddSubject(string.Format("{0} {1}",user.FirstName ,user.LastName))
                .AddIssuer("StampMe.API.Controller")
                .AddAudience("StampMe.API.Controller")
                .AddClaim("UserId", user.Id)
                .AddExpiry(99999)
                .Build();

            //return Ok(token);
            return Ok(new { UserId = user.Id,   token = token.Value});
        }
    }
}
