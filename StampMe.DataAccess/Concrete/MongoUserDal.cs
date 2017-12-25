using System;
using StampMe.Core.DataAccess;
using StampMe.Core.DataAccess.Context.MongoDB;
using StampMe.Core.DataAccess.MongoDB;
using StampMe.DataAccess.Abstract;
using StampMe.Entities.Concrete;

namespace StampMe.DataAccess.Concrete
{
    public class MongoUserDal : MongoDBRepositoryBase<User, MongoContext<User>>, IUserDal
    {

    }
}
