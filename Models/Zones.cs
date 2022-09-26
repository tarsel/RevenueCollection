using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RevenueCollection.Models
{
    public class Zones
    {
        public int zone_id { get; set; }
        public string zone_name { get; set; }
        public string zone_description { get; set; }
        public int town_id { get; set; }
        public string created_by { get; set; }
        public bool is_successful { get; set; }
        public int status_code { get; set; }
        public string message { get; set; }
    }
}