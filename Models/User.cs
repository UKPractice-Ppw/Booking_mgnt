using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheatreMgnt.Models
{
    public class User
    {
        public int User_id { get; set; }
        public string User_name { get; set; }
        public string User_password { get; set; }
        public string User_email { get; set; }
        public string City { get; set; }
        public string PhoneNo { get; set; }

    }
}