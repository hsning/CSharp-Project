<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/UnselectedTestDemo.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="AddDemo.aspx.cs" Inherits="Application.Pharmacist.AddDemo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="title">
        <asp:Label ID="Label1" runat="server" Text="View/Modify Demographic" CssClass="pageTitle"></asp:Label>
        <hr />
        <asp:Label runat="server" ID="StatusLabel" Font-Size="26px"></asp:Label>
        <asp:ValidationSummary ID="ValidationSummary" runat="server" CssClass="redList" HeaderText="Please correct the following:" ValidationGroup="vg"  ForeColor="red" ShowMessageBox="true" ShowSummary="False" />
    </div>
    
    
    <asp:Panel ID="DemoPanel" runat="server">
        <fieldset>
            <legend>Demographics</legend>
            <div class="DemographicSection">
                <div class="Labels">
                    Date of Birth
                </div>
                <div class="Controls TextboxDiv">
                    <div>
                        <asp:TextBox ID="BirthdateTextBox" runat="server" TextMode="Date" TabIndex="2"></asp:TextBox>&nbsp
                        <br />
                        <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="" ValidationGroup="vg" OnServerValidate="CustomValidator1_ServerValidate1" ForeColor="Red"></asp:CustomValidator>
                    </div>
                </div>
            </div>
           
            <div class="DemographicSection" style="padding-bottom: 0px;">
                <div class="Labels">
                    Race (check all that apply)
                </div>
                <div class="Controls">
                    <asp:CheckBoxList ID="RaceCheckBoxList" runat="server" ValidationGroup="checkboxlist" AutoPostBack="True" OnSelectedIndexChanged="CheckBoxList2_SelectedIndexChanged" TabIndex="4">
                        <asp:ListItem Value="race_1">White (Caucasian)</asp:ListItem>
                        <asp:ListItem Value="race_2">First Nations, Inuit or Metis</asp:ListItem>
                        <asp:ListItem Value="race_3">Black / African American</asp:ListItem>
                        <asp:ListItem Value="race_4">Latin American</asp:ListItem>
                        <asp:ListItem Value="race_5">Asian or Pacific Islander</asp:ListItem>
                        <asp:ListItem Value="race_6">South Asian</asp:ListItem>
                        <asp:ListItem Value="race_7">West Asian (Arab)</asp:ListItem>
                        <asp:ListItem Value="race_8">Other:</asp:ListItem>
                    </asp:CheckBoxList>

                </div>

            </div>
            <div class="DemographicSection" style="padding-top: 0px;">
                <div style="margin-top: 0px;"></div>
                <div style="text-align: left;" class="OtherCKB">
                    <asp:Panel runat="server" ID="SpecifyRacePanel" Style="padding-left: 20px;">
                        Please specify
                    <asp:TextBox ID="SpecifyRaceTextBox" runat="server" AutoPostBack="True" TabIndex="5"></asp:TextBox>
                        <br />
                        <asp:CustomValidator ID="SpecifyCV" runat="server" ForeColor="Red" ErrorMessage="" ValidationGroup="vg" OnServerValidate="SpecifyCV_ServerValidate"></asp:CustomValidator>
                    </asp:Panel>
                    <div>
                        <asp:CheckBox ID="CheckBox10" runat="server" Text="Not answered" AutoPostBack="true" OnCheckedChanged="CheckBox10_CheckedChanged" TabIndex="6" />
                    </div>
                    <asp:CustomValidator ID="RaceCustomValidator" runat="server" ErrorMessage="" ValidationGroup="vg" ForeColor="Red" OnServerValidate="CustomValidator5_ServerValidate"></asp:CustomValidator>
                </div>
            </div>
            <div class="DemographicSection">
                <div class="Labels">
                    Sex
                </div>
                <div class="Controls">
                    <div>
                        <asp:RadioButtonList ID="SexRadioButtonList" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" TabIndex="7">
                            <asp:ListItem Value="1">Male</asp:ListItem>
                            <asp:ListItem Value="2">Female</asp:ListItem>
                        </asp:RadioButtonList>
                        <div style="padding-left: 15px;">
                            <asp:CustomValidator ID="SexCustomValidator" runat="server" ErrorMessage="" ForeColor="Red" ValidationGroup="vg" OnServerValidate="SexCustomValidator_ServerValidate"></asp:CustomValidator>
                        </div>
                    </div>
                </div>
            </div>
            <div class="DemographicSection">
                <div class="Labels">
                    Height
                </div>
                <div class="Controls TextboxDiv">
                    <div>
                        <asp:TextBox ID="HeightTextBox" runat="server" AutoPostBack="True" OnTextChanged="HeightTextBox_TextChanged" ValidationGroup="vg" TextMode="Number" TabIndex="8"></asp:TextBox>&nbspcm&nbsp
                    <br />
                        <div>
                            <asp:CustomValidator runat="server" ID="HeightCustomValidator" ValidationGroup="vg" ErrorMessage="" OnServerValidate="CustomValidator1_ServerValidate" ForeColor="Red"></asp:CustomValidator>
                        </div>
                    </div>
                </div>
            </div>
            <div class="DemographicSection">
                <div class="Labels">
                    Weight
                </div>
                <div class="Controls TextboxDiv">
                    <div>
                        <asp:TextBox ID="WeightTextBox" runat="server" AutoPostBack="True" OnTextChanged="WeightTextBox_TextChanged" ValidationGroup="vg" TextMode="Number" TabIndex="9"></asp:TextBox>&nbspKg&nbsp
                    <br />
                        <div>
                            <asp:CustomValidator ID="WeightCustomValidator" runat="server" ErrorMessage="" ValidationGroup="vg" OnServerValidate="WeightCustomValidator_ServerValidate" ForeColor="Red"></asp:CustomValidator>
                        </div>
                    </div>
                </div>
            </div>
            <div class="DemographicSection">
                <div class="Labels">
                    BMI
                </div>
                <div class="Controls TextboxDiv">
                    <asp:TextBox ID="BMITextbox" runat="server" ReadOnly="true" BackColor="#cccccc" TextMode="Number"></asp:TextBox>
                </div>
            </div>
            <div class="DemographicSection">
                <div class="Labels">
                    Blood pressure
                </div>
                <div class="Controls TextboxDiv" style="display: block">
                    <div style="text-align: left;">
                        <asp:TextBox ID="SystolicTextBox" runat="server" placeholder="systolic" TextMode="Number" AutoPostBack="True" TabIndex="10"></asp:TextBox>
                        / 
                  <asp:TextBox ID="DiastolicTextBox" runat="server" placeholder="diastolic" TextMode="Number" AutoPostBack="True" TabIndex="11"></asp:TextBox>
                        mmHg
                        <br />
                        <asp:CustomValidator ID="DiastolicCustomValidator" runat="server" ErrorMessage="" ValidationGroup="vg" OnServerValidate="BloodPressureCustomValidator_ServerValidate" ForeColor="red"></asp:CustomValidator>
                    </div>
                    <asp:Panel runat="server" ID="underBPPanel" Visible="false">
                        <div style="padding-left: 2px;"><span style="font-style: italic;">systolic&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp diastolic</span></div>
                    </asp:Panel>
                </div>
            </div>
        </fieldset>

    </asp:Panel>
    <asp:Panel ID="RiskFactorsPanel" runat="server">
        <fieldset>
            <legend>Comorbidities/Risk Factors</legend>
            <div class="RFSection">
                <div class="Labels">
                    Diabetes
                </div>
                <div class="Controls">
                    <div>
                        <asp:RadioButtonList ID="DiabetesRadioButtonList" runat="server" RepeatDirection="Horizontal" TabIndex="12" AutoPostBack="True" ClientIDMode="Static">
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="2">No</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div></div>
                <div>
                    <asp:Panel ID="YearDiagnosedPanel" runat="server" CssClass="Controls TextboxDiv">
                        <div style="padding-left: 18px;">
                            If YES<br />
                            Year diagnosed
                        <asp:TextBox ID="YearDiagnosedTextBox" runat="server" TextMode="Number" AutoPostBack="True" TabIndex="13"></asp:TextBox>
                            <br />
                            <asp:CustomValidator ID="YearDiagnosedCustomValidator" runat="server" ErrorMessage="" ValidationGroup="vg" ForeColor="red" OnServerValidate="YearDiagnosed_ServerValidate"></asp:CustomValidator>
                        </div>
                    </asp:Panel>
                    <div class="RFCV">
                        <asp:CustomValidator ID="DiabetesRequiredCustomValidator" runat="server" ErrorMessage="" ForeColor="Red" ValidationGroup="vg" OnServerValidate="DiabetesRequiredCustomValidator_ServerValidate"></asp:CustomValidator>
                    </div>
                </div>

            </div>

            <div class="RFSection">
                <div class="Labels">
                    Current smoker
                </div>
                <div class="Controls">
                    <div>
                        <asp:RadioButtonList ID="SmokingRadioButtonList" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="SmokingRadioButtonList_SelectedIndexChanged" AutoPostBack="True" TabIndex="14">
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="2">No</asp:ListItem>
                            <asp:ListItem Value="3">Never</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div></div>
                <div>
                    <asp:Panel ID="SmokingPanel" runat="server" CssClass="Controls TextboxDiv">
                        <div style="padding-left: 18px;">
                            If NO<br />
                            Year stopped 
                        <asp:TextBox ID="YearStoppedTextBox" runat="server" TextMode="Number" AutoPostBack="True" TabIndex="15"></asp:TextBox>
                            <br />
                            <asp:CustomValidator ID="YeatStoppedCustomValidator" ValidationGroup="vg" runat="server" ErrorMessage="" OnServerValidate="CustomValidator3_ServerValidate" ForeColor="red"></asp:CustomValidator>
                        </div>
                    </asp:Panel>
                    <div class="RFCV">
                        <asp:CustomValidator ID="SmokeCustomValidator" runat="server" ErrorMessage="" ForeColor="Red" OnServerValidate="SmokeCustomValidator_ServerValidate" ValidationGroup="vg"></asp:CustomValidator>
                    </div>
                </div>
            </div>

            <div class="RFSection">
                <div class="Labels">
                    Alcohol consumption
                </div>
                <div class="Controls">
                    <div>
                        <asp:RadioButtonList ID="AlcoholRadioButtonList" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="AlcoholRadioButtonList_SelectedIndexChanged" AutoPostBack="True" TabIndex="16">
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="2">No</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div>
                </div>
                <asp:Panel ID="AlcoholPanel" runat="server" CssClass="Controls TextboxDiv">
                    <div style="padding-left: 20px;">
                        If YES<br />
                        <asp:TextBox ID="DrinksTextBox" runat="server" TextMode="Number" AutoPostBack="True" TabIndex="17"></asp:TextBox>&nbspdrinks per week
                    <br />
                        <asp:CustomValidator ID="DrinksCustomValidator" runat="server" ErrorMessage="" ValidationGroup="vg" OnServerValidate="CustomValidator4_ServerValidate" ForeColor="Red"></asp:CustomValidator>
                    </div>
                </asp:Panel>
                <div class="RFCV">
                    <asp:CustomValidator ID="AlcoholCustomValidator" runat="server" ErrorMessage="" ForeColor="Red" OnServerValidate="AlcoholCustomValidator_ServerValidate" ValidationGroup="vg"></asp:CustomValidator>
                </div>
            </div>

            <div class="RFSection">
                <div class="Labels">
                    Recreational drugs
                </div>
                <div class="Controls">
                    <div>
                        <asp:RadioButtonList ID="DrugsRadioButtonList" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" TabIndex="18">
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="2">No</asp:ListItem>
                        </asp:RadioButtonList>
                        <div style="padding-left: 15px;">
                            <asp:CustomValidator ID="DrugsCustomValidator" runat="server" ErrorMessage="" ForeColor="Red" OnServerValidate="DrugsCustomValidator_ServerValidate" ValidationGroup="vg"></asp:CustomValidator>
                        </div>
                    </div>
                </div>
            </div>
            <div class="RFSection">
                <div class="Labels">
                    Other
                </div>
                <div class="Controls">
                    <div>
                        <asp:RadioButtonList ID="OtherRadioButtonList" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="OtherRadioButtonList_SelectedIndexChanged" AutoPostBack="True" TabIndex="19">
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="2">No</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div>
                </div>
                <asp:Panel ID="SpecifyOtherPanel" CssClass="Controls TextboxDiv" runat="server">
                    <div style="padding-left: 20px;">
                        If YES,<br />
                        specify
                        <asp:TextBox ID="OtherTextBox" runat="server" AutoPostBack="True" TabIndex="20"></asp:TextBox>
                        <br />
                        <asp:CustomValidator ID="CustomValidator2" runat="server" ErrorMessage="" ValidationGroup="vg" ForeColor="Red" OnServerValidate="CustomValidator2_ServerValidate"></asp:CustomValidator>
                    </div>
                </asp:Panel>
                <div class="RFCV">
                    <asp:CustomValidator ID="OtherCustomValidator" runat="server" ErrorMessage="" ForeColor="Red" OnServerValidate="OtherCustomValidator_ServerValidate" ValidationGroup="vg"></asp:CustomValidator>
                </div>
            </div>

        </fieldset>
        <div class="submitDiv">
            <div>
            </div>
            <div class="sectionDiv" style="padding-left: 4px;">
                <div>
                    <asp:Label ID="SubmitLabel" runat="server"></asp:Label>
                </div>
                <div>
                    <asp:Button ID="SubmitBtn" runat="server" OnClick="SubmitBtn_Click" Text="Submit" TabIndex="21" ValidationGroup="vg" CausesValidation="False" />
                </div>
                
            </div>
        </div>
    </asp:Panel>
    <div style="margin-top:30px;">
        <hr />
    </div>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:LocalDatabaseConnectionString %>" SelectCommand="SELECT  [study_id], [birth_date], [race_1], [race_2], [race_3], [race_4], [race_5], [race_6], [race_7], [race_8], [race_9], [race_other], [sex], [height], [weight], [bmi], [systolic], [diastolic], [diabetes], [diabetes_year], [smoker], [stop_year], [alcohol], [drinks], [drugs], [other], [other_specify] FROM [Studies] WHERE ([study_id] = @id)">
        <SelectParameters>
            <asp:SessionParameter Name="id" SessionField="study_id" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
