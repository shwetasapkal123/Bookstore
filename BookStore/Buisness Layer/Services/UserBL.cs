using Buisness_Layer.Interface;
using Database_Layer;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness_Layer.Services
{
    public class UserBL:IUserBL
    {
        private  IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
        //method for user registration
        public UserModel Register(UserModel user)
        {
            try
            {
                return this.userRL.Register(user);
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Method for user login
        public UserLogin Login(string Email, string Password)
        {
            try
            {
                return this.userRL.Login(Email, Password);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string ForgotPassword(string email)
        {
            try
            {
                return this.userRL.ForgotPassword(email);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool ResetPassword(string email, string newPassword, string confirmPassword)
        {
            try
            {
                return this.userRL.ResetPassword(email, newPassword, confirmPassword);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
