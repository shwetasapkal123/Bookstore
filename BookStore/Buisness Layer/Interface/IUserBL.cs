using Database_Layer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness_Layer.Interface
{
    public interface IUserBL
    {
        public UserModel Register(UserModel user);
        public UserLogin Login(string Email, string Password);
        public string ForgotPassword(string email);
        public bool ResetPassword(string email, string newPassword, string confirmPassword);
    }
}
