using System;
using MongoDB.Driver;

namespace StampMe.Core.DataAccess.Context.MongoDB
{
    public class MongoContext<T>
    {
        private readonly IMongoDatabase _database = null;

        public MongoContext()
        {
            var client = new MongoClient("mongodb://127.0.0.1:27017/");
            if (client != null)
                _database = client.GetDatabase("StampMeDB");
        }

        public IMongoCollection<T> Db
        {
            get
            {
                return _database.GetCollection<T>(typeof(T).Name);
            }
        }

        public IMongoCollection<T> HistoryDb
        {
            get
            {
                return _database.GetCollection<T>(string.Format("{0}_{1}", typeof(T).Name, "History"));
            }
        }

    }
}
