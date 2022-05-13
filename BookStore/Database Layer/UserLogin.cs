using System;
using System.Collections.Generic;
using System.Text;

namespace Database_Layer
{
    public class UserLogin
    {
        public string FullName { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
