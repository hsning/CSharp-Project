using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Application.App_Code.DAL.DataSetUsersTableAdapters;

namespace Application.App_Code.BLL
{
    public class ConsentManager
    {
        public ConsentManager()
        {

        }
        private static ConsentTableAdapter adapter = new ConsentTableAdapter();
        public void InsertConsent(bool consent,string patient_id)
        {
            adapter.InsertConsentQuery(consent, true, patient_id);
        }

        public bool GetConsentByID(string id)
        {
            return Convert.ToBoolean(adapter.GetConsentByIDQuery(id));
        }

        public bool GetFlagByID(string id)
        {
            return Convert.ToBoolean(adapter.GetFlagByIDQuery(id));
        }

        public int GetStudyIDByPatientID(string id)
        {
            return Convert.ToInt32(adapter.GetStudyIDByUserIDQuery(id));
        }
    }
}