﻿using Buisness_Layer.Interface;
using Database_Layer;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness_Layer.Services
{
    public class AddressBL:IAddressBL
    {
        private  IAddressRL addressRL;
        public AddressBL(IAddressRL addressRL)
        {
            this.addressRL = addressRL;
        }
        public string AddAddress(AddressModel add, int userId)
        {
            try
            {
                return this.addressRL.AddAddress(add, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public AddressModel UpdateAddress(AddressModel add, int AddressId)
        {
            try
            {
                return this.addressRL.UpdateAddress(add, AddressId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteAddress(int addressId)
        {
            try
            {
                return this.addressRL.DeleteAddress(addressId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<AddressModel> GetAllAddress(int userId)
        {
            try
            {
                return this.addressRL.GetAllAddress(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
