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
    public partial class UserType : System.Web.UI.Page
    {
        readonly string baseUrl = "http://county-001-site1.atempurl.com/";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewAllUserTypes();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || txtName.Text == "")
            {
                lblMessage.Text = "User Type is Empty!";
            }
            else
            {
                CreateNewUserType();
                txtDescription.Text = "";
                txtName.Text = "";
            }
        }


        public void CreateNewUserType()
        {
            using (var client = new HttpClient())
            {
                UserTypes cust = new UserTypes { created_by = "system", user_type_description = txtDescription.Text, user_type_name = txtName.Text };

                client.BaseAddress = new Uri(baseUrl);

                try
                {
                    var response = client.PostAsJsonAsync("api/SetUp/CreateUserType", cust).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        lblMessage.Text = "User Type Created Successfully!";
                        ViewAllUserTypes();
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

        public void ViewAllUserTypes()
        {
            List<UserTypes> userTypes;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                try
                {
                    var response = client.GetAsync("api/SetUp/GetAllUserTypes").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        userTypes = response.Content.ReadAsAsync<List<UserTypes>>().Result;

                        gvUserType.DataSource = userTypes;
                        gvUserType.DataBind();
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

        protected void gvUserType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUserType.PageIndex = e.NewPageIndex;
            ViewAllUserTypes();
        }

        protected void gvUserType_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvUserType.EditIndex = -1;
            ViewAllUserTypes();
        }

        protected void gvUserType_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvUserType.EditIndex = e.NewEditIndex;
            ViewAllUserTypes();

        }

        protected void gvUserType_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            TextBox txtEditName = gvUserType.Rows[e.RowIndex].FindControl("txtEditName") as TextBox;
            TextBox txtEditDescription = gvUserType.Rows[e.RowIndex].FindControl("txtEditDescription") as TextBox;

            int userTypeId = Convert.ToInt16(gvUserType.DataKeys[e.RowIndex].Values["user_type_id"].ToString());

            using (var client = new HttpClient())
            {
                UserTypes cust = new UserTypes { updated_by = "system", user_type_name = txtEditName.Text, user_type_description = txtEditDescription.Text, user_type_id = userTypeId };

                client.BaseAddress = new Uri(baseUrl);

                try
                {
                    var response = client.PostAsJsonAsync("api/SetUp/UpdateUserType", cust).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        lblMessage.Text = "The User Type: (" + txtEditName.Text + ") has been Updated Successfully!";
                    }
                    else
                        lblMessage.Text = "";
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message;
                }
            }

            gvUserType.EditIndex = -1;
            ViewAllUserTypes();
        }
    }



}