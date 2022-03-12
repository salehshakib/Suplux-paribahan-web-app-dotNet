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
        PaymentInfo paymentInfo = new PaymentInfo();

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
            return View(paymentInfo);
        }

        public ActionResult CancelTicket()
        {
            return View();
        }

        [HttpPost]
        public ActionResult goToPayment(string contype) {
            
            paymentInfo.coachNo= contype;
            if (contype.Contains("M"))
            {
                paymentInfo.coachType = "AC (Multi)";
            }
            else if (contype.Contains("B"))
            {
                paymentInfo.coachType = "AC (Bi)";
            }
            else 
            {
                paymentInfo.coachType = "NON-AC";
            }
            return RedirectToAction("Payment","Bus");
        }

        [HttpPost]
        public ActionResult GetJourneyData(string from, string to, string date)
        {
            paymentInfo.startPoint = from;
            paymentInfo.destination = to;
            paymentInfo.tripDate = date;

            
            //card generation here
            return Json(date);

        }

        [HttpPost]
        public ActionResult GetSelectedSeatsData(string[] seats, string totalFare)
        {
            paymentInfo.totalFare = totalFare;
            //paymentInfo.seat = seats;

            //foreach(var seat in seats)
                //paymentInfo.seat.Add(seat);// = seat; 
            
            //Selected tables are fetching here 
            return Json(totalFare);

        }

        [HttpPost]
        public ActionResult List(string type)
        {
            string from = (string)Session["from"];
            string to = (string)Session["to"];
            List<tripData> tripData = new List<tripData>();
            if (type == "06-12")
            {

                string givenTime1 = "08";
                string givenTime2 = "12";
                string amOrPm = "%AM";

                var sqlQuery = "Select * from tripData where startingFrom = '" + from + "' and destination = '" + to + "' and departureTime like '" + amOrPm + "' and departureTime between '" + givenTime1 + "' and '" + givenTime2 + "' ";
                tripData = database.tripDatas.SqlQuery(sqlQuery).ToList();
                return View(tripData);
            }
            else if (type == "12-18")
            {

                string givenTime1 = "04";
                string givenTime2 = "06";
                string amOrPm = "%PM";

                var sqlQuery = "Select * from tripData where startingFrom = '" + from + "' and destination = '" + to + "' and departureTime like '" + amOrPm + "' and departureTime between '" + givenTime1 + "' and '" + givenTime2 + "' ";
                tripData = database.tripDatas.SqlQuery(sqlQuery).ToList();
                return View(tripData);
            }
            else if (type == "18-00")
            {

                string givenTime1 = "08";
                string givenTime2 = "12";
                string amOrPm = "%PM";

                var sqlQuery = "Select * from tripData where startingFrom = '" + from + "' and destination = '" + to + "' and departureTime like '" + amOrPm + "' and departureTime between '" + givenTime1 + "' and '" + givenTime2 + "' ";
                tripData = database.tripDatas.SqlQuery(sqlQuery).ToList();
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