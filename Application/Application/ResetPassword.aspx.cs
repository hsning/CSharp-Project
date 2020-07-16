using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.App_Code.BLL;
using System.Drawing;
using System.ComponentModel.DataAnnotations;

namespace Application
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (EmailTextBox.Text == "")
            {
                statusLabel.Text = "Please enter your email";
            }
            bool IsValidEmail = new EmailAddressAttribute().IsValid(EmailTextBox.Text);
            if (!IsValidEmail)
            {
                statusLabel.Text = ("Invalid email address entered");
                return;
            }
            TokenManager tokenManager = new TokenManager();
            string token = tokenManager.Generate();
            User user = new User();
            user.updateToken(token, EmailTextBox.Text);
            EmailManager emailManager = new EmailManager();
            emailManager.SendEmail(EmailTextBox.Text, EmailType.PasswordChange, token);
            statusLabel.ForeColor = Color.Green;
            statusLabel.Text = "A reset password email has already been sent out to you";
        }
    }
}