<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Site.Master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeBehind="Login.aspx.cs" Inherits="Application.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="css/login.css" rel="stylesheet" />
    <div class="title">
        <asp:Label ID="Label3" runat="server" Text="Login" Font-Size="36pt"></asp:Label>
    </div>
    <div id="form1" runat="server" class="form">
        <div class="loginForm">
            <div class="usernameInput">
                <img src="Images/username.png" /><asp:TextBox ID="usernameTextbox" placeholder="Username" runat="server"></asp:TextBox>
            </div>
            <div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Username should not be empty." ControlToValidate="usernameTextbox" ForeColor="#CC3300" Font-Size="16px"></asp:RequiredFieldValidator>
            </div>
            <div class="passwordInput">
                <img src="Images/password.png" /><asp:TextBox ID="passwordTextbox" runat="server" TextMode="Password" placeholder="Password"></asp:TextBox>
            </div>
            <div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Password should not be empty." ControlToValidate="passwordTextbox" ForeColor="#CC3300" Font-Size="16px"></asp:RequiredFieldValidator>
            </div>
            <div class="loginControl">
                <asp:Label ID="StatusLabel" runat="server" ForeColor="#FF3300"></asp:Label>
                <br />
                <asp:Button ID="Button1" runat="server" Text="Login" OnClick="Button1_Click" class="buttons" />
                <br />                <asp:HyperLink ID="ResetPasswordHyperLink" runat="server" NavigateUrl="ResetPassword.aspx" Style="color: purple;">Forgot your password?</asp:HyperLink>
            </div>
        </div>
    </div>
</asp:Content>
