using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SupLuxParibahanWebApp.Models;

namespace SupLuxParibahanWebApp.Controllers
{
    public class AdminController : Controller
    {
        SUPLUXDashboardEntities database = new SUPLUXDashboardEntities();
        SUPLUXDashboardEntities db = new SUPLUXDashboardEntities();

        // GET: Admin
        public ActionResult AdminHome()
        {
            if (Session["currentEmail"] != null)
            {
                String email = Session["currentEmail"].ToString();
                var getAdmin = database.Admins.Where(temp=>temp.adminEmail.Equals(email)).FirstOrDefault();
                if (getAdmin != null) 
                {
                    return View(getAdmin);
                }
            }
            
            return View();
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }


        // GET: Bus List
        //[HttpPost]
        public ActionResult BusList()
        {
            if(Session["currentEmail"] !=null && Session["currentEmail"].ToString().Contains("@suplux.com"))
            {

                List<tripData> tripdatas = new List<tripData>();
                tripdatas = db.tripDatas.ToList();
            
                //List<tripData> tripdatas = db.tripDatas.ToList();
                //var tripdatas = database.tripDatas.ToList();
                return View( tripdatas);  
            }
           
            return View();
        }
        [HttpPost]
        public ActionResult BusList(tripData tripInfo)
        {

            return View();
        }


        // GET: Add New Bus
        public ActionResult AddNewBus()
        {
           
            return View();
        }

        // GET: Reroute Bus
        public ActionResult RerouteBus()
        {
            
            return View();
        }
    }
}