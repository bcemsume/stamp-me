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

        public async Task Add(ContractDTO entity)
        {
            var contract = new Contract();

            contract.Description = entity.Description;
            contract.Id = ObjectId.GenerateNewId();
            contract.Price = entity.Price;
            contract.Type = entity.Type;

            await _contractDal.AddAsync(contract);
        }

        public async Task DeleteAsync(ContractDTO entity)
        {
            await _contractDal.DeleteAsync(x => x.Id == new ObjectId((string)entity.Id));
        }

        public async Task<IEnumerable<ContractDTO>> GetAllAsync()
        {
            var result = await _contractDal.GetAllAsync();

            return result.Select(x => new ContractDTO
            {
                Description = x.Description,
                Id = x.Id.ToString(),
                Price = x.Price,
                Type = x.Type
            }).ToList();
        }

        public async Task UpdateAsync(ContractDTO entity)
        {
            var contract = new Contract { Id = new ObjectId((string)entity.Id), Description = entity.Description, Price = entity.Price, Type = entity.Type };
            await _contractDal.UpdateAsync(x => x.Id == new ObjectId((string)entity.Id), contract);
        }
    }
}
