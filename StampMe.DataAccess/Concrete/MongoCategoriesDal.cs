using StampMe.Core.DataAccess.Context.MongoDB;
using StampMe.Core.DataAccess.MongoDB;
using StampMe.DataAccess.Abstract;
using StampMe.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace StampMe.DataAccess.Concrete
{
    public class MongoCategoriesDal : MongoDBRepositoryBase<Categories, MongoContext<Categories>>, ICategoriesDal
    {
        
    }
}