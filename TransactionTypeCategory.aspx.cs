using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.UI.WebControls;

using RevenueCollection.Models;


namespace RevenueCollection
{
    public partial class TransactionTypeCategory : System.Web.UI.Page
    {
        readonly string baseUrl = "http://county-001-site1.atempurl.com/";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblMessage.Text = "";
                ViewAllSubCategories();
                ViewAllTransactionTypes();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ddlTransactionType.SelectedIndex == 0)
            {
                lblMessage.Text = "Kindly Select an Item!";
            }
            else if (string.IsNullOrEmpty(txtName.Text) || txtName.Text == "")
            {
                lblMessage.Text = "Transaction Type Category is Empty!";
            }
            else if (string.IsNullOrEmpty(txtAmount.Text) || txtAmount.Text == "")
            {
                lblMessage.Text = "Amount is Empty!";
            }
            else
            {
                CreateNewCategories();
                ViewAllSubCategories();
                txtAmount.Text = "";
                txtName.Text = "";
            }
        }

        protected void gvTransactionTypeCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTransactionTypeCategory.PageIndex = e.NewPageIndex;
            ViewAllSubCategories();
        }

        protected void gvTransactionTypeCategory_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvTransactionTypeCategory.EditIndex = -1;
            ViewAllSubCategories();
        }

        protected void gvTransactionTypeCategory_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvTransactionTypeCategory.EditIndex = e.NewEditIndex;
            ViewAllSubCategories();
        }

        protected void gvTransactionTypeCategory_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            TextBox txtEditName = gvTransactionTypeCategory.Rows[e.RowIndex].FindControl("txtEditName") as TextBox;
            TextBox txtEditAmount = gvTransactionTypeCategory.Rows[e.RowIndex].FindControl("txtEditAmount") as TextBox;
            DropDownList ddlTransactionTypeGrid1 = gvTransactionTypeCategory.Rows[e.RowIndex].FindControl("ddlTransactionTypeGrid1") as DropDownList;

            int categoryId = Convert.ToInt16(gvTransactionTypeCategory.DataKeys[e.RowIndex].Values["transaction_type_category_id"].ToString());

            using (var client = new HttpClient())
            {
                TransactionTypeSubCategory cust = new TransactionTypeSubCategory { transaction_type_category_name = txtEditName.Text, transaction_type_category_id = categoryId, amount = int.Parse(txtEditAmount.Text), transaction_type_id = ddlTransactionTypeGrid1.SelectedIndex };

                client.BaseAddress = new Uri(baseUrl);

                try
                {
                    var response = client.PostAsJsonAsync("api/Transaction/UpdateTransactionTypeCategory", cust).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        lblMessage.Text = "The Category: (" + txtEditName.Text + ") has been Updated Successfully!";

                    }
                    else
                        lblMessage.Text = "";
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message;
                }
            }

            gvTransactionTypeCategory.EditIndex = -1;
            ViewAllSubCategories();

        }

        protected void gvTransactionTypeCategory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                List<TransactionTypes> transactionTypes;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);

                    try
                    {
                        var response = client.PostAsJsonAsync("api/Transaction/GetAllTransactionTypes", 0).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            transactionTypes = response.Content.ReadAsAsync<List<TransactionTypes>>().Result;


                            //check if is in edit mode
                            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                            {
                                DropDownList ddlTransactionTypeGrid1 = (DropDownList)e.Row.FindControl("ddlTransactionTypeGrid1");
                                //Bind subcategories data to dropdownlist
                                ddlTransactionTypeGrid1.DataTextField = "transaction_type_name";
                                ddlTransactionTypeGrid1.DataValueField = "transaction_type_id";
                                ddlTransactionTypeGrid1.DataSource = transactionTypes;
                                ddlTransactionTypeGrid1.DataBind();

                                ddlTransactionTypeGrid1.Items.Insert(0, new ListItem("<--Select Transaction Type-->", "0"));

                                //Select the Sub County of Customer in DropDownList
                                string transactionType1 = (e.Row.FindControl("lblTransactionType1") as Label).Text;
                                ddlTransactionTypeGrid1.Items.FindByValue(transactionType1).Selected = true;
                            }
                            else
                            {
                                DropDownList ddlTransactionTypeGrid = (e.Row.FindControl("ddlTransactionTypeGrid") as DropDownList);

                                ddlTransactionTypeGrid.DataSource = transactionTypes;
                                ddlTransactionTypeGrid.DataTextField = "transaction_type_name";
                                ddlTransactionTypeGrid.DataValueField = "transaction_type_id";
                                ddlTransactionTypeGrid.DataBind();

                                ddlTransactionTypeGrid.Items.Insert(0, new ListItem("<--Select Transaction Type-->", "0"));

                                //Select the Sub County of Customer in DropDownList
                                string transactionType = (e.Row.FindControl("lblTransactionType") as Label).Text;
                                ddlTransactionTypeGrid.Items.FindByValue(transactionType).Selected = true;
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

        public void CreateNewCategories()
        {
            using (var client = new HttpClient())
            {
                TransactionTypeSub cust = new TransactionTypeSub { amount = int.Parse(txtAmount.Text), transaction_type_id = ddlTransactionType.SelectedIndex, transaction_type_category_name = txtName.Text };

                client.BaseAddress = new Uri(baseUrl);

                try
                {
                    var response = client.PostAsJsonAsync("api/Transaction/CreateTransactionTypeCategory", cust).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        lblMessage.Text = "The Sub Category: (" + txtName.Text + ") has been Created Successfully!";

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

        public void ViewAllSubCategories()
        {
            List<TransactionTypeSubCategory> transactionTypeSubCategory;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                try
                {
                    var response = client.PostAsJsonAsync("api/Transaction/GetAllTransactionTypeCategory", 0).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        transactionTypeSubCategory = response.Content.ReadAsAsync<List<TransactionTypeSubCategory>>().Result;

                        gvTransactionTypeCategory.DataSource = transactionTypeSubCategory;
                        gvTransactionTypeCategory.DataBind();
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



        public void ViewAllTransactionTypes()
        {
            List<TransactionTypes> transactionTypes;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                try
                {
                    var response = client.PostAsJsonAsync("api/Transaction/GetAllTransactionTypes", 0).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        transactionTypes = response.Content.ReadAsAsync<List<TransactionTypes>>().Result;

                        ddlTransactionType.DataSource = transactionTypes;
                        ddlTransactionType.DataTextField = "transaction_type_name";
                        ddlTransactionType.DataValueField = "transaction_type_id";
                        ddlTransactionType.DataBind();

                        ddlTransactionType.Items.Insert(0, new ListItem("<--Select Sub Category-->", "0"));
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