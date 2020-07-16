using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Application.App_Code.BLL;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;

namespace Application.Pharmacist
{
    public partial class Report : System.Web.UI.Page
    {
        static FUP fup = new FUP();
        static StudyManager studyManager = new StudyManager();
        static int Range;
        static double ArraySize;
        static string PeriodOfTime;
        static int numberOfVisits = 10;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] == null)
                Response.Redirect("~/Error.aspx");
            if (Session["role_id"].ToString() != "APEMDASDWE")
                Response.Redirect("~/Error.aspx");
            if (!IsPostBack)
            {
                //if (Session["demographic_id"] == null)
                //    Response.Redirect("~/Error.aspx");
                Session["fupid"] = null;
                Label1.Text += " (Study ID: " + Session["study_id"] + ")";
                Fill();
            }
        }

        private void FillUpWeightChart()
        {
            List<DateTime> Dates = new List<DateTime>();
            List<double> Weights = new List<double>();
            DataRow[] returnedRows = new DataRow[numberOfVisits];
            returnedRows = fup.GetWeightsByDates(numberOfVisits, Convert.ToInt32(Session["study_id"]));
            foreach (DataRow dr in returnedRows)
            {
                if (dr != null)
                {
                    Dates.Add(Convert.ToDateTime(dr[3]));
                    Weights.Add(Convert.ToDouble(dr[5]));
                }
            }
            if (Weights.Count > 1)
                PeriodOfTime = "for the past " + Weights.Count + " visits";
            else
                PeriodOfTime = "for the past " + Weights.Count + " visit";
            WeightChart.Titles.Clear();
            WeightChart.Titles.Add("Weight " + PeriodOfTime);
            WeightChart.ChartAreas[0].AxisX.Title = "Dates";
            WeightChart.ChartAreas[0].AxisY.Title = "Weight";
            WeightChart.Series[0].Points.DataBindXY(Dates, Weights);
            WeightChart.Series[1].Points.DataBindXY(Dates, Weights);
            WeightChart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            WeightChart.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            WeightChart.Titles[0].Font = new Font("Arial", 22, FontStyle.Bold);

        }

        private void FillUpSystolicChart()
        {
            List<DateTime> Dates = new List<DateTime>();
            List<double> Systolics = new List<double>();
            DataRow[] returnedRows = new DataRow[numberOfVisits];
            returnedRows = fup.GetSystolicsByDates(numberOfVisits, Convert.ToInt32(Session["study_id"]));
            foreach (DataRow dr in returnedRows)
            {
                if (dr != null)
                {
                    Dates.Add(Convert.ToDateTime(dr[3]));
                    Systolics.Add(Convert.ToDouble(dr[10]));
                }
            }
            SystolicChart.Titles.Clear();
            SystolicChart.Titles.Add("Systolic " + PeriodOfTime);
            SystolicChart.ChartAreas[0].AxisX.Title = "Dates";
            SystolicChart.ChartAreas[0].AxisY.Title = "Systolic";
            SystolicChart.Series[0].Points.DataBindXY(Dates, Systolics);
            SystolicChart.Series[1].Points.DataBindXY(Dates, Systolics);
            SystolicChart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            SystolicChart.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            SystolicChart.Titles[0].Font = new Font("Arial", 22, FontStyle.Bold);
        }

        private void FillUpDiastolicChart()
        {
            List<DateTime> Dates = new List<DateTime>();
            List<double> Diastolics = new List<double>();
            DataRow[] returnedRows = new DataRow[numberOfVisits];
            returnedRows = fup.GetDiastolicsByDates(numberOfVisits, Convert.ToInt32(Session["study_id"]));
            foreach (DataRow dr in returnedRows)
            {
                if (dr != null)
                {
                    Dates.Add(Convert.ToDateTime(dr[3]));
                    Diastolics.Add(Convert.ToDouble(dr[11]));
                }
            }
            DiastolicChart.Titles.Clear();
            DiastolicChart.Titles.Add("Diastolic " + PeriodOfTime);
            DiastolicChart.ChartAreas[0].AxisX.Title = "Dates";
            DiastolicChart.ChartAreas[0].AxisY.Title = "Diastolic";
            DiastolicChart.Series[0].Points.DataBindXY(Dates, Diastolics);
            DiastolicChart.Series[1].Points.DataBindXY(Dates, Diastolics);
            DiastolicChart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            DiastolicChart.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            DiastolicChart.Titles[0].Font = new Font("Arial", 22, FontStyle.Bold);
        }

        private void FillUpCholesterolChart()
        {
            List<DateTime> Dates = new List<DateTime>();
            List<double> Cholesterols = new List<double>();
            DataRow[] returnedRows = new DataRow[numberOfVisits];
            returnedRows = fup.GetCholesterolsByDates(numberOfVisits, Convert.ToInt32(Session["study_id"]));
            foreach (DataRow dr in returnedRows)
            {
                if (dr != null)
                {
                    Dates.Add(Convert.ToDateTime(dr[3]));
                    Cholesterols.Add(Convert.ToDouble(dr[16]));
                }
            }
            CholesterolChart.Titles.Clear();
            CholesterolChart.Titles.Add("Cholesterol " + PeriodOfTime);
            CholesterolChart.ChartAreas[0].AxisX.Title = "Dates";
            CholesterolChart.ChartAreas[0].AxisY.Title = "Cholesterol";
            CholesterolChart.Series[0].Points.DataBindXY(Dates, Cholesterols);
            CholesterolChart.Series[1].Points.DataBindXY(Dates, Cholesterols);
            CholesterolChart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            CholesterolChart.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            CholesterolChart.Titles[0].Font = new Font("Arial", 22, FontStyle.Bold);
        }

        private void FillUpHDLChart()
        {
            List<DateTime> Dates = new List<DateTime>();
            List<double> HDLs = new List<double>();
            DataRow[] returnedRows = new DataRow[numberOfVisits];
            returnedRows = fup.GetHDLsByDates(numberOfVisits, Convert.ToInt32(Session["study_id"]));
            foreach (DataRow dr in returnedRows)
            {
                if (dr != null)
                {
                    Dates.Add(Convert.ToDateTime(dr[3]));
                    HDLs.Add(Convert.ToDouble(dr[18]));
                }
            }
            HDLChart.Titles.Clear();
            HDLChart.Titles.Add("HDL " + PeriodOfTime);
            HDLChart.ChartAreas[0].AxisX.Title = "Dates";
            HDLChart.ChartAreas[0].AxisY.Title = "HDL";
            HDLChart.Series[0].Points.DataBindXY(Dates, HDLs);
            HDLChart.Series[1].Points.DataBindXY(Dates, HDLs);
            HDLChart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            HDLChart.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            HDLChart.Titles[0].Font = new Font("Arial", 22, FontStyle.Bold);
        }

        private void FillUpLDLChart()
        {
            List<DateTime> Dates = new List<DateTime>();
            List<double> LDLs = new List<double>();
            DataRow[] returnedRows = new DataRow[numberOfVisits];
            returnedRows = fup.GetLDLsByDates(numberOfVisits, Convert.ToInt32(Session["study_id"]));
            foreach (DataRow dr in returnedRows)
            {
                if (dr != null)
                {
                    Dates.Add(Convert.ToDateTime(dr[3]));
                    LDLs.Add(Convert.ToDouble(dr[20]));
                }
            }
            LDLChart.Titles.Clear();
            LDLChart.Titles.Add("LDL " + PeriodOfTime);
            LDLChart.ChartAreas[0].AxisX.Title = "Dates";
            LDLChart.ChartAreas[0].AxisY.Title = "LDL";
            LDLChart.Series[0].Points.DataBindXY(Dates, LDLs);
            LDLChart.Series[1].Points.DataBindXY(Dates, LDLs);
            LDLChart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            LDLChart.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            LDLChart.Titles[0].Font = new Font("Arial", 22, FontStyle.Bold);
        }
        protected void ViewOptionsRBL_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fill();
        }

        private void Fill()
        {
            numberOfVisits = Convert.ToInt32(ViewOptionsRBL.SelectedValue);
            if (Range == 0)
            {
                if (studyManager.GetDayDifference() < 12)
                {
                    Range = 12;
                    PeriodOfTime = "for the past 12 days";
                }
                else
                {
                    Range = Convert.ToInt32(studyManager.GetDayDifference());
                    PeriodOfTime = "from the beginning";
                }
            }

            FillUpWeightChart();
            FillUpSystolicChart();
            FillUpDiastolicChart();
            FillUpCholesterolChart();
            FillUpHDLChart();
            FillUpLDLChart();
        }
    }
}