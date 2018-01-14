using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StampMe.Business.Abstract;
using StampMe.Common.CustomDTO;

namespace StampMe.API.Controllers {
    [Route("api/[controller]/[action]")]
    [EnableCors("MyPolicy")]
    public class ContractController: Controller {
        IContractService _contractService;
        public ContractController(IContractService contractService) {
            _contractService = contractService;
        }

        [HttpGet]
        public async Task<IEnumerable<ContractDTO>> GetAllAsync() {
            var result = await _contractService.GetAllAsync();
            return result;
        }

        [HttpPost]
        public async Task Add([FromBody]ContractDTO entity) {
            await _contractService.Add(entity);
        }
        [HttpPost]
        public async Task DeleteAsync([FromBody]ContractDTO entity) {
            await _contractService.DeleteAsync(entity);
        }
        [HttpPost]
        public async Task UpdateAsync([FromBody]ContractDTO entity) {
            await _contractService.UpdateAsync(entity);
        }
    }
}