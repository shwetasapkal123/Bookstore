using Database_Layer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness_Layer.Interface
{
    public interface IAdminBL
    {
        public AdminLogin AdminLogin(string email, string password);
    }
}
