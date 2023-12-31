﻿using System;
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
       
        SUPLUXDashboardEntities2 database = new SUPLUXDashboardEntities2();
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
            string date = fromTo.date;

            Session["from"]=from;
            Session["to"]=to;   
            Session["date"]=date;   
            List<tripData> tripData = new List<tripData>();
            tripData=database.tripDatas.Where(x=>x.startingFrom.Equals(from) && x.destination.Equals(to)).ToList();

            

            return View(tripData);
        }

        public ActionResult Payment()
        {
            if(Session["name"] == null)
            {
                paymentInfo.name = "Log in koren Vai";
                //return RedirectToAction("Login", "Account");
            }
            else
            {
                paymentInfo.name = Session["name"].ToString();
            }
            
            paymentInfo.startPoint = Session["from"].ToString();
            paymentInfo.destination = Session["to"].ToString();

            string Date = Session["date"].ToString();
            string format = "yyyy-MM-dd";
            CultureInfo provider = CultureInfo.InvariantCulture;

            DateTime result = DateTime.ParseExact(Date, format, provider);
            Date = result.ToString("dd-MMM-yyyy");
            paymentInfo.tripDate = Date;
            paymentInfo.coachType = Session["coachType"].ToString();
            paymentInfo.coachNo = Session["coachNo"].ToString();
            //paymentInfo.totalFare = Session["totalFare"].ToString();
            //System.Diagnostics.Debug.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA", Session["totalFare"]);
            //paymentInfo.seatConcat = Session["seats"].ToString();
            paymentInfo.departureTime = Session["departureTime"].ToString();

            return View(paymentInfo);
        }

        [HttpPost]
        public ActionResult goToModify(string from, string to, string date)
        {

            FromToData fromToData = new FromToData();

            string Date = date;
            string[] date2 = Date.Split('\'');
            Date = date2[0] + " " + date2[1];

            string format = "dd MMM yy";
            CultureInfo provider = CultureInfo.InvariantCulture;

            DateTime result = DateTime.ParseExact(Date, format, provider);
            Date = result.ToString("yyyy-MM-dd");

            fromToData.date = Date;
            fromToData.From = from;
            fromToData.To = to;



            TempData["fromto"] = fromToData;

            return RedirectToAction("List", "Bus");
        }

        public ActionResult CancelTicket()
        {
            return View();
        }

        [HttpPost]
        public ActionResult goToPayment(string contype) {

            //this.CoachNo = contype;

            string[] arr = contype.Split(' ');
            Session["coachNo"] = arr[0];
            Session["departureTime"] = arr[1];
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
            //Session["journeyDate"] = journeyDetails.date;
            //string journeyDate = journeyDetails.date;

            paymentInfo.startPoint = Session["starting"].ToString();
            paymentInfo.destination = Session["destination"].ToString();
            paymentInfo.tripDate = Session["date"].ToString();

            
           // paymentInfo.tripDate = journeyDate;

            //string date = journeyDate;

            string date = Session["date"].ToString(); ;
            string format = "yyyy-MM-dd";
            CultureInfo provider = CultureInfo.InvariantCulture;

            DateTime result = DateTime.ParseExact(date, format, provider);

            Session["date"] = result.ToString("dd-MMM-yyyy");



            if (!Session["starting"].Equals(""))
            {
                return RedirectToAction("goToBuslist", "Home");
            }

            else return Json(Session["date"]);

        }



        [HttpPost]
        public ActionResult GetSelectedSeatsData(string[] seats, string totalFare)
        {
            Session["totalFare"] = totalFare;
            Session["seats"] = seats[0];

            string str = seats[0];

            paymentInfo.totalFare = totalFare;

            for(int i = 1; i < seats.Length; i++)
            {
                str = str + "," + seats[i];
            }

            paymentInfo.seatConcat = str;

            return RedirectToAction("Payment", "Bus");

        }

        [HttpPost]
        public ActionResult List(string type)
        {
            string from = (string)Session["from"];
            string to = (string)Session["to"];

            string date = Session["date"].ToString();
            
            //string format = "dd-MMM-yyyy"; 
           // CultureInfo provider = CultureInfo.InvariantCulture;

            //DateTime result = DateTime.ParseExact(date, format, provider);

           // Session["date"] = result.ToString("yyyy-MM-dd");

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

        [HttpPost]
        public ActionResult CancelTicketUtk(string utkno)
        {
            Reservation reservation = new Reservation();
            reservation  = database.Reservations.SingleOrDefault(temp => temp.UTKNo.Equals(utkno));
            return RedirectToAction("CancelTicket", "Bus");
        }

        

        public ActionResult ConfirmPayment(string tripDate, string seats, string coachNo)
        {

            string[] seat = seats.Split(',');

            string utk = DateTime.Now.ToString("yyyyMMdd") + "-" + coachNo + seat[0] + "-" + Convert.ToDateTime(tripDate).ToString("yyyyMMdd");
            Reservation reservation = new Reservation();
            TransactionLog transactionLog = new TransactionLog();



            foreach (string sed in seat)
            {
                reservation.UTKNo = utk;
                reservation.userEmail = "mjaumi2864@gmail.com";
                reservation.coachNo = coachNo;
                reservation.bookedSeat = sed;
                string d = DateTime.Now.ToString("yyyy-MM-dd");
                reservation.reservationDate = d;
                reservation.dateOfJourney = Session["date"].ToString();
                reservation.paymentMethod = "bkash";

                string[] str = Session["totalFare"].ToString().Split(' ');
                reservation.duePayment = Int16.Parse(str[1]);

                database.Reservations.Add(reservation);
                database.SaveChanges();



                //Session["currentEmail"].ToString();



                System.Diagnostics.Debug.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAA", d);

                //string sqlQuery = "Insert into Reservation (UTKNo, userEmail, coachNo, bookedSeat, reservationDate, dateOfJourney, paymentMethod, duePayment) values ('" + reservation.UTKNo+ "' , '" + reservation.userEmail + ", '" + reservation.coachNo + "', '"+ reservation.bookedSeat +"', '"+  )";
                //database.Reservations.Add(reservation);

            }
            
            //database.SaveChanges();

            transactionLog.statusInfo = "Paid";
            transactionLog.userEmail = "mjaumi2864@gmail.com"; //Session["currentEmail"].ToString();
            transactionLog.transactionId = utk;

            database.TransactionLogs.Add(transactionLog);
            database.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

    }
}