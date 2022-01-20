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
        private ECommerceSiteEntities dbObj = new ECommerceSiteEntities();

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
                if (userFromDb.IsAdmin)
                {
                    return RedirectToAction("AdminArea", "AdminDashboard");
                }
                else
                {
                    return RedirectToAction("Menu", "Products");
                }
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

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(UserModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password == model.ConfirmPassword)
                {
                    var user = new User();
                    user.UserName = model.UserName;
                    user.Password = model.Password;
                    user.FirstName = model.FirstName;
                    user.MiddleName = model.MiddleName;
                    user.LastName = model.LastName;
                    user.Email = model.Email;
                    user.IsAdmin = false;
                    user.LastLoginonUtc = DateTime.UtcNow;

                    dbObj.Users.Add(user);
                    dbObj.SaveChanges();

                    return RedirectToAction("Login");
                }
            }
            return View(model);
        }
    }
}