using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SupLuxParibahanWebApp.Models;

namespace SupLuxParibahanWebApp.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult SignUp()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult SignUp(UserTable userTable)
        {

            return RedirectToAction("Login");
        }

        public ActionResult LogIn()
        {
            return View();


        }

        public ActionResult UserProfile()
        {
            return View();
        }
    }
}