using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RevenueCollection.Models
{
    public class TransactionTypeSub
    {
        public int transaction_type_category_id { get; set; }
        public int transaction_type_id { get; set; }
        public string transaction_type_category_name { get; set; }
        public int amount { get; set; }
    }
}