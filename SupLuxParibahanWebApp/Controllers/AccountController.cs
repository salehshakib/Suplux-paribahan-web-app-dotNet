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
        SUPLUXDashboardEntities db=new SUPLUXDashboardEntities();   
        // GET: Account
        public ActionResult SignUp()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult SignUp(UserTable userTable)
        {
            if (db.UserTables.Any(temp=>temp.userEmail==userTable.userEmail))
            {
                ViewBag.Notification = "This account already exists.";
                return View();
            }
            else {
                db.UserTables.Add(userTable);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            
        }

        public ActionResult LogIn()
        {
            return View();


        }

        [HttpPost]
        public ActionResult LogIn(UserTable userTable)
        {
            /*var checkLogin = db.UserTables.Where(temp=>temp.userEmail.Equals(userTable.userEmail) && temp.userPassword.Equals(userTable.userPassword));

            if (checkLogin!=null) {

                Session["currentEmail"]=userTable.userEmail.ToString();
                //Session["currentPassword"]=userTable.userPassword.ToString();

                return RedirectToAction("UserProfile");
            }
            else
            {
                return View();
            }*/

            if (db.UserTables.Any(temp => temp.userEmail.Equals(userTable.userEmail) && temp.userPassword.Equals(userTable.userPassword)))
            {

                Session["currentEmail"] = userTable.userEmail.ToString();
                //Session["currentPassword"]=userTable.userPassword.ToString();

                return RedirectToAction("UserProfile");
            }
            else
            {
                return View();
            }

        }

        public ActionResult LogOut() 
        {
            Session.Clear();
            return RedirectToAction("Index","Home");
        }


        public ActionResult UserProfile()
        {
            return View();
        }
    }
}