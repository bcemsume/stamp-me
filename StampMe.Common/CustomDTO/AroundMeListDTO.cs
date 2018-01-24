using System;
using System.Collections.Generic;
using System.Text;

namespace StampMe.Common.CustomDTO
{
    public class AroundMeListDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool isPromo { get; set; }
        public string Distance { get; set; }
        public byte[] Image { get; set; }
    }
}
