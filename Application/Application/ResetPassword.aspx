<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Site.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="Application.ResetPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="css/login.css" rel="stylesheet" />
    <div class="title">
        <asp:Label ID="Label1" runat="server" Text="Password Reset" Font-Size="36pt"></asp:Label>
    </div>
    <div id="form1" runat="server" class="form">
        <div>
            <div class="resetUsernameInput">
                <img src="Images/email.png" /><asp:TextBox ID="EmailTextBox" runat="server" Placeholder="Please enter your email here"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="statusLabel" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div class="loginControl">
                <asp:Button ID="Button1" runat="server" Text="Reset Password" OnClick="Button1_Click" />
            </div>
        </div>
    </div>
</asp:Content>
