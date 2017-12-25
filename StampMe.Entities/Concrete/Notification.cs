using System;
using StampMe.Core.Entities;
using MongoDB.Bson;

namespace StampMe.Entities.Concrete
{
    public class Notification : IEntity
    {
        public Notification()
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
        public ObjectId UserId
        {
            get;
            set;
        }
        public NotificationType NotificationType
        {
            get;
            set;
        }

    }

    public enum NotificationType
    {
        ScratchWin,
        FreeStamp,
        FreeCoffe
    }
}
