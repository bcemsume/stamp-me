using System;
using StampMe.Core.Entities;
using MongoDB.Bson;

namespace StampMe.Entities.Concrete
{
    public class LogData : IEntity
    {
        public ObjectId Id { get; set; }
        public DateTime LogDate { get; set; }
        public string CorrelationId { get; set; }
        public string RequestInfo { get; set; }
        public string IpAdress { get; set; }
        public string Message { get; set; }
        public int Type { get; set; }
        public long ElapsedTime { get; set; }
        public string StatusCode { get; set; }
    }
}
