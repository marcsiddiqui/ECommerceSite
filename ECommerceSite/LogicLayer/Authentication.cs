using ECommerceSite.DbConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerceSite.LogicLayer
{
  
    public class Authentication
    {
        private static ECommerceSiteEntities dbObj = new ECommerceSiteEntities();
        
        public static SessionDetail LogIn(string username, string password)
        {
            if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
            {
                var userFromDb = dbObj.Users.Where(x => x.UserName == username && x.Password == password).FirstOrDefault();
                if (userFromDb != null)
                {
                    userFromDb.LastLoginonUtc = DateTime.UtcNow;
                    dbObj.SaveChanges();
                    HttpContext.Current.Session["UserName"] = userFromDb.UserName;
                    HttpContext.Current.Session["UserId"] = userFromDb.Id;
                    HttpContext.Current.Session["IsAdmin"] = userFromDb.IsAdmin;
                    HttpContext.Current.Session["Email"] = userFromDb.Email;
                    HttpContext.Current.Session["LastLoginonUtc"] = userFromDb.LastLoginonUtc;
                    HttpContext.Current.Session["FullName"] = userFromDb.FirstName + " " + userFromDb.MiddleName + " " + userFromDb.LastName;

                    return GetSessionDetail();
                }
            }
            return null;
        }

        public static SessionDetail GetSessionDetail()
        {
            try
            {
                if (HttpContext.Current.Session["UserName"] != null && 
                    !string.IsNullOrWhiteSpace(HttpContext.Current.Session["UserName"].ToString()))
                {
                    SessionDetail obj = new SessionDetail();
                    obj.UserName = HttpContext.Current.Session["UserName"].ToString();
                    obj.UserId = Convert.ToInt32(HttpContext.Current.Session["UserId"]);
                    obj.Email = HttpContext.Current.Session["Email"].ToString();
                    obj.FullName = HttpContext.Current.Session["FullName"].ToString();
                    obj.IsAdmin = Convert.ToBoolean(HttpContext.Current.Session["IsAdmin"]);
                    obj.LastLoginDate = Convert.ToDateTime(HttpContext.Current.Session["LastLoginonUtc"]);
                    return obj;
                }
            }
            catch 
            {

            }

            return null;
        }
    }

    public class SessionDetail
    {
        public SessionDetail()
        {

        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public DateTime LastLoginDate { get; set; }
        public bool IsAdmin { get; set; }
    }
}

