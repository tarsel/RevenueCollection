<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="UserType.aspx.cs" Inherits="RevenueCollection.UserType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- page content -->
    <div class="row">
        <div class="col-lg-7">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>Set Up All User Types <small>
                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label></small></h5>
                    <div class="ibox-tools">
                        <a class="collapse-link">
                            <i class="fa fa-chevron-up"></i>
                        </a>

                    </div>
                </div>

                <div class="ibox-content">
                    <div class="form-group">
                        <asp:TextBox ID="txtName" runat="server" class="form-control" placeholder="Name"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <asp:TextBox ID="txtDescription" runat="server" class="form-control" placeholder="Description"></asp:TextBox>
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


        <!-- Grid View to Display the User Types -->
        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>User Types in the System! </h5>
                    <div class="ibox-tools">
                        <a class="collapse-link">
                            <i class="fa fa-chevron-up"></i>
                        </a>

                    </div>
                </div>
                <div class="ibox-content">
                    <asp:GridView ID="gvUserType" runat="server" class="table table-striped" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvUserType_PageIndexChanging" CellPadding="1" DataKeyNames="user_type_id" CellSpacing="1" AutoGenerateColumns="False" OnRowCancelingEdit="gvUserType_RowCancelingEdit" OnRowEditing="gvUserType_RowEditing" OnRowUpdating="gvUserType_RowUpdating">

                        <Columns>

                            <asp:TemplateField HeaderText="User Type">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditName" runat="server" Text='<%# Bind("user_type_name") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblUserTypeName" runat="server" Text='<%# Bind("user_type_name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Description">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditDescription" runat="server" Text='<%# Bind("user_type_description") %>'> </asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("user_type_description") %>'></asp:Label>
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
        <!-- End GridView-->


    </div>

</asp:Content>
