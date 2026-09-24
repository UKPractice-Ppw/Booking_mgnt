using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheatreMgnt.Models
{
    public class Booking
    {
        public int Booking_id { get; set; }
        public int User_id { get; set; }
        public int Cat_id { get; set; }
        public int Movie_id { get; set; }
        public int No_of_tickets { get; set; }
        public int Amount { get; set; }
        public string Cat_type { get; set; }
        public string Movie_name { get; set; }
        public int Movie_rate { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Movies { get; set; } = new List<SelectListItem>();
    }
}