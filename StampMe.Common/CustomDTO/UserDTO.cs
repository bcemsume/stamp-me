using System;
using System.Collections.Generic;
using System.Text;

namespace StampMe.Common.CustomDTO
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public byte Gender { get; set; }
        public string Email { get; set; }
        public DateTime BirthDay { get; set; }
    }
}
