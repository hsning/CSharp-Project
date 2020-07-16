<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Admin.Master" AutoEventWireup="true" CodeBehind="ViewLogs.aspx.cs" Inherits="Application.Admin.ViewLogs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="registerNewUserInputs registerNewUserSpanTwo sectionDiv" style="margin-bottom: 20px;">
        <asp:Label ID="Label2" runat="server" Text="View logs" class="pageTitle"></asp:Label>
    </div>
    <div class="registerNewUserInputs">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="study_id" HeaderText="Study ID"/>
                <asp:BoundField DataField="nature_of_change" HeaderText="Changed On"/>
                <asp:BoundField DataField="changed_on" HeaderText="Changed On"/>
                <asp:BoundField DataField="changed_by" HeaderText="Changed By"/>
                <asp:BoundField DataField="field" HeaderText="Changed By"/>
                <asp:BoundField DataField="old_value" HeaderText="Old Value"/>
                <asp:BoundField DataField="new_value" HeaderText="New Value"/>
            </Columns>
        </asp:GridView>
    </div>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:LocalDatabaseConnectionString %>" SelectCommand="SELECT * FROM [Studies_Logs]"></asp:SqlDataSource>
</asp:Content>
