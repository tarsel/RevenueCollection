using RevenueCollection.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.UI.WebControls;

namespace RevenueCollection
{
    public partial class Town : System.Web.UI.Page
    {
        readonly string baseUrl = "http://county-001-site1.atempurl.com/";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewAllTowns();
                ViewAllSubCounties();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ddlSubCounty.SelectedIndex == 0)
            {
                lblMessage.Text = "Kindly Select a SubCounty!";
            }
            else if (string.IsNullOrEmpty(txtName.Text) || txtName.Text == "")
            {
                lblMessage.Text = "Ward Name is Empty!";
            }
            else
            {
                CreateNewTowns();

                txtDescription.Text = "";
                txtName.Text = "";
            }
        }

        public void CreateNewTowns()
        {
            using (var client = new HttpClient())
            {
                Towns cust = new Towns { created_by = "system", town_description = txtDescription.Text, town_name = txtName.Text, sub_county_id = int.Parse(ddlSubCounty.SelectedValue) };

                client.BaseAddress = new Uri(baseUrl);

                try
                {
                    var response = client.PostAsJsonAsync("api/SetUp/CreateTown", cust).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        lblMessage.Text = "The Ward: (" + txtName.Text + ") has been Created Successfully!";

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

                        gvTowns.DataSource = towns;
                        gvTowns.DataBind();
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

        protected void gvTowns_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTowns.PageIndex = e.NewPageIndex;
            ViewAllTowns();
        }

        protected void gvTowns_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvTowns.EditIndex = -1;
            ViewAllTowns();
        }

        protected void gvTowns_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvTowns.EditIndex = e.NewEditIndex;
            ViewAllTowns();
        }

        protected void gvTowns_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            TextBox txtEditTownName = gvTowns.Rows[e.RowIndex].FindControl("txtEditTownName") as TextBox;
            TextBox txtEditDescription = gvTowns.Rows[e.RowIndex].FindControl("txtEditDescription") as TextBox;
            DropDownList ddlSubCountyGrid = gvTowns.Rows[e.RowIndex].FindControl("ddlSubCountyGrid") as DropDownList;

            int townId = Convert.ToInt16(gvTowns.DataKeys[e.RowIndex].Values["town_id"].ToString());

            using (var client = new HttpClient())
            {
                Towns cust = new Towns { updated_by = "system", town_description = txtEditDescription.Text, town_name = txtEditTownName.Text, sub_county_id = int.Parse(ddlSubCountyGrid.SelectedValue), town_id = townId };

                client.BaseAddress = new Uri(baseUrl);

                try
                {
                    var response = client.PostAsJsonAsync("api/SetUp/UpdateTown", cust).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        lblMessage.Text = "The Ward: (" + txtEditTownName.Text + ") has been Updated Successfully!";

                    }
                    else
                        lblMessage.Text = "";
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message;
                }
            }

            gvTowns.EditIndex = -1;
            ViewAllTowns();
        }

        protected void gvTowns_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                List<SubCounties> subCountyList;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);

                    try
                    {
                        var response = client.GetAsync("api/SetUp/GetAllSubCounties").Result;

                        if (response.IsSuccessStatusCode)
                        {
                            subCountyList = response.Content.ReadAsAsync<List<SubCounties>>().Result;


                            //check if is in edit mode
                            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                            {
                                DropDownList ddlSubCountyGrid = (DropDownList)e.Row.FindControl("ddlSubCountyGrid");
                                //Bind subcategories data to dropdownlist
                                ddlSubCountyGrid.DataTextField = "sub_county_name";
                                ddlSubCountyGrid.DataValueField = "sub_county_id";
                                ddlSubCountyGrid.DataSource = subCountyList;
                                ddlSubCountyGrid.DataBind();

                                ddlSubCountyGrid.Items.Insert(0, new ListItem("<--Select Sub County-->", "0"));

                                //Select the Sub County of Customer in DropDownList
                                string subCounty = (e.Row.FindControl("lblSubCounty1") as Label).Text;
                                ddlSubCountyGrid.Items.FindByValue(subCounty).Selected = true;
                            }
                            else
                            {
                                DropDownList ddlSubCountyGrid1 = (e.Row.FindControl("ddlSubCountyGrid1") as DropDownList);

                                ddlSubCountyGrid1.DataSource = subCountyList;
                                ddlSubCountyGrid1.DataTextField = "sub_county_name";
                                ddlSubCountyGrid1.DataValueField = "sub_county_id";
                                ddlSubCountyGrid1.DataBind();

                                ddlSubCountyGrid1.Items.Insert(0, new ListItem("<--Select Sub County-->", "0"));

                                //Select the Sub County of Customer in DropDownList
                                string subCounty = (e.Row.FindControl("lblSubCounty") as Label).Text;
                                ddlSubCountyGrid1.Items.FindByValue(subCounty).Selected = true;
                            }
                        }
                        else
                            lblMessage.Text = "System cannot fetch data.";
                    }
                    catch (Exception ex)
                    {
                        // lblMessage.Text = ex.Message;
                    }
                }


            }
        }
    }

}