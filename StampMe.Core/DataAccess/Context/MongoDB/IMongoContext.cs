using System;
using MongoDB.Driver;

namespace StampMe.Core.DataAccess.Context.MongoDB
{
    public interface IMongoContext<T> where T : class
    {
        IMongoCollection<T> _db { get; }
    }
}
