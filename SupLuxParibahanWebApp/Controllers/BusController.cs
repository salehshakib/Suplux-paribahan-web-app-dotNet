using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SupLuxParibahanWebApp.Models;
using System.Globalization;

namespace SupLuxParibahanWebApp.Controllers
{
    public class BusController : Controller
    {
        SUPLUXDashboardEntities database = new SUPLUXDashboardEntities();

        // GET: Bus
        public ActionResult List()
        {
            FromToData fromTo = TempData["fromto"] as FromToData;
            string from = fromTo.From;
            string to   = fromTo.To;

            Session["from"]=from;
            Session["to"]=to;   
            
            List<tripData> tripData = new List<tripData>();
            tripData=database.tripDatas.Where(x=>x.startingFrom.Equals(from) && x.destination.Equals(to)).ToList();

            return View(tripData);
        }

        public ActionResult Payment()
        {
            return View();
        }

        public ActionResult CancelTicket()
        {
            return View();
        }

        [HttpPost]
        public ActionResult List(string type)
        {
            string from = (string)Session["from"];
            string to = (string)Session["to"];
            List<tripData> tripData = new List<tripData>();
            if (type == "06-12")
            {

                string givenTime1 = "06:00AM";
                string givenTime2 = "12:00PM";
                string format = "h:mmtt";
                CultureInfo provider = CultureInfo.InvariantCulture;

                DateTime result1 = DateTime.ParseExact(givenTime1, format, provider);
                DateTime result2 = DateTime.ParseExact(givenTime2, format, provider);

                //string amOrPm = "%AM";
                //tripData = database.tripDatas.
                //tripData = database.tripDatas.Where(temp => temp.startingFrom.Equals(from) && temp.destination.Equals(to) && Convert.ToDateTime(temp.departureTime)>= givenTime1 && Convert.ToDateTime(temp.departureTime) <= givenTime1).ToList();
                return View(tripData);
            }
            else
            {
                
                tripData = database.tripDatas.Where(temp => temp.startingFrom.Equals(from) && temp.destination.Equals(to) && temp.coachType.Equals(type)).ToList();
                return View(tripData);
            }
            

      
    
        }

        
    }
}