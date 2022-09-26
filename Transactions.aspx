<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Transactions.aspx.cs" Inherits="RevenueCollection.Transactions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="refresh" content="8">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row">
        <div class="col-lg-7">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>Filter Transactions By: <small>
                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label></small></h5>
                    <div class="ibox-tools">
                        <a class="collapse-link">
                            <i class="fa fa-chevron-up"></i>
                        </a>

                    </div>
                </div>

                <div class="ibox-content">

                    <div class="form-group">
                        <asp:DropDownList ID="ddlSubCounty" runat="server" class="form-control" placeholder="Choose Sub County" OnSelectedIndexChanged="ddlSubCounty_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </div>

                    <div class="form-group">
                        <asp:DropDownList ID="ddlTown" runat="server" class="form-control" placeholder="Choose Sub County" AutoPostBack="true" OnSelectedIndexChanged="ddlTown_SelectedIndexChanged"></asp:DropDownList>
                    </div>

                    <div class="form-group">
                        <asp:TextBox ID="txtStartDate" runat="server" class="form-control" placeholder="Start Date"></asp:TextBox>

                    </div>

                    <div class="form-group">
                        <asp:TextBox ID="txtEndDate" runat="server" class="form-control" placeholder="End Date"></asp:TextBox>

                    </div>

                    <div class="form-group row">
                        <div class="col-md-9 col-sm-9  offset-md-3">
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-primary" OnClick="btnCancel_Click" />
                            <asp:Button ID="btnSubmit" runat="server" Text="Search" class="btn btn-success" />

                        </div>
                    </div>

                </div>
            </div>

        </div>



        <!-- Grid View to Display the User Types -->
        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>All Transactions in the System! </h5>
                    <div class="ibox-tools">
                        <a class="collapse-link">
                            <i class="fa fa-chevron-up"></i>
                        </a>

                    </div>
                </div>
                <div class="ibox-content" style=" overflow: scroll ;">
                    <asp:GridView ID="gvTransactions" runat="server" class="table table-striped" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvTransactions_PageIndexChanging" CellPadding="1" DataKeyNames="master_transaction_record_id" CellSpacing="1" AutoGenerateColumns="False" OnRowDataBound="gvTransactions_RowDataBound">
                        <Columns>

                            <asp:TemplateField HeaderText="Customer Name">

                                <ItemTemplate>
                                    <asp:Label ID="lblCustomer" runat="server" Text='<%# Bind("payer_id") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblCustomerNames" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Trxn Amount">

                                <ItemTemplate>
                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("amount") %>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Service">
                                <ItemTemplate>
                                    <asp:Label ID="lblService" runat="server" Text='<%# Bind("transaction_type_id") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblTransactionType" runat="server"></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Category">
                                <ItemTemplate>
                                    <asp:Label ID="lblCategory" runat="server" Text='<%# Bind("transaction_category_type_id") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblTransactionCategory" runat="server"></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Bal Before Trxn">
                                <ItemTemplate>
                                    <asp:Label ID="lblTrxnBefore" runat="server" Text='<%# Bind("payer_balance_before_transaction") %>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Bal. After Trxn">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditTrxnAfter" runat="server" Text='<%# Bind("payer_balance_after_transaction") %>'> </asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblTrxnAfter" runat="server" Text='<%# Bind("payer_balance_after_transaction") %>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Town">

                                <ItemTemplate>
                                    <asp:Label ID="lblTown" runat="server" Text='<%# Bind("town_id") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblWard" runat="server"></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Sub County">

                                <ItemTemplate>
                                    <asp:Label ID="lblSubCounty" runat="server" Text='<%# Bind("sub_county_id") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblSubCounty1" runat="server"></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Id Number">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditIdNumber" runat="server" Text='<%# Bind("id_number") %>'> </asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblIdNumber" runat="server" Text='<%# Bind("id_number") %>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Record">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditKeyIdentifier" runat="server" Text='<%# Bind("key_identifier") %>'> </asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblKeyIdentifier" runat="server" Text='<%# Bind("key_identifier") %>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Trxn Date">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditDate" runat="server" Text='<%# Bind("transaction_date") %>'> </asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text='<%# Bind("transaction_date") %>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>


                        </Columns>

                    </asp:GridView>

                </div>
            </div>
        </div>
        <!-- End GridView-->



    </div>

</asp:Content>
