using MongoDB.Bson;
using StampMe.Core.Entities;

namespace StampMe.Entities.Concrete
{
    public class Categories : IEntity
    {
        public ObjectId Id
        {
            get;
            set;
        }
        public string Definition
        {
            get;
            set;
        }
    }
}
