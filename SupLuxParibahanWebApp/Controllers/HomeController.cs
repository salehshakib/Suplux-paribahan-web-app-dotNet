using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SupLuxParibahanWebApp.Models;
using System.Threading;


namespace SupLuxParibahanWebApp.Controllers
{
    public class HomeController : Controller
    {
        SUPLUXDashboardEntities db = new SUPLUXDashboardEntities();
        public ActionResult Index()
        {
            if (Session["currentEmail"] != null)
            {
                String email = Session["currentEmail"].ToString();
                var getUser = db.UserTables.Where(temp => temp.userEmail.Equals(email)).FirstOrDefault();
                if (getUser != null)
                {
                    return View();
                }
                else { return View(); }
            }
            return View();
        }
        
        [HttpPost]
        public ActionResult goToBuslist(tripData tripData) {
            FromToData fromToData = new FromToData();
            fromToData.From = tripData.startingFrom; 
            fromToData.To = tripData.destination;

            
        
            TempData["fromto"]=fromToData;
            return RedirectToAction("List", "Bus");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}