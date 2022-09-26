<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="BusinessRegistration.aspx.cs" Inherits="RevenueCollection.BusinessRegistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- page content -->
    <%--<div class="wrapper wrapper-content animated fadeInRight">--%>
    <div class="row">
        <div class="col-lg-7">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>Register Business <small>
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
                                <asp:TextBox ID="txtIdNumber" runat="server" class="form-control" placeholder="Id Number" AutoPostBack="true" OnTextChanged="txtIdNumber_TextChanged" CausesValidation="true" ></asp:TextBox>
                                <span class="required"></span>
                            </div>

                            <div class="form-group">
                                <asp:TextBox ID="txtFullNames" runat="server" class="form-control" placeholder="Full Names" ReadOnly="true" ForeColor="Red" Font-Bold="true"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <asp:TextBox ID="txtBusinessName" runat="server" class="form-control" placeholder="Business Name" ></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <asp:TextBox ID="txtPermitNo" runat="server" class="form-control" placeholder="Permit No:" ></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <asp:DropDownList ID="ddlSubCounty" runat="server" class="form-control" placeholder="Choose Sub County" OnSelectedIndexChanged="ddlSubCounty_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>

                            <div class="form-group">
                                <asp:DropDownList ID="ddlWard" runat="server" class="form-control" placeholder="Choose Ward"></asp:DropDownList>
                            </div>

                            <div class="form-group">
                                <asp:TextBox ID="txtDetails" runat="server" class="form-control" placeholder="Details" ></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <asp:TextBox ID="txtPostalCode" runat="server" class="form-control" placeholder="Postal Code" ></asp:TextBox>
                            </div>


                        </div>
                        <div class="col-sm-6">

                            <div class="form-group">
                                <asp:TextBox ID="txtPlotNo" runat="server" class="form-control" placeholder="Plot No" ></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <asp:TextBox ID="txtRoadStreet" runat="server" class="form-control" placeholder="Road/Street" ></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <asp:TextBox ID="txtBuilding" runat="server" class="form-control" placeholder="Building" ></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <asp:TextBox ID="txtFloor" runat="server" class="form-control" placeholder="Floor" ></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <asp:TextBox ID="txtDoorStallNo" runat="server" class="form-control" placeholder="Door/Stall No" ></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <asp:TextBox ID="txtPinNo" runat="server" class="form-control" placeholder="PIN No" ></asp:TextBox>
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

        <div class="col-lg-5">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>Generate Business Permit</h5>
                    <div class="ibox-tools">
                        <a class="collapse-link">
                            <i class="fa fa-chevron-up"></i>
                        </a>

                    </div>
                </div>


                <div class="ibox-content">

                    <div class="form-group">
                        <asp:TextBox ID="txtGenerateIdNumber" runat="server" class="form-control" placeholder="Id Number" AutoPostBack="true" OnTextChanged="txtGenerateIdNumber_TextChanged" CausesValidation="true" ></asp:TextBox>
                        <span class="required"></span>
                    </div>

                    <div class="form-group">
                        <asp:TextBox ID="txtGenerateFullNames" runat="server" class="form-control" placeholder="Full Names" ReadOnly="true" ForeColor="Red" Font-Bold="true"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <asp:TextBox ID="txtGeneratePermitNumber" runat="server" class="form-control" placeholder="Permit Number" AutoPostBack="true" OnTextChanged="txtGeneratePermitNumber_TextChanged" CausesValidation="true"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <asp:TextBox ID="txtGeneratePaymentStatus" runat="server" class="form-control" placeholder="Payment Status" ReadOnly="true" ForeColor="Red" Font-Bold="true"></asp:TextBox>
                    </div>

                    <div class="form-group row">
                        <div class="col-md-9 col-sm-9  offset-md-3">
                            <asp:Button ID="btnGenerateCancel" runat="server" Text="Cancel" class="btn btn-primary" />
                            <asp:Button ID="btnGenerateSubmit" runat="server" Text="Generate" class="btn btn-success" OnClick="btnGenerateSubmit_Click" Enabled="false" />

                        </div>
                    </div>
                </div>


            </div>
        </div>


    </div>

    <div class="col-lg-12">
        <div class="ibox ">
            <div class="ibox-title">
                <h5>All Businessess </h5>
                <div class="ibox-tools">
                    <a class="collapse-link">
                        <i class="fa fa-chevron-up"></i>
                    </a>

                </div>
            </div>
            <div class="ibox-content" style=" overflow: scroll ;">
                <asp:GridView ID="gvBusinessReg" runat="server" class="table table-striped" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvBusinessReg_PageIndexChanging" DataKeyNames="business_registration_id" AutoGenerateColumns="False" OnRowCancelingEdit="gvBusinessReg_RowCancelingEdit" OnRowEditing="gvBusinessReg_RowEditing" OnRowUpdating="gvBusinessReg_RowUpdating" OnRowDataBound="gvBusinessReg_RowDataBound">
                    <Columns>

                        <asp:TemplateField HeaderText="Business Name">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditBusinessName" runat="server" Text='<%# Bind("business_registration_name") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblBusinessName" runat="server" Text='<%# Bind("business_registration_name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Full Names">
                            <ItemTemplate>
                                <asp:Label ID="lblFullNames" runat="server" Text='<%# Bind("full_names") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Registration Date">
                            <ItemTemplate>
                                <asp:Label ID="lblDateRegistered" runat="server" Text='<%# Bind("date_registered") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Permit No">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditPermitNo" runat="server" Text='<%# Bind("permit_no") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPermitNo" runat="server" Text='<%# Bind("permit_no") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

<%--                        <asp:TemplateField HeaderText="Fully Paid">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditFullyPaid" runat="server" Text='<%# Bind("fully_paid") %>' Enabled="false"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblFullyPaid" runat="server" Text='<%# Bind("fully_paid") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>

                        <asp:TemplateField HeaderText="Sub County">
                            <EditItemTemplate>
                                <asp:Label ID="lblSubCountyGrid" runat="server" Text='<%# Bind("sub_county_id") %>' Visible="false">        </asp:Label>
                                <asp:DropDownList runat="server" ID="ddlSubCountyGrid"></asp:DropDownList>

                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSubCountyGrid1" runat="server" Text='<%# Bind("sub_county_id") %>' Visible="false">        </asp:Label>
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

                        </asp:TemplateField>

                    </Columns>

                </asp:GridView>

            </div>
        </div>
    </div>

</asp:Content>
