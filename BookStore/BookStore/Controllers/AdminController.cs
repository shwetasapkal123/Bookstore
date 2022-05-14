using Buisness_Layer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminBL adminBL;
        public AdminController(IAdminBL adminBL)
        {
            this.adminBL = adminBL;
        }
        [HttpPost("AdminLogin")]
        public IActionResult AdminLogin(string email, string password)
        {
            try
            {
                var loginToken = this.adminBL.AdminLogin(email, password);
                if (loginToken != null)
                {
                    return this.Ok(new { Success = true, message = "Admin Login Sucessfully", Response = loginToken });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Admin Login Failed" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
    }
}
