<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Patient.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="Consent.aspx.cs" Inherits="Application.Patient.Consent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset>
        <legend>Consent</legend>
        <div class="ConsentSection">

            <div class="Labels">
                <asp:Label ID="ConsentLabel" runat="server" Text="Label">Would you like to participate in this trial? </asp:Label>
            </div>
            <div class="Controls">
                <asp:RadioButtonList ID="ConsentRadioButtonList" runat="server" RepeatDirection="Horizontal" CssClass="ShowSurvey" TabIndex="1" AutoPostBack="True">
                    <asp:ListItem Value="1">Yes</asp:ListItem>
                    <asp:ListItem Value="2">No</asp:ListItem>
                </asp:RadioButtonList>
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
                    <asp:Button ID="SubmitBtn" runat="server" Text="Submit" OnClick="SubmitBtn_Click" />
                </div>
                
            </div>
        </div>
</asp:Content>
