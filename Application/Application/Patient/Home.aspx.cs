using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.App_Code.BLL;

namespace Application.Patient
{
    public partial class Home : System.Web.UI.Page
    {
        private static User user = new User();
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["id"] == null)
            {
                Response.Redirect("~/Error.aspx");
                return;
            }
            ConsentManager consentManager = new ConsentManager();
            bool consent = consentManager.GetConsentByID(Session["id"].ToString());
            bool flag = consentManager.GetFlagByID(Session["id"].ToString());
            Session["consent"] = consent;
            Session["flag"] = flag;
            if (Convert.ToBoolean(Session["consent"]))
            {
                MasterPageFile = "~/Masters/PatientWithConsent.Master";
            }
            else
            {
                if(!Convert.ToBoolean(Session["flag"]))
                {
                    MasterPageFile = "~/Masters/Patient.Master";
                }
                else
                    MasterPageFile = "~/Masters/PatientWithNoConsent.Master";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //If the user has not answered the consent form
            if (!Convert.ToBoolean(Session["flag"]))
            {
                Response.Redirect("Consent.aspx");
                return;
            }
            if(!user.getDemoAdded(Session["id"].ToString()) && Convert.ToBoolean(Session["consent"]))
            {
                Response.Redirect("Demographic.aspx");
                return;
            }
            BulletedList1.Items.Add("None");

        }
    }
}