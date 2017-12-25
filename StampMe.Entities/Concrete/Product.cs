using System;
using StampMe.Core.Entities;
using MongoDB.Bson;


namespace StampMe.Entities.Concrete
{
    public class Product : IEntity
    {
        public Product()
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
        public string Description
        {
            get;
            set;
        }
        public DateTime DueDate
        {
            get;
            set;
        }
    }
}
