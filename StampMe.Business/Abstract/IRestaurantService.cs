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
        Task DeleteImageAsync(object restId,Object imgId);

        Task ApprovedPromotion(PromotionApprovedDTO item);
        Task AddUpdatePromotion(PromotionDTO item, object Id);
        Task AddUpdateProduct(ProductDTO item, object Id);
        Task ApprovedProduct(ProductApprovedDTO item);

        Task<IEnumerable<WaitApprovalItemDTO>> GetWaitingApprovalProduct();
        Task<IEnumerable<WaitApprovalItemDTO>> GetWaitingApprovalPromotion();
        Task<IEnumerable<WaitApprovalItemDTO>> GetApprovedProduct();
        Task<IEnumerable<WaitApprovalItemDTO>> GetApprovedPromotion();

        Task<IEnumerable<WaitApprovalItemDTO>> GetProductByRestaurant(object Id);
        Task<IEnumerable<WaitApprovalItemDTO>> GetPromotionByRestaurant(object Id);


        Task ApprovedImageAsync(ImageAprovedDTO item);
        Task<List<ImageDTO>> GetApprovedImage();
        Task<List<ImageDTO>> GetWatingApprovalImage();

        Task SaveRestaurantInfo(RestaurantInfoDTO item);
        Task<RestaurantInfoDTO> GetRestaurantInfo(object Id);


        Task MenuSave(MenuDTO item);
        Task<IEnumerable<MenuDTO>> GetMenuList(object Id);
        Task MenuDelete(MenuDTO item);

        Task<IEnumerable<AroundMeListDTO>> GetAroundMeList();
    }
}
