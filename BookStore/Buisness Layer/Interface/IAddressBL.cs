using Database_Layer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness_Layer.Interface
{
    public interface IAddressBL
    {
        public string AddAddress(AddressModel add, int userId);
        public AddressModel UpdateAddress(AddressModel add, int AddressId);
        public bool DeleteAddress(int addressId);
        public List<AddressModel> GetAllAddress(int userId);
    }
}
