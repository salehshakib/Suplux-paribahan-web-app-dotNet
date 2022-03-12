using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SupLuxParibahanWebApp.Models;

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
    }
}