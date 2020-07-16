<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/SelectedTestDemo.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="AddFUP.aspx.cs" Inherits="Application.Pharmacist.AddFUP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Text="Add New Visits" CssClass="pageTitle"></asp:Label>
        <asp:Label ID="VisitLabel" runat="server" Text=""></asp:Label>
        <hr />
        <asp:Label runat="server" ID="TopStatusLabel" ForeColor="Green" Font-Size="26px"></asp:Label>
    </div>
    <asp:Panel ID="DemoPanel" runat="server" Visible="false">
        <fieldset>
            <legend>Visit Information</legend>
            <div class="visitForm">
                <div class="formSection" style="margin-top: 0px;">
                    <div>
                        <div class="sectionDiv">
                            Date of Visit
                        </div>
                        <div>
                            <asp:TextBox ID="VisitDateTextBox" runat="server" TextMode="Date" TabIndex="2"></asp:TextBox>      
                        </div>
                        <div>
                            <asp:CustomValidator ForeColor="Red" ID="VisitDateCustomValidator" runat="server" ErrorMessage="" ValidationGroup="vg" OnServerValidate="VisitDateCustomValidator_ServerValidate"></asp:CustomValidator>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="vg" />
                        </div>
                        <div class="sectionDiv">
                            Length of Visit:
                        </div>
                        <div>
                            <asp:TextBox ID="VisitLengthTextBox" runat="server" TextMode="Number" AutoPostBack="True" TabIndex="3"></asp:TextBox>
                            minutes
                        </div>
                        <div>
                            <asp:CustomValidator ForeColor="Red" ID="VisitLengthCustomValidator" runat="server" ErrorMessage="" ValidationGroup="vg" OnServerValidate="VisitLengthCustomValidator_ServerValidate"></asp:CustomValidator>
                        </div>
                    </div>
                </div>
            </div>
        </fieldset>
        <fieldset>
            <legend>Physical and Laboratory Assessment</legend>
            <div class="visitForm">


                <div class="formSection" style="margin-top: 0px;">
                    <div class="sectionDiv">
                        Weight
                    </div>
                    <div>
                        <asp:TextBox ID="WeightTextBox" runat="server" OnTextChanged="WeightTextBox_TextChanged" AutoPostBack="True" ValidationGroup="vg" TextMode="Number" TabIndex="4"></asp:TextBox>
                        kg 
                    </div>
                    <div>
                        <asp:CustomValidator ID="WeightCustomValidator" runat="server" ErrorMessage="" ValidationGroup="vg" OnServerValidate="WeightCustomValidator_ServerValidate" ForeColor="Red"></asp:CustomValidator>
                    </div>
                    <div class="sectionDiv" style="margin-top: 0px;">
                        Height
                    </div>
                    <div>
                        <asp:TextBox ID="HeightTextBox" runat="server" AutoPostBack="True" ValidationGroup="vg" TextMode="Number" OnTextChanged="HeightTextBox_TextChanged" TabIndex="5"></asp:TextBox>
                        cm   
                    </div>
                    <div>
                        <asp:CustomValidator runat="server" ID="HeightCustomValidator" ValidationGroup="vg" ErrorMessage="" OnServerValidate="HeightCustomValidator_ServerValidate" ForeColor="Red"></asp:CustomValidator>
                    </div>
                    <div class="sectionDiv" style="margin-top: 0px;">
                        BMI
                    </div>
                    <div>
                        <asp:TextBox ID="BMITextbox" runat="server" ReadOnly="true" BackColor="#cccccc" TextMode="Number"></asp:TextBox>
                    </div>
                </div>
                <div class="formSection" style="margin-top: 0px;">
                    <div class="sectionDiv">Smoking</div>
                    <div>
                        <div>
                            <asp:RadioButtonList ID="SmokeRadioButtonList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="SmokeRadioButtonList_SelectedIndexChanged" RepeatDirection="Horizontal" TabIndex="6">
                                <asp:ListItem Value="1">Current</asp:ListItem>
                                <asp:ListItem Value="2">Former</asp:ListItem>
                                <asp:ListItem Value="3">Never</asp:ListItem>
                            </asp:RadioButtonList>


                            <asp:Panel ID="SmokingPanel" runat="server" Visible="false" Style="padding-left: 128px;">
                                If former, date stopped
                        <asp:TextBox ID="StopTextBox" runat="server" TextMode="Date" TabIndex="7"></asp:TextBox>
                                <asp:CustomValidator ForeColor="Red" ID="StopDateCustomValidator" runat="server" ErrorMessage="" ValidationGroup="vg" OnServerValidate="StopDateCustomValidator_ServerValidate"></asp:CustomValidator>
                            </asp:Panel>
                            <asp:CustomValidator CssClass="rdbCV" ForeColor="Red" ID="SmokeCustomValidator" runat="server" ErrorMessage="" ValidationGroup="vg" OnServerValidate="SmokeCustomValidator_ServerValidate"></asp:CustomValidator>
                        </div>
                    </div>
                </div>
                <div class="formSection">
                    <div class="sectionDiv">
                        Blood Pressure
                    </div>
                    <div style="display: block">
                        <div style="text-align: left;">
                            <asp:TextBox ID="SystolicTextBox" runat="server" placeholder="systolic" TextMode="Number" AutoPostBack="True" TabIndex="8"></asp:TextBox>
                            / 
                    <asp:TextBox ID="DiastolicTextBox" runat="server" placeholder="diastolic" TextMode="Number" AutoPostBack="True" TabIndex="9"></asp:TextBox>
                            mmHg
                        </div>
                        <div>
                            <asp:CustomValidator ID="DiastolicCustomValidator" runat="server" ErrorMessage="" ValidationGroup="vg" OnServerValidate="DiastolicValidator_ServerValidate" ForeColor="red"></asp:CustomValidator>
                        </div>
                        <asp:Panel runat="server" ID="underBPPanel" Visible="false">
                            <div style="padding-left: 2px; font-style: italic;">systolic&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp diastolic</div>
                        </asp:Panel>
                        <div style="text-align: left;">
                        </div>
                    </div>
                </div>
                <div class="formSection">
                    <div class="sectionDiv">
                        Blood Glucose
                    </div>
                    <div>
                        <div>
                            <asp:TextBox ID="GlucoseTextBox" TextMode="Number" runat="server" AutoPostBack="True" TabIndex="10"></asp:TextBox>
                            mmol/L
                        </div>
                        <div>
                            <asp:CustomValidator ID="GlucoseCustomValidator" runat="server" ErrorMessage="" ValidationGroup="vg" OnServerValidate="GlucoseValidator_ServerValidate" ForeColor="red"></asp:CustomValidator>
                        </div>
                    </div>
                    <div class="sectionDiv">
                        Test Type
                    </div>
                    <div>
                        <asp:RadioButtonList ID="TestTypeRadioButtonList" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" TabIndex="11">
                            <asp:ListItem Value="1">Random</asp:ListItem>
                            <asp:ListItem Value="2">Fasting</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:CustomValidator CssClass="rdbCV" ID="TestTypeCustomValidator" runat="server" ErrorMessage="" ValidationGroup="vg" OnServerValidate="TestTypeValidator_ServerValidate" ForeColor="red"></asp:CustomValidator>
                    </div>
                </div>
                <asp:PlaceHolder runat="server" ID="DatePlaceHolder">
                    <div class="formSection SpanTwo">
                        <div class="sectionDiv">
                            HbA1c
                        </div>
                        <div class="labAssessment">
                            <div>
                                <asp:TextBox ID="HBA1CTextBox" TabIndex="12" runat="server" TextMode="Number" AutoPostBack="true"></asp:TextBox>
                                %        
                            </div>
                            <div>
                                <asp:TextBox ID="HBA1CDateTextBox" TabIndex="13" runat="server" TextMode="Date"></asp:TextBox>
                                date taken
                    &nbsp&nbsp&nbsp&nbsp&nbsp
                                <asp:CheckBox ID="DateNotAvailableCheckBox" TabIndex="14" runat="server" Text="Date Not Available" OnCheckedChanged="DateNotAvailableCheckBox_CheckedChanged" AutoPostBack="True" />
                            </div>
                        </div>
                        <div class="labAssessment">
                            <div>
                                <asp:CustomValidator ID="HBA1CCustomValidator" runat="server" ErrorMessage="" ValidationGroup="vg" OnServerValidate="HBA1CValidator_ServerValidate" ForeColor="red"></asp:CustomValidator>
                            </div>
                            <div>
                                <asp:CustomValidator ID="HBA1CDateCustomValidator" runat="server" ErrorMessage="" ForeColor="Red" ValidationGroup="vg" OnServerValidate="HBA1CDateValidator_ServerValidate"></asp:CustomValidator>
                            </div>
                        </div>

                        <div class="sectionDiv">
                            Total Cholesterol
                        </div>
                        <div class="labAssessment">
                            <div>
                                <asp:TextBox ID="CholesterolTextBox" TabIndex="15" runat="server" TextMode="Number" AutoPostBack="true"></asp:TextBox>
                                mmol/L  
                                
                            </div>
                            <div>
                                <asp:TextBox ID="CholesterolDateTextBox" TabIndex="16" runat="server" TextMode="Date"></asp:TextBox>
                                date taken
                    
                            </div>
                        </div>
                        <div class="labAssessment">
                            <div>
                                <asp:CustomValidator ID="CholesterolCustomValidator" runat="server" ErrorMessage="" ValidationGroup="vg" OnServerValidate="CholesterolValidator_ServerValidate" ForeColor="red"></asp:CustomValidator>
                            </div>
                            <div>
                                <asp:CustomValidator ID="CholeDateCustomValidator" runat="server" ErrorMessage="" ForeColor="Red" ValidationGroup="vg" OnServerValidate="CholeDateValidator_ServerValidate"></asp:CustomValidator>
                            </div>
                        </div>
                        <div class="sectionDiv">
                            HDL
                        </div>
                        <div class="labAssessment">
                            <div>
                                <asp:TextBox ID="HDLTextBox" TabIndex="17" runat="server" TextMode="Number" AutoPostBack="true"></asp:TextBox>
                                mmol/L            
                            </div>
                            <div>
                                <asp:TextBox ID="HDLDateTextBox" TabIndex="18" runat="server" TextMode="Date"></asp:TextBox>
                                date taken
                            </div>
                        </div>
                        <div class="labAssessment">
                            <div>
                                <asp:CustomValidator ID="HDLCustomValidator" runat="server" ErrorMessage="" ValidationGroup="vg" OnServerValidate="HDLValidator_ServerValidate" ForeColor="red"></asp:CustomValidator>
                            </div>
                            <div>
                                <asp:CustomValidator ID="HDLDateCustomValidator" runat="server" ErrorMessage="" ForeColor="Red" ValidationGroup="vg" OnServerValidate="HDLDateValidator_ServerValidate"></asp:CustomValidator>
                            </div>
                        </div>
                        <div class="sectionDiv">
                            LDL
                        </div>
                        <div class="labAssessment">
                            <div>
                                <asp:TextBox ID="LDLTextBox" TabIndex="19" runat="server" TextMode="Number" AutoPostBack="true"></asp:TextBox>
                                mmol/L      
                    
                            </div>
                            <div>
                                <asp:TextBox ID="LDLDateTextBox" TabIndex="20" runat="server" TextMode="Date"></asp:TextBox>
                                date taken                    
                            </div>
                        </div>
                        <div class="labAssessment">
                            <div>
                                <asp:CustomValidator ID="LDLCustomValidator" runat="server" ErrorMessage="" ValidationGroup="vg" OnServerValidate="LDLValidator_ServerValidate" ForeColor="red"></asp:CustomValidator>
                            </div>
                            <div>
                                <asp:CustomValidator ID="LDLDateCustomValidator" runat="server" ErrorMessage="" ForeColor="Red" ValidationGroup="vg" OnServerValidate="LDLDateValidator_ServerValidate"></asp:CustomValidator>
                            </div>
                        </div>
                    </div>

                </asp:PlaceHolder>
                <div class="formSection SpanTwo">
                    <div class="sectionDiv">
                        Estimated Glomerular Filtration Rate (eGFR)
                    </div>
                    <div class="labAssessment">
                        <div>
                            <asp:TextBox ID="eGFRTextBox" TabIndex="21" runat="server" TextMode="Number" AutoPostBack="True"></asp:TextBox>
                            mL/min/1.73 m&#178
                        </div>
                        <div>
                            <asp:TextBox ID="eGFRDateTextBox" TabIndex="22" runat="server" TextMode="Date"></asp:TextBox>
                            date taken
                        </div>
                    </div>
                    <div class="labAssessment">
                        <div>
                            <asp:CustomValidator ID="egfrCustomValidator" runat="server" ErrorMessage="" ForeColor="Red" ValidationGroup="vg" OnServerValidate="egfrValidator_ServerValidate"></asp:CustomValidator>
                        </div>
                        <div>
                            <asp:CustomValidator ID="egfrDateCustomValidator" runat="server" ErrorMessage="" ForeColor="Red" ValidationGroup="vg" OnServerValidate="egfrDateValidator_ServerValidate"></asp:CustomValidator>
                        </div>
                    </div>
                    <div class="sectionDiv">
                        Random Urine Albumin-Creatinine Ratio (ACR)
                    </div>
                    <div class="labAssessment">
                        <div>
                            <asp:TextBox ID="ACRTextBox" TabIndex="23" runat="server" TextMode="Number" AutoPostBack="True"></asp:TextBox>
                            mg/mmol
                        </div>
                        <div>
                            <asp:TextBox ID="ACRDateTextBox" TabIndex="24" runat="server" TextMode="Date"></asp:TextBox>
                            date taken
                        </div>
                    </div>
                    <div class="labAssessment">
                        <div>
                            <asp:CustomValidator ID="ACRCustomValidator" runat="server" ErrorMessage="" ForeColor="Red" ValidationGroup="vg" OnServerValidate="ACRValidator_ServerValidate"></asp:CustomValidator>
                        </div>
                        <div>
                            <asp:CustomValidator ID="ACRDateCustomValidator" runat="server" ErrorMessage="" ForeColor="Red" ValidationGroup="vg" OnServerValidate="ACRDateValidator_ServerValidate"></asp:CustomValidator>
                        </div>
                    </div>
                </div>
            </div>
        </fieldset>
        <fieldset>
            <legend>Current Medications</legend>
            <div class="visitForm">

                <div class="formSection" style="margin-top: 0px;">
                    <div>
                        <div class="sectionDiv">
                            Is patient on any Anti-hyperglycemics agents?
                        </div>
                        <div>
                            <asp:RadioButtonList ID="AHARadioButtonList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="AHARadioButtonList_SelectedIndexChanged" RepeatDirection="Horizontal" TabIndex="25">
                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                <asp:ListItem Value="2">No</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:CustomValidator CssClass="rdbCV" ID="AHACustomValidator" runat="server" ErrorMessage="" ForeColor="Red" ValidationGroup="vg" OnServerValidate="AHAValidator_ServerValidate"></asp:CustomValidator>
                        </div>
                    </div>
                    <asp:Panel ID="AHAPanel" runat="server" Visible="false" CssClass="indented0">
                        <div style="padding-left: 4px; font-weight: bold;">If Yes, check all that apply</div>
                        <div class="indented0">
                            <asp:CheckBox ID="AcarboseCheckBox" runat="server" Text="Acarbose" AutoPostBack="True" OnCheckedChanged="AcarboseCheckBox_CheckedChanged" TabIndex="26" />
                        </div>
                        <asp:Panel ID="AcarbosePanel" runat="server" Visible="false" CssClass="medicationDose indented1">
                            Total Daily Dose
                    <asp:TextBox ID="AcarboseDoseTextBox" TextMode="Number" runat="server" AutoPostBack="True" TabIndex="27"></asp:TextBox>
                            mg
                    <asp:CustomValidator ID="AcarboseDoseCustomValidator" runat="server" ErrorMessage="" ForeColor="Red" ValidationGroup="vg" OnServerValidate="AcarboseDoseValidator_ServerValidate"></asp:CustomValidator>
                        </asp:Panel>
                        <div class="indented0">
                            <asp:CustomValidator ID="DPPCustomValidator" runat="server" ErrorMessage="" ForeColor="Red" ValidationGroup="vg" OnServerValidate="DPPValidator_ServerValidate"></asp:CustomValidator>
                            <asp:CheckBox ID="DPPCheckBox" runat="server" Text="DPP-4 inhibitor" AutoPostBack="True" OnCheckedChanged="DDPCheckBox_CheckedChanged" TabIndex="28" />
                        </div>
                        <div>
                        </div>
                        <asp:Panel ID="DPPPanel" runat="server" Visible="false" CssClass="indented1">
                            <div>
                                <asp:DropDownList runat="server" ID="dppDDL" AutoPostBack="True" Width="220px" TabIndex="29">
                                    <asp:ListItem Value="">Please select a medication</asp:ListItem>
                                    <asp:ListItem Value="1">Alogliptin</asp:ListItem>
                                    <asp:ListItem Value="2">Linagliptin</asp:ListItem>
                                    <asp:ListItem Value="3">Saxagliptin</asp:ListItem>
                                    <asp:ListItem Value="4">Sitagliptin</asp:ListItem>
                                </asp:DropDownList>
                                <asp:CustomValidator ID="DPPDLLCustomValidator" runat="server" ErrorMessage="" ForeColor="Red" ValidationGroup="vg" OnServerValidate="DPPDLLCustomValidator_ServerValidate"></asp:CustomValidator>
                            </div>

                            <div class="medicationDose">
                                Total Daily Dose
                    <asp:TextBox ID="dppTDDTextBox" runat="server" TextMode="Number" AutoPostBack="True" TabIndex="29"></asp:TextBox>
                                mg
                            <asp:CustomValidator ID="dppTDDCustomValidator" ForeColor="red" runat="server" ErrorMessage="" ValidationGroup="vg" OnServerValidate="dppTDDCustomValidator_ServerValidate"></asp:CustomValidator>
                            </div>
                        </asp:Panel>
                        <div class="indented0">
                            <asp:CustomValidator ID="glpCustomValidator" runat="server" ErrorMessage="" ForeColor="Red" ValidationGroup="vg" OnServerValidate="glpValidator_ServerValidate"></asp:CustomValidator>
                            <asp:CheckBox ID="GLPCheckBox" runat="server" Text="GLP-1 receptor agonist" AutoPostBack="True" OnCheckedChanged="GLPCheckBox_CheckedChanged" TabIndex="30" />
                        </div>
                        <div>
                        </div>
                        <asp:Panel ID="GLPPanel" runat="server" Visible="false" CssClass="indented1">

                            <div>
                                <asp:DropDownList runat="server" ID="glpDDL" AutoPostBack="True" Width="220px" TabIndex="31">
                                    <asp:ListItem Value="">Please select a medication</asp:ListItem>
                                    <asp:ListItem Value="1">Albiglutide</asp:ListItem>
                                    <asp:ListItem Value="2">Bydureon (Exenatide extended release)</asp:ListItem>
                                    <asp:ListItem Value="3">Dulaglutide</asp:ListItem>
                                    <asp:ListItem Value="4">Exenatide</asp:ListItem>
                                    <asp:ListItem Value="5">Liraglutide</asp:ListItem>
                                </asp:DropDownList>
                                <asp:CustomValidator ID="GLPDLLCustomValidator" runat="server" ErrorMessage="" ForeColor="Red" ValidationGroup="vg" OnServerValidate="GLPDLLCustomValidator_ServerValidate"></asp:CustomValidator>
                            </div>

                            <div class="medicationDose">
                                Total Daily Dose
                    <asp:TextBox ID="glpTDDTextBox" runat="server" TextMode="Number" AutoPostBack="True" TabIndex="32"></asp:TextBox>
                                mg
                            <asp:CustomValidator ID="glpTDDCustomValidator" ForeColor="red" runat="server" ErrorMessage="" OnServerValidate="glpTDDCustomValidator_ServerValidate" ValidationGroup="vg"></asp:CustomValidator>
                            </div>
                        </asp:Panel>
                    </asp:Panel>

                </div>
                <div class="formSection" style="margin-top: 0px;">
                    <div>
                        <div class="sectionDiv">
                            Is patient on Hypertension medications?
                        </div>
                        <div>
                            <asp:RadioButtonList ID="HypertensionRadioButtonList" runat="server" OnSelectedIndexChanged="RadioButtonList4_SelectedIndexChanged" AutoPostBack="True" RepeatDirection="Horizontal" TabIndex="33">
                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                <asp:ListItem Value="2">No</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:CustomValidator CssClass="rdbCV" ID="HypertensionCustomValidator" runat="server" ErrorMessage="" ForeColor="Red" ValidationGroup="vg" OnServerValidate="HypertensionValidator_ServerValidate"></asp:CustomValidator>
                        </div>
                    </div>
                    <asp:Panel ID="HypertensionPanel" runat="server" Visible="false" CssClass="indented0">
                        <div style="padding-left: 4px; font-weight: bold;">If Yes, check all that apply</div>
                        <div class="indented0">
                            <asp:CustomValidator ID="BBCustomValidator" runat="server" ErrorMessage="" ForeColor="Red" ValidationGroup="vg" OnServerValidate="BBValidator_ServerValidate"></asp:CustomValidator>
                            <asp:CheckBox ID="BBCheckBox" runat="server" Text="Beta Blocker" AutoPostBack="True" OnCheckedChanged="BBCheckBox_CheckedChanged" TabIndex="34" />
                        </div>
                        <asp:Panel ID="BBPanel" runat="server" Visible="false" CssClass="indented1">
                            <div>
                                <asp:DropDownList runat="server" ID="bbDDL" AutoPostBack="True" Width="220px" TabIndex="35">
                                    <asp:ListItem Value="">Please select a medication</asp:ListItem>
                                    <asp:ListItem Value="1">Acebutolol</asp:ListItem>
                                    <asp:ListItem Value="2">Atenolol</asp:ListItem>
                                    <asp:ListItem Value="3">Bisoprolol</asp:ListItem>
                                    <asp:ListItem Value="4">Carvedilol</asp:ListItem>
                                    <asp:ListItem Value="5">Labetelol</asp:ListItem>
                                    <asp:ListItem Value="6">Metoprolol</asp:ListItem>
                                </asp:DropDownList>
                                <asp:CustomValidator ID="BBDDLCustomValidator" runat="server" ErrorMessage="" ForeColor="Red" ValidationGroup="vg" OnServerValidate="BBDLLCustomValidator_ServerValidate"></asp:CustomValidator>
                            </div>

                            <div class="medicationDose">
                                Total Daily Dose
                    <asp:TextBox ID="bbTDDTextBox" runat="server" TextMode="Number" AutoPostBack="True" TabIndex="36"></asp:TextBox>
                                mg
                            <asp:CustomValidator ID="bbTDDCustomValidator" ForeColor="red" ValidationGroup="vg" runat="server" ErrorMessage="" OnServerValidate="bbTDDCustomValidator_ServerValidate"></asp:CustomValidator>
                            </div>

                        </asp:Panel>
                        <div class="indented0">
                            <asp:CustomValidator ID="CCBCustomValidator" runat="server" ErrorMessage="" ForeColor="Red" ValidationGroup="vg" OnServerValidate="CCBValidator_ServerValidate"></asp:CustomValidator>
                            <asp:CheckBox ID="CCBCheckBox" runat="server" Text="Calcium Channel Blocker" AutoPostBack="True" OnCheckedChanged="CCBCheckBox_CheckedChanged" TabIndex="37" />
                        </div>
                        <div>
                        </div>
                        <asp:Panel ID="CCBPanel" runat="server" Visible="false" CssClass="indented1">
                            <div id="ccbSelect">
                                <asp:DropDownList runat="server" ID="ccbDDL" AutoPostBack="True" Width="220px" TabIndex="38">
                                    <asp:ListItem Value="">Please select a medication</asp:ListItem>
                                    <asp:ListItem Value="1">Amlodipine</asp:ListItem>
                                    <asp:ListItem Value="2">Diltiazem</asp:ListItem>
                                    <asp:ListItem Value="3">Felodipine</asp:ListItem>
                                    <asp:ListItem Value="4">Nicardipine</asp:ListItem>
                                    <asp:ListItem Value="5">Nifedipine</asp:ListItem>
                                    <asp:ListItem Value="6">Nimodipine</asp:ListItem>
                                    <asp:ListItem Value="7">Verapamil</asp:ListItem>
                                </asp:DropDownList>
                                <asp:CustomValidator ID="CCBDDLCustomValidator" runat="server" ErrorMessage="" ForeColor="Red" ValidationGroup="vg" OnServerValidate="CCBDLLCustomValidator_ServerValidate"></asp:CustomValidator>
                            </div>

                            <div class="medicationDose">
                                Total Daily Dose
                    <asp:TextBox ID="ccbTDDTextBox" runat="server" TextMode="Number" AutoPostBack="True" TabIndex="39"></asp:TextBox>
                                mg
                            <asp:CustomValidator ID="ccbTDDCustomValidator" runat="server" ErrorMessage="" OnServerValidate="ccbTDDCustomValidator_ServerValidate" ForeColor="red" ValidationGroup="vg"></asp:CustomValidator>
                            </div>
                        </asp:Panel>
                    </asp:Panel>
                </div>
            </div>
        </fieldset>

        <div class="sectionDiv SpanTwo AddFUPSubmitDiv" style="display:block;">
            <div>
                <asp:Label ID="StatusLabel" runat="server" Text=""></asp:Label>
            </div>
            <div>
                <asp:Button ID="SubmitBtn" runat="server" Text="Submit" OnClick="Button1_Click" TabIndex="40" />
            </div>
            
        </div>

    </asp:Panel>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:LocalDatabaseConnectionString %>" SelectCommand="SELECT * FROM [FollowUps] WHERE ([follow_up_ID] = @follow_up_ID)">
        <SelectParameters>
            <asp:SessionParameter Name="follow_up_ID" SessionField="fupid" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
