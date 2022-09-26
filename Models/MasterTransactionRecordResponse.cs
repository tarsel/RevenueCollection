using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RevenueCollection.Models
{
    public class MasterTransactionRecordResponse
    {
        public int master_transaction_record_id { get; set; }
        public int payer_id { get; set; }
        public int payer_payment_instrument_id { get; set; }
        public int payee_id { get; set; }
        public int payee_payment_instrument_id { get; set; }
        public string transaction_reference { get; set; }
        public string short_description { get; set; }
        public int transaction_type_id { get; set; }
        public int transaction_error_code_id { get; set; }
        public int amount { get; set; }
        public int fee { get; set; }
        public int tax { get; set; }
        public DateTime transaction_date { get; set; }
        public int customer_type_id { get; set; }
        public int? payer_balance_before_transaction { get; set; }
        public int? payer_balance_after_transaction { get; set; }
        public int? payee_balance_before_transaction { get; set; }
        public int? payee_balance_after_transaction { get; set; }
        public bool is_test_transaction { get; set; }
        public int access_channel_id { get; set; }
        public string source_username { get; set; }
        public string destination_username { get; set; }
        public int? transaction_status_id { get; set; }
        public string third_party_transaction_id { get; set; }
        public int? reversed_transaction_original_type_id { get; set; }
        public int transaction_category_type_id { get; set; }
        public int business_registration_id { get; set; }
        public int town_id { get; set; }
        public int sub_county_id { get; set; }
        public string key_identifier { get; set; }
        public string id_number { get; set; }
        public bool is_successful { get; set; }
        public int status_code { get; set; }
        public string message { get; set; }
    }
}