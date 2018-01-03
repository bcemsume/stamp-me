using MongoDB.Bson;
using StampMe.Core.Entities;

namespace StampMe.Entities.Concrete
{
    public class Contract : IEntity
    {
        public ObjectId Id
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        public decimal Price
        {
            get;
            set;
        }
        public string Description
        {
            get;
            set;
        }
    }
}
