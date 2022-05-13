﻿using Buisness_Layer.Interface;
using Database_Layer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

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

        [HttpPost("login")]
        public IActionResult Login(string Email, string Password)
        {
            try
            {
                var result = this.userBL.Login(Email, Password);
                if (result != null)
                    return this.Ok(new { success = true, message = "Login Successful", data = result });
                else
                    return this.BadRequest(new { success = false, message = "Login UnSuccessful", data = result });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword(string email)
        {
            try
            {
                var forgotPasswordToken = this.userBL.ForgotPassword(email);
                if (forgotPasswordToken != null)
                {
                    return this.Ok(new { Success = true, message = " Mail Sent Successful", Response = forgotPasswordToken });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Enter Valid Email" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword(string newPassword, string confirmPassword)
        {
            try
            {
                var email = User.FindFirst("Email").Value.ToString();
                if (this.userBL.ResetPassword(email, newPassword, confirmPassword))
                {
                    return this.Ok(new { Success = true, message = " Password Changed Successfully " });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = " Password Change Unsuccessfully " });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
    }
}
