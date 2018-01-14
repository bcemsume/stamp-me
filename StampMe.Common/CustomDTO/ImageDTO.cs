using System;
namespace StampMe.Common.CustomDTO
{
    public class ImageDTO
    {
        public string Id
        {
            get;
            set;
        }
        public string RestName { get; set; }
        public string Info
        {
            get;
            set;
        }
        public string Data
        {
            get;
            set;
        }
        public StatusType Statu { get; set; }
    }
}
