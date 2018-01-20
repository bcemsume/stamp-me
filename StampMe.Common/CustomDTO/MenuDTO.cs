using System;
using System.Collections.Generic;
using System.Text;

namespace StampMe.Common.CustomDTO
{
    public class MenuDTO
    {
        public string Id { get; set; }
        public string RestId { get; set; }
        public byte[] Image { get; set; }
        public string MenuDetail { get; set; }

    }
}
