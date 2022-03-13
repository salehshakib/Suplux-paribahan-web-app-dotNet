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
        SUPLUXDashboardEntities2 database = new SUPLUXDashboardEntities2();
        //SUPLUXDashboardEntities db = new SUPLUXDashboardEntities();

        // GET: Admin
        public ActionResult AdminHome()
        {
            if (Session["currentEmail"] != null && Session["currentEmail"].ToString().Contains("@suplux.com"))
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
                tripdatas = database.tripDatas.ToList();
            
                //List<tripData> tripdatas = db.tripDatas.ToList();
                //var tripdatas = database.tripDatas.ToList();
                return View( tripdatas);  
            }
           
            return View();
        }

        

        // public



        [HttpPost]
        public ActionResult BusList(String coachNo, String coachType)
        {
            List<tripData> tripdatas = new List<tripData>();
            
            if (coachNo == null && coachType == "Select Coach Type")
            {
                tripdatas = database.tripDatas.ToList();

            }

            //else */ 
            if (coachNo != null || coachType != "Select Coach Type")
            {
                if (coachNo != null)
                {
                    tripdatas = database.tripDatas.Where(temp => temp.coachNo.Equals(coachNo) || temp.coachType.Equals(coachType)).ToList();
                }
                else
                {
                    if (coachType.Equals("NON-AC"))
                    {
                        coachType = "NON-AC";
                        tripdatas = database.tripDatas.Where(temp => temp.coachNo.Equals(coachNo) || temp.coachType.Equals(coachType)).ToList();
                    }

                    else if (coachType.Equals("AC(Bi-Axle)"))
                    {
                        coachType = "AC (Bi)";
                        tripdatas = database.tripDatas.Where(temp => temp.coachNo.Equals(coachNo) || temp.coachType.Equals(coachType)).ToList();

                    }
                    else if (coachType.Equals("AC(Multi-Axle)"))
                    {
                        coachType = "AC (Multi)";
                        tripdatas = database.tripDatas.Where(temp => temp.coachNo.Equals(coachNo) || temp.coachType.Equals(coachType)).ToList();
                    }

                }


                //tripdatas = database.tripDatas.Where(temp => temp.coachNo.Equals(coachNo) || temp.coachType.Equals(coachType)).ToList();

                return View(tripdatas);
            }
            else
            {

                TempData["notification"] = "no coach found";
            }

            return View();
            
        }

        public ActionResult AddNewBus() { return View(); }

        [HttpPost]
        public ActionResult AddNewBus(tripData tripData) 
        {
            //tripData.departureTime = "10:40AM";
            tripData.TripStatus = "available";
            database.tripDatas.Add(tripData);
            database.SaveChanges();
            TempData["notification"] = "add success";
            return RedirectToAction("AdminHome", "Admin");
        }

        // GET: Add New Bus
        [HttpPost]
        public ActionResult SearchBus(String coachType)
        {
            if(coachType.Equals("Select Coach Type"))
            {
                TempData["notification"] = "no coach found";
            }
            else
            {
                TempData["notification"] = "coach found";
                if (coachType.Equals("AC(Bi-Axle)"))
                {
                    coachType = "AC (Bi)";
                }
                else if (coachType.Equals("AC(Multi-Axle)"))
                {
                    coachType = "AC (Multi)";
                }
                List<tripData> tripdatas = new List<tripData>();
                tripdatas = database.tripDatas.Where(temp => temp.coachType.Equals(coachType)).ToList();

                String coachNo = tripdatas.Last().coachNo;

                String lastDigit = coachNo.Substring(coachNo.Length - 3);
                String cN = coachNo.Substring(0, coachNo.Length - 3);
                int id = int.Parse(lastDigit);
                id++;
                if (id < 100)
                {
                    cN = cN +"0"+ id.ToString();
                }
                else
                {
                    cN = cN + id.ToString();
                }

                Session["newCoachNo"] =cN;
                Session["newCoachType"] = coachType;

            }

            return RedirectToAction("AddNewBus", "Admin");
        }

        

        // GET: Reroute Bus
        public ActionResult SearchRerouteBus(String coachNo)
        {

            //List<tripData> tripdatas = new List<tripData>();
            var tripdatas = database.tripDatas.Where(temp => temp.coachNo.Equals(coachNo)).SingleOrDefault();

            if(tripdatas != null)
            {
                Session["coachNoForReroute"] = tripdatas.coachNo;
                Session["coachTypeForReroute"] = tripdatas.coachType;
                TempData["notification"] = "coach found";
            }

            else
            {
                TempData["notification"] = "no coach found";
            }

            return RedirectToAction("RerouteBus", "Admin");
        }

        [HttpPost]
        public ActionResult UpdateRoute(tripData tripdata) 
        { 
            tripData trip=new tripData();
            trip=database.tripDatas.Where(x=>x.coachNo.Equals(tripdata.coachNo)).SingleOrDefault();
            if (trip != null)
            {
                trip.coachNo=tripdata.coachNo;
                trip.coachType = tripdata.coachType;
                trip.destination = tripdata.destination;    
                trip.departureTime = tripdata.departureTime;
                trip.farePerSeat = tripdata.farePerSeat;
                trip.startingFrom = tripdata.startingFrom;
                trip.TripStatus = "available";

                database.Entry(trip).State = System.Data.Entity.EntityState.Modified;
                database.SaveChanges();
                TempData["notification"] = "reroute success";
                return RedirectToAction("AdminHome", "Admin");
            }

            return View("~/Views/Admin/RerouteBus.cshtml"); 
        }

        public ActionResult RerouteBus()
        {
            return View();
        }

        //getting coachNo for halting or maintaining  from front end
        [HttpPost]
        public ActionResult getCoachNo(string coachNo, string status)
        {
            if(status != null)
            {
                DateTime dateTime = DateTime.Now;

                //TODO here coach No returns the coachNo and status returns 'maintanence' or 'halt'

                //return Json(coachNo + " " + status);
                tripData trip = new tripData();
                trip = database.tripDatas.Where(x => x.coachNo.Equals(coachNo)).SingleOrDefault();
                if (trip != null)
                {
                    //trip.coachNo = coachNo;
                    trip.TripStatus = status;
                    trip.MHDate = dateTime.Date;

                    database.Entry(trip).State = System.Data.Entity.EntityState.Modified;
                    database.SaveChanges();
                    TempData["notification"] = "Status updated";
                    return RedirectToAction("AdminHome", "Admin");
                }
                TempData["notification"] = "Error! Status not updated";
                //return Json(coachNo + " " + status);
                return RedirectToAction("AdminHome", "Admin");
            }
            else
            {
                return Json("An Error Occoured");
            }
        }
    }
}