<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorActivation.aspx.cs" Inherits="Application.ErrorActivation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Font-Size="28pt" Text="You have errors activating your account. It is either because your link has expired or because your link is invalid. Please contact our admin for help."></asp:Label>
            <br />
            <br />
            <br />
            Click <a href="Default.aspx">here</a> to return to the home page.
        </div>
    </form>
</body>
</html>
