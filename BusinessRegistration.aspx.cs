using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.UI.WebControls;

using RevenueCollection.Models;

namespace RevenueCollection
{
    public partial class BusinessRegistration : System.Web.UI.Page
    {
        string baseUrl = "http://county-001-site1.atempurl.com/";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ViewAllSubCounties();
                ViewAllBusinessRegistrations();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBusinessName.Text) || txtBusinessName.Text == null) { lblMessage.Text = "Business Name is Empty!"; }
            else if (string.IsNullOrEmpty(txtPermitNo.Text) || txtPermitNo.Text == null) { lblMessage.Text = "Permit Number is Empty!"; }
            else if (string.IsNullOrEmpty(txtIdNumber.Text) || txtIdNumber.Text == null) { lblMessage.Text = "Id Number is Empty!"; }
            else if (string.IsNullOrEmpty(txtDoorStallNo.Text) || txtDoorStallNo.Text == null) { lblMessage.Text = "Door/Stall No is Empty!"; }
            else if (string.IsNullOrEmpty(txtFloor.Text) || txtFloor.Text == null) { lblMessage.Text = "Floor is Empty!"; }
            else if (string.IsNullOrEmpty(txtFullNames.Text) || txtFullNames.Text == null) { lblMessage.Text = "Full Names is Empty!"; }
            else if (string.IsNullOrEmpty(txtPinNo.Text) || txtPinNo.Text == null) { lblMessage.Text = "Pin No is Empty!"; }
            else if (string.IsNullOrEmpty(txtPlotNo.Text) || txtPlotNo.Text == null) { lblMessage.Text = "Plot No is Empty!"; }
            else if (string.IsNullOrEmpty(txtPostalCode.Text) || txtPostalCode.Text == null) { lblMessage.Text = "Postal Code is Empty!"; }
            else if (string.IsNullOrEmpty(txtRoadStreet.Text) || txtRoadStreet.Text == null) { lblMessage.Text = "Road/Street is Empty!"; }
            else if (ddlSubCounty.SelectedIndex == 0 || ddlWard.SelectedIndex == 0)
            {
                lblMessage.Text = "Kindly Select Sub County!";
            }
            else
            {
                CreateNewRegistration();
                ViewAllBusinessRegistrations();
            }
        }

        protected void gvBusinessReg_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBusinessReg.PageIndex = e.NewPageIndex;
            ViewAllBusinessRegistrations();
        }

        protected void gvBusinessReg_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvBusinessReg.EditIndex = -1;
            ViewAllBusinessRegistrations();
        }

        protected void gvBusinessReg_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvBusinessReg.EditIndex = e.NewEditIndex;
            ViewAllBusinessRegistrations();
        }

        protected void gvBusinessReg_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            TextBox txtEditBusinessName = gvBusinessReg.Rows[e.RowIndex].FindControl("txtEditBusinessName") as TextBox;
            TextBox txtEditPermitNo = gvBusinessReg.Rows[e.RowIndex].FindControl("txtEditPermitNo") as TextBox;
            //  DropDownList ddlCustomerGrid = gvBusinessReg.Rows[e.RowIndex].FindControl("ddlCustomerGrid") as DropDownList;
            DropDownList ddlSubCountyGrid = gvBusinessReg.Rows[e.RowIndex].FindControl("ddlSubCountyGrid") as DropDownList;
            DropDownList ddlWardGrid = gvBusinessReg.Rows[e.RowIndex].FindControl("ddlWardGrid") as DropDownList;

            int businessRegistrationId = Convert.ToInt16(gvBusinessReg.DataKeys[e.RowIndex].Values["business_registration_id"].ToString());

            using (var client = new HttpClient())
            {
                BusinessRegistrationRequest cust = new BusinessRegistrationRequest { business_registration_id = businessRegistrationId, business_registration_name = txtEditBusinessName.Text, permit_no = txtEditPermitNo.Text, sub_county_id = int.Parse(ddlSubCountyGrid.SelectedValue), town_id = int.Parse(ddlWardGrid.SelectedValue) };

                client.BaseAddress = new Uri(baseUrl);

                try
                {
                    var response = client.PostAsJsonAsync("api/Customer/UpdateBusinessRegistration", cust).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        lblMessage.Text = "The Entry: (" + txtEditBusinessName.Text + ") has been Updated Successfully!";

                    }
                    else
                        lblMessage.Text = "";
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message;
                }
            }

            gvBusinessReg.EditIndex = -1;

            ViewAllBusinessRegistrations();
        }

        protected void gvBusinessReg_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                List<SubCounties> subCounties;
                List<Towns> towns;
                // List<Customer> customers;


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
                                string lblSubCountyGrid = (e.Row.FindControl("lblSubCountyGrid") as Label).Text;
                                ddlSubCountyGrid.Items.FindByValue(lblSubCountyGrid).Selected = true;
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
                                string lblSubCountyGrid1 = (e.Row.FindControl("lblSubCountyGrid1") as Label).Text;
                                ddlSubCountyGrid1.Items.FindByValue(lblSubCountyGrid1).Selected = true;
                            }
                        }


                        var townsResponse = client.GetAsync("api/SetUp/GetAllTowns").Result;
                        if (townsResponse.IsSuccessStatusCode)
                        {
                            towns = townsResponse.Content.ReadAsAsync<List<Towns>>().Result;


                            //check if is in edit mode
                            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                            {
                                DropDownList ddlWardGrid = (DropDownList)e.Row.FindControl("ddlWardGrid");
                                //Bind subcategories data to dropdownlist
                                ddlWardGrid.DataTextField = "town_name";
                                ddlWardGrid.DataValueField = "town_id";
                                ddlWardGrid.DataSource = towns;
                                ddlWardGrid.DataBind();

                                ddlWardGrid.Items.Insert(0, new ListItem("<--Select Town-->", "0"));

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

                                ddlWardGrid1.Items.Insert(0, new ListItem("<--Select Town-->", "0"));

                                //Select the Sub County of Customer in DropDownList
                                string lblWardGrid1 = (e.Row.FindControl("lblWardGrid1") as Label).Text;
                                ddlWardGrid1.Items.FindByValue(lblWardGrid1).Selected = true;
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = ex.Message;
                    }
                }
            }
        }

        public void CreateNewRegistration()
        {
            using (var client = new HttpClient())
            {
                BusinessRegistrationRequest cust = new BusinessRegistrationRequest { business_registration_name = txtBusinessName.Text, details = txtDetails.Text, fully_paid = false, permit_no = txtPermitNo.Text, sub_county_id = int.Parse(ddlSubCounty.SelectedValue), town_id = int.Parse(ddlWard.SelectedValue), id_number = txtIdNumber.Text, building = txtBuilding.Text, door_stall_no = txtDoorStallNo.Text, floor = txtFloor.Text, full_names = txtFullNames.Text, pin_number = txtPinNo.Text, plot_no = txtPlotNo.Text, postal_code = txtPostalCode.Text, road_street = txtRoadStreet.Text };

                client.BaseAddress = new Uri(baseUrl);

                try
                {
                    var response = client.PostAsJsonAsync("api/Customer/BusinessRegistration", cust).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        lblMessage.Text = "The Business: (" + txtBusinessName.Text + ") has been Created Successfully!";

                    }
                    else
                        lblMessage.Text = "";
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

        public void ViewAllWards()
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

                        ddlWard.DataSource = towns;
                        ddlWard.DataTextField = "town_name";
                        ddlWard.DataValueField = "town_id";
                        ddlWard.DataBind();

                        ddlWard.Items.Insert(0, new ListItem("<--Select Ward!-->", "0"));
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

        public string GetCustomerByIdNumber(string idNumber)
        {
            string customerResponse = null;

            CustomerDetail cust = new CustomerDetail { id_number = idNumber };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                try
                {
                    var response = client.PostAsJsonAsync("api/Customer/GetCustomerByIdNumber", cust).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        Customer customer = response.Content.ReadAsAsync<Customer>().Result;

                        customerResponse = customer.first_name + " " + customer.middle_name + " " + customer.last_name;
                    }
                    else
                        lblMessage.Text = "System cannot fetch data.";
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message;
                }
            }

            return customerResponse;
        }

        public void ViewAllBusinessRegistrations()
        {

            List<BusinessRegistrationResponse> businessRegistrationResponse;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                try
                {
                    var response = client.PostAsJsonAsync("api/Customer/GetAllBusinessRegistrations", 0).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        businessRegistrationResponse = response.Content.ReadAsAsync<List<BusinessRegistrationResponse>>().Result;

                        gvBusinessReg.DataSource = businessRegistrationResponse;
                        gvBusinessReg.DataBind();
                    }
                    else
                    {
                        lblMessage.Text = "System cannot fetch data.";
                    }
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

                        ddlWard.DataSource = towns;
                        ddlWard.DataTextField = "town_name";
                        ddlWard.DataValueField = "town_id";
                        ddlWard.DataBind();

                        ddlWard.Items.Insert(0, new ListItem("<--Select Ward!-->", "0"));
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

        public void GetTransactionsByPermitNumber(string permitNumber)
        {
            GenericRequest transactionRequest = new GenericRequest { id_number = permitNumber, end_date = DateTime.Now, msisdn = 123456789, start_date = DateTime.Now, transaction_type_id = 1, ward_id = 1 };


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                try
                {
                    var response = client.PostAsJsonAsync("api/Transaction/VerifyPermitPaidByPermitNumber", transactionRequest).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        MasterTransactionRecordResponse transactions = response.Content.ReadAsAsync<MasterTransactionRecordResponse>().Result;

                        if (transactions.transaction_date.Year == DateTime.Now.Year)
                        {
                            txtGeneratePaymentStatus.Text = "PAID!";
                            txtGeneratePaymentStatus.BackColor = System.Drawing.Color.Green;
                            btnGenerateSubmit.Enabled = true;
                        }
                        else
                        {
                            txtGeneratePaymentStatus.Text = "NOT PAID!";
                            txtGeneratePaymentStatus.BackColor = System.Drawing.Color.Yellow;
                            btnGenerateSubmit.Enabled = false;
                        }
                    }
                    else
                    {
                        txtGeneratePaymentStatus.Text = "DOES NOT EXIST";
                        txtGeneratePaymentStatus.BackColor = System.Drawing.Color.Red;
                        btnGenerateSubmit.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message;
                }
            }
        }

        protected void ddlSubCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTownsBySubCountyId(int.Parse(ddlSubCounty.SelectedValue));
        }

        protected void txtIdNumber_TextChanged(object sender, EventArgs e)
        {
            string response = GetCustomerByIdNumber(txtIdNumber.Text);

            if (response == null)
            {
                txtFullNames.Text = "Customer Not Registered!";
            }
            else
            {
                txtFullNames.Text = response;
            }
        }

        protected void btnGenerateSubmit_Click(object sender, EventArgs e)
        {

        }

        protected void txtGeneratePermitNumber_TextChanged(object sender, EventArgs e)
        {
            GetTransactionsByPermitNumber(txtGeneratePermitNumber.Text);

        }

        protected void txtGenerateIdNumber_TextChanged(object sender, EventArgs e)
        {
            string response = GetCustomerByIdNumber(txtGenerateIdNumber.Text);

            if (response == null)
            {
                txtGenerateFullNames.Text = "Customer Not Registered!";
            }
            else
            {
                txtGenerateFullNames.Text = response;
            }
        }
    }

    public class BusinessRegistrationResponse
    {
        public int business_registration_id { get; set; }
        public string business_registration_name { get; set; }
        public int sub_county_id { get; set; }
        public int town_id { get; set; }
        public string permit_no { get; set; }
        public bool fully_paid { get; set; }
        public DateTime date_registered { get; set; }
        public string details { get; set; }
        public string id_number { get; set; }
        public string full_names { get; set; }
        public string postal_code { get; set; }
        public string plot_no { get; set; }
        public string road_street { get; set; }
        public string building { get; set; }
        public string floor { get; set; }
        public string door_stall_no { get; set; }
        public string pin_number { get; set; }
        public bool is_successful { get; set; }
        public int status_code { get; set; }
        public string message { get; set; }
    }


    public class BusinessRegistrationRequest
    {
        public int business_registration_id { get; set; }
        public string business_registration_name { get; set; }
        public int sub_county_id { get; set; }
        public int town_id { get; set; }
        public string permit_no { get; set; }
        public string id_number { get; set; }
        public string full_names { get; set; }
        public bool fully_paid { get; set; }
        public string details { get; set; }
        public string postal_code { get; set; }
        public string plot_no { get; set; }
        public string road_street { get; set; }
        public string building { get; set; }
        public string floor { get; set; }
        public string door_stall_no { get; set; }
        public string pin_number { get; set; }
    }


}