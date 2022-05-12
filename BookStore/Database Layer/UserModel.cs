using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Database_Layer
{
    public class UserModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage ="Full Name required.")]
        public string FullName { get; set; }

        [Required(ErrorMessage ="Address is required.")]
        public string Address { get; set; }

        [Required(ErrorMessage ="Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage ="Phone number is required")]
        public long Phone { get; set; }
    }
}
