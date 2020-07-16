using System;
using System.Collections.Generic;
using System.Linq;
using Application.App_Code.BLL;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Drawing;
using System.Configuration;

namespace Application.Admin
{
    public partial class Profile : System.Web.UI.Page
    {
        private UserProfile profileManager = new UserProfile();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] == null)
                Response.Redirect("~/Error.aspx");
            if (Session["role_id"].ToString() != "APLSDWEFE")
                Response.Redirect("~/Error.aspx");
            if (IsPostBack)
                return;
            Dictionary<string, string> profile = profileManager.getProfileByID(Session["id"].ToString());
            if (profile.Count > 0)
            {
                FirstNameTextbox.Text = profile["first_name"];
                LastNameTextbox.Text = profile["last_name"];
                ContactNumberTextbox.Text = profile["contact_number"];
                AddressTextbox.Text = profile["address"];
                CityTextbox.Text = profile["city"];
                ProvinceTextbox.Text = profile["province"];
                PostalCodeTextBox.Text = profile["postal_code"];
                CountryTextbox.Text = profile["country"];
                OrganizationTextbox.Text = profile["organization"];
                EmailTextbox.Text = Session["username"].ToString();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> profile = profileManager.getProfileByID(Session["id"].ToString());
            if (profile.Count > 0)
                profileManager.updateProfileByID(Session["id"].ToString(), FirstNameTextbox.Text, LastNameTextbox.Text, ContactNumberTextbox.Text, AddressTextbox.Text, CityTextbox.Text, ProvinceTextbox.Text, PostalCodeTextBox.Text, CountryTextbox.Text, OrganizationTextbox.Text);
            else
                profileManager.insertNewProfile(Session["id"].ToString(), FirstNameTextbox.Text, LastNameTextbox.Text, ContactNumberTextbox.Text, AddressTextbox.Text, CityTextbox.Text, ProvinceTextbox.Text, PostalCodeTextBox.Text, CountryTextbox.Text, OrganizationTextbox.Text);
            statusLabel.Text = "You have successfully updated your profile";
        }

        protected void ChangeEmailBtn_Click(object sender, EventArgs e)
        {
            bool IsValidEmail = new EmailAddressAttribute().IsValid(NewEmailTextBox.Text);
            if (!IsValidEmail)
            {
                emailStatusLabel.Text = ("Invalid email address entered");
                return;
            }
            User user = new User();
            string result = user.updateEmail(NewEmailTextBox.Text, Session["id"].ToString());
            if (result == "Success")
            {
                Session.Clear();
                Session.Abandon();
                Response.Redirect("~/LogoutChangeEmail.aspx");
            }
            else
            {
                emailStatusLabel.Text = result;
            }
        }

        protected void ChangePasswordBtn_Click(object sender, EventArgs e)
        {
            if (PasswordTextBox.Text == null || ReenterPasswordTextBox.Text == null)
            {
                passwordStatusLabel.Text = "Please enter your password please";
                return;
            }
            if (PasswordTextBox.Text != ReenterPasswordTextBox.Text)
            {
                passwordStatusLabel.Text = "Password does not match";
                return;
            }
            if (PasswordTextBox.Text.Length < 6)
            {
                passwordStatusLabel.Text = "Password needs to be aleast 6 characters long.";
                return;
            }
            if (PasswordTextBox.Text.Length > 12)
            {
                passwordStatusLabel.Text = "Password needs to be 12 characters long at max.";
                return;
            }
            if (PasswordTextBox.Text.Contains(" "))
            {
                passwordStatusLabel.Text = "Password cannot contain spaces.";
                return;
            }
            if (!PasswordTextBox.Text.Any(char.IsUpper))
            {
                passwordStatusLabel.Text = "Password needs to contain aleast 1 uppercase character.";
                return;
            }
            if (!PasswordTextBox.Text.Any(char.IsLower))
            {
                passwordStatusLabel.Text = "Password needs to contain aleast 1 lowercase character.";
                return;
            }
            if (!PasswordTextBox.Text.Any(char.IsDigit))
            {
                passwordStatusLabel.Text = "Password needs to contain aleast 1 digit.";
                return;
            }
            HttpClient client = new HttpClient();
            using (client)
            {
                string apiString = ConfigurationManager.AppSettings["apiURI"];
                client.BaseAddress = new Uri(apiString);
                var data = new FormUrlEncodedContent(
                    new Dictionary<string, string> { ["password"] = PasswordTextBox.Text, ["email"] = Session["username"].ToString() });
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
                    passwordStatusLabel.ForeColor = Color.Green;
                    passwordStatusLabel.Text = "Your password has been successfully updated";
                }
                catch (HttpRequestException exceptionMsg)
                {
                    return;
                }
            }
        }
    }
}