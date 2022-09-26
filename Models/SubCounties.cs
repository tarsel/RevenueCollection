using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RevenueCollection.Models
{
    public class SubCounties
    {
        public int sub_county_id { get; set; }
        public string sub_county_name { get; set; }
        public string sub_county_description { get; set; }
        public DateTime date_created { get; set; }
        public string created_by { get; set; }
        public DateTime date_updated { get; set; }
        public string updated_by { get; set; }
    }
}