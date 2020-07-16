<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Admin.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Application.Admin.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="editProfile" style="padding-bottom: 40px;">
        <div class="profileInputs profileSpanTwo sectionDiv">
            <asp:Label ID="Label11" runat="server" Text="View/Edit Credential" class="pageTitle"></asp:Label>
        </div>
        <div class="profileInputs">
            <asp:Label ID="Label12" runat="server" Text="Email"></asp:Label>
            <br />
            <asp:TextBox ID="EmailTextbox" runat="server" ReadOnly="True" BackColor="Silver" class="credentialInput"></asp:TextBox>
        </div>
        <div runat="server">
        </div>
        <div class="profileInputs" runat="server" style="padding-bottom: 0px;">
            <div class="usernameInput">
                <asp:Label ID="Label13" runat="server" Text="Please enter your new email:"></asp:Label>
                <br />
                <asp:TextBox ID="NewEmailTextBox" runat="server" class="credentialInput" ToolTip="Please note that you will be logged out and we will send you an email verification to your new email."></asp:TextBox>
            </div>
        </div>
        <div class="loginControl credentialButton" style="padding-bottom: 0px;">
            <div class="buttonDiv">
                <div>
                    <asp:Label ID="emailStatusLabel" runat="server" Text="" ForeColor="Red"></asp:Label>
                    <asp:Button ID="Button2" runat="server" Text="Change Email" OnClick="ChangeEmailBtn_Click" CssClass="buttons editButton" ToolTip="Please note that you will be logged out and we will send you an email verification to your new email." />
                </div>

            </div>
        </div>
    </div>

    <div class="editProfile" style="padding-bottom: 40px;">
        <div class="profileInputs">
            <asp:Label ID="Label15" runat="server" Text="New Password"></asp:Label>
            <br />
            <asp:TextBox ID="PasswordTextBox" runat="server" class="credentialInput" TextMode="Password"></asp:TextBox>
        </div>
        <div runat="server"></div>
        <div class="profileInputs" runat="server" style="padding-bottom: 0px;">
            <div class="usernameInput">
                <asp:Label ID="Label16" runat="server" Text="Confirm New Password"></asp:Label>
                <br />
                <asp:TextBox ID="ReenterPasswordTextBox" runat="server" class="credentialInput" TextMode="Password"></asp:TextBox>
            </div>
        </div>
        <div class="loginControl credentialButton" style="padding-bottom: 0px;">
            <div class="buttonDiv">
                <asp:Label ID="passwordStatusLabel" runat="server" Text="" ForeColor="Red"></asp:Label>
                <br />
                <asp:Button ID="passwordButton" runat="server" Text="Change Password" OnClick="ChangePasswordBtn_Click" CssClass="buttons editButton" ToolTip="Please note that you will be logged out and we will send you an email verification to your new email." />
            </div>
        </div>
    </div>
    <div class="editProfile">
        <div class="profileInputs profileSpanTwo sectionDiv" style="margin-bottom: 10px;">
            <asp:Label ID="Label10" runat="server" Text="View/Edit Profile" class="pageTitle"></asp:Label>
        </div>
        <div class="profileInputs">
            <asp:Label ID="Label1" runat="server" Text="First name"></asp:Label>
            <br />
            <asp:TextBox ID="FirstNameTextbox" runat="server" CssClass="profileInput"></asp:TextBox>
        </div>
        <div class="profileInputs">
            <asp:Label ID="Label3" runat="server" Text="Last name"></asp:Label>
            <br />
            <asp:TextBox ID="LastNameTextbox" runat="server" CssClass="profileInput"></asp:TextBox>
        </div>
        <div class="profileInputs">
            <asp:Label ID="Label5" runat="server" Text="Address"></asp:Label>
            <br />
            <asp:TextBox ID="AddressTextbox" runat="server" CssClass="profileInput"></asp:TextBox>
        </div>
        <div class="profileInputs">
            <asp:Label ID="Label2" runat="server" Text="City"></asp:Label>
            <br />
            <asp:TextBox ID="CityTextbox" runat="server" CssClass="profileInput"></asp:TextBox>
        </div>
        <div class="profileInputs">
            <asp:Label ID="Label6" runat="server" Text="Province"></asp:Label>
            <br />
            <asp:TextBox ID="ProvinceTextbox" runat="server" CssClass="profileInput"></asp:TextBox>
        </div>
        <div class="profileInputs">
            <asp:Label ID="Label7" runat="server" Text="Country"></asp:Label>
            <br />
            <asp:TextBox ID="CountryTextbox" runat="server" CssClass="profileInput"></asp:TextBox>
        </div>
        <div class="profileInputs">
            <asp:Label ID="Label9" runat="server" Text="Postal Code"></asp:Label>
            <br />
            <asp:TextBox ID="PostalCodeTextBox" runat="server" CssClass="profileInput"></asp:TextBox>
        </div>
        <div class="profileInputs">
            <asp:Label ID="Label4" runat="server" Text="Contact number"></asp:Label>
            <br />
            <asp:TextBox ID="ContactNumberTextbox" runat="server" CssClass="profileInput"></asp:TextBox>
        </div>
        <div class="profileInputs" style="padding-bottom: 0px;">
            <asp:Label ID="Label8" runat="server" Text="Organization"></asp:Label>
            <br />
            <asp:TextBox ID="OrganizationTextbox" runat="server" CssClass="profileInput"></asp:TextBox>
        </div>
        <div class="profileSpanTwo profileInputs buttonClass addChangeBtn">
            
            <div>
                <asp:Label ID="statusLabel" runat="server" ForeColor="#009933"></asp:Label>
                <br />
                <asp:Button ID="Button1" runat="server" Text="Save Changes" style="display:initial;" OnClick="Button1_Click" CssClass="buttons editButton" CausesValidation="False" />
            </div>

        </div>
    </div>

</asp:Content>
