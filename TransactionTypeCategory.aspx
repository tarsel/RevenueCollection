<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="TransactionTypeCategory.aspx.cs" Inherits="RevenueCollection.TransactionTypeCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- page content -->
    <div class="row">
        <div class="col-lg-7">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>Set Up All Transaction Type Categories <small>
                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label></small></h5>
                    <div class="ibox-tools">
                        <a class="collapse-link">
                            <i class="fa fa-chevron-up"></i>
                        </a>

                    </div>
                </div>

                <div class="ibox-content">

                    <div class="form-group">
                        <asp:DropDownList ID="ddlTransactionType" runat="server" class="form-control" placeholder="Choose Category"></asp:DropDownList>
                    </div>

                    <div class="form-group">
                        <asp:TextBox ID="txtName" runat="server" class="form-control" placeholder="Category Name" ></asp:TextBox>

                    </div>

                    <div class="form-group">
                        <asp:TextBox ID="txtAmount" runat="server" class="form-control" placeholder="Amount" ></asp:TextBox>
                    </div>

                    <div class="form-group row">
                        <div class="col-md-9 col-sm-9  offset-md-3">
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-primary" />
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btn btn-success" OnClick="btnSubmit_Click" />

                        </div>
                    </div>

                </div>
            </div>

        </div>


        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>Transaction Type Categories in the System! </h5>
                    <div class="ibox-tools">
                        <a class="collapse-link">
                            <i class="fa fa-chevron-up"></i>
                        </a>

                    </div>
                </div>
                <div class="ibox-content" style=" overflow: scroll ;">
                    <asp:GridView ID="gvTransactionTypeCategory" runat="server" class="table table-striped" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvTransactionTypeCategory_PageIndexChanging" CellPadding="1" DataKeyNames="transaction_type_category_id" CellSpacing="1" AutoGenerateColumns="False" OnRowCancelingEdit="gvTransactionTypeCategory_RowCancelingEdit" OnRowEditing="gvTransactionTypeCategory_RowEditing" OnRowUpdating="gvTransactionTypeCategory_RowUpdating" OnRowDataBound="gvTransactionTypeCategory_RowDataBound">
                        <Columns>

                            <asp:TemplateField HeaderText="Sub Category">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditName" runat="server" Text='<%# Bind("transaction_type_category_name") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("transaction_type_category_name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Transaction Type">
                                <EditItemTemplate>
                                    <asp:Label ID="lblTransactionType1" runat="server" Text='<%# Bind("transaction_type_id") %>' Visible="false">        </asp:Label>
                                    <asp:DropDownList runat="server" ID="ddlTransactionTypeGrid1"></asp:DropDownList>

                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblTransactionType" runat="server" Text='<%# Bind("transaction_type_id") %>' Visible="false">        </asp:Label>
                                    <asp:DropDownList runat="server" ID="ddlTransactionTypeGrid" Enabled="false"></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Amount">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditAmount" runat="server" Text='<%# Bind("amount") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("amount") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Operations">

                                <EditItemTemplate>
                                    <asp:Button ID="Button3" runat="server" CommandName="Update" Text="Update" class="btn btn-primary" />
                                    <asp:Button ID="Button4" runat="server" CommandName="Cancel" Text="Cancel" class="btn btn-primary" />
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <asp:Button ID="Button1" runat="server" CommandName="Edit" Text="Edit" class="btn btn-primary" Height="25px" />

                                </ItemTemplate>

                            </asp:TemplateField>

                        </Columns>

                    </asp:GridView>

                </div>
            </div>
        </div>


    </div>

</asp:Content>
