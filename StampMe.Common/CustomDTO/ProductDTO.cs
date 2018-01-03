using System;
namespace StampMe.Common.CustomDTO
{
    public class ProductDTO
    {
        public object Id
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

    public enum StatusType
    {
        WaitApproval,
        Approved
    }
}
