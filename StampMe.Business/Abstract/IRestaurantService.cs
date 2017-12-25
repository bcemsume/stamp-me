using System;
using System.Collections.Generic;
using StampMe.Entities.Concrete;
using System.Threading.Tasks;
using System.Linq.Expressions;
using StampMe.Common.CustomDTO;

namespace StampMe.Business.Abstract
{
    public interface IRestaurantService
    {
        Task<IEnumerable<Restaurant>> GetAllAsync();
        Task Add(Restaurant entity);
        Task QuickSaveAsync(RestaurantQuickSaveDTO entity);
        Task DeleteAsync(Restaurant entity);
        Task DeleteRangeAsync(List<object> ids);
        Task UpdateAsync(Restaurant entity);
        Task<Restaurant> FirstOrDefaultAsync(Expression<Func<Restaurant, bool>> filter);
        Task<IEnumerable<Restaurant>> WhereAsync(Expression<Func<Restaurant, bool>> filter);
        Task<IEnumerable<RestaurantListDTO>> GetAdminRestaurantList();
        Task<LoginDTO> LoginAsync(string userName, string password);
        Task AddImageAsync(ImageDTO item, object Id);
        Task<IEnumerable<ImageDTO>> GetImages(object Id);
    }
}
