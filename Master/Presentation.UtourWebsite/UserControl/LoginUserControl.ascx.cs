﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Impl;
using Domain.DataContracts;
using Domain.DataContracts.DTOs;
using ITI.Common.Utilities.Diagnostics.Trace;
using ITI.Nlayerd.Infrastructure.Data.UTour.Context;
using ITI.Nlayerd.Infrastructure.Data.UTour.Repositories;

public partial class UserControl_LoginUserControl : System.Web.UI.UserControl
{
    private UTOURDBEntities ctx = new UTOURDBEntities();
    private TraceManager traceManager = new TraceManager();
    
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void inputsubmit1_Click(object sender, EventArgs e)
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
                               UserType = UserTypes.Tourist
                           };
            var isValid = loginManagementService.AuthenticateUser(user);
            //MessageBox.Show(isValid.ToString());
            if (isValid.errorMessage!="")
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Sorry User Name Or Password Is Invalid.";
            }
            else
            {
                Session["UserName"] = txtUserName.Text;
                Response.Redirect(url: (Request.UrlReferrer!=null)?Request.UrlReferrer.ToString(): "Home.aspx");
                //this.Visible
            }
        }
        else
        {
            lblMessage.Visible = true;
            lblMessage.Text = "Sorry User Name Or Password Is Invalid.";
        }
    }
}