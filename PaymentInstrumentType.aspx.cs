using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RevenueCollection
{
    public partial class PaymentInstrumentType : System.Web.UI.Page
    {
        string baseUrl = "http://county-001-site1.atempurl.com/";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewAllPaymentInstrumentTypes();
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPaymentInstrumentTypeName.Text)|| txtPaymentInstrumentTypeName.Text=="")
            {
                lblMessage.Text = "Payment Instrument Type is Empty!";

            }
            else
            {
                CreateNewPaymentInstrumentType();

            }
            txtPaymentInstrumentTypeName.Text = "";
        }

        public void CreateNewPaymentInstrumentType()
        {
            using (var client = new HttpClient())
            {
                PaymentInstrumentTypes cust = new PaymentInstrumentTypes { payment_instrument_type_name = txtPaymentInstrumentTypeName.Text };

                client.BaseAddress = new Uri(baseUrl);

                try
                {
                    var response = client.PostAsJsonAsync("api/Transaction/CreatePaymentInstrumentType", cust).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        lblMessage.Text = "Payment Instrument Type Created Successfully!";
                        ViewAllPaymentInstrumentTypes();
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

        public void ViewAllPaymentInstrumentTypes()
        {
            List<PaymentInstrumentTypes> paymentInstrumentTypes;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                try
                {
                    var response = client.PostAsJsonAsync("api/Transaction/GetAllPaymentInstrumentTypes",1).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        paymentInstrumentTypes = response.Content.ReadAsAsync<List<PaymentInstrumentTypes>>().Result;

                        gvPiType.DataSource = paymentInstrumentTypes;
                        gvPiType.DataBind();
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

        protected void gvPiType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPiType.PageIndex = e.NewPageIndex;
            ViewAllPaymentInstrumentTypes();

        }

        protected void gvPiType_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvPiType.EditIndex = -1;
            ViewAllPaymentInstrumentTypes();

        }

        protected void gvPiType_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvPiType.EditIndex = e.NewEditIndex;
            ViewAllPaymentInstrumentTypes();

        }

        protected void gvPiType_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            TextBox txtEditName = gvPiType.Rows[e.RowIndex].FindControl("txtEditName") as TextBox;
            //TextBox txtEditDescription = gvPiType.Rows[e.RowIndex].FindControl("txtEditDescription") as TextBox;

            int paymentInstrumentTypeId = Convert.ToInt16(gvPiType.DataKeys[e.RowIndex].Values["payment_instrument_type_id"].ToString());

            using (var client = new HttpClient())
            {
                PaymentInstrumentTypes cust = new PaymentInstrumentTypes { payment_instrument_type_id = paymentInstrumentTypeId, payment_instrument_type_name = txtEditName.Text };

                client.BaseAddress = new Uri(baseUrl);

                try
                {
                    var response = client.PostAsJsonAsync("api/Transaction/UpdatePaymentInstrumentType", cust).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        lblMessage.Text = "The Payment Instrument Type: (" + txtEditName.Text + ") has been Updated Successfully!";
                    }
                    else
                        lblMessage.Text = "";
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message;
                }
            }

            gvPiType.EditIndex = -1;
            ViewAllPaymentInstrumentTypes();

        }
    }


    public class PaymentInstrumentTypes
    {
        public int payment_instrument_type_id { get; set; }
        public string payment_instrument_type_name { get; set; }
    }

}