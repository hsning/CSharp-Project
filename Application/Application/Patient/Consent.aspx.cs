using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.App_Code.BLL;

namespace Application.Patient
{
    public partial class Consent : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["id"] == null)
            {
                Response.Redirect("~/Error.aspx");
                return;
            }
            if (Convert.ToBoolean(Session["consent"]))
            {
                MasterPageFile = "~/Masters/PatientWithConsent.Master";
            }
            else
            {
                if (!Convert.ToBoolean(Session["flag"]))
                {
                    MasterPageFile = "~/Masters/Patient.Master";
                }
                else
                    MasterPageFile = "~/Masters/PatientWithNoConsent.Master";
            }
        }
        private static ConsentManager consentManager = new ConsentManager();
        private static StudyManager studyManager = new StudyManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] == null)
                Response.Redirect("~/Error.aspx");
            if (Session["role_id"].ToString() != "APPFREWGR")
                Response.Redirect("~/Error.aspx");
        }
        protected void SubmitBtn_Click(object sender, EventArgs e)
        {
            if (ConsentRadioButtonList.SelectedValue == "1")
            {
                consentManager.InsertConsent(true, Session["id"].ToString());
                Session["consent"] = true;
                int study_id=studyManager.AddStudy(Session["id"].ToString());
                Response.Redirect("Home.aspx");
            }
                
            else if (ConsentRadioButtonList.SelectedValue == "2")
            {
                consentManager.InsertConsent(false, Session["id"].ToString());
                Session["consent"] = false;
                Response.Redirect("Home.aspx");
            }
        }
    }
}