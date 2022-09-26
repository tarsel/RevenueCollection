using RevenueCollection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RevenueCollection
{
    public partial class TransactionType : System.Web.UI.Page
    {
        readonly string baseUrl = "http://county-001-site1.atempurl.com/";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewAllTransactionTypes();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTransactionTypeName.Text) || txtTransactionTypeName.Text == "")
            {
                lblMessage.Text = "Transaction Type Name is Empty!";
            }
            else if (string.IsNullOrEmpty(txtAmount.Text) || txtAmount.Text == "")
            {
                lblMessage.Text = "Amount is Empty!";
            }
            else
            {
                CreateNewTransactionType();
                txtTransactionTypeName.Text = "";
                txtAmount.Text = "";
            }
        }


        public void CreateNewTransactionType()
        {
            using (var client = new HttpClient())
            {
                TransactionTypes cust = new TransactionTypes { amount = int.Parse(txtAmount.Text), transaction_type_name = txtTransactionTypeName.Text };

                client.BaseAddress = new Uri(baseUrl);

                try
                {
                    var response = client.PostAsJsonAsync("api/Transaction/CreateTransactionType", cust).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        lblMessage.Text = "Transaction Type Created Successfully!";
                        ViewAllTransactionTypes();
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

        public void ViewAllTransactionTypes()
        {
            List<TransactionTypes> transactionTypes;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                try
                {
                    var response = client.PostAsJsonAsync("api/Transaction/GetAllTransactionTypes", 1).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        transactionTypes = response.Content.ReadAsAsync<List<TransactionTypes>>().Result;

                        gvTransactionType.DataSource = transactionTypes;
                        gvTransactionType.DataBind();
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


        protected void gvTransactionType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTransactionType.PageIndex = e.NewPageIndex;
            ViewAllTransactionTypes();

        }

        protected void gvTransactionType_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvTransactionType.EditIndex = -1;
            ViewAllTransactionTypes();

        }

        protected void gvTransactionType_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvTransactionType.EditIndex = e.NewEditIndex;
            ViewAllTransactionTypes();

        }

        protected void gvTransactionType_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            TextBox txtEditName = gvTransactionType.Rows[e.RowIndex].FindControl("txtEditName") as TextBox;
            TextBox txtEditAmount = gvTransactionType.Rows[e.RowIndex].FindControl("txtEditAmount") as TextBox;

            int transactionTypeId = Convert.ToInt16(gvTransactionType.DataKeys[e.RowIndex].Values["transaction_type_id"].ToString());

            using (var client = new HttpClient())
            {
                TransactionTypes cust = new TransactionTypes { amount = int.Parse(txtEditAmount.Text), transaction_type_name = txtEditName.Text, transaction_type_id = transactionTypeId, friendly_name = txtEditName.Text };

                client.BaseAddress = new Uri(baseUrl);

                try
                {
                    var response = client.PostAsJsonAsync("api/Transaction/UpdateTransactionType", cust).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        lblMessage.Text = "The Transaction Type: (" + txtEditName.Text + ") has been Updated Successfully!";
                    }
                    else
                        lblMessage.Text = "";
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message;
                }
            }

            gvTransactionType.EditIndex = -1;
            ViewAllTransactionTypes();

        }

    }
}