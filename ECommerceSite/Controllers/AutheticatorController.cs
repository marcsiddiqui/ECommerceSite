using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerceSite.DbConfig;
using ECommerceSite.LogicLayer;
using ECommerceSite.Models;

namespace ECommerceSite.Controllers
{
    public class AutheticatorController : Controller
    {
        // GET: Autheticator
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserModel model)
        {
            // LINQ = Line Integrated Query
            var userFromDb = Authentication.LogIn(model.UserName, model.Password);
            if (userFromDb != null)
            {
                return RedirectToAction("AdminArea", "AdminDashboard");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Logout()
        {
            Session.Remove("UserName");
            Session.Remove("IsAdmin");
            Session.Remove("Email");
            Session.Remove("LastLoginonUtc");
            Session.Remove("FullName");

            return RedirectToAction("Login");
        }
    }
}