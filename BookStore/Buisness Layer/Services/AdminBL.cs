using Buisness_Layer.Interface;
using Database_Layer;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness_Layer.Services
{
    public class AdminBL:IAdminBL
    {
        private  IAminRL adminRL;
        public AdminBL(IAminRL adminRL)
        {
            this.adminRL = adminRL;
        }
        public AdminLogin AdminLogin(string email, string password)
        {
            try
            {
                return this.adminRL.AdminLogin(email, password);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
