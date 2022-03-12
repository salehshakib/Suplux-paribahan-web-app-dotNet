using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SupLuxParibahanWebApp.Models
{
    public class PaymentInfo
    {
       public string name { get; set; }
       public string startPoint { get; set; }
       public string tripDate { get; set; }
       public string totalFare { get; set; }
       public string destination { get; set; } 
       public string coachNo { get; set; }
       public string coachType { get; set; }
       public string[] seat { get; set; }
       public string departureTime { get; set; }

    }
}