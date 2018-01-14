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

namespace StampMe.Business.Concrete {
    public class RestaurantService: IRestaurantService {
        IRestaurantDal _restaurantDal;
        public RestaurantService(IRestaurantDal restaurantDal) {
            _restaurantDal = restaurantDal;
        }

        public async Task Add(Restaurant entity) {
            await _restaurantDal.AddAsync(entity);
        }

        public async Task AddImageAsync(ImageDTO item, object Id) {
            var rest = await _restaurantDal.GetAsync(x => x.Id == new MongoDB.Bson.ObjectId((string) Id));

            if(rest == null)
                throw new Exception("Restaurant Bulunumadı..!!");

            if(rest.Images == null)
                rest.Images = new List<Images>();

            var image = new Images() { Description = item.Info, Image = Convert.FromBase64String(item.Data), Id = ObjectId.GenerateNewId() };
            rest.Images.Add(image);

            await _restaurantDal.UpdateAsync(x => x.Id == rest.Id, rest);
        }

        public async Task DeleteAsync(Restaurant entity) {
            await _restaurantDal.DeleteAsync(x => x.Id == entity.Id);
        }

        public async Task ApprovedImageAsync(ImageAprovedDTO item) {
            var rest = await _restaurantDal.GetAsync(x => x.Id == new MongoDB.Bson.ObjectId((string) item.RestId));

            if(rest == null)
                throw new Exception("Restaurant Bulunumadı..!!");

            var image = rest.Images.FirstOrDefault(x => x.Id == new ObjectId((string) item.ImageId));
            if(image == null)
                throw new Exception("Resim Bulunamadı..!!");

            image.Statu = StatusType.Approved;

            await UpdateAsync(rest);
        }
        public async Task<List<ImageDTO>> GetApprovedImage() {
            var lst = new List<ImageDTO>();
            var rest = await _restaurantDal.GetAllAsync();

            if(rest == null)
                throw new Exception("Restaurant Bulunumadı..!!");

<<<<<<< HEAD

            foreach(var item in rest) {
                if(item.Images == null)
                    continue;
                foreach(var m in item.Images.Where(x => x.Statu == StatusType.Approved).ToList()) {
=======
            foreach (var item in rest)
            {
                foreach (var m in item.Images.Where(x=> x.Statu == StatusType.Approved).ToList())
                {
>>>>>>> 930b9f9... image
                    lst.Add(new ImageDTO() {
                        Id = m.Id.ToString(),
                        Statu = m.Statu,
                        Data = Convert.ToBase64String(m.Image),
                        Info = m.Description,
                        RestName = item.Name,
                        RestId = item.Id.ToString()
                    });
                }
            }
            return lst;
        }

        public async Task<List<ImageDTO>> GetWatingApprovalImage() {
            var lst = new List<ImageDTO>();
            var rest = await _restaurantDal.GetAllAsync();

            if(rest == null)
                throw new Exception("Restaurant Bulunumadı..!!");


<<<<<<< HEAD
            foreach(var item in rest) {
                if(item.Images == null)
                    continue;
                foreach(var m in item.Images.Where(x => x.Statu == StatusType.WaitApproval).ToList()) {
                    lst.Add(new ImageDTO() {
=======
            foreach (var item in rest)
            {
                foreach (var m in item.Images.Where(x => x.Statu == StatusType.WaitApproval).ToList())
                {
                    lst.Add(new ImageDTO()
                    {
>>>>>>> 930b9f9... image
                        Id = m.Id.ToString(),
                        Statu = m.Statu,
                        Data = Convert.ToBase64String(m.Image),
                        Info = m.Description,
                        RestName = item.Name,
                        RestId = item.Id.ToString()
                    });
                }
            }
            return lst;
        }


        public async Task DeleteImageAsync(object restId, object imgId) {
            var rest = await _restaurantDal.GetAsync(x => x.Id == new MongoDB.Bson.ObjectId((string) restId));

            if(rest == null)
                throw new Exception("Restaurant Bulunumadı..!!");

            var img = rest.Images.FirstOrDefault(x => x.Id == new ObjectId((string) imgId));

            if(img == null)
                throw new Exception("Resim Bulunumadı..!!");

            rest.Images.Remove(img);

            await _restaurantDal.UpdateAsync(x => x.Id == rest.Id, rest);
        }

        public async Task DeleteRangeAsync(List<object> ids) {
            foreach(var item in ids) {
                await DeleteAsync(new Restaurant { Id = new MongoDB.Bson.ObjectId((string) item) });
            }
        }

        public async Task<Restaurant> FirstOrDefaultAsync(Expression<Func<Restaurant, bool>> filter) {
            return await _restaurantDal.GetAsync(filter);
        }


        public async Task<IEnumerable<RestaurantListDTO>> GetAdminRestaurantList() {
            var list = await _restaurantDal.GetAllAsync();
            return list.Select(x => new RestaurantListDTO { isPromo = x.isPromo, isActive = x.isActive, Adress = x.Info.Adress.AdressDetail, Email = x.Email, Id = x.Id.ToString(), Name = x.Name, Password = x.Password, UserName = x.UserName });

        }

        public async Task<IEnumerable<Restaurant>> GetAllAsync() {
            return await _restaurantDal.GetAllAsync();
        }

        public async Task<IEnumerable<ImageDTO>> GetImages(object Id) {
            var result = await _restaurantDal.GetAsync(x => x.Id == new MongoDB.Bson.ObjectId((string) Id));

            if(result == null)
                throw new Exception("Restaurant Bulunumadı..!!");

            if(result.Images == null)
                result.Images = new List<Images>();

            return result.Images.Select(x => new ImageDTO { Info = x.Description, Data = Convert.ToBase64String(x.Image), Id = x.Id.ToString() });
        }

        public async Task<LoginDTO> LoginAsync(string userName, string password) {
            var result = new LoginDTO();
            var rest = await _restaurantDal.GetAsync(x => x.UserName == userName && x.Password == password);
            if(rest == null)
                rest = await _restaurantDal.GetAsync(x => x.Email == userName && x.Password == password);

            result.Id = rest.Id.ToString();
            result.Name = rest.Name;

            return result;
        }

        public async Task AddUpdatePromotion(PromotionDTO item, object Id) {
            var rest = await FirstOrDefaultAsync(x => x.Id == new ObjectId((string) Id));
            if(rest == null)
                throw new Exception("Restaurant Bulunumadı..!!");
            bool isNew = false;

            var pro = rest.Promotion.FirstOrDefault(x => x.Id == new ObjectId((string) item.Id));
            if(pro == null) {
                isNew = true;
                pro = new Promotion();

            }
            if(isNew)
                rest.Promotion.Add(new Promotion() { Claim = item.Claim, Id = ObjectId.GenerateNewId(), ProductId = item.ProductId, Status = item.Status });
            else {
                pro.Claim = item.Claim;
                pro.ProductId = item.ProductId;
                pro.Status = item.Status;
            }
            await UpdateAsync(rest);
        }

        public async Task AddUpdateProduct(ProductDTO item, object Id) {
            var rest = await FirstOrDefaultAsync(x => x.Id == new ObjectId((string) Id));
            if(rest == null)
                throw new Exception("Restaurant Bulunumadı..!!");
            bool isNew = false;

            var pro = rest.Product.FirstOrDefault(x => x.Id == new ObjectId((string) item.Id));
            if(pro == null) {
                isNew = true;
                pro = new Product();

            }
            if(isNew)
                rest.Product.Add(new Product() { Description = item.Description, DueDate = item.DueDate, Id = ObjectId.GenerateNewId(), Status = item.Status });
            else {
                pro.Description = item.Description;
                pro.DueDate = item.DueDate;
                pro.Status = item.Status;
            }
            await UpdateAsync(rest);
        }

        public async Task ApprovedPromotion(PromotionApprovedDTO item) {
            var rest = await FirstOrDefaultAsync(x => x.Id == new ObjectId(item.restId));
            if(rest == null)
                throw new Exception("Restaurant Bulunumadı..!!");

            var pro = rest.Promotion.FirstOrDefault(x => x.Id == new ObjectId((string) item.PromId));
            if(pro == null)
                throw new Exception("Promosyon Bulunumadı..!!");

            pro.Status = StatusType.Approved;
            await UpdateAsync(rest);


        }


        public async Task ApprovedProduct(ProductApprovedDTO item) {
            var rest = await FirstOrDefaultAsync(x => x.Id == new ObjectId(item.restId));
            if(rest == null)
                throw new Exception("Restaurant Bulunumadı..!!");

            var pro = rest.Product.FirstOrDefault(x => x.Id == new ObjectId(item.ProdId));

            if(pro == null)
                throw new Exception("Ürün Bulunumadı..!!");

            pro.Status = StatusType.Approved;
            await UpdateAsync(rest);

        }

        public async Task<IEnumerable<WaitApprovalItemDTO>> GetWaitingApprovalProduct() {
            var list = new List<WaitApprovalItemDTO>();
            var rests = await GetAllAsync();

            var product = rests.Where(x => x.Product.Any(z => z.Status == StatusType.WaitApproval));

            foreach(var item in product) {
                foreach(var pro in item.Product) {
                    if(pro.Status == StatusType.WaitApproval) {
                        list.Add(new WaitApprovalItemDTO() {
                            ProductId = pro.Id.ToString(),
                            RestId = item.Id.ToString(),
                            ProductName = pro.Description,
                            RestName = item.Name,
                            Status = pro.Status.ToString(),
                            Claim = 0
                        });
                    }
                }
            }
            return list;
        }

        public async Task<IEnumerable<WaitApprovalItemDTO>> GetWaitingApprovalPromotion() {
            var list = new List<WaitApprovalItemDTO>();

            var rests = await GetAllAsync();
            var promotion = rests.Where(x => x.Promotion.Any(z => z.Status == StatusType.WaitApproval));

            foreach(var item in promotion) {
                foreach(var pro in item.Promotion) {
                    if(pro.Status == StatusType.WaitApproval) {
                        var prod = item.Product.FirstOrDefault(x => x.Id == (ObjectId) pro.ProductId);

                        list.Add(new WaitApprovalItemDTO() {
                            ProductId = prod.Id.ToString(),
                            RestId = item.Id.ToString(),
                            PromotionId = pro.Id.ToString(),
                            ProductName = prod.Description,
                            RestName = item.Name,
                            Status = pro.Status.ToString(),
                            Claim = pro.Claim
                        });
                    }
                }
            }
            return list;
        }

        public async Task<IEnumerable<WaitApprovalItemDTO>> GetApprovedProduct() {
            var list = new List<WaitApprovalItemDTO>();
            var rests = await GetAllAsync();

            var product = rests.Where(x => x.Product.Any(z => z.Status == StatusType.Approved));

            foreach(var item in product) {
                foreach(var pro in item.Product) {
                    if(pro.Status == StatusType.Approved) {
                        list.Add(new WaitApprovalItemDTO() {
                            ProductId = pro.Id.ToString(),
                            RestId = item.Id.ToString(),
                            PromotionId = pro.Id.ToString(),
                            ProductName = pro.Description,
                            RestName = item.Name,
                            Status = pro.Status.ToString(),
                            Claim = 0
                        });
                    }
                }
            }
            return list;
        }

        public async Task<IEnumerable<WaitApprovalItemDTO>> GetApprovedPromotion() {
            var list = new List<WaitApprovalItemDTO>();

            var rests = await GetAllAsync();
            var promotion = rests.Where(x => x.Promotion.Any(z => z.Status == StatusType.Approved));

            foreach(var item in promotion) {
                foreach(var pro in item.Promotion) {
                    if(pro.Status == StatusType.Approved) {
                        var prod = item.Product.FirstOrDefault(x => x.Id == (ObjectId) pro.ProductId);

                        list.Add(new WaitApprovalItemDTO() {
                            ProductId = prod.Id.ToString(),
                            RestId = item.Id.ToString(),
                            PromotionId = pro.Id.ToString(),
                            ProductName = prod.Description,
                            RestName = item.Name,
                            Status = pro.Status.ToString(),
                            Claim = pro.Claim
                        });
                    }
                }
            }
            return list;
        }

        public async Task QuickSaveAsync(RestaurantQuickSaveDTO entity) {
            var id = entity.Id == null ? new MongoDB.Bson.ObjectId() : new MongoDB.Bson.ObjectId(entity.Id);
            var r = await FirstOrDefaultAsync(x => x.Id == id);
            bool isNew = false;
            if(r == null) {
                r = new Restaurant();
                isNew = true;
                r.Categories = new List<Categories>();
                r.Product = new List<Product>();
                r.Promotion = new List<Promotion>();
            }

            r.Name = entity.Name;
            r.Email = entity.Email;
            r.Password = entity.Password;
            r.UserName = entity.UserName;
            r.isActive = entity.isActive;
            r.isPromo = entity.isPromo;
            if(!string.IsNullOrEmpty((string) entity.ContractId))
                r.ContractId = new ObjectId((string) entity.ContractId);

            if(entity.Product != null)
                foreach(var item in entity.Product) {
                    r.Product.Add(new Product() { Id = ObjectId.GenerateNewId(), Description = item.Description, DueDate = item.DueDate, Status = StatusType.Approved });
                }

            if(entity.Promotion != null)
                foreach(var item in entity.Promotion) {
                    try {
                        var tmpProd = entity.Product.FirstOrDefault(z => z.Id.ToString() == item.ProductId.ToString());
                        var prodId = r.Product.FirstOrDefault(x => x.Description == tmpProd.Description);

                        r.Promotion.Add(new Promotion() { Id = ObjectId.GenerateNewId(), Status = StatusType.Approved, Claim = item.Claim, ProductId = prodId.Id });
                    } catch(Exception ex) {
                        throw ex;
                    }
                }

            if(entity.Categories != null)
                foreach(var item in entity.Categories) {
                    r.Categories.Add(new Categories() { Id = ObjectId.GenerateNewId(), Definition = item.Definition });
                }

            r.Info = (new Info() { Adress = new Adress { AdressDetail = entity.Adress } });

            if(isNew)
                await Add(r);
            else
                await UpdateAsync(r);
        }

        public async Task UpdateAsync(Restaurant entity) {
            await _restaurantDal.UpdateAsync(x => x.Id == entity.Id, entity);
        }

        public async Task<IEnumerable<Restaurant>> WhereAsync(Expression<Func<Restaurant, bool>> filter) {
            return await _restaurantDal.GetAllAsync(filter);
        }
    }
}
