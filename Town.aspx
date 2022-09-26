<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Town.aspx.cs" Inherits="RevenueCollection.Town" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- page content -->
    <div class="row">
        <div class="col-lg-7">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>Set Up All Wards <small>
                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label></small></h5>
                    <div class="ibox-tools">
                        <a class="collapse-link">
                            <i class="fa fa-chevron-up"></i>
                        </a>

                    </div>
                </div>

                <div class="ibox-content">

                    <div class="form-group">
                        <asp:DropDownList ID="ddlSubCounty" runat="server" class="form-control" placeholder="Choose Sub County"></asp:DropDownList>

                    </div>

                    <div class="form-group">
                        <asp:TextBox ID="txtName" runat="server" class="form-control" placeholder="Ward Name" ></asp:TextBox>

                    </div>

                    <div class="form-group">
                        <asp:TextBox ID="txtDescription" runat="server" class="form-control" placeholder="Description" ></asp:TextBox>
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
                    <h5>All Wards in the System! </h5>
                    <div class="ibox-tools">
                        <a class="collapse-link">
                            <i class="fa fa-chevron-up"></i>
                        </a>

                    </div>
                </div>
                <div class="ibox-content" style=" overflow: scroll ;">
                    <asp:GridView ID="gvTowns" runat="server" class="table table-striped" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvTowns_PageIndexChanging" CellPadding="1" DataKeyNames="town_id" CellSpacing="1" AutoGenerateColumns="False" OnRowCancelingEdit="gvTowns_RowCancelingEdit" OnRowEditing="gvTowns_RowEditing" OnRowUpdating="gvTowns_RowUpdating" OnRowDataBound="gvTowns_RowDataBound">
                        <Columns>

                            <asp:TemplateField HeaderText="Ward">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditTownName" runat="server" Text='<%# Bind("town_name") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblTownName" runat="server" Text='<%# Bind("town_name") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtAddTownName" runat="server"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Sub County">
                                <EditItemTemplate>
                                    <asp:Label ID="lblSubCounty1" runat="server" Text='<%# Bind("sub_county_id") %>' Visible="false">        </asp:Label>
                                    <asp:DropDownList runat="server" ID="ddlSubCountyGrid"></asp:DropDownList>

                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSubCounty" runat="server" Text='<%# Bind("sub_county_id") %>' Visible="false">        </asp:Label>
                                    <asp:DropDownList runat="server" ID="ddlSubCountyGrid1" Enabled="false"></asp:DropDownList>

                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtAddSubcounty" runat="server"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Description">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditDescription" runat="server" Text='<%# Bind("town_description") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("town_description") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtAddDescription" runat="server"></asp:TextBox>
                                </FooterTemplate>
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
