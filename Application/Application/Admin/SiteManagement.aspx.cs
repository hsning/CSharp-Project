using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Net.Http;
using System.Drawing;
using System.Data;

namespace Application.Admin
{
    public partial class SiteManagement : System.Web.UI.Page
    {
        private static int rowCounts = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] == null)
                Response.Redirect("~/Error.aspx");
            if (Session["role_id"].ToString() != "APLSDWEFE")
                Response.Redirect("~/Error.aspx");
            if(!IsPostBack)
            {
                CityDDL.DataBind();
                CityDDL.Items.Insert(0, new ListItem("Please select a city", ""));
                CityFilterDDL.DataBind();
                CityFilterDDL.Items.Insert(0, new ListItem("All", ""));
            }
        }

        protected void AddSiteBtn_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;
            string postAPIAddress = ConfigurationManager.AppSettings["apiURI"];
            using (var client = new HttpClient())
            {
                var data = new FormUrlEncodedContent(
                    new Dictionary<string, string> { ["site_prefix"] = CityDDL.SelectedValue
                    ,["site_name"] = SiteNameTextbox.Text
                    ,["site_description"] = SiteDescriptionTextBox.Text 
                    ,["address"]=AddressTextbox.Text
                    ,["phone_number"]=PhoneTextbox.Text
                    ,["postal_code"]=PostalCodeTextbox.Text
                    ,["city"]=CityDDL.SelectedItem.Text});
                client.BaseAddress = new Uri(postAPIAddress);
                try
                {
                    var responseTask = client.PostAsync("api/AddSite/", data);
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
                            statusLabel.Text = "A new site has been successfully added.";
                            SiteNameTextbox.Text = "";
                            SiteDescriptionTextBox.Text = "";
                        }
                        else
                        {
                            statusLabel.ForeColor = Color.Red;
                            statusLabel.Text = "Add site failed";
                        }
                    }
                    GridView1.DataBind();
                }
                catch
                {
                    return;
                }

                return;
            }
        }

        protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            if (e.Exception != null)
            {
                if (e.Exception.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                {
                    gridStatusLabel.Text = "This action cannot be performed because this site is currently linked to at least 1 user.";
                    e.ExceptionHandled = true;
                }
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
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
                e.Row.Cells[0].Text = "Total number of sites is : " + rowCounts;
                e.Row.Cells[0].ForeColor = Color.White;
            }
        }

        protected void FilterSiteTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        protected void DDLCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if(CityDDL.SelectedItem.Value== "")
            {
                args.IsValid = false;
                return;
            }
            args.IsValid = true;
        }

        protected void CityFilterDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CityFilterDDL.SelectedValue == "")
                SqlDataSource1.FilterExpression = "";
            else
            {
                SqlDataSource1.FilterExpression = "site_id like '" + CityFilterDDL.SelectedValue+"%'";
            }
            GridView1.DataBind();
        }
    }
}