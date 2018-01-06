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

        public string ProductName
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
