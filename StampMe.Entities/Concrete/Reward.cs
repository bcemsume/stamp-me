using System;
using StampMe.Core.Entities;
using MongoDB.Bson;


namespace StampMe.Entities.Concrete
{
    public class Reward : IEntity
    {
        public Reward()
        {
        }

        public ObjectId Id
        {
            get;
            set;
        }

        public ObjectId UserId
        {
            get;
            set;
        }

        public ObjectId RestaurantId
        {
            get;
            set;
        }

        public ObjectId ProductId
        {
            get;
            set;
        }

        public byte ClaimCount
        {
            get;
            set;
        }

        public bool isUsed
        {
            get;
            set;
        }
    }
}
