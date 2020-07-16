using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Application.App_Code.DAL.DataSetUsersTableAdapters;

namespace Application.App_Code.BLL
{
    public class Visit
    {
        private VisitsTableAdapter adapter = new VisitsTableAdapter();
        public Visit()
        {

        }

        public void enterVisit(string sex,
                string age,
                string visit_type,
                string symptoms,
                string no_symptom_category_1,
                string no_symptom_category_2,
                string no_symptom_category_3,
                string symptom_1,
                string symptom_2,
                string symptom_3,
                string symptom_4,
                string symptom_5,
                string symptom_6,
                string symptom_7,
                string symptom_8,
                string complicating_factor_1,
                string complicating_factor_2,
                string complicating_factor_3,
                string complicating_factor_4,
                string complicating_factor_5,
                string complicating_factor_6,
                string complicating_factor_7,
                string red_flag_1,
                string red_flag_2,
                string red_flag_3,
                string red_flag_4,
                string red_flag_5,
                string red_flag_6,
                string red_flag_7,
                string action,
                string the_plan,
                string user_created
                )
        {
            adapter.InsertVisitQuery(
                string.IsNullOrEmpty(sex)?(int?)null:Convert.ToInt32(sex),
                string.IsNullOrEmpty(age)?(int?)null:Convert.ToInt32(age),
                string.IsNullOrEmpty(visit_type) ? (int?)null : Convert.ToInt32(visit_type),
                DateTime.Now,
                string.IsNullOrEmpty(symptoms) ? (bool?)null : Convert.ToBoolean(symptoms),
                string.IsNullOrEmpty(no_symptom_category_1) ? (bool?)null : Convert.ToBoolean(no_symptom_category_1),
                string.IsNullOrEmpty(no_symptom_category_2) ? (bool?)null : Convert.ToBoolean(no_symptom_category_2),
                string.IsNullOrEmpty(no_symptom_category_3) ? (bool?)null : Convert.ToBoolean(no_symptom_category_3),
                string.IsNullOrEmpty(symptom_1) ? (bool?)null : Convert.ToBoolean(symptom_1),
                string.IsNullOrEmpty(symptom_2) ? (bool?)null : Convert.ToBoolean(symptom_2),
                string.IsNullOrEmpty(symptom_3) ? (bool?)null : Convert.ToBoolean(symptom_3),
                string.IsNullOrEmpty(symptom_4) ? (bool?)null : Convert.ToBoolean(symptom_4),
                string.IsNullOrEmpty(symptom_5) ? (bool?)null : Convert.ToBoolean(symptom_5),
                string.IsNullOrEmpty(symptom_6) ? (bool?)null : Convert.ToBoolean(symptom_6),
                string.IsNullOrEmpty(symptom_7) ? (bool?)null : Convert.ToBoolean(symptom_7),
                string.IsNullOrEmpty(symptom_8) ? (bool?)null : Convert.ToBoolean(symptom_8),
                string.IsNullOrEmpty(complicating_factor_1) ? (bool?)null : Convert.ToBoolean(complicating_factor_1),
                string.IsNullOrEmpty(complicating_factor_2) ? (bool?)null : Convert.ToBoolean(complicating_factor_2),
                string.IsNullOrEmpty(complicating_factor_3) ? (bool?)null : Convert.ToBoolean(complicating_factor_3),
                string.IsNullOrEmpty(complicating_factor_4) ? (bool?)null : Convert.ToBoolean(complicating_factor_4),
                string.IsNullOrEmpty(complicating_factor_5) ? (bool?)null : Convert.ToBoolean(complicating_factor_5),
                string.IsNullOrEmpty(complicating_factor_6) ? (bool?)null : Convert.ToBoolean(complicating_factor_6),
                string.IsNullOrEmpty(complicating_factor_7) ? (bool?)null : Convert.ToBoolean(complicating_factor_7),
                string.IsNullOrEmpty(red_flag_1) ? (bool?)null : Convert.ToBoolean(red_flag_1),
                string.IsNullOrEmpty(red_flag_2) ? (bool?)null : Convert.ToBoolean(red_flag_2),
                string.IsNullOrEmpty(red_flag_3) ? (bool?)null : Convert.ToBoolean(red_flag_3),
                string.IsNullOrEmpty(red_flag_4) ? (bool?)null : Convert.ToBoolean(red_flag_4),
                string.IsNullOrEmpty(red_flag_5) ? (bool?)null : Convert.ToBoolean(red_flag_5),
                string.IsNullOrEmpty(red_flag_6) ? (bool?)null : Convert.ToBoolean(red_flag_6),
                string.IsNullOrEmpty(red_flag_7) ? (bool?)null : Convert.ToBoolean(red_flag_7),
                string.IsNullOrEmpty(action) ? (int?)null : Convert.ToInt32(action),
                string.IsNullOrEmpty(the_plan) ? (int?)null : Convert.ToInt32(the_plan),
                user_created
                );
        }
    }
}