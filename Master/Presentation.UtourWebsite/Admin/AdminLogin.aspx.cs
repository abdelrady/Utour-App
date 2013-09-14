using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITI.Nlayerd.Infrastructure.Data.UTour.Repositories;
using ITI.Nlayerd.Infrastructure.Data.UTour.Context;
using ITI.Common.Utilities.Diagnostics.Trace;
using Application.Impl;
using Domain.DataContracts;
using Domain.DataContracts.DTOs;

public partial class AdminHomePage : System.Web.UI.Page
{
    private UTOURDBEntities ctx = new UTOURDBEntities();
    private TraceManager traceManager = new TraceManager();

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtUserName.Text) && !string.IsNullOrEmpty(txtPassword.Text) &&
           !string.IsNullOrWhiteSpace(txtUserName.Text) && !string.IsNullOrWhiteSpace(txtPassword.Text))
        {

            AdminUsersRepository adminUsersRepository = new AdminUsersRepository(ctx, traceManager);
            TouristRepository touristRepository = new TouristRepository(ctx, traceManager);

            LoginManagementService loginManagementService = new LoginManagementService(adminUsersRepository,
                                                                                       touristRepository);
            var user = new UserAuthInfo()
                           {
                               UserName = txtUserName.Text,
                               Password = txtPassword.Text,
                               UserType = UserTypes.Admin
                           };
            var isValid = loginManagementService.AuthenticateUser(user);
            if (isValid.errorMessage!="")
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Sorry User Name Or Password Is Invalid.";
            }
            else
            {
                Session["AdminUserName"] = txtUserName.Text;
                Response.Redirect("AdminHome.aspx");
            }
        }
        else
        {
            lblMessage.Visible = true;
            lblMessage.Text = "Sorry User Name Or Password Is Invalid.";
        }
    }
}