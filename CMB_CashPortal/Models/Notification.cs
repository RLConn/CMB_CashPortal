using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMB_CashPortal.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int HouseholdId { get; set; }
        public DateTime Created { get; set; }
        public bool Read { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}