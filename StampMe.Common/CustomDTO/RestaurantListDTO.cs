using System;
using MongoDB.Bson;

namespace StampMe.Common.CustomDTO
{
    public class RestaurantListDTO
    {
        public object Id
        {
            get;
            set;
        }
       public string Name
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }
        public string UserName
        {
            get;
            set;
        }
        public string Password
        {
            get;
            set;
        }
        public string Adress
        {
            get;
            set;
        }
        public bool isActive
        {
            get;
            set;
        }
    }
}
