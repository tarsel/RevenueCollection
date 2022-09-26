using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.UI.WebControls;

using RevenueCollection.Models;

namespace RevenueCollection
{
    public partial class CustomerType : System.Web.UI.Page
    {
        string baseUrl = "http://county-001-site1.atempurl.com/";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewAllCustomerTypes();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || txtName.Text == null)
            {
                lblMessage.Text = "Customer Type is Empty!";
            }
            else
            {
                CreateNewCustomerType();
            }
        }

        public void CreateNewCustomerType()
        {
            using (var client = new HttpClient())
            {
                CustomerTypes cust = new CustomerTypes { created_by = "system", customer_type_description = txtDescription.Text, customer_type_name = txtName.Text };

                client.BaseAddress = new Uri(baseUrl);

                try
                {
                    var response = client.PostAsJsonAsync("api/SetUp/CreateCustomerType", cust).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        lblMessage.Text = "Customer Type Created Successfully!";
                        ViewAllCustomerTypes();
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

        public void ViewAllCustomerTypes()
        {
            List<CustomerTypes> customerTypes;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                try
                {
                    var response = client.GetAsync("api/SetUp/GetAllCustomerTypes").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        customerTypes = response.Content.ReadAsAsync<List<CustomerTypes>>().Result;

                        gvCustomerType.DataSource = customerTypes;
                        gvCustomerType.DataBind();
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

        protected void gvCustomerType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCustomerType.PageIndex = e.NewPageIndex;
            ViewAllCustomerTypes();
        }

        protected void gvCustomerType_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvCustomerType.EditIndex = -1;
            ViewAllCustomerTypes();
        }

        protected void gvCustomerType_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvCustomerType.EditIndex = e.NewEditIndex;
            ViewAllCustomerTypes();
        }

        protected void gvCustomerType_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            TextBox txtEditName = gvCustomerType.Rows[e.RowIndex].FindControl("txtEditName") as TextBox;
            TextBox txtEditDescription = gvCustomerType.Rows[e.RowIndex].FindControl("txtEditDescription") as TextBox;

            int customerTypeId = Convert.ToInt16(gvCustomerType.DataKeys[e.RowIndex].Values["customer_type_id"].ToString());

            using (var client = new HttpClient())
            {
                CustomerTypes cust = new CustomerTypes { updated_by = "system", customer_type_name = txtEditName.Text, customer_type_description = txtEditDescription.Text, customer_type_id = customerTypeId };

                client.BaseAddress = new Uri(baseUrl);

                try
                {
                    var response = client.PostAsJsonAsync("api/SetUp/UpdateCustomerType", cust).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        lblMessage.Text = "The Customer Type: (" + txtEditName.Text + ") has been Updated Successfully!";
                    }
                    else
                        lblMessage.Text = "";
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message;
                }
            }

            gvCustomerType.EditIndex = -1;
            ViewAllCustomerTypes();
        }
    }


}