using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Application.App_Code.DAL.DataSetUsersTableAdapters;

namespace Application.App_Code.BLL
{
    public class LogManager
    {
        private static Studies_LogsTableAdapter studies_Logger_Adapter = new Studies_LogsTableAdapter();
        public LogManager()
        {

        }
        public void InsertLog(Dictionary<string, object> parameter)
        {
            if (parameter["table"].ToString() == "Studies")
            {
                try
                {
                    studies_Logger_Adapter.InsertStudiesLogsQuery(
                                         Convert.ToInt32(parameter["study_id"]),
                                         parameter["patient_id"] == null ? null : parameter["patient_id"].ToString(),
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
                                         parameter["changed_by"] == null ? null : parameter["changed_by"].ToString(),
                                         parameter["changed_on"] == null ? (DateTime?)null : Convert.ToDateTime(parameter["changed_on"]),
                                         parameter["nature_of_change"] == null ? null : parameter["nature_of_change"].ToString()
                                         );
                }
                catch(Exception e)
                {

                }
            }

        }
    }
}