using StampMe.Common.CustomDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StampMe.Business.Abstract
{
    public interface ICategoriesService
    {
        Task<IEnumerable<CategoriesDTO>> GetAllAsync();
        Task Add(CategoriesDTO entity);
        Task DeleteAsync(CategoriesDTO entity);
        Task UpdateAsync(CategoriesDTO entity);
    }
}