using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.App_Code.BLL;
using System.Drawing;
using System.Data;


namespace Application.Pharmacist
{
    public partial class AddDemo : System.Web.UI.Page
    {
        static StudyManager studyManager = new StudyManager();
        static ConsentManager consentManager = new ConsentManager();
        static User user = new User();
        private Dictionary<string, object> result = new Dictionary<string, object>
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
        private double Height;
        private double Weight;
        private static double BMI;
        static bool Submitted = false;
        private static bool edit = false;
        private static string username;
        private static bool alreadyFocus = false;
        private static bool bdFocus = false;
        private string demoID;
        private static bool MsgBox = false;


        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = "~/Masters/SelectedTestDemo.Master";
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            var onBlurScript = Page.ClientScript.GetPostBackEventReference(BirthdateTextBox, "OnBlur");
            BirthdateTextBox.Attributes.Add("onblur", onBlurScript);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] == null)
                Response.Redirect("~/Error.aspx");
            if (Session["role_id"].ToString() != "APEMDASDWE")
                Response.Redirect("~/Error.aspx");
            if (!IsPostBack)
            {
                if (Session["NewlyCreatedDemo"] != null && Session["demographic_id"] != null)
                {
                    if (Convert.ToBoolean(Session["NewlyCreatedDemo"]))
                    {
                        StatusLabel.Text = "Success. A new demographic was successfully added to the database. The demographic id is: " + Session["demographic_id"].ToString();
                    }
                }
                Session["NewlyCreatedDemo"] = false;
                bdFocus = false;
                alreadyFocus = false;
                Submitted = false;
                SubmitLabel.Text = "";
                ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
                DemoPanel.Visible = false;
                SpecifyRacePanel.Visible = false;
                RiskFactorsPanel.Visible = false;
                YearDiagnosedPanel.Visible = false;
                SmokingPanel.Visible = false;
                AlcoholPanel.Visible = false;
                SpecifyOtherPanel.Visible = false;
                edit = false;
                if (Session["study_id"] != null)
                {
                    Label1.Text = "Modify demographic";
                    edit = true;
                    FillData();
                }
            }

            if (Page.IsPostBack)
            {
                alreadyFocus = false;
                SubmitLabel.Text = "";
                var ctrlName = Request.Params[Page.postEventSourceID];
                var args = Request.Params[Page.postEventArgumentID];
                Check();
                HandleCustomPostbackEvent(ctrlName, args);
                SetFocusAfterPostBack();
                if (Submitted)
                    Page.Validate("vg");
            }
        }
        private void HandleCustomPostbackEvent(string ctrlName, string args)
        {
            //Since this will get called for every postback, we only
            // want to handle a specific combination of control
            // and argument.
            // Check();
            //if (Submitted)
            //    Page.Validate("vg");
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


        //protected void DiabetesRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Check();
        //    if (Submitted)
        //        Page.Validate("vg");
        //}

        protected void SmokingRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Check();
            if (Submitted)
                Page.Validate("vg");
        }

        protected void AlcoholRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Check();
            if (Submitted)
                Page.Validate("vg");
        }

        protected void OtherRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Check();
            if (Submitted)
                Page.Validate("vg");
        }

        protected void CheckBox10_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox10.Checked)
            {
                foreach (ListItem item in RaceCheckBoxList.Items)
                    if (item.Selected)
                        item.Selected = false;
                SpecifyRacePanel.Visible = false;
                SpecifyRaceTextBox.Text = "";
            }
        }

        protected void CheckBox_Change(object sender, EventArgs e)
        {
            return;
        }
        protected void Check()
        {
            if (DiabetesRadioButtonList.SelectedValue == "1")
                YearDiagnosedPanel.Visible = true;
            else if (DiabetesRadioButtonList.SelectedValue == "2")
            {
                YearDiagnosedPanel.Visible = false;
                YearDiagnosedTextBox.Text = "";
            }

            if (OtherRadioButtonList.SelectedValue == "1")
                SpecifyOtherPanel.Visible = true;
            else if (OtherRadioButtonList.SelectedValue == "2")
            {
                SpecifyOtherPanel.Visible = false;
                OtherTextBox.Text = "";
            }
            if (AlcoholRadioButtonList.SelectedValue == "1")
                AlcoholPanel.Visible = true;
            else if (AlcoholRadioButtonList.SelectedValue == "2")
            {
                AlcoholPanel.Visible = false;
                DrinksTextBox.Text = "";
            }

            if (SmokingRadioButtonList.SelectedValue == "2")
                SmokingPanel.Visible = true;
            else
            {
                SmokingPanel.Visible = false;
                YearStoppedTextBox.Text = "";
            }
            foreach (ListItem item in RaceCheckBoxList.Items)
            {
                if (item.Value == "race_8")
                    if (item.Selected)
                    {
                        SpecifyRacePanel.Visible = true;
                    }
                    else
                    {
                        SpecifyRacePanel.Visible = false;
                    }
            }
        }
        protected void CheckBoxList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListItem item in RaceCheckBoxList.Items)
            {
                if (item.Selected)
                    CheckBox10.Checked = false;
                if (item.Value == "race_8")
                    if (item.Selected)
                    {
                        SpecifyRacePanel.Visible = true;
                        CheckBox10.Checked = false;
                    }
                    else
                    {
                        SpecifyRacePanel.Visible = false;
                        SpecifyRaceTextBox.Text = "";
                    }
            }
        }

        protected void HeightTextBox_TextChanged(object sender, EventArgs e)
        {
            CalculateBMI();
        }

        protected void WeightTextBox_TextChanged(object sender, EventArgs e)
        {
            CalculateBMI();
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
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
            HeightCustomValidator.ErrorMessage = "";
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
                WeightCustomValidator.ErrorMessage = "Invalid value. Weight must be a numeric value between 50 and 400kg";
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

        protected void CustomValidator1_ServerValidate1(object source, ServerValidateEventArgs args)
        {
            DateTime datetime;
            if (BirthdateTextBox.Text == "")
            {
                args.IsValid = false;
                CustomValidator1.ErrorMessage = "Birth-date field cannot be empty";
                if (!alreadyFocus && !bdFocus)
                {
                    BirthdateTextBox.Focus();
                    alreadyFocus = true;
                    bdFocus = true;
                }
                return;
            }
            bool isDatetime = DateTime.TryParse(BirthdateTextBox.Text, out datetime);
            if (!isDatetime)
            {
                args.IsValid = false;
                CustomValidator1.ErrorMessage = "Birth-date: invalid entry";
                if (!alreadyFocus && !bdFocus)
                {
                    BirthdateTextBox.Focus();
                    alreadyFocus = true;
                    bdFocus = true;
                }
                return;
            }
            if (datetime > DateTime.Now || datetime < new DateTime(1900, 01, 01))
            {
                args.IsValid = false;
                CustomValidator1.ErrorMessage = "Birth-date: invalid entry";
                if (!alreadyFocus && !bdFocus)
                {
                    BirthdateTextBox.Focus();
                    alreadyFocus = true;
                    bdFocus = true;
                }
                return;
            }
            CustomValidator1.ErrorMessage = "";
            args.IsValid = true;
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void SubmitBtn_Click(object sender, EventArgs e)
        {
            bool submitResult;
            Submitted = true;
            Page.Validate("vg");
            if (Page.IsValid)
            {
                submitResult = Submit();
                if (edit && submitResult)
                {
                    SubmitLabel.ForeColor = Color.Green;
                    SubmitLabel.Text = "Success. Demographic ";
                    SubmitLabel.Text += result["id"] + " has been successfully updated.";
                }
                else if (!edit && submitResult)
                {
                    Session["demographic_id"] = demoID;
                    Session["NewlyCreatedDemo"] = true;
                    Response.Redirect("~/AddDemo.aspx");
                }
                else if (!edit && !submitResult)
                {
                    SubmitLabel.ForeColor = Color.Red;
                    SubmitLabel.Text = "Update failed.";
                }
                else
                {
                    SubmitLabel.ForeColor = Color.Red;
                    SubmitLabel.Text = "Add new demographic failed.";
                }
            }
            else
            {
                string message = "There is more than 1 error on this form.";
                Response.Write("<script type='text/javascript'> alert('" + message + "') </script>");
            }
        }

        protected void BloodPressureCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
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

            int diastolic;
            bool isIntDia = int.TryParse(DiastolicTextBox.Text, out diastolic);
            int systolic;
            bool isIntSys = int.TryParse(SystolicTextBox.Text, out systolic);

            if (!isIntSys || systolic < 40 || systolic > 360)
            {
                DiastolicCustomValidator.ErrorMessage = "One of the blood pressures entered contains invalid value";
                args.IsValid = false;
                if (!alreadyFocus)
                {
                    SystolicTextBox.Focus();
                    alreadyFocus = true;
                }
            }
            if (!isIntDia || diastolic < 40 || diastolic > 360)
            {
                DiastolicCustomValidator.ErrorMessage = "One of the blood pressures entered contains invalid value";
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

        protected void YearDiagnosed_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (YearDiagnosedTextBox.Text == "")
            {
                args.IsValid = false;
                YearDiagnosedCustomValidator.ErrorMessage = "Year diagnosed field cannot be empty";
                if (!alreadyFocus)
                {
                    YearDiagnosedTextBox.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            DateTime birthdate;
            DateTime.TryParse(BirthdateTextBox.Text, out birthdate);
            int year;
            bool isInt = int.TryParse(YearDiagnosedTextBox.Text, out year);
            if (!isInt)
            {
                YearDiagnosedCustomValidator.ErrorMessage = "Year diagnosed: invalid year";
                args.IsValid = false;
                if (!alreadyFocus)
                {
                    YearDiagnosedTextBox.Focus();
                    alreadyFocus = true;
                }
                return;
            }

            if (year < birthdate.Year || year > DateTime.Today.Year || year < 1900)
            {
                YearDiagnosedCustomValidator.ErrorMessage = "Year diagnosed: invalid year";
                args.IsValid = false;
                if (!alreadyFocus)
                {
                    YearDiagnosedTextBox.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            YearDiagnosedCustomValidator.ErrorMessage = "";
            args.IsValid = true;
        }

        protected void CustomValidator4_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (DrinksTextBox.Text == "")
            {
                DrinksCustomValidator.ErrorMessage = "Drinks field cannot be empty";
                args.IsValid = false;
                if (!alreadyFocus)
                {
                    DrinksTextBox.Focus();
                    alreadyFocus = true;
                }
                return;
            }

            DrinksCustomValidator.ErrorMessage = "";
            args.IsValid = true;
        }

        protected void CustomValidator3_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (YearStoppedTextBox.Text == "")
            {
                args.IsValid = false;
                YeatStoppedCustomValidator.ErrorMessage = "Year stopped cannot be empty";
                if (!alreadyFocus)
                {
                    YearStoppedTextBox.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            DateTime birthdate;
            DateTime.TryParse(BirthdateTextBox.Text, out birthdate);
            int year;
            bool isInt = int.TryParse(YearStoppedTextBox.Text, out year);
            if (!isInt)
            {
                YeatStoppedCustomValidator.ErrorMessage = "Year stopped: invalid entry";
                args.IsValid = false;
                if (!alreadyFocus)
                {
                    YearStoppedTextBox.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            if (year < birthdate.Year || year > DateTime.Today.Year || year < 1900)
            {
                YeatStoppedCustomValidator.ErrorMessage = "Year stopped: invalid entry";
                args.IsValid = false;
                if (!alreadyFocus)
                {
                    YearStoppedTextBox.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            YeatStoppedCustomValidator.ErrorMessage = "";
            args.IsValid = true;
        }

        protected void CustomValidator5_ServerValidate(object source, ServerValidateEventArgs args)
        {
            bool checkboxlistchecked = false;
            foreach (ListItem item in RaceCheckBoxList.Items)
            {
                if (item.Selected)
                    checkboxlistchecked = true;
            }
            if (!CheckBox10.Checked && !checkboxlistchecked)
            {
                args.IsValid = false;
                RaceCustomValidator.ErrorMessage = "Race: at least 1 checkbox should be checked";
                if (!alreadyFocus)
                {
                    RaceCheckBoxList.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            RaceCustomValidator.ErrorMessage = "";
            args.IsValid = true;
        }

        protected void SexCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (SexRadioButtonList.SelectedValue == "")
            {
                args.IsValid = false;
                SexCustomValidator.ErrorMessage = "Sex field cannot be empty";
                if (!alreadyFocus)
                {
                    SexRadioButtonList.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            SexCustomValidator.ErrorMessage = "";
            args.IsValid = true;
        }



        protected void DiabetesRequiredCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (DiabetesRadioButtonList.SelectedValue == "")
            {
                DiabetesRequiredCustomValidator.ErrorMessage = "Diabetes field cannot be empty";
                args.IsValid = false;
                if (!alreadyFocus)
                {
                    DiabetesRadioButtonList.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            DiabetesRequiredCustomValidator.ErrorMessage = "";
            args.IsValid = true;
        }

        protected void SmokeCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (SmokingRadioButtonList.SelectedValue == "")
            {
                SmokeCustomValidator.ErrorMessage = "Current smoker field cannot be empty";
                args.IsValid = false;
                if (!alreadyFocus)
                {
                    SmokingRadioButtonList.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            SmokeCustomValidator.ErrorMessage = "";
            args.IsValid = true;
        }

        protected void AlcoholCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (AlcoholRadioButtonList.SelectedValue == "")
            {
                AlcoholCustomValidator.ErrorMessage = "Alcohol consumption field cannot be empty";
                args.IsValid = false;
                if (!alreadyFocus)
                {
                    AlcoholRadioButtonList.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            AlcoholCustomValidator.ErrorMessage = "";
            args.IsValid = true;
        }

        protected void DrugsCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (DrugsRadioButtonList.SelectedValue == "")
            {
                DrugsCustomValidator.ErrorMessage = "Recreational drugs field cannot be empty";
                args.IsValid = false;
                if (!alreadyFocus)
                {
                    DrugsRadioButtonList.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            DrugsCustomValidator.ErrorMessage = "";
            args.IsValid = true;
        }

        protected void OtherCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (OtherRadioButtonList.SelectedValue == "")
            {
                OtherCustomValidator.ErrorMessage = "Other comorbidities/risk factors field cannot be empty";
                args.IsValid = false;
                if (!alreadyFocus)
                {
                    OtherRadioButtonList.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            OtherCustomValidator.ErrorMessage = "";
            args.IsValid = true;
        }

        private bool Submit()
        {
            bool postResult;
            CalculateBMI();
            result["consent"] = "1";
            result["birth_date"] = BirthdateTextBox.Text;
            foreach (ListItem item in RaceCheckBoxList.Items)
                result[item.Value] = item.Selected;
            result["race_9"] = SpecifyRaceTextBox.Text == "" ? null : SpecifyRaceTextBox.Text;
            result["race_10"] = CheckBox10.Checked;
            result["sex"] = SexRadioButtonList.SelectedValue;
            result["height"] = HeightTextBox.Text;
            result["weight"] = WeightTextBox.Text;
            result["bmi"] = BMI;
            result["systolic"] = SystolicTextBox.Text;
            result["diastolic"] = DiastolicTextBox.Text;
            result["diabetes"] = Convert.ToInt32(DiabetesRadioButtonList.SelectedValue);
            result["diabetes_year"] = string.IsNullOrEmpty(YearDiagnosedTextBox.Text) ? null : YearDiagnosedTextBox.Text;
            result["smoker"] = Convert.ToInt32(SmokingRadioButtonList.SelectedValue);
            result["stop_year"] = string.IsNullOrEmpty(YearStoppedTextBox.Text) ? null : YearStoppedTextBox.Text;
            result["alcohol"] = Convert.ToInt32(AlcoholRadioButtonList.SelectedValue);
            result["drinks"] = string.IsNullOrEmpty(DrinksTextBox.Text) ? null : DrinksTextBox.Text;
            result["drugs"] = Convert.ToInt32(DrugsRadioButtonList.SelectedValue);
            result["other"] = Convert.ToInt32(OtherRadioButtonList.SelectedValue);
            result["other_specify"] = string.IsNullOrEmpty(OtherTextBox.Text) ? null : OtherTextBox.Text;
            result["id"] = demoID;
            result["region_code"] = Session["site_id"];
            result["changed_by"] = Session["id"];
            result["changed_on"] = DateTime.Now;
            if (edit)
            {
                result["id"] = username;
                postResult = studyManager.UpdateVisit(result);
            }
            else
            {
                postResult = studyManager.UpdateVisit(result);
            }
            return postResult;
        }
        private void CalculateBMI()
        {
            Double.TryParse(HeightTextBox.Text, out Height);
            Double.TryParse(WeightTextBox.Text, out Weight);
            if (Height != null && Weight != null)
            {
                BMI = Weight / (Math.Pow(Height / 100.0, 2));
                BMITextbox.Text = string.Format("{0:F1}", BMI);
            }
        }

        protected void FillData()
        {
            if(!user.getDemoAdded(studyManager.GetPatientIDByStudyID(Convert.ToInt32(Session["study_id"]))))
            {
                Label1.Text += " (Study ID: " + Session["study_id"].ToString() + ")";
                StatusLabel.Text = "This patient has not filled up his/her demographic yet.";
                return;
            }
            SqlDataSource2.DataBind();
            DataView dv = (DataView)SqlDataSource2.Select(DataSourceSelectArguments.Empty);
            
            DataTable dt = dv.ToTable();
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            int i = 2;
            if (!DemoPanel.Visible)
                DemoPanel.Visible = true;
            if (!RiskFactorsPanel.Visible)
                RiskFactorsPanel.Visible = true;
            foreach (DataRow dr in dv.Table.Rows)
            {
                username = dr[0].ToString();
                BirthdateTextBox.Text = Convert.ToDateTime(dr[1]).Date.ToString("yyyy-MM-dd");
                foreach (ListItem item in RaceCheckBoxList.Items)
                {
                    item.Selected = Convert.ToBoolean(dr[i]);
                    i++;
                }
                if (RaceCheckBoxList.Items[5].Selected)
                    SpecifyRaceTextBox.Text = dr[11] == System.DBNull.Value ? "" : dr[11].ToString();
                else
                    SpecifyRaceTextBox.Text = dr[11] == System.DBNull.Value ? "" : dr[11].ToString();
                CheckBox10.Checked = Convert.ToBoolean(dr[10]);
                SexRadioButtonList.SelectedValue = dr["sex"].ToString();
                HeightTextBox.Text = dr[13] == System.DBNull.Value ? "" : dr[13].ToString();
                WeightTextBox.Text = dr[14] == System.DBNull.Value ? "" : dr[14].ToString();
                BMITextbox.Text = dr[15] == System.DBNull.Value ? "" : dr[15].ToString();
                SystolicTextBox.Text = dr[16] == System.DBNull.Value ? "" : dr[16].ToString();
                DiastolicTextBox.Text = dr[17] == System.DBNull.Value ? "" : dr[17].ToString();
                DiabetesRadioButtonList.SelectedValue = dr[18].ToString();
                YearDiagnosedTextBox.Text = dr[19].ToString();
                SmokingRadioButtonList.SelectedValue = dr[20].ToString();
                YearStoppedTextBox.Text = dr[21].ToString();
                AlcoholRadioButtonList.SelectedValue = dr[22].ToString();
                DrinksTextBox.Text = dr[23].ToString();
                DrugsRadioButtonList.SelectedValue = dr[24].ToString();
                OtherRadioButtonList.SelectedValue = dr[25].ToString();
                OtherTextBox.Text = dr[26].ToString();
                edit = true;
                underBPPanel.Visible = true;
                SubmitBtn.Text = "Save Changes";
                Check();
            }
        }

        protected void UnselectButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Test.aspx");
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Detail")
            {
                Response.Redirect("FollowupForm.aspx?visitid=" + e.CommandArgument);
            }
        }

        protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (OtherTextBox.Text == "")
            {
                args.IsValid = false;
                CustomValidator2.ErrorMessage = "Other comorbidities/risk factors field cannot be empty";
                if (!alreadyFocus)
                {
                    OtherTextBox.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            CustomValidator2.ErrorMessage = "";
            args.IsValid = true;
        }

        protected void SpecifyCV_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (SpecifyRaceTextBox.Text == "")
            {
                args.IsValid = false;
                SpecifyCV.ErrorMessage = "Race field cannot be empty";
                if (!alreadyFocus)
                {
                    SpecifyRaceTextBox.Focus();
                    alreadyFocus = true;
                }
                return;
            }
            SpecifyCV.ErrorMessage = "";
            args.IsValid = true;
        }
    }
}