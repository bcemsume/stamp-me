using System;
namespace StampMe.Common.CustomDTO
{
    public class WaitApprovalItemDTO
    {
        public string RestName
        {
            get;
            set;
        }
        public string RestId { get; set; }

        public string ProductName
        {
            get;
            set;
        }
        public string ProductId { get; set; }
        public string Status
        {
            get;
            set;
        }
        public string PromotionId { get; set; }
        public byte Claim
        {
            get;
            set;
        }
    }
}
