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
using System.Web.SessionState;
using System.Configuration;
using Application.App_Code.BLL;

namespace Application
{
    public partial class Login : System.Web.UI.Page
    {
        private static User user = new User();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["role_id"] != null)
                {
                    if (Session["role_id"].ToString() == "APEMDASDWE")
                        Response.Redirect("~/Pages/Pharmacist/PharmacistHome.aspx");
                    else if (Session["role_id"].ToString() == "APLSDWEFE")
                        Response.Redirect("~/Pages/Admin/AdminHome.aspx");
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            HttpClient client = new HttpClient();
            using (client)
            {
                string apiString = ConfigurationManager.AppSettings["apiURI"];
                client.BaseAddress = new Uri(apiString + "api/Authentication/" + usernameTextbox.Text + "/" + passwordTextbox.Text + "/");
                try
                {
                    var responseTask = client.GetAsync("");
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<List<string>>();
                        readTask.Wait();
                        string result1 = readTask.Result[0];
                        string result2 = "";
                        if (readTask.Result.Count > 1)
                            result2 = readTask.Result[1];
                        if (result1 == "false")
                        {
                            StatusLabel.Text = "You have entered an incorrect username or password";
                        }
                        else
                        {
                            StatusLabel.ForeColor = Color.Green;
                            StatusLabel.Text = "You are logged in";
                            Session["username"] = usernameTextbox.Text;
                            Session["role_id"] = result1;
                            Session["id"] = result2;
                            if (result1 == "APLSDWEFE")
                                Response.Redirect("Admin/Home.aspx");
                            else if (result1 == "APEMDASDWE")
                            {
                                
                                Session["site_id"] = user.getSiteID(result2);
                                Response.Redirect("Pharmacist/Dashboard.aspx");
                            }   
                            else if (result1 == "APPFREWGR")
                            {
                                if (user.CheckPasswordChanged(result2))
                                    Response.Redirect("Patient/Home.aspx");
                                else
                                    Response.Redirect("~/PatientResetPassword.aspx");
                            }  
                        }
                    }
                }
                catch
                {
                    return;
                }

            }
        }
    }
}