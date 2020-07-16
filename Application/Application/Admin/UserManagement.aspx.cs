using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Net.Http;
using System.Drawing;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Application.Admin
{
    public partial class UserManagement : System.Web.UI.Page
    {
        private static int rowCounts = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["id"] == null)
                    Response.Redirect("~/Error.aspx");
                if (Session["role_id"].ToString() != "APLSDWEFE")
                    Response.Redirect("~/Error.aspx");
                GridView1.DataBind();
                SiteDDL.DataBind();
                SiteDDL.Items.Insert(0,new ListItem("Please choose a site", ""));
                SiteDDL2.DataBind();
                SiteDDL2.Items.Insert(0, new ListItem("All", ""));
            }
        }

        public override void RenderControl(HtmlTextWriter writer)
        {
            ClientScript.RegisterForEventValidation(registerButton.UniqueID);
            base.RenderControl(writer);
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string username = Session["username"].ToString();
                if (e.Row.Cells[1].Text == username)
                    e.Row.Cells[0].Text = "";
                CheckBox cb = e.Row.Cells[4].Controls[0] as CheckBox;
                if (cb.Checked)
                    e.Row.Cells[10].Text = "";
            }
            DataView dv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
            //dv.RowFilter = SqlDataSource1.FilterExpression;
            rowCounts = dv.Count;
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                //Get the Total RowCount values
                int intNoOfMergeCol = e.Row.Cells.Count; /*except last column */
                for (int intCellCol = 1; intCellCol < intNoOfMergeCol; intCellCol++)
                    e.Row.Cells.RemoveAt(1);
                e.Row.Cells[0].ColumnSpan = intNoOfMergeCol;
                e.Row.Cells[0].Text = "Total number of cities is : " + rowCounts;
                e.Row.Cells[0].ForeColor = Color.White;
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Resend")
            {
                string email = e.CommandArgument.ToString();
                string postAPIAddress = ConfigurationManager.AppSettings["apiURI"];
                using (var client = new HttpClient())
                {
                    var data = new FormUrlEncodedContent(
                        new Dictionary<string, string> { ["email"] = email });
                    client.BaseAddress = new Uri(postAPIAddress);
                    try
                    {
                        var responseTask = client.PostAsync("api/ResendEmail/", data);
                        responseTask.Wait();
                        var result = responseTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            var readTask = result.Content.ReadAsStringAsync();
                            readTask.Wait();
                            var finalResult = readTask.Result.Trim('\"');
                            if (finalResult == "Success")
                            {
                                statusLabel.ForeColor = Color.Green;
                                GridView1.DataBind();
                                statusLabel.Text = "Resend Success. An activation email has already been sent out to " + email;
                            }
                            else
                            {
                                statusLabel.ForeColor = Color.Red;
                                statusLabel.Text = "Resend failed";
                            }
                        }
                    }
                    catch
                    {
                        return;
                    }
                    return;
                }
            }
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            bool dsf = Convert.ToBoolean(e.NewValues["suspended"]);
            e.NewValues["suspended_date"] = null;
            if (Convert.ToBoolean(e.NewValues["suspended"]) == true)
            {
                e.NewValues["suspended_date"] = DateTime.Now;
            }
            e.NewValues["update_date"] = DateTime.Now;
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void registerButton_Click(object sender, EventArgs e)
        {
            string postAPIAddress = ConfigurationManager.AppSettings["apiURI"];
            //Verify if the email address entered matches the email format
            bool IsValidEmail = new EmailAddressAttribute().IsValid(emailTextbox.Text);
            if (!IsValidEmail)
            {
                statusLabel.Text = ("Invalid email address entered");
                return;
            }
            using (var client = new HttpClient())
            {
                var data = new FormUrlEncodedContent(
                    new Dictionary<string, string> { ["email"] = emailTextbox.Text, ["role"] = "APEMDASDWE", ["first_name"] = FirstNameTextBox.Text, ["last_name"] = LastNameTextBox.Text, ["site_id"] = SiteDDL.SelectedValue, ["user_type"] = "pharmacist" });
                client.BaseAddress = new Uri(postAPIAddress);
                try
                {
                    var responseTask = client.PostAsync("api/RegisterUser/", data);
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        var finalResult = readTask.Result.Trim('\"');
                        if (finalResult == "Success")
                        {
                            statusLabel.ForeColor = Color.Green;
                            statusLabel.Text = "Register Success. An activation email has already been sent out to " + emailTextbox.Text;
                            GridView1.DataBind();
                        }
                        else
                        {
                            statusLabel.ForeColor = Color.Red;
                            statusLabel.Text = finalResult;
                            emailTextbox.Text = "";
                            emailTextbox.Focus();
                        }
                    }
                }
                catch
                {
                    return;
                }
                return;
            }
        }


        protected void SiteDDL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SiteDDL2.SelectedValue == "")
            {
                SqlDataSource1.FilterExpression = "";
            }
        }

        protected void FilterNameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {

        }

        protected void SearchBtn_Click1(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        protected void SiteDDLCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if(SiteDDL.SelectedValue=="")
            {
                args.IsValid = false;
                return;
            }
            args.IsValid = true;
        }
    }
}