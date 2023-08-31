using Role_based.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Role_based.Controllers
{
    public class HomeController : Controller
    {
        MVCDatabaseEntities mvcdb = new MVCDatabaseEntities();

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(User user) 
        {
            //MVCDatabaseEntities mvcdb = new MVCDatabaseEntities();
            Validate_User_Result roleUser = mvcdb.Validate_User(user.Username, user.Password).FirstOrDefault();
            string message = string.Empty;
            switch (roleUser.UserId.Value)
            {
                case -1:
                    message = "Username / Password is incorrect.";
                    break;
                case -2:
                    message = "Account has not been activated.";
                    break;
                default:
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, user.Username, DateTime.Now, DateTime.Now.AddMinutes(30), false, roleUser.Roles, FormsAuthentication.FormsCookiePath);
                    string hash = FormsAuthentication.Encrypt(ticket);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
                    if (ticket.IsPersistent)
                    {
                        cookie.Expires = ticket.Expiration;
                    }
                    Response.Cookies.Add(cookie);
                    return RedirectToAction("UserDetails");
            }
            ViewBag.Message = message;
            return View(user);
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Register(User newUser)
        {
            if (ModelState.IsValid)
            {
                mvcdb.Users.Add(newUser);
                mvcdb.SaveChanges();
                return RedirectToAction("Login");
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult UserDetails()
        {
            List<User> user = mvcdb.Users.ToList();
            return View(user);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}