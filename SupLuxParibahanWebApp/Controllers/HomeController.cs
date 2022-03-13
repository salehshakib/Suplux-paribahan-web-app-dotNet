using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SupLuxParibahanWebApp.Models;
using System.Threading;
using System.Globalization;

namespace SupLuxParibahanWebApp.Controllers
{
    public class HomeController : Controller
    {
        SUPLUXDashboardEntities2 db = new SUPLUXDashboardEntities2();
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
        public ActionResult goToBuslist(string startingFrom, string destination, string date) {

            FromToData fromToData = new FromToData();
            fromToData.From = startingFrom; 
            fromToData.To = destination;

            string Date = date;
            string[] date2 = Date.Split('\'');
            Date = date2[0] + " " + date2[1];
            
            string format = "dd MMM yy";
            CultureInfo provider = CultureInfo.InvariantCulture;

            DateTime result = DateTime.ParseExact(Date, format, provider);
            Date = result.ToString("yyyy-MM-dd");

            fromToData.date = Date;

            Session["journeyDate"] = Date;
            


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