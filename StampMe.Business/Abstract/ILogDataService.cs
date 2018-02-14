using System;
using System.Collections.Generic;
using StampMe.Entities.Concrete;
using System.Threading.Tasks;
using System.Linq.Expressions;
using StampMe.Common.CustomDTO;

namespace StampMe.Business.Abstract
{
    public interface ILogDataService
    {
        Task Add(LogDataDTO entity);

    }
}
