using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

using Dapper;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RevenueCollection.Models;
using RevenueCollection.Repository;

namespace RevenueCollection
{
    public partial class Register : System.Web.UI.Page
    {
        MenuRepository menuRepository = new MenuRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                PopulateRoles();
                PopulateUsers();
                PopulateGridRoles();
            }
        }

        protected void CreateUser_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(UserName.Text) || UserName.Text == "")
            {
                lblMessage.Text = "User Name is Empty!";
            }
            else if (string.IsNullOrEmpty(txtEmail.Text) || txtEmail.Text == "")
            {
                lblMessage.Text = "Email is Emtpy!";
            }
            else if (string.IsNullOrEmpty(txtPhoneNo.Text) || txtPhoneNo.Text == "")
            {
                lblMessage.Text = "Phone No is Empty!";
            }
            else if (string.IsNullOrEmpty(Password.Text) || Password.Text == "")
            {
                lblMessage.Text = "Password is Empty!";
            }
            else
            {
                // Default UserStore constructor uses the default connection string named: DefaultConnection
                var manager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
                var user = new IdentityUser() { UserName = UserName.Text, Email = txtEmail.Text, PhoneNumber = txtPhoneNo.Text, EmailConfirmed = true };
                IdentityResult result = manager.Create(user, Password.Text);

                string selectedRole = ddlRoles.SelectedItem.Text;

                if (result.Succeeded)
                {
                    CreateRole(selectedRole);//Create Role if it doesnt Exist

                    manager.AddToRole(user.Id, selectedRole);//Map the user to the role selected
                    PopulateUsers();
                    lblMessage.Text = string.Format("User {0} was created successfully!", user.UserName);
                }
                else
                {
                    lblMessage.Text = result.Errors.FirstOrDefault();
                }
            }

            UserName.Text = "";
            txtEmail.Text = "";
            txtPhoneNo.Text = "";
        }

        public void CreateRole(string selectedRole)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());

            if (!roleManager.RoleExists(selectedRole))
            {
                // first we create Admin role
                var role = new IdentityRole();
                role.Name = selectedRole;
                roleManager.Create(role);
            }
            PopulateRoles();
        }

        public void PopulateRoles()
        {
            ddlRoles.DataSource = menuRepository.GetAllRoles();
            ddlRoles.DataTextField = "Name";
            ddlRoles.DataValueField = "Id";
            ddlRoles.DataBind();

            ddlRoles.Items.Insert(0, new ListItem("<---- Select Role ---->", "0"));
        }


        public void PopulateUsers()
        {
            gvUsers.DataSource = menuRepository.GetAllUsers();
            gvUsers.DataBind();
        }

        public void PopulateGridRoles()
        {
            gvRoles.DataSource = menuRepository.GetAllRoles();
            gvRoles.DataBind();
        }

        protected void gvUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUsers.PageIndex = e.NewPageIndex;
            PopulateUsers();

        }

        protected void gvUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvUsers.EditIndex = -1;

            PopulateUsers();
        }

        protected void gvUsers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvUsers.EditIndex = e.NewEditIndex;

            PopulateUsers();
        }

        protected void gvUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
        }

        protected void gvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btnCreateRole_Click(object sender, EventArgs e)
        {
            CreateRole(txtRoleName.Text);
            Response.Redirect("/Register.aspx");
            lblMessage.Text = "Role Created Successfully!";
        }

        protected void gvRoles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRoles.PageIndex = e.NewPageIndex;
            PopulateGridRoles();
        }

        protected void gvRoles_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvRoles.EditIndex = e.NewEditIndex;
            PopulateGridRoles();
        }

        protected void gvRoles_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            TextBox txtEditRoleName = gvRoles.Rows[e.RowIndex].FindControl("txtEditRoleName") as TextBox;

            int roleId = Convert.ToInt16(gvRoles.DataKeys[e.RowIndex].Values["Id"].ToString());

            menuRepository.UpdateRole(roleId, txtEditRoleName.Text);
        }
        protected void gvRoles_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvRoles.EditIndex = -1;
            PopulateGridRoles();
        }

    }
}