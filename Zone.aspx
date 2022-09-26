<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Zone.aspx.cs" Inherits="RevenueCollection.Zone" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- page content -->
    <div class="row">
        <div class="col-lg-7">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>Set Up All Zones <small>
                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label></small></h5>
                    <div class="ibox-tools">
                        <a class="collapse-link">
                            <i class="fa fa-chevron-up"></i>
                        </a>

                    </div>
                </div>

                <div class="ibox-content">

                    <div class="form-group">
                        <asp:DropDownList ID="ddlTown" runat="server" class="form-control" placeholder="Choose Ward"></asp:DropDownList>
                    </div>

                    <div class="form-group">
                        <asp:TextBox ID="txtName" runat="server" class="form-control" placeholder="Zone Name" ></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <asp:TextBox ID="txtDescription" runat="server" class="form-control has-feedback-left" placeholder="Description"></asp:TextBox>
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
                    <h5>Zones in the System! </h5>
                    <div class="ibox-tools">
                        <a class="collapse-link">
                            <i class="fa fa-chevron-up"></i>
                        </a>

                    </div>
                </div>
                <div class="ibox-content">
                    <asp:GridView ID="gvZones" runat="server" class="table table-striped" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvZones_PageIndexChanging" CellPadding="1" DataKeyNames="zone_id" CellSpacing="1" AutoGenerateColumns="False" OnRowCancelingEdit="gvZones_RowCancelingEdit" OnRowEditing="gvZones_RowEditing" OnRowUpdating="gvZones_RowUpdating" OnRowDataBound="gvZones_RowDataBound">
                        <Columns>

                            <asp:TemplateField HeaderText="Zone">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditZoneName" runat="server" Text='<%# Bind("zone_name") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblZoneName" runat="server" Text='<%# Bind("zone_name") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtAddZoneName" runat="server"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Town">
                                <EditItemTemplate>
                                    <asp:Label ID="lblTown1" runat="server" Text='<%# Bind("town_id") %>' Visible="false">        </asp:Label>
                                    <asp:DropDownList runat="server" ID="ddlTownGrid"></asp:DropDownList>

                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblTown" runat="server" Text='<%# Bind("town_id") %>' Visible="false">        </asp:Label>
                                    <asp:DropDownList runat="server" ID="ddlTownGrid1" Enabled="false"></asp:DropDownList>

                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtAddTown" runat="server"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Description">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditDescription" runat="server" Text='<%# Bind("zone_description") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("zone_description") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtAddDescription" runat="server"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Operations">

                                <EditItemTemplate>
                                    <asp:Button ID="Button3" runat="server" CommandName="Update" Text="Update" class="btn btn-primary" UseSubmitBehavior="false" />
                                    <asp:Button ID="Button4" runat="server" CommandName="Cancel" Text="Cancel" class="btn btn-primary" UseSubmitBehavior="false" />
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <asp:Button ID="Button1" runat="server" CommandName="Edit" Text="Edit" class="btn btn-primary" Height="25px" UseSubmitBehavior="false" />

                                </ItemTemplate>

                            </asp:TemplateField>

                        </Columns>

                    </asp:GridView>

                </div>
            </div>
        </div>
    </div>

</asp:Content>
