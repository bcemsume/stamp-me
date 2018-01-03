using System.Collections.Generic;
using StampMe.Core.Entities;
using MongoDB.Bson;
using System;
using StampMe.Common.CustomDTO;

namespace StampMe.Entities.Concrete
{
    public class Restaurant : IEntity
    {
        public Restaurant()
        {
        }

        public ObjectId Id
        {
            get;
            set;
        }
        public string Name
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
        public string Email
        {
            get;
            set;
        }
        public List<Images> Images
        {
            get;
            set;
        }
        public List<Images> Notice
        {
            get;
            set;
        }

        public Info Info
        {
            get;
            set;
        }
        public List<Product> Product
        {
            get;
            set;
        }
        public List<Promotion> Promotion
        {
            get;
            set;
        }

        public List<Categories> Categories { get; set; }
        public Contract Contract
        {
            get;
            set;
        }

        public string FacebookToken
        {
            get;
            set;
        }
        public bool isPromo
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
    public class Info
    {
        public string WorkingHours
        {
            get;
            set;
        }
        public Adress Adress
        {
            get;
            set;
        }
        public Menu Menu
        {
            get;
            set;
        }
    }
    public class Menu
    {
        public List<Images> Image
        {
            get;
            set;
        }

        public string MenuDetail
        {
            get;
            set;
        }
    }
    public class Promotion
    {
        public ObjectId Id
        {
            get;
            set;
        }
        public object ProductId
        {
            get;
            set;
        }
        public StatusType Status
        {
            get;
            set;
        }
        public byte Claim
        {
            get;
            set;
        }
    }

    public class Adress
    {
        public string Lang
        {
            get;
            set;
        }
        public string Latd
        {
            get;
            set;
        }
        public string AdressDetail
        {
            get;
            set;
        }
    }
    public class Images
    {
        public ObjectId Id
        {
            get;
            set;
        }
        public string Description
        {
            get;
            set;
        }
        public byte[] Image
        {
            get;
            set;
        }
    }

    public class Product
    {
        public ObjectId Id
        {
            get;
            set;
        }
        public string Description
        {
            get;
            set;
        }
        public StatusType Status
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
