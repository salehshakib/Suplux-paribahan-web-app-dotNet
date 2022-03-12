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
        JourneyDetails journeyDetails = new JourneyDetails();
        
        string[] seats;
        string CoachNo;
        
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
            
            paymentInfo.startPoint = Session["from"].ToString();
            paymentInfo.destination = Session["to"].ToString();
            paymentInfo.tripDate = Session["date"].ToString();
            paymentInfo.coachType = Session["coachType"].ToString();
            paymentInfo.coachNo = Session["coachNo"].ToString();
            paymentInfo.totalFare = Session["totalFare"].ToString();


            paymentInfo.seatConcat = Session["seats"].ToString();

            return View(paymentInfo);
        }

        public ActionResult CancelTicket()
        {
            return View();
        }

        [HttpPost]
        public ActionResult goToPayment(string contype) {
            
            //this.CoachNo = contype;
            Session["coachNo"] = contype;
            if (contype.Contains("M"))
            {
                
                Session["coachType"] = "AC (Multi)";
            }
            else if (contype.Contains("B"))
            {
                
                Session["coachType"] = "AC (Bi)";
            }
            else 
            {
                
                Session["coachType"] = "NON-AC";
            }
            return RedirectToAction("Payment","Bus");
        }

        [HttpPost]
        public ActionResult GetJourneyData(JourneyDetails journeyDetails)
        {
            this.journeyDetails = journeyDetails;

            Session["starting"] = journeyDetails.from;
            Session["destination"] = journeyDetails.to;
            Session["date"] = journeyDetails.date;

            paymentInfo.startPoint = Session["starting"].ToString();
            paymentInfo.destination = Session["destination"].ToString();
            paymentInfo.tripDate = Session["date"].ToString();

            string date = Session["date"].ToString();
            string format = "yyyy-MM-dd";
            CultureInfo provider = CultureInfo.InvariantCulture;

            DateTime result = DateTime.ParseExact(date, format, provider);

            Session["date"] = result.ToString("dd-MMM-yyyy");



            if (!Session["starting"].Equals(""))
            {
                //return Json(Session["starting"]);
                return RedirectToAction("goToBuslist", "Home");
            }

            else return Json(journeyDetails.to);

        }



        [HttpPost]
        public ActionResult GetSelectedSeatsData(string[] seats, string totalFare)
        {
            Session["totalFare"] = totalFare;

            Session["seats"] = seats[0];

            

            for(int i = 1; i < seats.Length; i++)
            {
               Session["seats"] = Session["seats"] +", " + seats[i];
            }


            //Selected tables are fetching here 
            return Json(Session["seats"]);

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