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
            if (db.UserTables.Any(temp=>temp.userEmail==userTable.userEmail || temp.userPhoneNumber==userTable.userPhoneNumber))
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

                return RedirectToAction("Index", "Home");
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
            if (Session["currentEmail"] != null)
            {
                String email = Session["currentEmail"].ToString();
                var getUser = db.UserTables.Where(temp => temp.userEmail.Equals(email)).FirstOrDefault();
                if (getUser != null)
                {
                    return View(getUser);
                }
                else { return View(); }
            }
            return View();
        }

        [HttpPost]
        public ActionResult UpdateUserInfo(UserTable userTable)
        {
            String email = Session["currentEmail"].ToString();
            UserTable user = new UserTable();
            user = db.UserTables.SingleOrDefault(x=>x.userEmail.Equals(email));
            if (user != null) { 
                user.userName = userTable.userName.ToString();
                user.userGender = userTable.userGender.ToString();  
                user.userPhoneNumber = userTable.userPhoneNumber.ToString();
                user.userAddress = userTable.userAddress.ToString();
                user.userNID = userTable.userNID.ToString();
                db.Entry(user).State=System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UserProfile","Account");  //redirecting to userprofile once again
            }
            return View("~/Views/Account/UserProfile.cshtml"); // this line of code can destroy the program. Expected errors from here
        }

        [HttpPost]
        public ActionResult UpdateUserPassword(String currentPassword,String newPassword,String confirmPassword)
        {
            String email = Session["currentEmail"].ToString();
            UserTable user = new UserTable();
            user = db.UserTables.SingleOrDefault(x => x.userEmail.Equals(email));
            
            if (user != null)
            {
                if (user.userPassword.Equals(currentPassword))
                {
                    if (confirmPassword.Equals(newPassword)) 
                    {
                        user.userPassword = newPassword;
                        db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("UserProfile", "Account");
                    }
                }
            }

            return View("~/Views/Account/UserProfile.cshtml");// this line of code can destroy the program. Expected errors from here
        }
    }
}