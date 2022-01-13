using ECommerceSite.LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerceSite.Controllers
{
    public class AdminDashboardController : Controller
    {
        // GET: AdminDashboard
        public ActionResult AdminArea()
        {
            if (Authentication.GetSessionDetail() == null)
            {
                return RedirectToAction("Login", "Autheticator");
            }
            return View();
        }
    }
}