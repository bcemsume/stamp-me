using System;
using StampMe.Core.Entities;
using MongoDB.Bson;
using System.Collections.Generic;

namespace StampMe.Entities.Concrete
{
    public class User : IEntity
    {
        public ObjectId Id
        {
            get;
            set;
        }
        public string FirstName
        {
            get;
            set;
        }
        public string LastName
        {
            get;
            set;
        }
        public byte Gender
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
        public DateTime BirthDay
        {
            get;
            set;
        }
        public string Password
        {
            get;
            set;
        }
        public List<Reward> Reward
        {
            get;
            set;
        }
        public string SocialToken
        {
            get;
            set;
        }
    }

    public class Reward
    {

        public ObjectId Id
        {
            get;
            set;
        }

        public ObjectId PromotionId { get; set; }
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

        public DateTime StampDate
        {
            get;
            set;
        }
    }
}
