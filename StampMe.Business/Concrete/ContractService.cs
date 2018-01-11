using StampMe.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using StampMe.Common.CustomDTO;
using StampMe.DataAccess.Abstract;
using StampMe.Entities.Concrete;
using System.Linq;
using MongoDB.Bson;

namespace StampMe.Business.Concrete
{
    public class ContractService : IContractService
    {
        IContractDal _contractDal;
        public ContractService(IContractDal contractDal)
        {
            _contractDal = contractDal;
        }


    }
}
