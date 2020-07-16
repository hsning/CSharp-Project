<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Application.Error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" runat="server" Text="You are unauthorized to visit this page."></asp:Label>
        <br />
        Click <a href="Default.aspx">here</a> to return to the home page.
        <div>
        </div>
</asp:Content>
