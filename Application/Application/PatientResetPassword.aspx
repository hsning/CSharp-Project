<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Site.Master" AutoEventWireup="true" CodeBehind="PatientResetPassword.aspx.cs" Inherits="Application.PatientResetPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="css/login.css" rel="stylesheet" />
    <div class="title">
        <asp:Label ID="Label2" runat="server" Font-Size="32pt" Font-Strikeout="False" Text="Please change your password here:"></asp:Label>
    </div>
    <div id="form1" runat="server" class="form">

        <div style="padding-top: 20px;">
            <div class="resetPasswordInput">
                <img src="Images/password.png" /><asp:TextBox ID="PasswordTxtbox" runat="server" TextMode="Password" Placeholder="New password"></asp:TextBox>
            </div>
            <div class="resetPasswordInput">
                <img src="Images/password.png" /><asp:TextBox ID="ReenterPasswordTxtbox" runat="server" TextMode="Password" Placeholder="Re-enter new password"></asp:TextBox>
            </div>
            <div class="loginControl" style="padding-top: 20px;">

                <asp:Label ID="statusLabel" runat="server" ForeColor="Red"></asp:Label>
                <br />
                <asp:Button ID="ChangePasswordBtn" runat="server" OnClick="Button1_Click" Text="Change Password" class="buttons" />
                <br />
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Login.aspx">Login</asp:HyperLink>
            </div>
        </div>
    </div>
</asp:Content>
