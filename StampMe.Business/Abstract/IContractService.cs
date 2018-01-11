using StampMe.Common.CustomDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StampMe.Business.Abstract
{
    public interface IContractService
    {
        Task<IEnumerable<ContractDTO>> GetAllAsync();
        Task Add(ContractDTO entity);
        Task DeleteAsync(ContractDTO entity);
        Task UpdateAsync(ContractDTO entity);
    }
}
