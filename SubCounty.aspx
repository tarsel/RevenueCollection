<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SubCounty.aspx.cs" Inherits="RevenueCollection.SubCounty" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- page content -->
    <div class="row">
        <div class="col-lg-7">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>Set Up All Sub Counties <small>
                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label></small></h5>
                    <div class="ibox-tools">
                        <a class="collapse-link">
                            <i class="fa fa-chevron-up"></i>
                        </a>

                    </div>
                </div>

                <div class="ibox-content">
                    <div class="form-group">
                        <asp:TextBox ID="txtName" runat="server" class="form-control" placeholder="Name" ></asp:TextBox>
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


        <!-- Grid View to Display the Sub Counties -->
        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>All Sub Counties in the System! </h5>
                    <div class="ibox-tools">
                        <a class="collapse-link">
                            <i class="fa fa-chevron-up"></i>
                        </a>

                    </div>
                </div>
                <div class="ibox-content">
                    <asp:GridView ID="gvSubCounty" runat="server" class="table table-striped" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvSubCounty_PageIndexChanging" CellPadding="1" DataKeyNames="sub_county_id" CellSpacing="1" AutoGenerateColumns="False" OnRowCancelingEdit="gvSubCounty_RowCancelingEdit" OnRowEditing="gvSubCounty_RowEditing" OnRowUpdating="gvSubCounty_RowUpdating">

                        <Columns>

                            <asp:TemplateField HeaderText="Sub County">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditName" runat="server" Text='<%# Bind("sub_county_name") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSubCountyName" runat="server" Text='<%# Bind("sub_county_name") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtAddSubCountyName" runat="server"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Description">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditDescription" runat="server" Text='<%# Bind("sub_county_description") %>'> </asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("sub_county_description") %>'></asp:Label>
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
