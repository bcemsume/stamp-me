using System;
using StampMe.Core.DataAccess.Context.MongoDB;
using StampMe.Core.DataAccess.MongoDB;
using StampMe.DataAccess.Abstract;
using StampMe.Entities.Concrete;

namespace StampMe.DataAccess.Concrete
{
    public class MongoRestaurantDal: MongoDBRepositoryBase<Restaurant, MongoContext<Restaurant>>, IRestaurantDal
    {
        
    }
}
