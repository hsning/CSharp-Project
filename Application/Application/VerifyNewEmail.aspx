<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerifyNewEmail.aspx.cs" Inherits="Application.VerifyNewEmail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="statusLabel" runat="server" Text="You have now verified your account. Please click the link below to login."></asp:Label>
        </div>
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Login.aspx">Login</asp:HyperLink>
    </form>
</body>
</html>
