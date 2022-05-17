using Buisness_Layer.Interface;
using Database_Layer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        IAddressBL addressBL;
        public AddressController(IAddressBL addressBL)
        {
            this.addressBL = addressBL;
        }

        [Authorize]
        [HttpPost("AddAddress")]
        public IActionResult AddAddress(AddressModel addressModel)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var addressData = this.addressBL.AddAddress(addressModel, userId);
                if (addressData != null)
                {
                    return this.Ok(new { success = true, message = "Address Added SuccessFully ", response = addressData });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Address Added Unsuccessfully" });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("UpdateAddress")]
        public IActionResult UpdateAddress(AddressModel add, int AddressId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var addressData = this.addressBL.UpdateAddress(add, AddressId);
                if (addressData != null)
                {
                    return this.Ok(new { success = true, message = "Address Updated Successfully ", response = addressData });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Address Updated failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpDelete]
        public IActionResult DeleteAddress(int addressId)
        {
            try
            {
                int adrId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (this.addressBL.DeleteAddress(addressId))
                {
                    return this.Ok(new { success = true, message = "Address Deleted Successfully " });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Address Deleted failed" });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpGet]
       public IActionResult GetAllAddress()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var addressData = this.addressBL.GetAllAddress(userId);
                if (addressData != null)
                {
                    return this.Ok(new { success = true, message = "address Data Fetched Successfully ", response = addressData });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Enter Valid UserId" });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
