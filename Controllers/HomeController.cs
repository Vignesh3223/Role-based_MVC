using Role_based.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
                    return RedirectToAction("Welcome");
            }
            ViewBag.Message = message;
            return View(user);
        }

        public ActionResult Welcome()
        {
            return View();
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
                return RedirectToAction("Index");
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult UserDetails()
        {
            List<User> user = mvcdb.Users.ToList();
            return View(user);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            User user = mvcdb.Users.Find(id);
            return View(user);
        }

        public ActionResult Edit(int? id)
        {
            User user = mvcdb.Users.Find(id);
            List<Role> role = mvcdb.Roles.ToList();
            ViewBag.Roles = new SelectList(role, "RoleID", "RoleName");
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,Username,RoleID,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                mvcdb.Entry(user).State = System.Data.Entity.EntityState.Modified;
                mvcdb.SaveChanges();
                return RedirectToAction("UserDetails");
            }
            return View();
        }
        public ActionResult Delete(int? id)
        {
            User user = mvcdb.Users.Find(id);
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = mvcdb.Users.Find(id);
            mvcdb.Users.Remove(user);
            mvcdb.SaveChanges();
            return RedirectToAction("UserDetails");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}