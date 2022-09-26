using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using RevenueCollection.Models;

namespace RevenueCollection
{
    public partial class CreateCustomer : System.Web.UI.Page
    {
        readonly string baseUrl = "http://county-001-site1.atempurl.com/";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetAllCustomers();
                ViewAllSubCounties();
                // ViewAllTowns();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            CreateNewCustomer();
            GetAllCustomers();
        }

        public void CreateNewCustomer()
        {
            if (ddlSubCounty.SelectedIndex == 0 || ddlTown.SelectedIndex == 0)
            {
                lblMessage.Text = "Kindly select the drop down list!";
            }
            else if (string.IsNullOrEmpty(txtEmail.Text) || txtEmail.Text == null) { lblMessage.Text = "Email is Empty!"; }
            else if (string.IsNullOrEmpty(txtFirstName.Text) || txtFirstName.Text == null) { lblMessage.Text = "First Name is Empty!"; }
            else if (string.IsNullOrEmpty(txtIdNumber.Text) || txtIdNumber.Text == null) { lblMessage.Text = "Id No is Empty!"; }
            else if (string.IsNullOrEmpty(txtLastName.Text) || txtLastName.Text == null) { lblMessage.Text = "Last Name is Empty!"; }
            else if (string.IsNullOrEmpty(txtMiddleName.Text) || txtMiddleName.Text == null) { lblMessage.Text = "Middle Name is Empty!"; }
            else if (string.IsNullOrEmpty(txtPhoneNumber.Text) || txtPhoneNumber.Text == null) { lblMessage.Text = "Phone No is Empty!"; }
            else
            {
                using (var client = new HttpClient())
                {
                    Customer cust = new Customer { customer_type_id = 1, email_address = txtEmail.Text, first_name = txtFirstName.Text, fully_registered = true, id_number = txtIdNumber.Text, id_type_id = 1, is_test_customer = false, language_id = 1, last_name = txtLastName.Text, middle_name = txtMiddleName.Text, msisdn = txtPhoneNumber.Text, registered_by_userName = "SYSTEM", shared_msisdn = false, sub_county_id = int.Parse(ddlSubCounty.SelectedValue), town_id = int.Parse(ddlTown.SelectedValue), user_name = "tarsel", user_type_id = 1 };

                    client.BaseAddress = new Uri(baseUrl);

                    try
                    {
                        bool result = GetCustomerByIdNumber(txtIdNumber.Text);

                        if (result == false)
                        {
                            var response = client.PostAsJsonAsync("api/Customer/CreateCustomer", cust).Result;

                            if (response.IsSuccessStatusCode)
                            {
                                lblMessage.Text = "Customer Created Successfully!";
                            }
                            else
                                lblMessage.Text = "";
                        }
                        else
                        {
                            lblMessage.Text = "The Customer with Id Number: (" + txtIdNumber.Text + ") Already Exists!";
                        }

                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = ex.Message;
                    }
                }
            }
        }


