<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Patient.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Application.Patient.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="Label1" runat="server" Text="" Font-Bold="true" Font-Size="26px">What you need to do:</asp:Label><br />
    <asp:Label ID="StatusLabel" runat="server" Text=""></asp:Label>
    <asp:BulletedList ID="BulletedList1" runat="server"></asp:BulletedList>

</asp:Content>
