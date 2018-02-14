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


namespace StampMe.Business.Concrete
{
    public class LogDataService : ILogDataService
    {
        ILogDataDal _logDataDal;

        public LogDataService(ILogDataDal logDataDal)
        {
            _logDataDal = logDataDal;
        }

        public async Task Add(LogDataDTO entity)
        {
            await _logDataDal.AddAsync(new LogData()
            {
                CorrelationId = entity.CorrelationId,
                ElapsedTime = entity.ElapsedTime,
                Id = entity.Id,
                IpAdress = entity.IpAdress,
                LogDate = entity.LogDate,
                Message = entity.Message,
                RequestInfo = entity.RequestInfo,
                StatusCode = entity.StatusCode,
                Type = entity.Type
            });
        }
    }
}
