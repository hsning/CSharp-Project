using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Application.App_Code.DAL.DataSetUsersTableAdapters;
using System.Data;

namespace Application.App_Code.BLL
{
    public class FUP
    {
        private FollowUpsTableAdapter adapter = new FollowUpsTableAdapter();
        public FUP()
        {

        }

        public bool UpdateFollowUps(Dictionary<string, object> parameter)
        {
            try
            {
                adapter.UpdateFUPQuery(
                    parameter["visit_type"] == null ? (int?)null : Convert.ToInt32(parameter["visit_type"]),
                    parameter["visit_date"] == null ? null : parameter["visit_date"].ToString(),
                    parameter["visit_length"] == null ? (int?)null : Convert.ToInt32(Convert.ToDouble(parameter["visit_length"])),
                    parameter["weight"] == null ? (decimal?)null : Convert.ToDecimal(parameter["weight"]),
                    parameter["height"] == null ? (decimal?)null : Convert.ToDecimal(parameter["height"]),
                    parameter["bmi"] == null ? (decimal?)null : Math.Round(Convert.ToDecimal(parameter["bmi"]), 1),
                    parameter["smoker"] == null ? (int?)null : Convert.ToInt32(parameter["smoker"]),
                    parameter["stop_date"] == null ? null : parameter["stop_date"].ToString(),
                    parameter["systolic"] == null ? (int?)null : Convert.ToInt32(Convert.ToDouble(parameter["systolic"])),
                    parameter["diastolic"] == null ? (int?)null : Convert.ToInt32(Convert.ToDouble(parameter["diastolic"])),
                    parameter["glucose"] == null ? (decimal?)null : Convert.ToDecimal(parameter["glucose"]),
                    parameter["test_type"] == null ? (int?)null : Convert.ToInt32(parameter["test_type"]),
                    parameter["hba1c"] == null ? (decimal?)null : Convert.ToDecimal(parameter["hba1c"]),
                    parameter["hba1c_date"] == null ? null : (parameter["hba1c_date"].ToString()),
                    parameter["cholesterol"] == null ? (decimal?)null : Convert.ToDecimal(parameter["cholesterol"]),
                    parameter["chole_date"] == null ? null : (parameter["chole_date"].ToString()),
                    parameter["hdl"] == null ? (decimal?)null : Convert.ToDecimal(parameter["hdl"]),
                    parameter["hdl_date"] == null ? null : (parameter["hdl_date"].ToString()),
                    parameter["ldl"] == null ? (decimal?)null : Convert.ToDecimal(parameter["ldl"]),
                    parameter["ldl_date"] == null ? null : (parameter["ldl_date"].ToString()),
                    parameter["date_not_available"] == null ? (bool?)null : Convert.ToBoolean(parameter["date_not_available"]),
                    parameter["egfr"] == null ? (decimal?)null : Convert.ToDecimal(parameter["egfr"]),
                    parameter["egfr_date"] == null ? null : parameter["egfr_date"].ToString(),
                    parameter["acr"] == null ? (decimal?)null : Convert.ToDecimal(parameter["acr"]),
                    parameter["acr_date"] == null ? null : parameter["acr_date"].ToString(),
                    parameter["aha"] == null ? (int?)null : Convert.ToInt32(parameter["aha"]),
                    parameter["acarbose"] == null ? (bool?)null : Convert.ToBoolean(parameter["acarbose"]),
                    parameter["acarbose_tdd"] == null ? (int?)null : Convert.ToInt32(parameter["acarbose_tdd"]),
                    parameter["dpp_4"] == null ? (bool?)null : Convert.ToBoolean(parameter["dpp_4"]),
                    parameter["dpp_4_name"] == null ? (int?)null : Convert.ToInt32(parameter["dpp_4_name"]),
                    parameter["dpp_4_tdd"] == null ? (int?)null : Convert.ToInt32(parameter["dpp_4_tdd"]),
                    parameter["glp_1"] == null ? (bool?)null : Convert.ToBoolean(parameter["glp_1"]),
                    parameter["glp_1_name"] == null ? (int?)null : Convert.ToInt32(parameter["glp_1_name"]),
                    parameter["glp_1_tdd"] == null ? (int?)null : Convert.ToInt32(parameter["glp_1_tdd"]),
                    parameter["hypertension"] == null ? (int?)null : Convert.ToInt32(parameter["hypertension"]),
                    parameter["bb"] == null ? (bool?)null : Convert.ToBoolean(parameter["bb"]),
                    parameter["bb_name"] == null ? (int?)null : Convert.ToInt32(parameter["bb_name"]),
                    parameter["bb_tdd"] == null ? (int?)null : Convert.ToInt32(parameter["bb_tdd"]),
                    parameter["ccb"] == null ? (bool?)null : Convert.ToBoolean(parameter["ccb"]),
                    parameter["ccb_name"] == null ? (int?)null : Convert.ToInt32(parameter["ccb_name"]),
                    parameter["ccb_tdd"] == null ? (int?)null : Convert.ToInt32(parameter["ccb_tdd"]),
                    parameter["fupid"].ToString()
                    );
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool InsertFollowUps(Dictionary<string, object> parameter)
        {
            try
            {

                adapter.InsertFUPQuery(
                    parameter["fupid"].ToString(),
                    parameter["visit_type"] == null ? (int?)null : Convert.ToInt32(parameter["visit_type"]),
                    parameter["visit_date"] == null ? null : (parameter["visit_date"]).ToString(),
                    parameter["visit_length"] == null ? (int?)null : Convert.ToInt32(Convert.ToDouble(parameter["visit_length"])),
                    parameter["weight"] == null ? (decimal?)null : Convert.ToDecimal(parameter["weight"]),
                    parameter["height"] == null ? (decimal?)null : Convert.ToDecimal(parameter["height"]),
                    parameter["bmi"] == null ? (decimal?)null : Math.Round(Convert.ToDecimal(parameter["bmi"]), 1),
                    parameter["smoker"] == null ? (int?)null : Convert.ToInt32(parameter["smoker"]),
                    parameter["stop_date"] == null ? null : (parameter["stop_date"]).ToString(),
                    parameter["systolic"] == null ? (int?)null : Convert.ToInt32(Convert.ToDouble(parameter["systolic"])),
                    parameter["diastolic"] == null ? (int?)null : Convert.ToInt32(Convert.ToDouble(parameter["diastolic"])),
                    parameter["glucose"] == null ? (decimal?)null : Convert.ToDecimal(parameter["glucose"]),
                    parameter["test_type"] == null ? (int?)null : Convert.ToInt32(parameter["test_type"]),
                    parameter["hba1c"] == null ? (decimal?)null : Convert.ToDecimal(parameter["hba1c"]),
                    parameter["hba1c_date"] == null ? null : (parameter["hba1c_date"]).ToString(),
                    parameter["cholesterol"] == null ? (decimal?)null : Convert.ToDecimal(parameter["cholesterol"]),
                    parameter["chole_date"] == null ? null : (parameter["chole_date"]).ToString(),
                    parameter["hdl"] == null ? (decimal?)null : Convert.ToDecimal(parameter["hdl"]),
                    parameter["hdl_date"] == null ? null : (parameter["hdl_date"]).ToString(),
                    parameter["ldl"] == null ? (decimal?)null : Convert.ToDecimal(parameter["ldl"]),
                    parameter["ldl_date"] == null ? null : (parameter["ldl_date"]).ToString(),
                    parameter["date_not_available"] == null ? (bool?)null : Convert.ToBoolean(parameter["date_not_available"]),
                    parameter["egfr"] == null ? (decimal?)null : Convert.ToDecimal(parameter["egfr"]),
                    parameter["egfr_date"] == null ? null : (parameter["egfr_date"]).ToString(),
                    parameter["acr"] == null ? (decimal?)null : Convert.ToDecimal(parameter["acr"]),
                    parameter["acr_date"] == null ? null : (parameter["acr_date"]).ToString(),
                    parameter["aha"] == null ? (int?)null : Convert.ToInt32(parameter["aha"]),
                    parameter["acarbose"] == null ? (bool?)null : Convert.ToBoolean(parameter["acarbose"]),
                    parameter["acarbose_tdd"] == null ? (int?)null : Convert.ToInt32(parameter["acarbose_tdd"]),
                    parameter["dpp_4"] == null ? (bool?)null : Convert.ToBoolean(parameter["dpp_4"]),
                    parameter["dpp_4_name"] == null ? (int?)null : Convert.ToInt32(parameter["dpp_4_name"]),
                    parameter["dpp_4_tdd"] == null ? (int?)null : Convert.ToInt32(parameter["dpp_4_tdd"]),
                    parameter["glp_1"] == null ? (bool?)null : Convert.ToBoolean(parameter["glp_1"]),
                    parameter["glp_1_name"] == null ? (int?)null : Convert.ToInt32(parameter["glp_1_name"]),
                    parameter["glp_1_tdd"] == null ? (int?)null : Convert.ToInt32(parameter["glp_1_tdd"]),
                    parameter["hypertension"] == null ? (int?)null : Convert.ToInt32((parameter["hypertension"])),
                    parameter["bb"] == null ? (bool?)null : Convert.ToBoolean(parameter["bb"]),
                    parameter["bb_name"] == null ? (int?)null : Convert.ToInt32(parameter["bb_name"]),
                    parameter["bb_tdd"] == null ? (int?)null : Convert.ToInt32(parameter["bb_tdd"]),
                    parameter["ccb"] == null ? (bool?)null : Convert.ToBoolean(parameter["ccb"]),
                    parameter["ccb_name"] == null ? (int?)null : Convert.ToInt32(parameter["ccb_name"]),
                    parameter["ccb_tdd"] == null ? (int?)null : Convert.ToInt32(parameter["ccb_tdd"]),
                    parameter["patient_id"]==null? (int?)null : Convert.ToInt32(parameter["patient_id"]));
                return true;
            }
            catch(Exception e)
            {
                string msg = e.Message;
                return false;
            }
        }

        public DataRow[] GetWeightsByDates(int numberOfVisits, int demoID)
        {
            DataRow[] returnedRows = new DataRow[numberOfVisits];
            DataRowCollection weights = adapter.GetWeightByDemo(demoID).Rows;
            for (int i = 0; i < numberOfVisits; i++)
            {
                if (i < (weights.Count))
                    returnedRows[i] = weights[i];
            }
            return returnedRows;
        }

        public DataRow[] GetSystolicsByDates(int numberOfVisits, int demoID)
        {
            DataRow[] returnedRows = new DataRow[numberOfVisits];
            var systolic = adapter.GetSystolicByDemo(demoID).Rows;
            for (int i = 0; i < numberOfVisits; i++)
            {
                if (i < (systolic.Count))
                    returnedRows[i] = systolic[i];
            }
            return returnedRows;
        }
        public DataRow[] GetDiastolicsByDates(int numberOfVisits, int demoID)
        {
            DataRow[] returnedRows = new DataRow[numberOfVisits];
            var diastolic = adapter.GetDiastolicByDemo(demoID).Rows;
            for (int i = 0; i < numberOfVisits; i++)
            {
                if (i < (diastolic.Count))
                    returnedRows[i] = diastolic[i];
            }
            return returnedRows;
        }
        public DataRow[] GetCholesterolsByDates(int numberOfVisits, int demoID)
        {
            DataRow[] returnedRows = new DataRow[numberOfVisits];
            var cholesterol = adapter.GetCholesterolByDemo(demoID).Rows;
            for (int i = 0; i < numberOfVisits; i++)
            {
                if (i < (cholesterol.Count))
                    returnedRows[i] = cholesterol[i];
            }
            return returnedRows;
        }
        public DataRow[] GetHDLsByDates(int numberOfVisits, int demoID)
        {
            DataRow[] returnedRows = new DataRow[numberOfVisits];
            var HDL = adapter.GetHDLByDemo(demoID).Rows;
            for (int i = 0; i < numberOfVisits; i++)
            {
                if (i < (HDL.Count))
                    returnedRows[i] = HDL[i];
            }
            return returnedRows;
        }
        public DataRow[] GetLDLsByDates(int numberOfVisits, int demoID)
        {
            DataRow[] returnedRows = new DataRow[numberOfVisits];
            var LDL = adapter.GetLDLByDemo(demoID).Rows;
            for (int i = 0; i < numberOfVisits; i++)
            {
                if (i < (LDL.Count))
                    returnedRows[i] = LDL[i];
            }
            return returnedRows;
        }

        public bool CheckFUPID(string fupid)
        {
            List<DataRow> returnedRows = new List<DataRow>();
            var FUPID = adapter.CheckFollowUpID(fupid);
            foreach(var f in FUPID)
            {
                returnedRows.Add(f);
            }
            if (returnedRows.Count == 0)
                return true;
            else
                return false;
        }
    }
}