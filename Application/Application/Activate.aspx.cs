using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Drawing;
using System.Configuration;

namespace Application
{
    public partial class Activate : System.Web.UI.Page
    {
        bool tokenWithinTimeLimit;
        bool IsValidToken;
        protected void Page_Load(object sender, EventArgs e)
        {
            string token = (Request.QueryString["token"]);
            TokenManager tokenManager = new TokenManager();
            IsValidToken = tokenManager.IsValidToken(token);
            if (!IsValidToken)
            {
                Response.Redirect("ErrorActivation.aspx");
                return;
            }
            tokenWithinTimeLimit = tokenManager.VerifyTimeLimit(token);
            if (!tokenWithinTimeLimit)
            {
                Response.Redirect("ErrorActivation.aspx");
                return;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string token = Request.QueryString["token"];
            if (PasswordTxtbox.Text == null || ReenterPasswordTxtbox.Text == null)
            {
                statusLabel.Text = "Please enter your password please";
                return;
            }
            if (PasswordTxtbox.Text != ReenterPasswordTxtbox.Text)
            {
                statusLabel.Text = "Password does not match";
                return;
            }
            if (PasswordTxtbox.Text.Length < 6)
            {
                statusLabel.Text = "Password needs to be aleast 6 characters long.";
                return;
            }
            if (PasswordTxtbox.Text.Length > 12)
            {
                statusLabel.Text = "Password needs to be 12 characters long at max.";
                return;
            }
            if (PasswordTxtbox.Text.Contains(" "))
            {
                statusLabel.Text = "Password cannot contain spaces.";
                return;
            }
            if (!PasswordTxtbox.Text.Any(char.IsUpper))
            {
                statusLabel.Text = "Password needs to contain aleast 1 uppercase character.";
                return;
            }
            if (!PasswordTxtbox.Text.Any(char.IsLower))
            {
                statusLabel.Text = "Password needs to contain aleast 1 lowercase character.";
                return;
            }
            if (!PasswordTxtbox.Text.Any(char.IsDigit))
            {
                statusLabel.Text = "Password needs to contain aleast 1 digit.";
                return;
            }
            HttpClient client = new HttpClient();
            using (client)
            {
                string apiString = ConfigurationManager.AppSettings["apiURI"];
                client.BaseAddress = new Uri(apiString);
                var data = new FormUrlEncodedContent(
                    new Dictionary<string, string> { ["password"] = PasswordTxtbox.Text, ["token"] = token });
                try
                {
                    var responseTask = client.PostAsync("api/UpdatePassword/", data);
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        //string finalResult = readTask.Result.Trim('\"');
                    }
                }
                catch (HttpRequestException exceptionMsg)
                {
                    return;
                }
                try
                {
                    data = new FormUrlEncodedContent(
                    new Dictionary<string, string> { ["token"] = token });
                    var responseTask = client.PostAsync("api/ActivateUser/", data);
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        //string finalResult = readTask.Result.Trim('\"');
                        statusLabel.ForeColor = Color.Green;
                        statusLabel.Text = "You have successfully changed your password and you are now activated.";
                    }
                }
                catch (HttpRequestException exceptionMsg)
                {
                    return;
                }

            }
        }
    }
}