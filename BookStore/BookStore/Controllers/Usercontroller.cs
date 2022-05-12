using Buisness_Layer.Interface;
using Database_Layer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Usercontroller : ControllerBase
    {
        private  IUserBL userBL;
        const string SessionFullName = "FullName";
        const string SessionEmail = "Email";
        public Usercontroller(IUserBL userBL)
        {
            this.userBL = userBL;
        }
        [HttpPost("Register")]
        public IActionResult AddUser(UserModel userRegistration)
        {
            try
            {
                HttpContext.Session.SetString(SessionFullName, userRegistration.FullName);
                HttpContext.Session.SetString(SessionEmail, userRegistration.Email);
                var user = this.userBL.Register(userRegistration);
                if (user != null)
                {
                    var name = HttpContext.Session.GetString(SessionFullName);
                    var email = HttpContext.Session.GetString(SessionEmail);
                    return this.Ok(new { Success = true, message = "User Added Sucessfully", Response = user });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "User Added Unsuccessfully" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
    }
}
