using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.DataVisualization.Charting;
using Application.App_Code.BLL;
using System.Drawing;

namespace Application.Pharmacist
{
    public partial class Dashboard : System.Web.UI.Page
    {
        static StudyManager studyManager = new StudyManager();
        static User user = new User();
        static int Range;
        static double ArraySize;
        static double numberOfPoints = 12.0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] == null)
                Response.Redirect("~/Error.aspx");
            if (Session["role_id"].ToString() != "APEMDASDWE")
                Response.Redirect("~/Error.aspx");
            Session["demographic_id"] = null;
            GenderChartFill();
            PatientNumberFill();
            Range = Convert.ToInt32(ViewOptionsRBL.SelectedValue);
            TitleLabel.Text += " (Site ID: "+Session["site_id"]+")";
        }

        private void GenderChartFill()
        {
            int NumberOfDemo = studyManager.GetCountOfDemo((Session["site_id"].ToString()));
            if(NumberOfDemo==0)
                return;
            string[] Genders = new string[2] { "Male", "Female" };
            float[] Proportions = new float[2] { studyManager.GetGenderProportion(1, (Session["site_id"]).ToString()), studyManager.GetGenderProportion(2, (Session["site_id"]).ToString()) };
            GenderChart.Series[0].Points.DataBindXY(Genders, Proportions);
            GenderChart.Titles.Add("Proportion by Gender");
            GenderChart.Series[0].Name = "Proportion by Gender";
            GenderChart.Series[0].ChartType = SeriesChartType.Pie;
            GenderChart.Series[0].Label = "#PERCENT";
            GenderChart.Legends.Add("Legend1");
            GenderChart.Series[0].LegendText = "#VALX";
            GenderChart.Legends[0].Docking = Docking.Bottom;
            GenderChart.Legends[0].Alignment = StringAlignment.Center;
            GenderChart.Legends[0].IsDockedInsideChartArea = true;
            GenderChart.Titles[0].Font = new Font("Arial", 22, FontStyle.Bold);
        }

        private void PatientNumberFill()
        {
            PatientsChart.Titles.Clear();
            if (Range == 0)
            {
                if (studyManager.GetDayDifference() < numberOfPoints)
                    Range = Convert.ToInt32(numberOfPoints);
                else
                    Range = Convert.ToInt32(studyManager.GetDayDifference());
                PatientsChart.Titles.Add("Number of patients with consent up to date");
            }
            else if (Range == 30)
                PatientsChart.Titles.Add("Number of patients with consent over past month");
            else if (Range == 90)
                PatientsChart.Titles.Add("Number of patients with consent over past 3 months");
            else if (Range == 180)
                PatientsChart.Titles.Add("Number of patients with consent over past 6 months");
            else if (Range == 365)
                PatientsChart.Titles.Add("Number of patients with consent over past 12 months");
            else if (Range == 730)
                PatientsChart.Titles.Add("Number of patients with consent over past 2 years");
            else
                PatientsChart.Titles.Add("Number of patients with consent over past " + Range + " days");
            ArraySize = Range / numberOfPoints;
            DateTime[] Dates = new DateTime[Convert.ToInt32(numberOfPoints) + 1];
            int?[] NumOfDemos = new int?[Convert.ToInt32(numberOfPoints) + 1];
            int i = 0;
            for (DateTime date = DateTime.Now.AddDays(-Range); i <= Convert.ToInt32(numberOfPoints); i++)
            {
                Dates[i] = date.AddDays((i) * ArraySize);
                NumOfDemos[i] = user.GetNumbersByDates(date.AddDays((i) * ArraySize),(Session["site_id"].ToString()));
            }
            PatientsChart.Series[0].Points.DataBindXY(Dates, NumOfDemos);
            PatientsChart.ChartAreas[0].AxisX.Title = "Dates";
            PatientsChart.ChartAreas[0].AxisY.Title = "# of Patients";
            PatientsChart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            PatientsChart.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            PatientsChart.Series[0].ChartType = SeriesChartType.Line;
            PatientsChart.Titles[0].Font = new Font("Arial", 22, FontStyle.Bold);
        }

        protected void ViewOptionsRBL_SelectedIndexChanged(object sender, EventArgs e)
        {
            PatientNumberFill();
        }
    }
}