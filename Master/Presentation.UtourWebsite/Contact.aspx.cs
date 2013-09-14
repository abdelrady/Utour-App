using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;

public partial class Contact_Us : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void btnSendmail_Click(object sender, EventArgs e)
    {

        //// System.Web.Mail.SmtpMail.SmtpServer is obsolete in 2.0
        //// System.Net.Mail.SmtpClient is the alternate class for this in 2.0
        //SmtpClient smtpClient = new SmtpClient();
        //MailMessage message = new MailMessage();

        //try
        //{
        //    MailAddress fromAddress = new MailAddress(txtemail.Text, txtname.Text);

        //    // You can specify the host name or ipaddress of your server
        //    // Default in IIS will be localhost 
        //    smtpClient.Host = "localhost";

        //    //Default port will be 25
        //    smtpClient.Port = 25;

        //    //From address will be given as a MailAddress Object
        //    message.From = fromAddress;

        //    // To address collection of MailAddress
        //    message.To.Add("somaia_mail@yahoo.com");
        //    message.Subject = "Feedback";

        //    // CC and BCC optional
        //    // MailAddressCollection class is used to send the email to various users
        //    // You can specify Address as new MailAddress("admin1@yoursite.com")
        //    message.CC.Add("somaia_mail@yahoo.com");
        //    message.CC.Add("somaia_mail@yahoo.com");

        //    // You can specify Address directly as string
        //    message.Bcc.Add(new MailAddress("somaia_mail@yahoo.com"));
        //    message.Bcc.Add(new MailAddress("somaia_mail@yahoo.com"));

        //    //Body can be Html or text format
        //    //Specify true if it  is html message
        //    message.IsBodyHtml = false;

        //    // Message body content
        //    message.Body = txtmessage.Text;

        //    // Send SMTP mail
        //    smtpClient.Send(message);

        //    lblStatus.Text = "Email successfully sent.";
        //}
        //catch (Exception ex)
        //{
        //    lblStatus.Text = "Send Email Failed.<br>" + ex.Message;
        //}




        // System.Web.Mail.SmtpMail.SmtpServer is obsolete in 2.0
        // System.Net.Mail.SmtpClient is the alternate class for this in 2.0
        SmtpClient smtpClient = new SmtpClient();
        MailMessage message = new MailMessage();

        try
        {
            MailAddress fromAddress = new MailAddress(txtEmail.Text, txtName.Text);
            NetworkCredential Mynet = new NetworkCredential("abdelrady@gmail.com", "abdelrady_iti");
            // You can specify the host name or ipaddress of your server
            // Default in IIS will be localhost 
            smtpClient.Host = "smtp.gmail.com";

            //Default port will be 25
            smtpClient.Port = 587;

            //From address will be given as a MailAddress Object
            message.From = fromAddress;

            // To address collection of MailAddress
            message.To.Add("somaia_mail@yahoo.com");
            message.Subject = "Feedback";

            // CC and BCC optional
            // MailAddressCollection class is used to send the email to various users
            // You can specify Address as new MailAddress("admin1@yoursite.com")
            //message.CC.Add("somaia_mail@yahoo.com");
            //message.CC.Add("somaia_mail@yahoo.com");

            // You can specify Address directly as string
            //message.Bcc.Add(new MailAddress("somaia_mail@yahoo.com"));
            //message.Bcc.Add(new MailAddress("somaia_mail@yahoo.com"));

            //Body can be Html or text format
            //Specify true if it  is html message
            message.IsBodyHtml = false;

            // Message body content
            message.Body = txtMessage.Text;
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = Mynet;
            // Send SMTP mail
            smtpClient.Send(message);

            lblStatus.Text = "Email successfully sent.";
        }
        catch (Exception ex)
        {
            throw ex;
            //lblStatus.Text = "Send Email Failed.<br>" + ex.Message +"<br />" + ex.InnerException.Message;
        }

    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        //txtname.Text = "";
        //txtmessage.Text = "";
        //txtemail.Text = "";

        txtName.Text = "";
        txtMessage.Text = "";
        txtEmail.Text = "";
    }
}