using System;
using System.Collections.Generic;
using System.Text;

namespace StampMe.Common.CustomDTO
{
    public class RewardDTO
    {
        public string RestId { get; set; }
        public string RestName { get; set; }
        public int Claim { get; set; }
        public bool isUsed { get; set; }
        public DateTime StampDate { get; set; }

    }
}
