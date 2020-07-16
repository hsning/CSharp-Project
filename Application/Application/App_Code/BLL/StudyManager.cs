using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Application.App_Code.DAL.DataSetUsersTableAdapters;
using System.Data;

namespace Application.App_Code.BLL
{
    public class StudyManager
    {
        private static StudiesTableAdapter adapter=new StudiesTableAdapter();
        private static LogManager logManager = new LogManager();
        private Dictionary<string, object> parameter = new Dictionary<string, object>
        {
            {"id",null },
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
        public StudyManager()
        {

        }

        public int AddStudy(string patient_id)
        {
            int study_id = Convert.ToInt32(adapter.InsertStudyQuery(patient_id));
            parameter["study_id"] = study_id;
            parameter["patient_id"] = patient_id;
            parameter["nature_of_change"] = "insert";
            parameter["table"] = "Studies";
            logManager.InsertLog(parameter);
            return study_id;
        }

        public int GetStudyIDByPatientID(string patient_id)
        {
            int study_id = Convert.ToInt32(adapter.GetStudyIDByPatientIDQuery(patient_id));
            return study_id;
        }

        public string GetPatientIDByStudyID(int study_id)
        {
            string patient_id = adapter.GetPatientIDByStudyIDQuery(study_id);
            return patient_id;
        }
        public bool UpdateVisit(Dictionary<string, object> parameter)
        {
            try
            {
                adapter.UpdateDemoQuery(
                    parameter["birth_date"] == null ? null : parameter["birth_date"].ToString(),
                    parameter["race_1"] == null ? (bool?)null : Convert.ToBoolean(parameter["race_1"]),
                    parameter["race_2"] == null ? (bool?)null : Convert.ToBoolean(parameter["race_2"]),
                    parameter["race_3"] == null ? (bool?)null : Convert.ToBoolean(parameter["race_3"]),
                    parameter["race_4"] == null ? (bool?)null : Convert.ToBoolean(parameter["race_4"]),
                    parameter["race_5"] == null ? (bool?)null : Convert.ToBoolean(parameter["race_5"]),
                    parameter["race_6"] == null ? (bool?)null : Convert.ToBoolean(parameter["race_6"]),
                    parameter["race_7"] == null ? (bool?)null : Convert.ToBoolean(parameter["race_7"]),
                    parameter["race_8"] == null ? (bool?)null : Convert.ToBoolean(parameter["race_8"]),
                    parameter["race_9"] == null ? null : (parameter["race_9"].ToString()),
                    parameter["race_10"] == null ? (bool?)null : Convert.ToBoolean(parameter["race_10"]),
                    parameter["sex"] == null ? (int?)null : Convert.ToInt32(parameter["sex"]),
                    parameter["height"] == null ? (decimal?)null : Convert.ToDecimal(parameter["height"]),
                    parameter["weight"] == null ? (decimal?)null : Convert.ToDecimal(parameter["weight"]),
                    parameter["bmi"] == null ? (decimal?)null : Math.Round(Convert.ToDecimal(parameter["bmi"]), 1),
                    parameter["systolic"] == null ? (int?)null : Convert.ToInt32(parameter["systolic"]),
                    parameter["diastolic"] == null ? (int?)null : Convert.ToInt32(parameter["diastolic"]),
                    parameter["diabetes"] == null ? (int?)null : Convert.ToInt32(parameter["diabetes"]),
                    parameter["diabetes_year"] == null ? (int?)null : Convert.ToInt32(parameter["diabetes_year"]),
                    parameter["smoker"] == null ? (int?)null : Convert.ToInt32(parameter["smoker"]),
                    parameter["stop_year"] == null ? (int?)null : Convert.ToInt32(parameter["stop_year"]),
                    parameter["alcohol"] == null ? (int?)null : Convert.ToInt32(parameter["alcohol"]),
                    parameter["drinks"] == null ? (int?)null : Convert.ToInt32(parameter["drinks"]),
                    parameter["drugs"] == null ? (int?)null : Convert.ToInt32(parameter["drugs"]),
                    parameter["other"] == null ? (int?)null : Convert.ToInt32(parameter["other"]),
                    parameter["other_specify"] == null ? null : parameter["other_specify"].ToString(),
                    Convert.ToInt32(parameter["study_id"]));
                parameter["nature_of_change"] = "update";
                parameter["table"] = "Studies";
                logManager.InsertLog(parameter);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public float GetGenderProportion(int Gender, string Site_ID)
        {
            var r = adapter.SelectGenderProportionQuery(Gender, Site_ID);
            return (float)Convert.ToDecimal(r);
        }

        public int? GetNumbersByDates(DateTime date, string siteID)
        {
            int? i = 0;
            i = (int?)adapter.CountByConsentDateQuery(date, siteID);
            return i;
        }

        public int? GetDayDifference()
        {
            return (int?)adapter.GetDayDifferenceQuery();
        }


        public DateTime GetBDay(int studyID)
        {
            var returnedObject = adapter.GetBDayByDemoQuery(studyID);
            return Convert.ToDateTime(returnedObject);
        }

        public int GetCountOfDemo(string siteID)
        {
            return Convert.ToInt32(adapter.GetCountOfDemoQuery(siteID));
        }
    }
}