//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SupLuxParibahanWebApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TransactionLog
    {
        public int transactionNo { get; set; }
        public string transactionId { get; set; }
        public string userEmail { get; set; }
        public string statusInfo { get; set; }
    
        public virtual UserTable UserTable { get; set; }
    }
}
