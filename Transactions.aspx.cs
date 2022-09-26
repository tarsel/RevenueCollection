using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.UI.WebControls;

using RevenueCollection.Models;


namespace RevenueCollection
{
    public partial class Transactions : System.Web.UI.Page
    {
        readonly string baseUrl = "http://county-001-site1.atempurl.com/";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetAllDefaultTransactions();
                ViewAllSubCounties();
            }
        }

        private void GetAllDefaultTransactions()
        {
            List<MasterTransactionRecordResponse> rootobject;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                try
                {
                    var response = client.PostAsJsonAsync("api/Transaction/GetAllDefaultTransactions", 0).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        rootobject = response.Content.ReadAsAsync<List<MasterTransactionRecordResponse>>().Result;

                        gvTransactions.DataSource = rootobject;
                        gvTransactions.DataBind();
                    }
                    // else
                    // lblMessage.Text = "System cannot fetch data.";
                }
                catch (Exception ex)
                {
                    // lblMessage.Text = ex.Message;
                }
            }

        }


        protected void gvTransactions_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);

                    try
                    {
                        string lblService = (e.Row.FindControl("lblService") as Label).Text;
                        string lblCategory = (e.Row.FindControl("lblCategory") as Label).Text;
                        string lblCustomer = (e.Row.FindControl("lblCustomer") as Label).Text;
                        string lblTown = (e.Row.FindControl("lblTown") as Label).Text;
                        string lblSubCounty = (e.Row.FindControl("lblSubCounty") as Label).Text;

                        TransactionTypeSub transactionTypeSub = new TransactionTypeSub { transaction_type_category_id = int.Parse(lblCategory) };
                        TransactionTypeRoot transactionTypeRoot = new TransactionTypeRoot { transaction_type_id = int.Parse(lblService) };
                        CustomerDetail customerDetail = new CustomerDetail { customer_id = int.Parse(lblCustomer) };
                        RootRequest rootRequest = new RootRequest { sub_county_id = int.Parse(lblSubCounty), town_id = int.Parse(lblTown) };

                        var transactionTypeResponse = client.PostAsJsonAsync("api/Transaction/GetTransactionTypeByTransactionTypeId", transactionTypeRoot).Result;

                        if (transactionTypeResponse.IsSuccessStatusCode)
                        {
                            TransactionTypes transactionType = transactionTypeResponse.Content.ReadAsAsync<TransactionTypes>().Result;

                            Label lblTransactionType = e.Row.FindControl("lblTransactionType") as Label;

                            lblTransactionType.Text = transactionType.transaction_type_name;

                        }

                        var transactionTypeCategoryResponse = client.PostAsJsonAsync("api/Transaction/GetTransactionTypeCategoryByCategoryId", transactionTypeSub).Result;

                        if (transactionTypeCategoryResponse.IsSuccessStatusCode)
                        {
                            TransactionTypeSubCategory transactionTypeCategory = transactionTypeCategoryResponse.Content.ReadAsAsync<TransactionTypeSubCategory>().Result;

                            Label lblTransactionCategory = e.Row.FindControl("lblTransactionCategory") as Label;

                            lblTransactionCategory.Text = transactionTypeCategory.transaction_type_category_name;

                        }

                        var customerResponse = client.PostAsJsonAsync("api/Customer/GetCustomerByCustomerId", customerDetail).Result;

                        if (customerResponse.IsSuccessStatusCode)
                        {
                            CustomerUpdate customerUpdate = customerResponse.Content.ReadAsAsync<CustomerUpdate>().Result;

                            Label lblCustomerNames = e.Row.FindControl("lblCustomerNames") as Label;

                            lblCustomerNames.Text = customerUpdate.first_name + " " + customerUpdate.last_name;
                        }


                        var subcountyResponse = client.PostAsJsonAsync("api/SetUp/GetSubCountyBySubCountyId", rootRequest).Result;

                        if (subcountyResponse.IsSuccessStatusCode)
                        {
                            SubCounties subCounties = subcountyResponse.Content.ReadAsAsync<SubCounties>().Result;

                            Label lblSubCounty1 = e.Row.FindControl("lblSubCounty1") as Label;

                            lblSubCounty1.Text = subCounties.sub_county_name;
                        }


                        var townResponse = client.PostAsJsonAsync("api/SetUp/GetTownByTownId", rootRequest).Result;

                        if (townResponse.IsSuccessStatusCode)
                        {
                            Towns townsResponse = townResponse.Content.ReadAsAsync<Towns>().Result;

                            Label lblWard = e.Row.FindControl("lblWard") as Label;

                            lblWard.Text = townsResponse.town_name;

                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
        }

        protected void ddlSubCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTownsBySubCountyId(int.Parse(ddlSubCounty.SelectedValue));
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

                        ddlSubCounty.DataSource = townSubCounties;
                        ddlSubCounty.DataTextField = "sub_county_name";
                        ddlSubCounty.DataValueField = "sub_county_id";
                        ddlSubCounty.DataBind();

                        ddlSubCounty.Items.Insert(0, new ListItem("<--Select Sub County-->", "0"));
                    }
                    //else
                    //    lblMessage.Text = "System cannot fetch data.";
                }
                catch (Exception ex)
                {
                    // lblMessage.Text = ex.Message;
                }
            }
        }


        public void GetTownsBySubCountyId(int subCountyId)
        {
            TownsRequest townsRequest = new TownsRequest { sub_county_id = subCountyId, town_id = subCountyId };
            List<Towns> towns;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                try
                {
                    var response = client.PostAsJsonAsync("api/SetUp/GetTownsBySubCountyId", townsRequest).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        towns = response.Content.ReadAsAsync<List<Towns>>().Result;

                        ddlTown.DataSource = towns;
                        ddlTown.DataTextField = "town_name";
                        ddlTown.DataValueField = "town_id";
                        ddlTown.DataBind();

                        ddlTown.Items.Insert(0, new ListItem("<--Select Ward!-->", "0"));
                    }
                    //else
                    //    lblMessage.Text = "System cannot fetch data.";
                }
                catch (Exception ex)
                {
                    // lblMessage.Text = ex.Message;
                }
            }
        }

        private void GetTransactionsByWardId(int wardId)
        {
            List<MasterTransactionRecordResponse> rootobject;

            GenericRequest genericRequest = new GenericRequest { ward_id = wardId };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                try
                {
                    var response = client.PostAsJsonAsync("api/Transaction/GetTransactionsByWard", genericRequest).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        rootobject = response.Content.ReadAsAsync<List<MasterTransactionRecordResponse>>().Result;

                        gvTransactions.DataSource = rootobject;
                        gvTransactions.DataBind();
                    }
                    // else
                    // lblMessage.Text = "System cannot fetch data.";
                }
                catch (Exception ex)
                {
                    // lblMessage.Text = ex.Message;
                }
            }

        }

        private void GetTransactionsByDate(DateTime startDate, DateTime endDate)
        {
            List<MasterTransactionRecordResponse> rootobject;
            GenericRequest genericRequest = new GenericRequest { end_date = endDate, start_date = startDate };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                try
                {
                    var response = client.PostAsJsonAsync("api/Transaction/GetTransactionsByDate", genericRequest).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        rootobject = response.Content.ReadAsAsync<List<MasterTransactionRecordResponse>>().Result;

                        gvTransactions.DataSource = rootobject;
                        gvTransactions.DataBind();
                    }
                    // else
                    // lblMessage.Text = "System cannot fetch data.";
                }
                catch (Exception ex)
                {
                    // lblMessage.Text = ex.Message;
                }
            }

        }

        protected void ddlTown_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTransactionsByWardId(int.Parse(ddlTown.SelectedValue));
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Transactions.aspx");
        }

        protected void gvTransactions_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTransactions.PageIndex = e.NewPageIndex;
            GetAllDefaultTransactions();
        }
    }


    public class TransactionTypeRoot
    {
        public int transaction_type_id { get; set; }
        public string transaction_type_name { get; set; }
        public string friendly_name { get; set; }
        public int amount { get; set; }
    }


    public class TransactionTypeCategoryRoot
    {
        public int transaction_type_category_id { get; set; }
        public int transaction_type_id { get; set; }
        public string transaction_type_category_name { get; set; }
        public int amount { get; set; }
    }


    public class RootRequest
    {
        public int town_id { get; set; }
        public int sub_county_id { get; set; }
    }




}