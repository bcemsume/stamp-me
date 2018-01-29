using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StampMe.Business.Abstract;
using StampMe.Common.CustomDTO;

namespace StampMe.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [EnableCors("MyPolicy")]
    public class CategoriesController : Controller
    {
        ICategoriesService _categoriesService;
        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }
        [HttpGet]
        public async Task<IEnumerable<CategoriesDTO>> GetAllAsync()
        {
            var result = await _categoriesService.GetAllAsync();
            return result;
        }
        [HttpPost]
        public async Task Add([FromBody]CategoriesDTO entity)
        {
            await _categoriesService.Add(entity);
        }
        [HttpPost]
        public async Task DeleteAsync([FromBody]CategoriesDTO entity)
        {
            await _categoriesService.DeleteAsync(entity);
        }
        [HttpPost]
        public async Task UpdateAsync([FromBody]CategoriesDTO entity)
        {
            await _categoriesService.UpdateAsync(entity);
        }
    }
}
