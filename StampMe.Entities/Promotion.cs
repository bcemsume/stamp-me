using MongoDB.Bson;
using StampMe.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StampMe.Entities
{
    public class Promotion : IEntity
    {
        public ObjectId Id { get; set; }
        public ObjectId RestId { get; set; }
        public ObjectId PromId { get; set; }
    }
}
