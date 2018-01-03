using System;
namespace StampMe.Common.CustomDTO
{
    public class PromotionDTO
    {
        public object Id
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
}
