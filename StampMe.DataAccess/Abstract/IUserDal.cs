using System;
using StampMe.Core.DataAccess;
using StampMe.Entities.Concrete;

namespace StampMe.DataAccess.Abstract
{
    public interface IUserDal : IRepository<User>
    {
    }

}
