using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RevenueCollection.Models
{
    public class TransactionTypeSubCategory
    {
        public int transaction_type_category_id { get; set; }
        public int transaction_type_id { get; set; }
        public string transaction_type_category_name { get; set; }
        public string friendly_name { get; set; }
        public int amount { get; set; }
        public bool is_active { get; set; }
        public bool is_successful { get; set; }
        public int status_code { get; set; }
        public string message { get; set; }
    }
}