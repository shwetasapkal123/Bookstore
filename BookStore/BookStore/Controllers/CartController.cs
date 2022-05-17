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
    public class CartController : ControllerBase
    {
        private  ICartBL cartBL;
        private  IMemoryCache memoryCache;
        private  IDistributedCache distributedCache;
        public CartController(ICartBL cartBL, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.cartBL = cartBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }
        [Authorize]
        [HttpPost("AddCart")]
        public IActionResult AddCart(CartModel cart)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var cartData = this.cartBL.AddCart(cart, userId);
                if (cartData != null)
                {
                    return this.Ok(new { success = true, message = "Book Added SuccessFully in Cart ", response = cartData });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Cart Added Unsuccessfully" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, response = ex.Message });
            }
        }
        [Authorize]
        [HttpPut("UpdateCart")]
        public IActionResult UpdateCart(CartModel cart)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var cartData = this.cartBL.UpdateCart(cart, userId);
                if (cartData != null)
                {
                    return this.Ok(new { success = true, message = "Book Updated Successfully in Cart ", response = cartData });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Cart Updated failed" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, response = ex.Message });
            }
        }
        [Authorize]
        [HttpDelete("DeleteCart")]
        public IActionResult DeleteCart(int cartId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (this.cartBL.DeleteCart(cartId, userId))
                {
                    return this.Ok(new { success = true, message = "Book Deleted Successfully from Cart " });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Cart Deleted failed" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, response = ex.Message });
            }
        }
       [Authorize]
        [HttpGet("{UserId}/GetCart")]
        public IActionResult GetCart()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var cartData = this.cartBL.GetCartByUserId(userId);
                if (cartData != null)
                {
                    return this.Ok(new { success = true, message = "Cart Data Fetched Successfully ", response = cartData });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Enter Valid UserId" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, response = ex.Message });
            }
        }
    }
}
