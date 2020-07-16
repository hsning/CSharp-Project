<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/SelectedTestDemo.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="ViewVisits.aspx.cs" Inherits="Application.Pharmacist.ViewVisits" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language='javascript'>
<!--
    function beforeDelete() { return (confirm('Are you sure you want to delete the selected visit?')); }
//-->
    </script>
    <div>
        <asp:Label ID="Label1" runat="server" Text="View Visits" CssClass="pageTitle"></asp:Label>
    </div>

    <hr />
    <div style="display: block;">
        <div>
            <asp:Label ID="FUPLabel" runat="server" Text="This demographic has no visits at the moment."></asp:Label>
        </div>
        <asp:GridView ID="GridView1" runat="server" CssClass="GridView" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None" OnRowCommand="GridView1_RowCommand" AllowPaging="True" AllowSorting="True" OnDataBound="GridView1_DataBound" ShowFooter="True" OnRowDataBound="GridView1_RowDataBound" DataKeyNames="follow_up_ID">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField ItemStyle-CssClass="deleteButtontd" ControlStyle-CssClass="editButton">
                    <ItemTemplate>
                        <asp:Button runat="server" Text="View Visit" CommandName="ViewFollow-up" CommandArgument='<%#Eval("follow_up_ID")%>' />
                    </ItemTemplate>
                    <ControlStyle CssClass="editButton"></ControlStyle>
                    <ItemStyle CssClass="deleteButtontd" HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
                <asp:BoundField DataField="follow_up_ID" HeaderText="follow_up_ID" SortExpression="follow_up_ID" InsertVisible="False" ReadOnly="True" Visible="False"></asp:BoundField>
                <asp:BoundField DataField="visit_date" HeaderText="Visit Date" SortExpression="visit_date" DataFormatString="{0:MMM/dd/yyyy}">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField ItemStyle-CssClass="deleteButtontd" ControlStyle-CssClass="deleteButton">
                    <ItemTemplate>
                        <asp:Button runat="server" CommandName="Delete" Text="Delete" OnClientClick="return(beforeDelete())" />
                    </ItemTemplate>
                    <ControlStyle CssClass="deleteButton"></ControlStyle>
                    <ItemStyle CssClass="deleteButtontd" HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
        <div class="sectionDiv" style="padding-top: 20px;">
            <asp:Button ID="AddButton" runat="server" Text="Add New Visit" OnClick="AddButton_Click" />
        </div>

    </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:LocalDatabaseConnectionString %>" SelectCommand="SELECT [follow_up_ID],[visit_date] FROM [FollowUps] WHERE ([study_id] = (select study_id from Studies where patient_id= @visitID)) order by [visit_date] desc" DeleteCommand="delete from FollowUps where follow_up_ID=@follow_up_ID" OnSelected="SqlDataSource1_Selected">
        <DeleteParameters>
            <asp:Parameter Name="follow_up_ID" />
        </DeleteParameters>
        <SelectParameters>
            <asp:SessionParameter Name="visitID" SessionField="patient_id" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:LocalDatabaseConnectionString %>" SelectCommand="SELECT * FROM [FollowUps] WHERE ([follow_up_ID] = @follow_up_ID)">
        <SelectParameters>
            <asp:ControlParameter ControlID="GridView1" Name="follow_up_ID" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
