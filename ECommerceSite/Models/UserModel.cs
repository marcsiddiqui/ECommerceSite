using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerceSite.Models
{
    public class UserModel
    {
        public UserModel()
        {

        }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}