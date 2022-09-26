using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RevenueCollection.Models
{
    public class Towns
    {
        public int town_id { get; set; }
        public string town_name { get; set; }
        public int sub_county_id { get; set; }
        public string town_description { get; set; }
        public DateTime date_created { get; set; }
        public string created_by { get; set; }
        public DateTime date_updated { get; set; }
        public string updated_by { get; set; }
    }
}