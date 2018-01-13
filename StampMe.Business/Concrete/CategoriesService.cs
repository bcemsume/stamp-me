using StampMe.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using StampMe.Common.CustomDTO;
using StampMe.DataAccess.Abstract;
using StampMe.Entities.Concrete;
using System.Linq;
using MongoDB.Bson;

namespace StampMe.Business.Concrete
{
    public class CategoriesService : ICategoriesService
    {
        ICategoriesDal _categoriesDal;

        public CategoriesService(ICategoriesDal categoriesDal)
        {
            _categoriesDal = categoriesDal;

        }

        public async Task Add(CategoriesDTO entity)
        {
            var category = new Categories();

            category.Definition = entity.Definition;
            category.Id = ObjectId.GenerateNewId();
        
            await _categoriesDal.AddAsync(category);
        }

        public async Task DeleteAsync(CategoriesDTO entity)
        {
            await _categoriesDal.DeleteAsync(x => x.Id == new ObjectId((string)entity.Id));
        }

        public async Task<IEnumerable<CategoriesDTO>> GetAllAsync()
        {
            var result = await _categoriesDal.GetAllAsync();

            return result.Select(x => new CategoriesDTO
            {
                Definition = x.Definition,
                Id = x.Id.ToString(),
            }).ToList();
        }

        public async Task UpdateAsync(CategoriesDTO entity)
        {
            var contract = new Categories
            {
                Id = new ObjectId((string)entity.Id),
                Definition = entity.Definition
            };
            await _categoriesDal.UpdateAsync(x => x.Id == new ObjectId((string)entity.Id), contract);
        }
    }
}