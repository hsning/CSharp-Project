using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Drawing;
using Application.App_Code.BLL;

namespace Application.Pharmacist
{
    public partial class AddFUP : System.Web.UI.Page
    {
        private static string testDate = null;
        public static int visitID;
        private double Height;
        private double Weight;
        private static double BMI;
        private static string dppDose;
        private static string glpDose;
        private static string bbDose;
        private static string ccbDose;
        private static string fupid;
        private static bool edit = false;
        private static bool submitted = false;
        private static string emptyErrorMsg = "This field cannot be empty";
        private static string invalidErrorMsg = "Invalid entry";
        private static StudyManager studyManager = new StudyManager();
        private static DateTime birthday;
        private static bool alreadyFocus = false;
        private static bool hba1cFocus = false;
        private static bool choleFocus = false;
        private static bool hdlFocus = false;
        private static bool ldlFocus = false;
        private static bool visitDateFocus = false;
        private static bool stopDateFocus = false;
        private static bool egfrDateFocus = false;
        private static bool acrDateFocus = false;
        private string resultingFUP;
        private static FUP fup = new FUP();
        private static ConsentManager consentManager = new ConsentManager();
        private Dictionary<string, object> result = new Dictionary<string, object>
        {
            {"fupid", null},
            {"visitID",null },
            {"visit_type",null },
            {"visit_date",null },
            {"visit_length",null },
            {"weight",null },
            {"height",null },
            {"bmi",null },
            {"smoker",null },
            {"stop_date",null },
            {"systolic",null },
            {"diastolic",null },
            {"glucose",null },
            {"hba1c",null },
            {"cholesterol",null },
            {"hdl",null },
            {"ldl",null },
            {"test_type",null },
            {"date_not_available",null },
            {"test_date",null },
            {"egfr",null },
            {"egfr_date",null },
            {"acr",null },
            {"acr_date",null },
            {"aha",null },
            {"acarbose",null },
            {"acarbose_tdd",null },
            {"dpp_4",null },
            {"dpp_4_name",null },
            {"dpp_4_tdd",null },
            {"glp_1",null },
            {"glp_1_name",null },
            {"glp_1_tdd",null },
            {"bb",null },
            {"bb_name",null },
            {"bb_tdd",null },
            {"ccb",null },
            {"ccb_name",null },
            {"ccb_tdd",null }
        };

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["fupid"] != null)
                MasterPageFile = "~/Masters/SelectedFUP.Master";
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            var onBlurScript1 = Page.ClientScript.GetPostBackEventReference(HBA1CDateTextBox, "OnBlur");
            HBA1CDateTextBox.Attributes.Add("onblur", onBlurScript1);
            var onBlurScript2 = Page.ClientScript.GetPostBackEventReference(CholesterolDateTextBox, "OnBlur");
            CholesterolDateTextBox.Attributes.Add("onblur", onBlurScript2);
            var onBlurScript3 = Page.ClientScript.GetPostBackEventReference(HDLDateTextBox, "OnBlur");
            HDLDateTextBox.Attributes.Add("onblur", onBlurScript3);
            var onBlurScript4 = Page.ClientScript.GetPostBackEventReference(LDLDateTextBox, "OnBlur");
            LDLDateTextBox.Attributes.Add("onblur", onBlurScript4);
            var onBlurScript5 = Page.ClientScript.GetPostBackEventReference(VisitDateTextBox, "OnBlur");
            VisitDateTextBox.Attributes.Add("onblur", onBlurScript5);
            var onBlurScript6 = Page.ClientScript.GetPostBackEventReference(StopTextBox, "OnBlur");
            StopTextBox.Attributes.Add("onblur", onBlurScript6);
            var onBlurScript7 = Page.ClientScript.GetPostBackEventReference(eGFRDateTextBox, "OnBlur");
            eGFRDateTextBox.Attributes.Add("onblur", onBlurScript7);
            var onBlurScript8 = Page.ClientScript.GetPostBackEventReference(ACRDateTextBox, "OnBlur");
            ACRDateTextBox.Attributes.Add("onblur", onBlurScript8);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] == null)
                Response.Redirect("~/Error.aspx");
            if (Session["role_id"].ToString() != "APEMDASDWE")
                Response.Redirect("~/Error.aspx");
            if (!IsPostBack)
            {
                if (Session["NewlyCreatedFUP"] != null && Session["fupid"] != null)
                {
                    if (Convert.ToBoolean(Session["NewlyCreatedFUP"]))
                    {
                        TopStatusLabel.Text = "New visit has been successfully added. The visit id is " + Session["fupid"].ToString();
                    }
                }
                Session["NewlyCreatedFUP"] = false;
                alreadyFocus = false;
                hba1cFocus = false;
                choleFocus = false;
                hdlFocus = false;
                ldlFocus = false;
                visitDateFocus = false;
                stopDateFocus = false;
                egfrDateFocus = false;
                acrDateFocus = false;
                alreadyFocus = false;
                result["fupid"] = null;
                edit = false;
                testDate = null;
                submitted = false;
                //if (Session["patient_id"] == null)
                //    Response.Redirect("~/Error.aspx");
                StatusLabel.Text = "";
                ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
                DemoPanel.Visible = true;
                if (Session["fupid"] != null)
                {
                    FillForm();
                    Label1.Text = "Modify existing visit";
                    fupid = Session["fupid"].ToString();
                    Label1.Text += " (Study ID: " + Session["study_id"] + " & Visit ID:" + Session["fupid"] + " )";
                }
                else
                {
                    Label1.Text += " (Study ID: " + Session["study_id"] + ")";
                }
                if(Session["study_id"]!=null)
                {
                    visitID = Convert.ToInt32(Session["study_id"]);
                    birthday = studyManager.GetBDay(visitID);
                }

            }
            if (Page.IsPostBack)
            {
                alreadyFocus = false;
                Page.MaintainScrollPositionOnPostBack = true;
                var ctrlName = Request.Params[Page.postEventSourceID];
                var args = Request.Params[Page.postEventArgumentID];
                HandleCustomPostbackEvent(ctrlName, args);
                SetFocusAfterPostBack();
            }
            StatusLabel.Text = "";
            if (submitted)
                Page.Validate("vg");

        }
        public static void SetFocusAfterPostBack()
        {
            var page = HttpContext.Current.Handler as Page;
            if (page == null)
            {
                return;
            }
            var postBackCtl = page.FindControl(HttpContext.Current.Request.Form["__EVENTTARGET"]) as WebControl;
            if (postBackCtl == null || postBackCtl.TabIndex == 0)
            {
                return;
            }
            var ctl = GetCtlByTabIndex(page, postBackCtl.TabIndex + 1);
            if (ctl != null)
            {
                ctl.Focus();
            }
        }
        private static WebControl GetCtlByTabIndex(System.Web.UI.Control ParentCtl, int TabIndex)
        {
            foreach (System.Web.UI.Control ctl in ParentCtl.Controls)
            {
                var webCtl = ctl as WebControl;
                if (webCtl != null)
                {
                    if (webCtl.TabIndex == TabIndex)
                    {
                        return webCtl;
                    }
                }
                var retCtl = GetCtlByTabIndex(ctl, TabIndex);
                if (retCtl != null)
                {
                    return retCtl;
                }
            }
            return null;
        }
        private void HandleCustomPostbackEvent(string ctrlName, string args)
        {
            //Since this will get called for every postback, we only
            // want to handle a specific combination of control
            // and argument.
            if (ctrlName == HBA1CDateTextBox.UniqueID && args == "OnBlur")
            {
                if (testDate == null)
                {
                    testDate = HBA1CDateTextBox.Text;
                    populateDates();
                }
            }
            if (ctrlName == CholesterolDateTextBox.UniqueID && args == "OnBlur")
            {
                if (testDate == null)
                {
                    testDate = CholesterolDateTextBox.Text;
                    populateDates();
                }
            }
            if (ctrlName == HDLDateTextBox.UniqueID && args == "OnBlur")
            {
                if (testDate == null)
                {
                    testDate = HDLDateTextBox.Text;
                    populateDates();
                }
            }
            if (ctrlName == LDLDateTextBox.UniqueID && args == "OnBlur")
            {
                if (testDate == null)
                {
                    testDate = LDLDateTextBox.Text;
                    populateDates();
                }
            }
            if (submitted)
                Page.Validate("vg");
        }
        protected void HeightCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (HeightTextBox.Text == "")
            {
                args.IsValid = false;
                HeightCustomValidator.ErrorMessage = "Height field cannot be empty";
                if (!alreadyFocus)
                {
                    HeightTextBox.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            decimal height;
            bool isNumeric = decimal.TryParse(HeightTextBox.Text, out height);
            if (!isNumeric || height < 50 || height > 250)
            {
                args.IsValid = false;
                HeightCustomValidator.ErrorMessage = "Invalid value. Height must be a numeric value between 50 and 250cm.";
                if (!alreadyFocus)
                {
                    HeightTextBox.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            args.IsValid = true;
        }

        protected void WeightCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (WeightTextBox.Text == "")
            {
                args.IsValid = false;
                WeightCustomValidator.ErrorMessage = "Weight field cannot be empty";
                if (!alreadyFocus)
                {
                    WeightTextBox.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            decimal weight;
            bool isNumeric = decimal.TryParse(WeightTextBox.Text, out weight);
            if (!isNumeric || weight < 50 || weight > 400)
            {
                args.IsValid = false;
                WeightCustomValidator.ErrorMessage = "Invalid value. Weight must be a numeric value between 50 and 400kg.";
                if (!alreadyFocus)
                {
                    WeightTextBox.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            WeightCustomValidator.ErrorMessage = "";
            args.IsValid = true;
        }


        protected void DiastolicValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (SystolicTextBox.Text == "")
            {
                DiastolicCustomValidator.ErrorMessage = "Systolic and Diastolic fields cannot be empty";
                args.IsValid = false;
                if (!alreadyFocus)
                {
                    SystolicTextBox.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            if (DiastolicTextBox.Text == "")
            {
                DiastolicCustomValidator.ErrorMessage = "Systolic and Diastolic fields cannot be empty";
                args.IsValid = false;
                if (!alreadyFocus)
                {
                    DiastolicTextBox.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            double diastolic;
            bool isNumberD = double.TryParse(DiastolicTextBox.Text, out diastolic);
            double systolic;
            bool isNumberS = double.TryParse(SystolicTextBox.Text, out systolic);
            if (!isNumberS || systolic < 40 || systolic > 360)
            {
                DiastolicCustomValidator.ErrorMessage = "One or both of the blood pressures entered is invalid";
                args.IsValid = false;
                if (!alreadyFocus)
                {
                    SystolicTextBox.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            if (!isNumberD || diastolic < 40 || diastolic > 360)
            {
                DiastolicCustomValidator.ErrorMessage = "One or both of the blood pressures entered is invalid";
                args.IsValid = false;
                if (!alreadyFocus)
                {
                    DiastolicTextBox.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            DiastolicCustomValidator.ErrorMessage = "";
            args.IsValid = true;
        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {

        }

        protected void DDPCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Check();
        }

        private void Check()
        {
            if (AHARadioButtonList.SelectedValue == "1")
                AHAPanel.Visible = true;
            else
            {
                AHAPanel.Visible = false;
                AcarboseCheckBox.Checked = false;
                DPPCheckBox.Checked = false;
                GLPCheckBox.Checked = false;
            }
            if (HypertensionRadioButtonList.SelectedValue == "1")
                HypertensionPanel.Visible = true;
            else if (HypertensionRadioButtonList.SelectedValue == "2")
            {
                HypertensionPanel.Visible = false;
                BBCheckBox.Checked = false;
                CCBCheckBox.Checked = false;
            }

            if (DPPCheckBox.Checked)
                DPPPanel.Visible = true;
            else
            {
                DPPPanel.Visible = false;
                dppDDL.SelectedValue = "";
                dppTDDTextBox.Text = "";
            }
            if (GLPCheckBox.Checked)
                GLPPanel.Visible = true;
            else
            {
                GLPPanel.Visible = false;
                glpDDL.SelectedValue = "";
                glpTDDTextBox.Text = "";
            }
            if (BBCheckBox.Checked)
                BBPanel.Visible = true;
            else
            {
                BBPanel.Visible = false;
                bbDDL.SelectedValue = "";
                bbTDDTextBox.Text = "";
            }

            if (CCBCheckBox.Checked)
                CCBPanel.Visible = true;
            else
            {
                CCBPanel.Visible = false;
                ccbDDL.SelectedValue = "";
                ccbTDDTextBox.Text = "";
            }
            if (DateNotAvailableCheckBox.Checked)
            {
                foreach (var TextBoxItem in DatePlaceHolder.Controls.OfType<TextBox>())
                {
                    if (TextBoxItem.TextMode == TextBoxMode.Date)
                    {
                        TextBoxItem.BackColor = Color.FromArgb(230, 230, 230);
                        TextBoxItem.ReadOnly = true;
                        TextBoxItem.Text = "";
                        testDate = null;
                    }
                }
            }
            else
            {
                foreach (var TextBoxItem in DatePlaceHolder.Controls.OfType<TextBox>())
                {
                    if (TextBoxItem.TextMode == TextBoxMode.Date)
                    {
                        TextBoxItem.BackColor = Color.White;
                        TextBoxItem.ReadOnly = false;
                    }
                }
            }
            if (AcarboseCheckBox.Checked)
                AcarbosePanel.Visible = true;
            else
            {
                AcarbosePanel.Visible = false;
                AcarboseDoseTextBox.Text = "";
            }

        }

        protected void GLPCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Check();
        }

        protected void RadioButtonList4_SelectedIndexChanged(object sender, EventArgs e)
        {
            Check();
        }

        protected void BBCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Check();
        }

        protected void CCBCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Check();
        }

        protected void AHARadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Check();
        }

        protected void DateNotAvailableCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Check();
        }

        protected void populateDates()
        {
            foreach (var TextBoxItem in DatePlaceHolder.Controls.OfType<TextBox>())
            {
                if (TextBoxItem.TextMode == TextBoxMode.Date)
                {
                    TextBoxItem.BackColor = Color.White;
                    TextBoxItem.ReadOnly = false;
                    TextBoxItem.Text = testDate;
                }
            }
            if (submitted)
                Page.Validate("vg");
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            bool submitResult;
            submitted = true;
            Page.Validate("vg");
            if (Page.IsValid)
            {
                if (edit)
                {
                    submitResult = Submit();
                    if (submitResult)
                    {
                        StatusLabel.ForeColor = Color.Green;
                        StatusLabel.Text = "This visit has been successfully updated";
                    }
                    else
                    {
                        StatusLabel.ForeColor = Color.Red;
                        StatusLabel.Text = "Visit update failed";
                    }
                }
                else
                {
                    string fupPrefix = Session["study_id"].ToString() +"-"+ VisitDateTextBox.Text.ToString();
                    bool FUPIDConstraintPassed=fup.CheckFUPID(fupPrefix);
                    if(!FUPIDConstraintPassed)
                    {
                        StatusLabel.ForeColor = Color.Red;
                        StatusLabel.Text = "A visit already exists on this visit date. ONE VISIT PER DAY.";
                        return;
                    }
                    result["fupid"] = fupPrefix + "-1";
                    submitResult = Submit();

                    if (submitResult)
                    {
                        Session["NewlyCreatedFUP"] = true;
                        Session["fupid"] = result["fupid"];
                        Response.Redirect("AddFUP.aspx");
                    }
                    else
                    {
                        StatusLabel.ForeColor = Color.Red;
                        StatusLabel.Text = "Visit add failed";
                    }
                }
            }
            else
            {
                string message = "There is more than 1 error on this form.";
                Response.Write("<script type='text/javascript'> alert('" + message + "') </script>");
            }
        }

        private void calculateBMI()
        {
            Double.TryParse(HeightTextBox.Text, out Height);
            Double.TryParse(WeightTextBox.Text, out Weight);
            if (Height != null && Weight != null)
            {
                BMI = Weight / (Math.Pow(Height / 100.0, 2));
                BMITextbox.Text = string.Format("{0:F1}", BMI);
            }
        }
        private bool Submit()
        {
            calculateBMI();
            bool postResult;
            dppDose = dppTDDTextBox.Text;
            glpDose = glpTDDTextBox.Text;
            bbDose = bbTDDTextBox.Text;
            ccbDose = ccbTDDTextBox.Text;
            //result["visitID"] = visitID;
            result["visit_date"] = VisitDateTextBox.Text;
            result["visit_length"] = VisitLengthTextBox.Text;
            result["weight"] = WeightTextBox.Text;
            result["height"] = HeightTextBox.Text;
            result["bmi"] = BMI;
            result["smoker"] = SmokeRadioButtonList.SelectedValue;
            result["stop_date"] = StopTextBox.Text != "" ? StopTextBox.Text : null;
            result["systolic"] = SystolicTextBox.Text;
            result["diastolic"] = DiastolicTextBox.Text;
            result["glucose"] = GlucoseTextBox.Text;
            result["hba1c"] = HBA1CTextBox.Text;
            result["cholesterol"] = CholesterolTextBox.Text;
            result["hdl"] = HDLTextBox.Text;
            result["ldl"] = LDLTextBox.Text;
            result["test_type"] = TestTypeRadioButtonList.SelectedValue;
            result["date_not_available"] = DateNotAvailableCheckBox.Checked;
            result["hba1c_date"] = HBA1CDateTextBox.Text == "" ? null : HBA1CDateTextBox.Text;
            result["chole_date"] = CholesterolDateTextBox.Text == "" ? null : CholesterolDateTextBox.Text;
            result["hdl_date"] = HDLDateTextBox.Text == "" ? null : HDLDateTextBox.Text;
            result["ldl_date"] = LDLDateTextBox.Text == "" ? null : LDLDateTextBox.Text;
            result["egfr"] = eGFRTextBox.Text;
            result["egfr_date"] = eGFRDateTextBox.Text;
            result["acr"] = ACRTextBox.Text;
            result["acr_date"] = ACRDateTextBox.Text;
            result["aha"] = AHARadioButtonList.SelectedValue;
            result["acarbose"] = AcarboseCheckBox.Checked == true ? true : (bool?)null;
            result["acarbose_tdd"] = AcarboseDoseTextBox.Text != "" ? AcarboseDoseTextBox.Text : null;
            result["dpp_4"] = DPPCheckBox.Checked == true ? true : (bool?)null;
            result["dpp_4_name"] = DPPCheckBox.Checked ? dppDDL.SelectedValue : null;
            result["dpp_4_tdd"] = dppDose == "" ? null : dppDose;
            result["glp_1"] = GLPCheckBox.Checked == true ? true : (bool?)null;
            result["glp_1_name"] = GLPCheckBox.Checked ? glpDDL.SelectedValue : null;
            result["glp_1_tdd"] = glpDose == "" ? null : glpDose;
            result["hypertension"] = HypertensionRadioButtonList.SelectedValue;
            result["bb"] = BBCheckBox.Checked == true ? true : (bool?)null;
            result["bb_name"] = BBCheckBox.Checked ? bbDDL.SelectedValue : null;
            result["bb_tdd"] = bbDose == "" ? null : bbDose;
            result["ccb"] = CCBCheckBox.Checked == true ? true : (bool?)null;
            result["ccb_name"] = CCBCheckBox.Checked ? ccbDDL.SelectedValue : null;
            result["ccb_tdd"] = ccbDose == "" ? null : ccbDose;
            result["patient_id"] = Convert.ToInt32(Session["study_id"]);
            if (edit)
            {
                result["fupid"] = fupid;
                postResult = fup.UpdateFollowUps(result);
            }
            else
            {
                postResult = fup.InsertFollowUps(result);
            }
            return postResult;
        }

        protected void VisitDateCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (VisitDateTextBox.Text == "")
            {
                VisitDateCustomValidator.ErrorMessage = "Visit date field cannot be empty";
                args.IsValid = false;
                if (!alreadyFocus && !visitDateFocus)
                {
                    VisitDateTextBox.Focus();
                    alreadyFocus = true;
                    visitDateFocus = true;
                }
                return;
            }
            if (Convert.ToDateTime(VisitDateTextBox.Text) > DateTime.Now)
            {
                VisitDateCustomValidator.ErrorMessage = "Visit date: invalid entry";
                args.IsValid = false;
                if (!alreadyFocus && !visitDateFocus)
                {
                    VisitDateTextBox.Focus();
                    alreadyFocus = true;
                    visitDateFocus = true;
                }
                return;
            }
            if (birthday != null)
            {
                if (Convert.ToDateTime(VisitDateTextBox.Text) < birthday)
                {
                    VisitDateCustomValidator.ErrorMessage = "Visit date: invalid entry";
                    args.IsValid = false;
                    if (!alreadyFocus && !visitDateFocus)
                    {
                        VisitDateTextBox.Focus();
                        alreadyFocus = true;
                        visitDateFocus = true;
                    }
                    return;
                }
            }
            VisitDateCustomValidator.ErrorMessage = "";
            args.IsValid = true;
        }

        protected void VisitLengthCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (VisitLengthTextBox.Text == "")
            {
                VisitLengthCustomValidator.ErrorMessage = "Visit length field cannot be empty";
                args.IsValid = false;
                if (!alreadyFocus)
                {
                    VisitLengthTextBox.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            if (Convert.ToInt32(Convert.ToDouble(VisitLengthTextBox.Text)) > 1000)
            {
                VisitLengthCustomValidator.ErrorMessage = "Visit length: invalid entry";
                args.IsValid = false;
                if (!alreadyFocus)
                {
                    VisitLengthTextBox.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            VisitLengthCustomValidator.ErrorMessage = "";
            args.IsValid = true;
        }

        protected void StopDateCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (StopTextBox.Text == "")
            {
                StopDateCustomValidator.ErrorMessage = "Stop date field cannot be empty";
                args.IsValid = false;
                if (!alreadyFocus && !stopDateFocus)
                {
                    StopTextBox.Focus();
                    alreadyFocus = true;
                    stopDateFocus = true;
                }
                return;
            }
            if (Convert.ToDateTime(StopTextBox.Text) > DateTime.Now)
            {
                StopDateCustomValidator.ErrorMessage = "Stop date: invalid entry";
                args.IsValid = false;
                if (!alreadyFocus && !stopDateFocus)
                {
                    StopTextBox.Focus();
                    alreadyFocus = true;
                    stopDateFocus = true;
                }
                return;
            }
            if (birthday != null)
            {
                if (Convert.ToDateTime(StopTextBox.Text) < birthday)
                {
                    StopDateCustomValidator.ErrorMessage = "Stop date: invalid entry";
                    args.IsValid = false;
                    if (!alreadyFocus && !stopDateFocus)
                    {
                        StopTextBox.Focus();
                        alreadyFocus = true;
                        stopDateFocus = true;
                    }
                    return;
                }
            }
            StopDateCustomValidator.ErrorMessage = "";
            args.IsValid = true;
        }

        protected void SmokeCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (SmokeRadioButtonList.SelectedValue == "")
            {
                SmokeCustomValidator.ErrorMessage = "Smoking field cannot be empty";
                args.IsValid = false;
                if (!alreadyFocus)
                {
                    SmokeRadioButtonList.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            SmokeCustomValidator.ErrorMessage = "";
            args.IsValid = true;
        }

        protected void SmokeRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
        {
            SmokeCheck();
        }
        private void SmokeCheck()
        {
            if (SmokeRadioButtonList.SelectedValue == "2")
                SmokingPanel.Visible = true;
            else
            {
                StopTextBox.Text = "";
                SmokingPanel.Visible = false;
            }
            if (submitted)
                Page.Validate("vg");
        }
        protected void GlucoseValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (GlucoseTextBox.Text == "")
            {
                GlucoseCustomValidator.ErrorMessage = "Glucose field cannot be empty";
                args.IsValid = false;
                if (!alreadyFocus)
                {
                    GlucoseTextBox.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            GlucoseCustomValidator.ErrorMessage = "";
            args.IsValid = true;
        }

        protected void TestTypeValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (TestTypeRadioButtonList.SelectedValue == "")
            {
                TestTypeCustomValidator.ErrorMessage = "Test type field cannot be empty";
                args.IsValid = false;
                if (!alreadyFocus)
                {
                    TestTypeRadioButtonList.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            TestTypeCustomValidator.ErrorMessage = "";
            args.IsValid = true;
        }

        protected void HBA1CValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (HBA1CTextBox.Text == "")
            {
                HBA1CCustomValidator.ErrorMessage = "HbA1c field cannot be empty";
                args.IsValid = false;
                if (!alreadyFocus)
                {
                    HBA1CTextBox.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            HBA1CCustomValidator.ErrorMessage = "";
            args.IsValid = true;
        }

        protected void CholesterolValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (CholesterolTextBox.Text == "")
            {
                CholesterolCustomValidator.ErrorMessage = "Cholesterol field cannot be empty";
                args.IsValid = false;
                if (!alreadyFocus)
                {
                    CholesterolTextBox.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            CholesterolCustomValidator.ErrorMessage = "";
            args.IsValid = true;
        }

        protected void HDLValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (HDLTextBox.Text == "")
            {
                HDLCustomValidator.ErrorMessage = "HDL field cannot be empty";
                args.IsValid = false;
                if (!alreadyFocus)
                {
                    HDLTextBox.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            HDLCustomValidator.ErrorMessage = "";
            args.IsValid = true;
        }

        protected void LDLValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (LDLTextBox.Text == "")
            {
                LDLCustomValidator.ErrorMessage = "LDL field cannot be empty";
                args.IsValid = false;
                if (!alreadyFocus)
                {
                    LDLTextBox.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            LDLCustomValidator.ErrorMessage = "";
            args.IsValid = true;
        }
        protected void LDLDateValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!DateNotAvailableCheckBox.Checked)
            {
                if (!DateNotAvailableCheckBox.Checked && LDLDateTextBox.Text == "")
                {
                    LDLDateCustomValidator.ErrorMessage = "Please check the Date Not Available box or fill up the date";
                    args.IsValid = false;
                    if (!alreadyFocus && !ldlFocus)
                    {
                        LDLDateTextBox.Focus();
                        alreadyFocus = true;
                        ldlFocus = true;
                    }
                    return;
                }
                if (Convert.ToDateTime(LDLDateTextBox.Text) > DateTime.Now)
                {
                    LDLDateCustomValidator.ErrorMessage = "LDL date: invalid entry";
                    args.IsValid = false;
                    if (!alreadyFocus && !ldlFocus)
                    {
                        LDLDateTextBox.Focus();
                        alreadyFocus = true;
                        ldlFocus = true;
                    }
                    return;
                }
                if (birthday != null)
                {
                    if (Convert.ToDateTime(LDLDateTextBox.Text) < birthday)
                    {
                        LDLDateCustomValidator.ErrorMessage = "LDL date: invalid entry";
                        args.IsValid = false;
                        if (!alreadyFocus && !ldlFocus)
                        {
                            LDLDateTextBox.Focus();
                            alreadyFocus = true;
                            ldlFocus = true;
                        }
                        return;
                    }
                }
            }

            LDLDateCustomValidator.ErrorMessage = "";
            args.IsValid = true;
        }
        protected void HBA1CDateValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!DateNotAvailableCheckBox.Checked)
            {
                if (!DateNotAvailableCheckBox.Checked && HBA1CDateTextBox.Text == "")
                {
                    HBA1CDateCustomValidator.ErrorMessage = "Please check the Date Not Available box or fill up the date";
                    args.IsValid = false;
                    if (!alreadyFocus && !hba1cFocus)
                    {
                        HBA1CDateTextBox.Focus();
                        alreadyFocus = true;
                        hba1cFocus = true;
                    }
                    return;
                }
                if (Convert.ToDateTime(HBA1CDateTextBox.Text) > DateTime.Now)
                {
                    HBA1CDateCustomValidator.ErrorMessage = "HBA1C date: invalid entry";
                    args.IsValid = false;
                    if (!alreadyFocus && !hba1cFocus)
                    {
                        HBA1CDateTextBox.Focus();
                        alreadyFocus = true;
                        hba1cFocus = true;
                    }
                    return;
                }
                if (birthday != null)
                {
                    if (Convert.ToDateTime(HBA1CDateTextBox.Text) < birthday)
                    {
                        HBA1CDateCustomValidator.ErrorMessage = "HbA1c date: invalid entry";
                        args.IsValid = false;
                        if (!alreadyFocus && !hba1cFocus)
                        {
                            HBA1CDateTextBox.Focus();
                            alreadyFocus = true;
                            hba1cFocus = true;
                        }
                        return;
                    }
                }
            }
            HBA1CDateCustomValidator.ErrorMessage = "";
            args.IsValid = true;
        }
        protected void CholeDateValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!DateNotAvailableCheckBox.Checked)
            {
                if (!DateNotAvailableCheckBox.Checked && CholesterolDateTextBox.Text == "")
                {
                    CholeDateCustomValidator.ErrorMessage = "Please check the Date Not Available box or fill up the date";
                    args.IsValid = false;
                    if (!alreadyFocus && !choleFocus)
                    {
                        CholesterolDateTextBox.Focus();
                        alreadyFocus = true;
                        choleFocus = true;
                    }
                    return;
                }
                if (Convert.ToDateTime(CholesterolDateTextBox.Text) > DateTime.Now)
                {
                    CholeDateCustomValidator.ErrorMessage = "Cholesterol date: invalid entry";
                    args.IsValid = false;
                    if (!alreadyFocus && !choleFocus)
                    {
                        CholesterolDateTextBox.Focus();
                        alreadyFocus = true;
                        choleFocus = true;
                    }
                    return;
                }
                if (birthday != null)
                {
                    if (Convert.ToDateTime(CholesterolDateTextBox.Text) < birthday)
                    {
                        CholeDateCustomValidator.ErrorMessage = "Cholesterol date: invalid entry";
                        args.IsValid = false;
                        if (!alreadyFocus && !choleFocus)
                        {
                            CholesterolDateTextBox.Focus();
                            alreadyFocus = true;
                            choleFocus = true;
                        }
                        return;
                    }
                }
            }

            CholeDateCustomValidator.ErrorMessage = "";
            args.IsValid = true;
        }
        protected void HDLDateValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!DateNotAvailableCheckBox.Checked)
            {
                if (!DateNotAvailableCheckBox.Checked && HDLDateTextBox.Text == "")
                {
                    HDLDateCustomValidator.ErrorMessage = "Please check the Date Not Available box or fill up the date";
                    args.IsValid = false;
                    if (!alreadyFocus && !hdlFocus)
                    {
                        HDLDateTextBox.Focus();
                        alreadyFocus = true;
                        hdlFocus = true;
                    }
                    return;
                }
                if (Convert.ToDateTime(HDLDateTextBox.Text) > DateTime.Now)
                {
                    HDLDateCustomValidator.ErrorMessage = "HDL date: invalid entry";
                    args.IsValid = false;
                    if (!alreadyFocus && !hdlFocus)
                    {
                        HDLDateTextBox.Focus();
                        alreadyFocus = true;
                        hdlFocus = true;
                    }
                    return;
                }
                if (birthday != null)
                {
                    if (Convert.ToDateTime(HDLDateTextBox.Text) < birthday)
                    {
                        HDLDateCustomValidator.ErrorMessage = "HDL date: invalid entry";
                        args.IsValid = false;
                        if (!alreadyFocus && !hdlFocus)
                        {
                            HDLDateTextBox.Focus();
                            alreadyFocus = true;
                            hdlFocus = true;
                        }
                        return;
                    }
                }
                HDLDateCustomValidator.ErrorMessage = "";
                args.IsValid = true;
            }
        }

        protected void ACRValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ACRTextBox.Text == "")
            {
                ACRCustomValidator.ErrorMessage = "ACR field cannot be empty";
                args.IsValid = false;
                if (!alreadyFocus)
                {
                    ACRTextBox.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            ACRCustomValidator.ErrorMessage = "";
            args.IsValid = true;
        }

        protected void egfrDateValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (eGFRDateTextBox.Text == "")
            {
                egfrDateCustomValidator.ErrorMessage = "eGFR date field cannot be empty";
                args.IsValid = false;
                if (!alreadyFocus && !egfrDateFocus)
                {
                    eGFRDateTextBox.Focus();
                    alreadyFocus = true;
                    egfrDateFocus = true;
                }
                return;
            }
            if (Convert.ToDateTime(eGFRDateTextBox.Text) > DateTime.Now)
            {
                egfrDateCustomValidator.ErrorMessage = "eGFR date: invalid entry";
                args.IsValid = false;
                if (!alreadyFocus && !egfrDateFocus)
                {
                    eGFRDateTextBox.Focus();
                    alreadyFocus = true;
                    egfrDateFocus = true;
                }
                return;
            }
            if (birthday != null)
            {
                if (Convert.ToDateTime(eGFRDateTextBox.Text) < birthday)
                {
                    egfrDateCustomValidator.ErrorMessage = "eGFR date: invalid entry";
                    args.IsValid = false;
                    if (!alreadyFocus && !egfrDateFocus)
                    {
                        eGFRDateTextBox.Focus();
                        alreadyFocus = true;
                        egfrDateFocus = true;
                    }
                    return;
                }
            }
            egfrDateCustomValidator.ErrorMessage = "";
            args.IsValid = true;
        }

        protected void ACRDateValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ACRDateTextBox.Text == "")
            {
                ACRDateCustomValidator.ErrorMessage = "ACR date field cannot be empty";
                args.IsValid = false;
                if (!alreadyFocus && !acrDateFocus)
                {
                    ACRDateTextBox.Focus();
                    alreadyFocus = true;
                    acrDateFocus = true;
                }
                return;
            }
            if (Convert.ToDateTime(ACRDateTextBox.Text) > DateTime.Now)
            {
                ACRDateCustomValidator.ErrorMessage = "ACR date: invalid entry";
                args.IsValid = false;
                if (!alreadyFocus && !acrDateFocus)
                {
                    ACRDateTextBox.Focus();
                    alreadyFocus = true;
                    acrDateFocus = true;
                }
                return;
            }
            if (birthday != null)
            {
                if (Convert.ToDateTime(ACRDateTextBox.Text) < birthday)
                {
                    ACRDateCustomValidator.ErrorMessage = "ACR date: invalid entry";
                    args.IsValid = false;
                    if (!alreadyFocus && !acrDateFocus)
                    {
                        ACRDateTextBox.Focus();
                        alreadyFocus = true;
                        acrDateFocus = true;
                    }
                    return;
                }
            }
            ACRDateCustomValidator.ErrorMessage = "";
            args.IsValid = true;
        }

        protected void egfrValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (eGFRTextBox.Text == "")
            {
                egfrCustomValidator.ErrorMessage = "eGFR field cannot be empty";
                args.IsValid = false;
                if (!alreadyFocus)
                {
                    eGFRTextBox.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            egfrCustomValidator.ErrorMessage = "";
            args.IsValid = true;
        }

        protected void AHAValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (AHARadioButtonList.SelectedValue == "")
            {
                AHACustomValidator.ErrorMessage = "Anti-hyperglycemics checkbox cannot be unchecked";
                args.IsValid = false;
                if (!alreadyFocus)
                {
                    AHARadioButtonList.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            AHACustomValidator.ErrorMessage = "";
            args.IsValid = true;
        }


        protected void AcarboseDoseValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (AcarboseCheckBox.Checked)
            {
                if (AcarboseDoseTextBox.Text == "")
                {
                    AcarboseDoseCustomValidator.ErrorMessage = "Acarbose dose field cannot be empty";
                    args.IsValid = false;
                    if (!alreadyFocus)
                    {
                        AcarboseDoseTextBox.Focus();
                        alreadyFocus = true;
                    }
                    return;
                }
                AcarboseDoseCustomValidator.ErrorMessage = "";
                args.IsValid = true;
            }
            else
            {
                AcarboseDoseCustomValidator.ErrorMessage = "";
                args.IsValid = true;
            }

        }
        protected void HypertensionValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (HypertensionRadioButtonList.SelectedValue == "")
            {
                HypertensionCustomValidator.ErrorMessage = "Hypertension checkbox cannot be unchecked";
                args.IsValid = false;
                if (!alreadyFocus)
                {
                    HypertensionRadioButtonList.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            HypertensionCustomValidator.ErrorMessage = "";
            args.IsValid = true;
        }

        protected void DPPValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (DPPCheckBox.Checked && dppDDL.SelectedValue == null)
            {
                args.IsValid = false;
                DPPCustomValidator.ErrorMessage = "Please check one of the options below";
                if (!alreadyFocus)
                {
                    dppDDL.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            DPPCustomValidator.ErrorMessage = "";
            args.IsValid = true;

        }

        protected void glpValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (GLPCheckBox.Checked && glpDDL.SelectedValue == null)
            {
                args.IsValid = false;
                glpCustomValidator.ErrorMessage = "Please check one of the options below";
                if (!alreadyFocus)
                {
                    glpDDL.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            glpCustomValidator.ErrorMessage = "";
            args.IsValid = true;
        }

        protected void BBValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (BBCheckBox.Checked && bbDDL.SelectedValue == null)
            {
                args.IsValid = false;
                BBCustomValidator.ErrorMessage = "Please check one of the options below";
                if (!alreadyFocus)
                {
                    bbDDL.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            BBCustomValidator.ErrorMessage = "";
            args.IsValid = true;
        }

        protected void CCBValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (CCBCheckBox.Checked && ccbDDL.SelectedValue == null)
            {
                args.IsValid = false;
                CCBCustomValidator.ErrorMessage = "Please check one of the options below";
                if (!alreadyFocus)
                {
                    ccbDDL.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            CCBCustomValidator.ErrorMessage = "";
            args.IsValid = true;
        }

        protected void HeightTextBox_TextChanged(object sender, EventArgs e)
        {
            calculateBMI();
        }

        protected void WeightTextBox_TextChanged(object sender, EventArgs e)
        {
            calculateBMI();
        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            DemoPanel.Visible = true;
        }

        private void FillForm()
        {
            DemoPanel.Visible = true;
            SqlDataSource2.DataBind();
            DataView dv = (DataView)SqlDataSource2.Select(DataSourceSelectArguments.Empty);
            DataTable dt = dv.ToTable();
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            foreach (DataRow dr in dv.Table.Rows)
            {
                VisitDateTextBox.Text = Convert.ToDateTime(dr[2]).ToString("yyyy-MM-dd");
                VisitLengthTextBox.Text = dr[3].ToString();
                WeightTextBox.Text = dr[4].ToString();
                HeightTextBox.Text = dr[5].ToString();
                BMITextbox.Text = dr[6].ToString();
                SmokeRadioButtonList.SelectedValue = dr[7].ToString();
                StopTextBox.Text = dr[8] == System.DBNull.Value ? "" : Convert.ToDateTime(dr[8]).ToString("yyyy-MM-dd");
                SystolicTextBox.Text = dr[9].ToString();
                DiastolicTextBox.Text = dr[10].ToString();
                GlucoseTextBox.Text = dr[11].ToString();
                TestTypeRadioButtonList.SelectedValue = dr[12].ToString();
                HBA1CTextBox.Text = dr[13].ToString();
                HBA1CDateTextBox.Text = dr[14] == System.DBNull.Value ? "" : Convert.ToDateTime(dr[14]).ToString("yyyy-MM-dd");
                CholesterolTextBox.Text = dr[15].ToString();
                CholesterolDateTextBox.Text = dr[16] == System.DBNull.Value ? "" : Convert.ToDateTime(dr[16]).ToString("yyyy-MM-dd");
                HDLTextBox.Text = dr[17].ToString();
                HDLDateTextBox.Text = dr[18] == System.DBNull.Value ? "" : Convert.ToDateTime(dr[18]).ToString("yyyy-MM-dd");
                LDLTextBox.Text = dr[19].ToString();
                LDLDateTextBox.Text = dr[20] == System.DBNull.Value ? "" : Convert.ToDateTime(dr[20]).ToString("yyyy-MM-dd");
                DateNotAvailableCheckBox.Checked = Convert.ToBoolean(dr[21]);
                eGFRTextBox.Text = dr[22].ToString();
                eGFRDateTextBox.Text = Convert.ToDateTime(dr[23]).ToString("yyyy-MM-dd");
                ACRTextBox.Text = dr[24].ToString();
                ACRDateTextBox.Text = Convert.ToDateTime(dr[25]).ToString("yyyy-MM-dd");
                AHARadioButtonList.SelectedValue = dr[26].ToString();
                AcarboseCheckBox.Checked = dr[27] == System.DBNull.Value ? false : true;
                AcarboseDoseTextBox.Text = dr[28].ToString();
                DPPCheckBox.Checked = dr[29] == System.DBNull.Value ? false : true;
                dppDDL.SelectedValue = dr[30].ToString();
                dppTDDTextBox.Text = dr[31].ToString();
                GLPCheckBox.Checked = dr[32] == System.DBNull.Value ? false : true;
                glpDDL.SelectedValue = dr[33].ToString();
                glpTDDTextBox.Text = dr[34].ToString();
                HypertensionRadioButtonList.SelectedValue = dr[35].ToString();
                BBCheckBox.Checked = dr[36] == System.DBNull.Value ? false : true;
                bbDDL.SelectedValue = dr[37].ToString();
                bbTDDTextBox.Text = dr[38].ToString();
                CCBCheckBox.Checked = dr[39] == System.DBNull.Value ? false : true;
                ccbDDL.SelectedValue = dr[40].ToString();
                ccbTDDTextBox.Text = dr[41].ToString();
                fupid = dr[0].ToString();
                Check();
                SmokeCheck();
                underBPPanel.Visible = true;
                SubmitBtn.Text = "Save changes";
                edit = true;
            }
        }


        protected void dppTDDCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (DPPCheckBox.Checked && dppTDDTextBox.Text == "")
            {
                args.IsValid = false;
                dppTDDCustomValidator.ErrorMessage = "DPP-4 total daily dose field cannot be empty";
                if (!alreadyFocus)
                {
                    dppTDDTextBox.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            dppTDDCustomValidator.ErrorMessage = "";
            args.IsValid = true;
        }

        protected void glpTDDCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (GLPCheckBox.Checked && glpTDDTextBox.Text == "")
            {
                args.IsValid = false;
                glpTDDCustomValidator.ErrorMessage = "GLP-1 total daily dose field cannot be empty";
                if (!alreadyFocus)
                {
                    glpTDDTextBox.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            glpTDDCustomValidator.ErrorMessage = "";
            args.IsValid = true;
        }

        protected void bbTDDCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (BBCheckBox.Checked && bbTDDTextBox.Text == "")
            {
                args.IsValid = false;
                bbTDDCustomValidator.ErrorMessage = "Beta Blocker total daily dose field cannot be empty";
                if (!alreadyFocus)
                {
                    bbTDDTextBox.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            bbTDDCustomValidator.ErrorMessage = "";
            args.IsValid = true;
        }

        protected void ccbTDDCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (CCBCheckBox.Checked && ccbTDDTextBox.Text == "")
            {
                args.IsValid = false;
                ccbTDDCustomValidator.ErrorMessage = "Calcium Channel Blocker total daily dose field cannot be empty";
                if (!alreadyFocus)
                {
                    ccbTDDTextBox.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            ccbTDDCustomValidator.ErrorMessage = "";
            args.IsValid = true;
        }

        protected void AcarboseCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Check();
        }

        protected void LDLCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void DPPDLLCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (dppDDL.SelectedValue == "")
            {
                args.IsValid = false;
                DPPDLLCustomValidator.ErrorMessage = "DPP-4 medication field cannot be empty";
                if (!alreadyFocus)
                {
                    dppDDL.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            DPPDLLCustomValidator.ErrorMessage = "";
            args.IsValid = true;
        }

        protected void GLPDLLCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (glpDDL.SelectedValue == "")
            {
                args.IsValid = false;
                GLPDLLCustomValidator.ErrorMessage = "GLP-1 medication field cannot be empty";
                if (!alreadyFocus)
                {
                    glpDDL.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            GLPDLLCustomValidator.ErrorMessage = "";
            args.IsValid = true;
        }

        protected void BBDLLCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (bbDDL.SelectedValue == "")
            {
                args.IsValid = false;
                BBDDLCustomValidator.ErrorMessage = "Beta Blocker medication field cannot be empty";
                if (!alreadyFocus)
                {
                    bbDDL.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            BBDDLCustomValidator.ErrorMessage = "";
            args.IsValid = true;
        }

        protected void CCBDLLCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ccbDDL.SelectedValue == "")
            {
                args.IsValid = false;
                CCBDDLCustomValidator.ErrorMessage = "Calcium Channel Blocker medication field cannot be empty";
                if (!alreadyFocus)
                {
                    ccbDDL.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            CCBDDLCustomValidator.ErrorMessage = "";
            args.IsValid = true;
        }
    }
}