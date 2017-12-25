using System;
using MongoDB.Bson;
using StampMe.Core.Entities;

namespace StampMe.Entities.Concrete
{
    public class Event : IEntity
    {
        public Event()
        {
        }

        public ObjectId Id
        {
            get;
            set;
        }
        public ObjectId RestaurantId
        {
            get;
            set;
        }
        public object EventInfo
        {
            get;
            set;
        }
    }
}
