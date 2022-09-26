using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RevenueCollection
{
    public partial class IdType : System.Web.UI.Page
    {
        string baseUrl = "http://county-001-site1.atempurl.com/";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewAllIdTypes();
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
                CreateNewIdType();
            }
        }

        public void CreateNewIdType()
        {
            using (var client = new HttpClient())
            {
                IdTypes cust = new IdTypes { created_by = "system", id_type_description = txtDescription.Text, id_type_name = txtName.Text };

                client.BaseAddress = new Uri(baseUrl);

                try
                {
                    var response = client.PostAsJsonAsync("api/SetUp/CreateIdType", cust).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        lblMessage.Text = "Id Type Created Successfully!";
                        ViewAllIdTypes();
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

        public void ViewAllIdTypes()
        {
            List<IdTypes> idTypes;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                try
                {
                    var response = client.GetAsync("api/SetUp/GetAllIdTypes").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        idTypes = response.Content.ReadAsAsync<List<IdTypes>>().Result;

                        gvIdType.DataSource = idTypes;
                        gvIdType.DataBind();
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

        protected void gvIdType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvIdType.PageIndex = e.NewPageIndex;
            ViewAllIdTypes();
        }

        protected void gvIdType_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvIdType.EditIndex = -1;
            ViewAllIdTypes();

        }

        protected void gvIdType_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvIdType.EditIndex = e.NewEditIndex;
            ViewAllIdTypes();

        }

        protected void gvIdType_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            TextBox txtEditName = gvIdType.Rows[e.RowIndex].FindControl("txtEditName") as TextBox;
            TextBox txtEditDescription = gvIdType.Rows[e.RowIndex].FindControl("txtEditDescription") as TextBox;

            int idTypeId = Convert.ToInt16(gvIdType.DataKeys[e.RowIndex].Values["id_type_id"].ToString());

            using (var client = new HttpClient())
            {
                IdTypes cust = new IdTypes { updated_by = "system", id_type_name = txtEditName.Text, id_type_description = txtEditDescription.Text, id_type_id = idTypeId };

                client.BaseAddress = new Uri(baseUrl);

                try
                {
                    var response = client.PostAsJsonAsync("api/SetUp/UpdateIdType", cust).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        lblMessage.Text = "The Id Type: (" + txtEditName.Text + ") has been Updated Successfully!";
                    }
                    else
                        lblMessage.Text = "";
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message;
                }
            }

            gvIdType.EditIndex = -1;
            ViewAllIdTypes();
        }
    }


    public class IdTypes
    {
        public int id_type_id { get; set; }
        public string id_type_name { get; set; }
        public string id_type_description { get; set; }
        public DateTime date_created { get; set; }
        public string created_by { get; set; }
        public DateTime date_updated { get; set; }
        public string updated_by { get; set; }
    }
}