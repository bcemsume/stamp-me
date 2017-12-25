using System;
using StampMe.Core.Entities;
using MongoDB.Bson;

namespace StampMe.Entities.Concrete
{
    public class User : IEntity
    {
        public User()
        {
        }

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
        public string SocialToken
        {
            get;
            set;
        }
    }
}
