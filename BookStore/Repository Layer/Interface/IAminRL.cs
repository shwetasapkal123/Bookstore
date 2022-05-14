using Database_Layer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interface
{
    public interface IAminRL
    {
        public AdminLogin AdminLogin(string email, string password);
    }
}
