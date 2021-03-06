using Buisness_Layer.Interface;
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
    public class BookWishlistController : ControllerBase
    {
        public class WishListController : ControllerBase
        {
            private IWishlistBL wishlistBL;
            private IMemoryCache memoryCache;
            private IDistributedCache distributedCache;

            public WishListController(IWishlistBL wishlistBL, IMemoryCache memoryCache, IDistributedCache distributedCache)
            {
                this.wishlistBL = wishlistBL;
                this.memoryCache = memoryCache;
                this.distributedCache = distributedCache;
            }

            [HttpPost("AddWishlist")]
            public IActionResult AddInWishlist(int bookId)
            {
                try
                {
                    int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                    var result = this.wishlistBL.AddInWishlist(bookId, userId);
                    if (result.Equals("Successfully Book added in Wishlist"))
                    {
                        return this.Ok(new { Status = true, Message = result });
                    }
                    else if (result.Equals("Book is Already in Wishlist"))
                    {
                        return this.Ok(new { Status = true, Message = result });
                    }
                    else
                    {
                        return this.BadRequest(new { Status = false, Message = result });
                    }
                }
                catch (Exception ex)
                {
                    return this.BadRequest(new { Success = false, message = ex.Message });
                }
            }
            [HttpDelete("DeleteFromWishlist")]
            public IActionResult DeleteFromWishlist(int wishlistId)
            {
                try
                {
                    int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                    if (this.wishlistBL.DeleteFromWishlist(userId, wishlistId))
                    {
                        return this.Ok(new { Status = true, Message = "Successfully Deleted From Wishlist" });
                    }
                    else
                    {
                        return this.BadRequest(new { Status = false, Message = "Something Went Wrong" });
                    }
                }
                catch (Exception ex)
                {
                    return this.BadRequest(new { Success = false, message = ex.Message });
                }
            }
            [HttpGet("{UserId}/ GetAllRecordFromWishlist")]
            public IActionResult GetAllRecordFromWishlist()
            {
                try
                {
                    int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                    var wishData = this.wishlistBL.GetAllFromWishlist(userId);
                    if (wishData != null)
                    {
                        return this.Ok(new { success = true, message = "All Wishlist Records Fetched Successfully ", response = wishData });
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
}
