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

        public async Task<IEnumerable<CategoriesDTO>> GetAllAsync()
        {
            var result = await _categoriesService.GetAllAsync();
            return result;
        }
        public async Task Add(CategoriesDTO entity)
        {
            await _categoriesService.Add(entity);
        }
        public async Task DeleteAsync(CategoriesDTO entity)
        {
            await _categoriesService.DeleteAsync(entity);
        }
        public async Task UpdateAsync(CategoriesDTO entity)
        {
            await _categoriesService.UpdateAsync(entity);
        }
    }
}
