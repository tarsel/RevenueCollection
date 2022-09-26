<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CreateCustomer.aspx.cs" Inherits="RevenueCollection.CreateCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- page content -->
    <div class="row">
        <div class="col-lg-7">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>Customer Registration <small>
                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label></small></h5>
                    <div class="ibox-tools">
                        <a class="collapse-link">
                            <i class="fa fa-chevron-up"></i>
                        </a>

                    </div>
                </div>


                <div class="ibox-content">
                    <div class="row">
                        <div class="col-sm-6 b-r">

                            <div class="form-group">
                                <asp:TextBox ID="txtFirstName" runat="server" class="form-control" placeholder="First Name" ></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <asp:TextBox ID="txtMiddleName" runat="server" class="form-control" placeholder="Middle Name" ></asp:TextBox>

                            </div>

                            <div class="form-group">
                                <asp:TextBox ID="txtLastName" runat="server" class="form-control" placeholder="Last Name" ></asp:TextBox>

                            </div>

                            <div class="form-group">
                                <asp:TextBox ID="txtEmail" runat="server" class="form-control" placeholder="Email" ></asp:TextBox>

                            </div>


                        </div>
                        <div class="col-sm-6">
                            
                            <div class="form-group">
                                <asp:TextBox ID="txtPhoneNumber" runat="server" class="form-control" placeholder="Phone" ></asp:TextBox>

                            </div>

                            <div class="form-group">
                                <asp:TextBox ID="txtIdNumber" runat="server" class="form-control" placeholder="Id Number" ></asp:TextBox>

                            </div>

                            <div class="form-group">
                                <asp:DropDownList ID="ddlSubCounty" runat="server" class="form-control" placeholder="Choose Sub County" OnSelectedIndexChanged="ddlSubCounty_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>

                            </div>


                            <div class="form-group">
                                <asp:DropDownList ID="ddlTown" runat="server" class="form-control" placeholder="Choose Sub County"></asp:DropDownList>

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

            </div>
        </div>

        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>Customers in the Entire County! </h5>
                    <div class="ibox-tools">
                        <a class="collapse-link">
                            <i class="fa fa-chevron-up"></i>
                        </a>

                    </div>
                </div>
                <div class="ibox-content" style=" overflow: scroll ;">
                    <asp:GridView ID="gvCustomers" runat="server" class="table table-striped" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvCustomers_PageIndexChanging" OnRowCancelingEdit="gvCustomers_RowCancelingEdit" OnRowEditing="gvCustomers_RowEditing" OnRowUpdating="gvCustomers_RowUpdating" EmptyDataText="No records has been added." OnRowDataBound="gvCustomers_RowDataBound" AutoGenerateColumns="false" DataKeyNames="customer_id">
                        <Columns>
                            <asp:TemplateField HeaderText="First Name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditFirstName" runat="server" Text='<%# Bind("first_name") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblFirstName" runat="server" Text='<%# Bind("first_name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Middle Name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditMiddleName" runat="server" Text='<%# Bind("middle_name") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblMiddleName" runat="server" Text='<%# Bind("middle_name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Last Name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditLastName" runat="server" Text='<%# Bind("last_name") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblLastName" runat="server" Text='<%# Bind("last_name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Id Number">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditIdNumber" runat="server" Text='<%# Bind("id_number") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblIdNumber" runat="server" Text='<%# Bind("id_number") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Customer Type">
                                <EditItemTemplate>
                                    <asp:Label ID="lblCustomerTypeGrid" runat="server" Text='<%# Bind("customer_type_id") %>' Visible="false">        </asp:Label>
                                    <asp:DropDownList runat="server" ID="ddlCustomerTypeGrid"></asp:DropDownList>

                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCustomerTypeGrid1" runat="server" Text='<%# Bind("customer_type_id") %>' Visible="false">        </asp:Label>
                                    <asp:DropDownList runat="server" ID="ddlCustomerTypeGrid1" Enabled="false"></asp:DropDownList>

                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Sub County">
                                <EditItemTemplate>
                                    <asp:Label ID="lblSubCounty" runat="server" Text='<%# Bind("sub_county_id") %>' Visible="false">        </asp:Label>
                                    <asp:DropDownList runat="server" ID="ddlSubCountyGrid" OnSelectedIndexChanged="ddlSubCountyGrid_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>

                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSubCounty1" runat="server" Text='<%# Bind("sub_county_id") %>' Visible="false">        </asp:Label>
                                    <asp:DropDownList runat="server" ID="ddlSubCountyGrid1" Enabled="false"></asp:DropDownList>

                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Ward">
                                <EditItemTemplate>
                                    <asp:Label ID="lblWardGrid" runat="server" Text='<%# Bind("town_id") %>' Visible="false">        </asp:Label>
                                    <asp:DropDownList runat="server" ID="ddlWardGrid"></asp:DropDownList>

                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblWardGrid1" runat="server" Text='<%# Bind("town_id") %>' Visible="false">        </asp:Label>
                                    <asp:DropDownList runat="server" ID="ddlWardGrid1" Enabled="false"></asp:DropDownList>

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

                                <FooterTemplate>
                                    <asp:Button ID="addnew" runat="server" CommandName="Add New" Text="Add New" class="btn btn-success" />
                                </FooterTemplate>

                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </div>
            </div>

        </div>


    </div>




</asp:Content>
