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
        public async Task<IEnumerable<ImageDTO>> GetImages(string Id)
        {
            var result = await _restaurantService.GetImages(Id);
            return result;
        }

        [HttpGet]
        public async Task DeleteImageAsync(string Id, string imgId)
        {
            await _restaurantService.DeleteImageAsync(Id, imgId);
        }
        // DELETE api/values/5
        [HttpGet]
        public async Task DeleteAsync(string id)
        {
            await _restaurantService.DeleteAsync(new Restaurant { Id = new ObjectId(id) });
        }


        [HttpPost]
        public async Task ApprovedPromotion([FromBody]PromotionDTO item, string Id)
        {
            await _restaurantService.ApprovedPromotion(item, Id);
        }
        [HttpPost]
        public async Task AddUpdatePromotion([FromBody]PromotionDTO item, string Id)
        {
            await _restaurantService.AddUpdatePromotion(item, Id);

        }
        [HttpPost]
        public async Task AddUpdateProduct([FromBody]ProductDTO item, string Id)
        {
            await _restaurantService.AddUpdateProduct(item, Id);

        }
        [HttpPost]
        public async Task ApprovedProduct([FromBody]ProductDTO item, string Id)
        {
            await _restaurantService.ApprovedProduct(item, Id);

        }

        [HttpGet]
        public async Task<IEnumerable<WaitApprovalItemDTO>> GetWaitingApprovalProduct() { return await _restaurantService.GetWaitingApprovalProduct(); }
        [HttpGet]
        public async Task<IEnumerable<WaitApprovalItemDTO>> GetWaitingApprovalPromotion() { return await _restaurantService.GetWaitingApprovalPromotion(); }
        [HttpGet]
        public async Task<IEnumerable<WaitApprovalItemDTO>> GetApprovedProduct() { return await _restaurantService.GetApprovedProduct(); }
        [HttpGet]
        public async Task<IEnumerable<WaitApprovalItemDTO>> GetApprovedPromotion() { return await _restaurantService.GetApprovedPromotion(); }
    }
}
