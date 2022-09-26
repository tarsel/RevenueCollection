using RevenueCollection.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.UI.WebControls;

namespace RevenueCollection
{
    public partial class Zone : System.Web.UI.Page
    {
        readonly string baseUrl = "http://county-001-site1.atempurl.com/";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewAllZones();
                ViewAllTowns();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ddlTown.SelectedIndex == 0)
            {
                lblMessage.Text = "Kindly Select a Ward!";
            }
            else if (string.IsNullOrEmpty(txtName.Text) || txtName.Text == "")
            {
                lblMessage.Text = "Zone is Empty!";
            }
            else
            {
                CreateNewZones();
                txtName.Text = "";
                txtDescription.Text = "";
            }
        }

        protected void gvZones_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvZones.PageIndex = e.NewPageIndex;
            ViewAllZones();
        }

        protected void gvZones_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvZones.EditIndex = -1;
            ViewAllZones();
        }

        protected void gvZones_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvZones.EditIndex = e.NewEditIndex;
            ViewAllZones();
        }

        protected void gvZones_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            TextBox txtEditZoneName = gvZones.Rows[e.RowIndex].FindControl("txtEditZoneName") as TextBox;
            TextBox txtEditDescription = gvZones.Rows[e.RowIndex].FindControl("txtEditDescription") as TextBox;
            DropDownList ddlTownGrid = gvZones.Rows[e.RowIndex].FindControl("ddlTownGrid") as DropDownList;

            int zoneId = Convert.ToInt16(gvZones.DataKeys[e.RowIndex].Values["zone_id"].ToString());

            using (var client = new HttpClient())
            {
                Zones cust = new Zones { zone_description = txtEditDescription.Text, zone_name = txtEditZoneName.Text, town_id = int.Parse(ddlTownGrid.SelectedValue), zone_id = zoneId };

                client.BaseAddress = new Uri(baseUrl);

                try
                {
                    var response = client.PostAsJsonAsync("api/SetUp/UpdateZone", cust).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        lblMessage.Text = "The Zone: (" + txtEditZoneName.Text + ") has been Updated Successfully!";

                    }
                    else
                        lblMessage.Text = "";
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message;
                }
            }

            gvZones.EditIndex = -1;
            ViewAllZones();

        }

        protected void gvZones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                List<Towns> townsList;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);

                    try
                    {
                        var response = client.GetAsync("api/SetUp/GetAllTowns").Result;

                        if (response.IsSuccessStatusCode)
                        {
                            townsList = response.Content.ReadAsAsync<List<Towns>>().Result;


                            //check if is in edit mode
                            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                            {
                                DropDownList ddlTownGrid = (DropDownList)e.Row.FindControl("ddlTownGrid");
                                //Bind subcategories data to dropdownlist
                                ddlTownGrid.DataTextField = "town_name";
                                ddlTownGrid.DataValueField = "town_id";
                                ddlTownGrid.DataSource = townsList;
                                ddlTownGrid.DataBind();

                                ddlTownGrid.Items.Insert(0, new ListItem("<--Select Ward-->", "0"));

                                //Select the Sub County of Customer in DropDownList
                                string Town = (e.Row.FindControl("lblTown1") as Label).Text;
                                ddlTownGrid.Items.FindByValue(Town).Selected = true;
                            }
                            else
                            {
                                DropDownList ddlTownGrid1 = (DropDownList)e.Row.FindControl("ddlTownGrid1");

                                ddlTownGrid1.DataSource = townsList;
                                ddlTownGrid1.DataTextField = "town_name";
                                ddlTownGrid1.DataValueField = "town_id";
                                ddlTownGrid1.DataBind();

                                ddlTownGrid1.Items.Insert(0, new ListItem("<--Select Town-->", "0"));

                                //Select the Sub County of Customer in DropDownList
                                string Town = (e.Row.FindControl("lblTown") as Label).Text;
                                ddlTownGrid1.Items.FindByValue(Town).Selected = true;
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


        public void ViewAllZones()
        {
            List<Zones> zones;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                try
                {
                    var response = client.GetAsync("api/SetUp/GetAllZones").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        zones = response.Content.ReadAsAsync<List<Zones>>().Result;

                        gvZones.DataSource = zones;
                        gvZones.DataBind();
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

        public void CreateNewZones()
        {
            using (var client = new HttpClient())
            {
                Zones cust = new Zones { created_by = "system", zone_description = txtDescription.Text, zone_name = txtName.Text, town_id = int.Parse(ddlTown.SelectedValue) };

                client.BaseAddress = new Uri(baseUrl);

                try
                {
                    var response = client.PostAsJsonAsync("api/SetUp/CreateZone", cust).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        lblMessage.Text = "The Zone: (" + txtName.Text + ") has been Created Successfully!";
                        ViewAllZones();
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

                        ddlTown.DataSource = towns;
                        ddlTown.DataTextField = "town_name";
                        ddlTown.DataValueField = "town_id";
                        ddlTown.DataBind();

                        ddlTown.Items.Insert(0, new ListItem("<--Select Ward-->", "0"));
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

}