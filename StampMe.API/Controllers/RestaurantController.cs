using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using StampMe.Business.Abstract;
using StampMe.Common.CustomDTO;
using StampMe.Entities.Concrete;
using System.Linq;
using Newtonsoft.Json;

namespace StampMe.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [EnableCors("MyPolicy")]
    public class RestaurantController : Controller
    {
        IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet]
        public async Task<IEnumerable<Restaurant>> Get()
        {
            return await _restaurantService.GetAllAsync();
        }

        [HttpGet]
        public async Task<IEnumerable<RestaurantListDTO>> GetAdminRestaurantList()
        {
            return await _restaurantService.GetAdminRestaurantList();
        }

        [HttpGet("{id}")]
        public async Task<Restaurant> Get(string id)
        {
            return await _restaurantService.FirstOrDefaultAsync(x => x.Id == new ObjectId(id));
        }

        [HttpGet]
        public async Task<LoginDTO> Login(string userName, string password)
        {
            var rest = await _restaurantService.LoginAsync(userName, password);

            return rest;
        }

        // POST api/values
        [HttpPost]
        public async Task AddAsync([FromBody]Restaurant item)
        {
            await _restaurantService.Add(item);
        }

        [HttpPost]
        public async Task QuickSave([FromBody]RestaurantQuickSaveDTO item)
        {
            await _restaurantService.QuickSaveAsync(item);

        }


        // PUT api/values/5
        [HttpPost]
        public async Task UpdateAsync(string id, [FromBody]Restaurant item)
        {
            if (item == null)
                throw new Exception("Restaurant bulunamadı.!!");
            item.Id = new ObjectId(id);
            await _restaurantService.UpdateAsync(item);
        }

        [HttpPost]
        public async Task AddImageAsync(string id, [FromBody]ImageDTO item)
        {
            await _restaurantService.AddImageAsync(item, id);
        }

        [HttpGet]
        public async Task<IEnumerable<ImageDTO>> GetImages(object Id)
        {
            var result = await _restaurantService.GetImages(Id);
            return result;
        }

        // DELETE api/values/5
        [HttpGet]
        public async Task DeleteAsync(string id)
        {
            await _restaurantService.DeleteAsync(new Restaurant { Id = new ObjectId(id)});
        }

    }
}
