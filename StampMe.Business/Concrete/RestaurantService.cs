using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using StampMe.Business.Abstract;
using StampMe.Common.CustomDTO;
using StampMe.DataAccess.Abstract;
using StampMe.Entities.Concrete;
using System.Linq;
using MongoDB.Bson;

namespace StampMe.Business.Concrete
{
    public class RestaurantService : IRestaurantService
    {
        IRestaurantDal _restaurantDal;
        public RestaurantService(IRestaurantDal restaurantDal)
        {
            _restaurantDal = restaurantDal;
        }

        public async  Task Add(Restaurant entity)
        {
            await _restaurantDal.AddAsync(entity);
        }

        public async Task AddImageAsync(ImageDTO item, object Id)
        {
            var rest = await _restaurantDal.GetAsync(x=> x.Id == new MongoDB.Bson.ObjectId((string)Id));

            if (rest == null)
                throw new Exception("Restaurant Bulunumadı..!!");

            if (rest.Images == null)
                rest.Images = new List<Images>();

            var image = new Images() { Description = item.Info, Image = Convert.FromBase64String(item.Data), Id = ObjectId.GenerateNewId() };
            rest.Images.Add(image);

            await _restaurantDal.UpdateAsync(x => x.Id == rest.Id, rest);
        }

        public async Task DeleteAsync(Restaurant entity)
        {
            await _restaurantDal.DeleteAsync(x => x.Id == entity.Id);
        }

        public async Task DeleteImageAsync(object  restId, object imgId)
        {
            var rest = await _restaurantDal.GetAsync(x => x.Id == new MongoDB.Bson.ObjectId((string)restId));

            if (rest == null)
                throw new Exception("Restaurant Bulunumadı..!!");

           var img = rest.Images.FirstOrDefault(x=> x.Id == new ObjectId((string)imgId));

            if (img == null)
                throw new Exception("Resim Bulunumadı..!!");

            rest.Images.Remove(img);

            await _restaurantDal.UpdateAsync(x => x.Id == rest.Id, rest);
        }

        public async Task DeleteRangeAsync(List<object> ids)
        {
            foreach (var item in ids)
            {
                await DeleteAsync(new Restaurant { Id = new MongoDB.Bson.ObjectId((string)item) });
            }
        }

        public async Task<Restaurant> FirstOrDefaultAsync(Expression<Func<Restaurant, bool>> filter)
        {
            return await _restaurantDal.GetAsync(filter);
        }

        public async Task<IEnumerable<RestaurantListDTO>> GetAdminRestaurantList()
        {
            var list = await _restaurantDal.GetAllAsync();
            return list.Select(x => new RestaurantListDTO { isActive= x.isActive, Adress = x.Info.Adress.AdressDetail, Email = x.Email, Id = x.Id.ToString(), Name = x.Name, Password = x.Password,  UserName = x.UserName });
                                           
        }

        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            return await _restaurantDal.GetAllAsync();
        }

        public async Task<IEnumerable<ImageDTO>> GetImages(object Id)
        {
            var result = await _restaurantDal.GetAsync(x => x.Id == new MongoDB.Bson.ObjectId((string)Id));

            if (result == null)
                throw new Exception("Restaurant Bulunumadı..!!");

            if (result.Images == null)
                result.Images = new List<Images>();

            return result.Images.Select(x => new ImageDTO { Info = x.Description, Data = Convert.ToBase64String(x.Image), Id = x.Id.ToString() });
        }

        public async Task<LoginDTO> LoginAsync(string userName, string password)
        {
            var result = new LoginDTO();
            var rest = await _restaurantDal.GetAsync(x => x.UserName == userName && x.Password == password);
            if (rest == null)
                rest = await _restaurantDal.GetAsync(x => x.Email == userName && x.Password == password);

            result.Id = rest.Id.ToString();
            result.Name = rest.Name;

            return result;
        }

        public async Task QuickSaveAsync(RestaurantQuickSaveDTO entity)
        {
            var id = entity.Id == null ?  new MongoDB.Bson.ObjectId() : new MongoDB.Bson.ObjectId(entity.Id);
            var r = await FirstOrDefaultAsync(x => x.Id == id);
            bool isNew = false;
            if (r == null)
            {
                r = new Restaurant();
                isNew = true;
            }

            r.Name = entity.Name;
            r.Email = entity.Email;
            r.Password = entity.Password;
            r.UserName = entity.UserName;
            r.isActive = entity.isActive;
            r.Info = new Info() { Adress = new Adress { AdressDetail = entity.Adress } };

            if (isNew)
                await Add(r);
            else
                await UpdateAsync(r);
        }

        public async Task UpdateAsync(Restaurant entity)
        {
            await _restaurantDal.UpdateAsync(x => x.Id == entity.Id, entity);
        }

        public async Task<IEnumerable<Restaurant>> WhereAsync(Expression<Func<Restaurant, bool>> filter)
        {
            return await _restaurantDal.GetAllAsync(filter);
        }
    }
}
