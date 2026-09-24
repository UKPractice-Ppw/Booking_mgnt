using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheatreMgnt.Models
{
    public class Movie
    {
        public int Movie_id { get; set; }
        public string Movie_name { get; set; }

        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.Date)]
        public DateTime Release_date { get; set; }
        public int Cat_id { get; set; }
        public IEnumerable<SelectListItem> category { get; set; }
        public int Movie_rate { get; set; }
    }
}