        protected void gvCustomers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCustomers.PageIndex = e.NewPageIndex;
            GetAllCustomers();
        }

        protected void gvCustomers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvCustomers.EditIndex = -1;
            GetAllCustomers();
        }

        protected void gvCustomers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvCustomers.EditIndex = e.NewEditIndex;
            GetAllCustomers();
        }

        protected void gvCustomers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            var user = authenticationManager.User.Identity.Name;

            TextBox txtEditFirstName = gvCustomers.Rows[e.RowIndex].FindControl("txtEditFirstName") as TextBox;
            TextBox txtEditMiddleName = gvCustomers.Rows[e.RowIndex].FindControl("txtEditMiddleName") as TextBox;
            TextBox txtEditLastName = gvCustomers.Rows[e.RowIndex].FindControl("txtEditLastName") as TextBox;
            TextBox txtEditIdNumber = gvCustomers.Rows[e.RowIndex].FindControl("txtEditIdNumber") as TextBox;
            DropDownList ddlCustomerTypeGrid = gvCustomers.Rows[e.RowIndex].FindControl("ddlCustomerTypeGrid") as DropDownList;
            DropDownList ddlSubCountyGrid = gvCustomers.Rows[e.RowIndex].FindControl("ddlSubCountyGrid") as DropDownList;
            DropDownList ddlWardGrid = gvCustomers.Rows[e.RowIndex].FindControl("ddlWardGrid") as DropDownList;

            int customerId = Convert.ToInt16(gvCustomers.DataKeys[e.RowIndex].Values["customer_id"].ToString());


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                CustomerDetail cust1 = new CustomerDetail { customer_id = customerId };
                var response1 = client.PostAsJsonAsync("api/Customer/GetCustomerByCustomerId", cust1).Result;
                CustomerUpdate customerUpdate = response1.Content.ReadAsAsync<CustomerUpdate>().Result;

                CustomerUpdate cust = new CustomerUpdate { customer_id = customerId, customer_type_id = int.Parse(ddlCustomerTypeGrid.SelectedValue), email_address = customerUpdate.email_address, first_name = txtEditFirstName.Text, fully_registered = true, id_number = txtEditIdNumber.Text, id_type_id = customerUpdate.id_type_id, is_test_customer = false, language_id = customerUpdate.language_id, last_name = txtEditLastName.Text, middle_name = txtEditMiddleName.Text, msisdn = customerUpdate.msisdn, sub_county_id = int.Parse(ddlSubCountyGrid.SelectedValue), town_id = int.Parse(ddlWardGrid.SelectedValue), user_type_id = int.Parse(ddlCustomerTypeGrid.SelectedValue), updated_by = user, access_channel_id = customerUpdate.access_channel_id, deactivated_account = customerUpdate.deactivated_account, information_mode_id = customerUpdate.information_mode_id, is_blacklisted = customerUpdate.is_blacklisted, is_staff = customerUpdate.is_staff };


                try
                {
                    var response = client.PostAsJsonAsync("api/Customer/UpdateCustomer", cust).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        lblMessage.Text = "The Customer: (" + txtEditFirstName.Text + ") has been Updated Successfully!";

                    }
                    else
                        lblMessage.Text = "";
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message;
                }
            }

            gvCustomers.EditIndex = -1;
            GetAllCustomers();

        }


        public bool GetCustomerByIdNumber(string idNumber)
        {
            bool result = false;
            Customer customer;
            try
            {
                using (var client = new HttpClient())
                {
                    CustomerDetail cust = new CustomerDetail { id_number = idNumber };

                    client.BaseAddress = new Uri(baseUrl);


                    var response = client.PostAsJsonAsync("api/Customer/GetCustomerByIdNumber", cust).Result;

                    customer = response.Content.ReadAsAsync<Customer>().Result;

                    if (customer.customer_id > 0)
                    {
                        lblMessage.Text = "Customer Exists!";

                        result = true;
                    }
                    else
                        result = false;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }

            return result;
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

                        gvCustomers.DataSource = customers;
                        gvCustomers.DataBind();
                    }
                    else
                        lblMessage.Text = "System cannot fetch data.";
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message;
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

                        ddlSubCounty.DataSource = townSubCounties;
                        ddlSubCounty.DataTextField = "sub_county_name";
                        ddlSubCounty.DataValueField = "sub_county_id";
                        ddlSubCounty.DataBind();

                        ddlSubCounty.Items.Insert(0, new ListItem("<--Select Sub County-->", "0"));
                    }
                    else
                        lblMessage.Text = "System cannot fetch data.";
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message;
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
                    else
                        lblMessage.Text = "System cannot fetch data.";
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message;
                }
            }

        }

        public void GetEditedTownsBySubCountyId(int subCountyId)
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

                        DropDownList ddlWardGrid = (DropDownList)gvCustomers.Rows[1].FindControl("ddlWardGrid");
                        //Bind subcategories data to dropdownlist
                        ddlWardGrid.DataSource = towns;
                        ddlWardGrid.DataTextField = "town_name";
                        ddlWardGrid.DataValueField = "town_id";
                        ddlWardGrid.DataBind();

                        ddlWardGrid.Items.Insert(0, new ListItem("<--Select Ward-->", "0"));

                        ////Select the Sub County of Customer in DropDownList
                        //string lblWardGrid = ((Label)gvCustomers.Rows[0].FindControl("ddlWardGrid")).Text;
                        //ddlWardGrid.Items.FindByValue(lblWardGrid).Selected = true;
                    }
                    else
                        lblMessage.Text = "System cannot fetch data.";
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message;
                }
            }
        }

        public void ViewAllTowns()
        {
            List<Towns> towns;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                try
                {
                    var response = client.GetAsync("api/SetUp/GetAllTowns").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        towns = response.Content.ReadAsAsync<List<Towns>>().Result;

                        ddlTown.DataSource = towns;
                        ddlTown.DataTextField = "town_name";
                        ddlTown.DataValueField = "town_id";
                        ddlTown.DataBind();

                        ddlTown.Items.Insert(0, new ListItem("<--Select Ward!-->", "0"));
                    }
                    else
                        lblMessage.Text = "System cannot fetch data.";
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message;
                }
            }
        }

        protected void gvCustomers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                List<SubCounties> subCounties;
                List<Towns> towns;
                List<CustomerTypes> customerTypes;


                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);

                    try
                    {
                        var subcountyResponse = client.GetAsync("api/SetUp/GetAllSubCounties").Result;

                        if (subcountyResponse.IsSuccessStatusCode)
                        {
                            subCounties = subcountyResponse.Content.ReadAsAsync<List<SubCounties>>().Result;

                            //check if is in edit mode
                            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                            {
                                DropDownList ddlSubCountyGrid = (DropDownList)e.Row.FindControl("ddlSubCountyGrid");
                                //Bind subcategories data to dropdownlist
                                ddlSubCountyGrid.DataTextField = "sub_county_name";
                                ddlSubCountyGrid.DataValueField = "sub_county_id";
                                ddlSubCountyGrid.DataSource = subCounties;
                                ddlSubCountyGrid.DataBind();

                                ddlSubCountyGrid.Items.Insert(0, new ListItem("<--Select Sub County-->", "0"));

                                //Select the Sub County of Customer in DropDownList
                                string lblSubCountyGrid = (e.Row.FindControl("lblSubCounty") as Label).Text;
                                ddlSubCountyGrid.Items.FindByValue(lblSubCountyGrid).Selected = true;

                                //  GetEditedTownsBySubCountyId(int.Parse(lblSubCountyGrid),  e);

                            }
                            else
                            {
                                DropDownList ddlSubCountyGrid1 = (e.Row.FindControl("ddlSubCountyGrid1") as DropDownList);

                                ddlSubCountyGrid1.DataSource = subCounties;
                                ddlSubCountyGrid1.DataTextField = "sub_county_name";
                                ddlSubCountyGrid1.DataValueField = "sub_county_id";
                                ddlSubCountyGrid1.DataBind();

                                ddlSubCountyGrid1.Items.Insert(0, new ListItem("<--Select Sub County-->", "0"));

                                //Select the Sub County of Customer in DropDownList
                                string lblSubCountyGrid1 = (e.Row.FindControl("lblSubCounty1") as Label).Text;
                                ddlSubCountyGrid1.Items.FindByValue(lblSubCountyGrid1).Selected = true;
                            }
                        }
                        else
                            lblMessage.Text = "System cannot fetch data.";

                        var townsResponse = client.GetAsync("api/SetUp/GetAllTowns").Result;
                        if (townsResponse.IsSuccessStatusCode)
                        {
                            towns = townsResponse.Content.ReadAsAsync<List<Towns>>().Result;

                            //check if is in edit mode
                            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                            {
                                DropDownList ddlWardGrid = (DropDownList)e.Row.FindControl("ddlWardGrid");
                                //Bind subcategories data to dropdownlist
                                ddlWardGrid.DataSource = towns;
                                ddlWardGrid.DataTextField = "town_name";
                                ddlWardGrid.DataValueField = "town_id";
                                ddlWardGrid.DataBind();

                                ddlWardGrid.Items.Insert(0, new ListItem("<--Select Ward-->", "0"));

                                //Select the Sub County of Customer in DropDownList
                                string lblWardGrid = (e.Row.FindControl("lblWardGrid") as Label).Text;
                                ddlWardGrid.Items.FindByValue(lblWardGrid).Selected = true;
                            }
                            else
                            {
                                DropDownList ddlWardGrid1 = (e.Row.FindControl("ddlWardGrid1") as DropDownList);

                                ddlWardGrid1.DataSource = towns;
                                ddlWardGrid1.DataTextField = "town_name";
                                ddlWardGrid1.DataValueField = "town_id";
                                ddlWardGrid1.DataBind();

                                ddlWardGrid1.Items.Insert(0, new ListItem("<--Select Ward-->", "0"));

                                //Select the Sub County of Customer in DropDownList
                                string lblWardGrid1 = (e.Row.FindControl("lblWardGrid1") as Label).Text;
                                ddlWardGrid1.Items.FindByValue(lblWardGrid1).Selected = true;
                            }
                        }
                        else
                            lblMessage.Text = "System cannot fetch data.";

                        var customerTypeResponse = client.GetAsync("api/SetUp/GetAllCustomerTypes").Result;
                        if (customerTypeResponse.IsSuccessStatusCode)
                        {
                            customerTypes = customerTypeResponse.Content.ReadAsAsync<List<CustomerTypes>>().Result;

                            //check if is in edit mode
                            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                            {
                                DropDownList ddlCustomerTypeGrid = (DropDownList)e.Row.FindControl("ddlCustomerTypeGrid");
                                //Bind subcategories data to dropdownlist
                                ddlCustomerTypeGrid.DataSource = customerTypes;
                                ddlCustomerTypeGrid.DataTextField = "customer_type_name";
                                ddlCustomerTypeGrid.DataValueField = "customer_type_id";
                                ddlCustomerTypeGrid.DataBind();

                                ddlCustomerTypeGrid.Items.Insert(0, new ListItem("<--Select Customer Type-->", "0"));

                                //Select the Sub County of Customer in DropDownList
                                string lblCustomerTypeGrid = (e.Row.FindControl("lblCustomerTypeGrid") as Label).Text;
                                ddlCustomerTypeGrid.Items.FindByValue(lblCustomerTypeGrid).Selected = true;
                            }
                            else
                            {
                                DropDownList ddlCustomerTypeGrid1 = (e.Row.FindControl("ddlCustomerTypeGrid1") as DropDownList);

                                ddlCustomerTypeGrid1.DataSource = customerTypes;
                                ddlCustomerTypeGrid1.DataTextField = "customer_type_name";
                                ddlCustomerTypeGrid1.DataValueField = "customer_type_id";
                                ddlCustomerTypeGrid1.DataBind();

                                ddlCustomerTypeGrid1.Items.Insert(0, new ListItem("<--Select Customer Type-->", "0"));

                                //Select the Sub County of Customer in DropDownList
                                string lblCustomerTypeGrid1 = (e.Row.FindControl("lblCustomerTypeGrid1") as Label).Text;
                                ddlCustomerTypeGrid1.Items.FindByValue(lblCustomerTypeGrid1).Selected = true;
                            }
                        }
                        else
                            lblMessage.Text = "System cannot fetch data.";
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = ex.Message;
                    }
                }
            }
        }

        protected void ddlSubCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTownsBySubCountyId(int.Parse(ddlSubCounty.SelectedValue));
        }

        protected void ddlSubCountyGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlSubCountyGrid1 = (DropDownList)gvCustomers.Rows[0].FindControl("ddlSubCountyGrid1");

            GetEditedTownsBySubCountyId(int.Parse(ddlSubCountyGrid1.SelectedValue));
        }

    }

    public class CustomerDetail
    {
        public int customer_id { get; set; }
        public string user_name { get; set; }
        public string id_number { get; set; }
        public int msisdn { get; set; }
    }


    public class CustomerUpdate
    {
        public int customer_id { get; set; }
        public int access_channel_id { get; set; }
        public int customer_type_id { get; set; }
        public bool deactivated_account { get; set; }
        public string deactivate_msisdns { get; set; }
        public string email_address { get; set; }
        public string first_name { get; set; }
        public bool fully_registered { get; set; }
        public string id_number { get; set; }
        public int id_type_id { get; set; }
        public int information_mode_id { get; set; }
        public bool is_blacklisted { get; set; }
        public bool is_staff { get; set; }
        public bool is_test_customer { get; set; }
        public int language_id { get; set; }
        public string last_name { get; set; }
        public string middle_name { get; set; }
        public string postal_address { get; set; }
        public string tax_number { get; set; }
        public int town_id { get; set; }
        public int user_type_id { get; set; }
        public int sub_county_id { get; set; }
        public string msisdn { get; set; }
        public string updated_by { get; set; }
    }



    public class TownsRequest
    {
        public int town_id { get; set; }
        public int sub_county_id { get; set; }
    }

}