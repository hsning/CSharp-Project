using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.App_Code.BLL;
using System.Drawing;
using System.Data;
using System.Net.Http;
using System.Configuration;

namespace Application.Pharmacist
{
    public partial class ViewDemo : System.Web.UI.Page
    {
        private static StudyManager studyManager = new StudyManager();
        private static LogManager logManager = new LogManager();
        private static bool logInserted = false;
        private Dictionary<string, object> parameter = new Dictionary<string, object>
        {
            {"patient_id",null },
            {"region_code",null },
            {"consent",null },
            {"birth_date",null },
            {"race_1",null },
            {"race_2",null },
            {"race_3",null },
            {"race_4",null },
            {"race_5",null },
            {"race_6",null },
            {"race_7",null },
            {"race_8",null },
            {"race_9",null },
            {"race_10",null },
            {"sex",null },
            {"height",null },
            {"weight",null },
            {"bmi",null },
            {"systolic",null },
            {"diastolic",null },
            {"diabetes",null },
            {"diabetes_year",null },
            {"smoker",null },
            {"stop_year",null },
            {"alcohol",null },
            {"drinks",null },
            {"drugs",null },
            {"other",null },
            {"other_specify",null },
            {"changed_by",null },
            {"changed_on",null},
            {"nature_of_change",null },
            {"table",null }
        };
        protected void Page_Load(object sender, EventArgs e)
        {
            logInserted = false;
            if (!Page.IsPostBack)
            {
                if (Session["id"] == null)
                    Response.Redirect("~/Error.aspx");
                if (Session["role_id"].ToString() != "APEMDASDWE")
                    Response.Redirect("~/Error.aspx");
                Session["demographic_id"] = null;
                Session["patient_id"] = null;
                SqlDataSource1.FilterExpression = "";
                Label2.Text = "View/Modify Patients (Site ID: " + Session["site_id"] + ")";
            }

        }
        private static int rowCounts;


        protected void SearchButton_Click(object sender, EventArgs e)
        {
            if (filterDDL.SelectedValue == "")
                SqlDataSource1.FilterExpression = "";
            else if (filterDDL.SelectedValue == "1")
                SqlDataSource1.FilterExpression = "have_consent = 1";
            else if (filterDDL.SelectedValue == "2")
                SqlDataSource1.FilterExpression = "have_consent is null";
            GridView1.DataBind();
        }
        protected void AddButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AddDemo.aspx");
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["patient_id"] = GridView1.SelectedValue.ToString();
            int studyID = studyManager.GetStudyIDByPatientID(GridView1.SelectedValue.ToString());
            Session["study_id"] = studyID;
            Response.Redirect("AddDemo.aspx");
        }


        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Detail")
            {
                Session["study_id"] = e.CommandArgument.ToString();
                Response.Redirect("ViewVisits.aspx");
            }
        }

        protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            if (e.Exception != null)
            {
                if (e.Exception.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                {
                    gridviewStatusLabel.ForeColor = Color.Red;
                    gridviewStatusLabel.Text = "This action cannot be performed because this demographic is currently linked to at least 1 visit. You must delete all associated visits first.";
                    e.ExceptionHandled = true;
                }
            }
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
                e.Row.Cells[0].Text = "Total number of patients is : " + rowCounts;
                e.Row.Cells[0].ForeColor = Color.White;
            }
            if (rowCounts > 0)
                DemoLabel.Text = "";
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox cb1 = e.Row.Cells[6].Controls[0] as CheckBox;
                if (!cb1.Checked)
                    e.Row.Cells[1].Text = "";
                CheckBox cb2 = e.Row.Cells[9].Controls[0] as CheckBox;
                cb2.Checked = !cb2.Checked;
            }
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            string postAPIAddress = ConfigurationManager.AppSettings["apiURI"];
            RegisterHashing hash = new RegisterHashing();
            string password = hash.Hash(PasswordTextBox.Text);
            //Verify if the email address entered matches the email format

            using (var client = new HttpClient())
            {
                var data = new FormUrlEncodedContent(
                    new Dictionary<string, string> { ["username"] = UsernameTextBox.Text, ["password"] = password, ["site_id"] = Session["site_id"].ToString(), ["user_type"] = "patient", ["created_by"] = Session["id"].ToString(), ["unhashed_password"] = PasswordTextBox.Text });
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
                            statusLabel.Text = "Register Success.";
                            //GridView1.DataBind();
                        }
                        else
                        {
                            statusLabel.ForeColor = Color.Red;
                            statusLabel.Text = finalResult;
                            UsernameTextBox.Text = "";
                            UsernameTextBox.Focus();
                        }
                    }
                }
                catch
                {
                    return;
                }
                GridView1.DataBind();
                return;
            }
          
        }

        protected void filterDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (filterDDL.SelectedValue == "")
                SqlDataSource1.FilterExpression = "";
            else if (filterDDL.SelectedValue == "1")
                SqlDataSource1.FilterExpression = "have_consent = 1";
            else if (filterDDL.SelectedValue == "2")
                SqlDataSource1.FilterExpression = "have_consent is null";
            GridView1.DataBind();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (e.Keys.Contains("id"))
            {
                if (!logInserted)
                {
                    string patientID = e.Keys["id"].ToString();
                    int studyID = studyManager.GetStudyIDByPatientID(patientID);
                    if (studyID != 0)
                    {
                        parameter["study_id"] = studyID;
                        parameter["changed_by"] = Session["id"];
                        parameter["changed_on"] = DateTime.Now;
                        parameter["nature_of_change"] = "delete";
                        parameter["table"] = "Studies";
                        logManager.InsertLog(parameter);
                    }

                }

            }
        }
    }
}