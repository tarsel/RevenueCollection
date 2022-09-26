using RevenueCollection.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.UI.WebControls;

namespace RevenueCollection
{
    public partial class SubCounty : System.Web.UI.Page
    {
        string baseUrl = "http://county-001-site1.atempurl.com/";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewAllSubCounties();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || txtName.Text == "")
            {
                lblMessage.Text = "Sub County Name is Empty!";

            }
            else
            {
                CreateNewSubCounty();
                txtName.Text = "";
                txtDescription.Text = "";
            }
        }

        public void CreateNewSubCounty()
        {
            using (var client = new HttpClient())
            {
                SubCounties cust = new SubCounties { created_by = "system", sub_county_description = txtDescription.Text, sub_county_name = txtName.Text };

                client.BaseAddress = new Uri(baseUrl);

                try
                {
                    var response = client.PostAsJsonAsync("api/SetUp/CreateSubCounty", cust).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        lblMessage.Text = "Sub County Created Successfully!";
                        ViewAllSubCounties();
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
            List<SubCounties> subCounties;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                try
                {
                    var response = client.GetAsync("api/SetUp/GetAllSubCounties").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        subCounties = response.Content.ReadAsAsync<List<SubCounties>>().Result;

                        gvSubCounty.DataSource = subCounties;
                        gvSubCounty.DataBind();
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

        protected void gvSubCounty_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSubCounty.PageIndex = e.NewPageIndex;
            ViewAllSubCounties();
        }

        protected void gvSubCounty_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvSubCounty.EditIndex = -1;
            ViewAllSubCounties();
        }

        protected void gvSubCounty_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvSubCounty.EditIndex = e.NewEditIndex;
            ViewAllSubCounties();
        }


        protected void gvSubCounty_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            TextBox txtEditName = gvSubCounty.Rows[e.RowIndex].FindControl("txtEditName") as TextBox;
            TextBox txtEditDescription = gvSubCounty.Rows[e.RowIndex].FindControl("txtEditDescription") as TextBox;

            int subCountyId = Convert.ToInt16(gvSubCounty.DataKeys[e.RowIndex].Values["sub_county_id"].ToString());

            using (var client = new HttpClient())
            {
                SubCounties cust = new SubCounties { updated_by = "system", sub_county_name = txtEditName.Text, sub_county_description = txtEditDescription.Text, sub_county_id = subCountyId };

                client.BaseAddress = new Uri(baseUrl);

                try
                {
                    var response = client.PostAsJsonAsync("api/SetUp/UpdateSubCounty", cust).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        lblMessage.Text = "The Sub County: (" + txtEditName.Text + ") has been Updated Successfully!";
                    }
                    else
                        lblMessage.Text = "";
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message;
                }
            }

            gvSubCounty.EditIndex = -1;
            ViewAllSubCounties();
        }

    }


}
