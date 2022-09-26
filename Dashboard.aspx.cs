using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

using Dapper;

using RevenueCollection.Models;

namespace RevenueCollection
{
    public partial class Dashboard : System.Web.UI.Page
    {
        readonly string baseUrl = "http://county-001-site1.atempurl.com/";

        private readonly string sqlConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString.ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            // ViewAllTowns();
            ViewAllSubCounties();
            GetAllCustomers();
            ViewAllTransactions();
            //   GetAllUsers();
        }

        public void GetAllCustomers()
        {

            List<Customer> customers;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                try
                {
                    var response = client.GetAsync("api/Customer/GetAllCustomers").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        customers = response.Content.ReadAsAsync<List<Customer>>().Result;
                        lblCustomers.Text = customers.Count.ToString();
                    }
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                }
            }
        }

        public void ViewAllSubCounties()
        {
            List<SubCounties> townSubCounties;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                try
                {
                    var response = client.GetAsync("api/SetUp/GetAllSubCounties").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        townSubCounties = response.Content.ReadAsAsync<List<SubCounties>>().Result;
                        lblSubCounties.Text = townSubCounties.Count.ToString();
                    }
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                }
            }

        }


        //public void ViewAllTowns()
        //{
        //    List<Towns> towns;

        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(baseUrl);
        //        try
        //        {
        //            var response = client.GetAsync("api/SetUp/GetAllTowns").Result;

        //            if (response.IsSuccessStatusCode)
        //            {
        //                towns = response.Content.ReadAsAsync<List<Towns>>().Result;

        //                lblTowns.Text = towns.Count.ToString();
        //            }
        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //    }
        //}


        public void ViewAllTransactions()
        {
            List<MasterTransactionRecord> masterTransactionRecord;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                try
                {
                    var response = client.PostAsJsonAsync("api/Transaction/GetAllDefaultTransactions", 0).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        masterTransactionRecord = response.Content.ReadAsAsync<List<MasterTransactionRecord>>().Result;

                        lblTotalCollections.Text = masterTransactionRecord.Count.ToString();

                        long value = masterTransactionRecord.Sum(s => s.amount);

                        lblTotalAmountCollected.Text = value.ToString();
                    }
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                }
            }
        }

        //public void GetAllUsers()
        //{
        //    List<Users> record = new List<Users>();

        //    using (var connection = new SqlConnection(sqlConnectionString))
        //    {

        //        connection.Open();

        //        record = connection.Query<Users>("SELECT * FROM AspNetUsers").ToList();

        //        connection.Close();

        //        lblUsers.Text = record.Count.ToString();

        //    }

        //}
    }


    public class MasterTransactionRecord
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
        public int payer_balance_before_transaction { get; set; }
        public int payer_balance_after_transaction { get; set; }
        public int payee_balance_before_transaction { get; set; }
        public int payee_balance_after_transaction { get; set; }
        public bool is_test_transaction { get; set; }
        public int access_channel_id { get; set; }
        public string source_username { get; set; }
        public string destination_username { get; set; }
        public int transaction_status_id { get; set; }
        public string third_party_transaction_id { get; set; }
        public int reversed_transaction_original_type_id { get; set; }
        public bool is_successful { get; set; }
        public int status_code { get; set; }
        public string message { get; set; }
    }

}