<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/UnselectedTestDemo.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="ViewDemo.aspx.cs" Inherits="Application.Pharmacist.ViewDemo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language='javascript'>
<!--
    function beforeDelete() { return (confirm('Are you sure you want to delete the selected patient?')); }
//-->
    </script>
    <div>
        <asp:Label ID="Label1" runat="server" Text="Add New Patient" CssClass="pageTitle"></asp:Label>
    </div>
    <div>
        <div>
            Username:
            <br />
            <asp:TextBox ID="UsernameTextBox" runat="server" ValidationGroup="adduser"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="UsernameRequiredFieldValidator" ForeColor="Red" runat="server" ErrorMessage="Username cannot be empty" ControlToValidate="UsernameTextBox" ValidationGroup="adduser"></asp:RequiredFieldValidator>
        </div>
        <div style="margin-bottom:0px;">
            Password:
            <br />
            <asp:TextBox ID="PasswordTextBox" runat="server" ValidationGroup="adduser"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="PasswordRequiredFieldValidator" ForeColor="Red" runat="server" ErrorMessage="Password cannot be empty" ControlToValidate="PasswordTextBox" ValidationGroup="adduser"></asp:RequiredFieldValidator>
        </div>
        <div>
            <asp:Label ID="statusLabel" runat="server" Text=""></asp:Label>
            <br />
            <asp:Button ID="AddBtn" runat="server" ValidationGroup="adduser" Text="Register Patient" OnClick="AddBtn_Click" />
        </div>
    </div>
    <div style="margin-top:40px;">
        <asp:Label ID="Label2" runat="server" Text="View/Modify Patients" CssClass="pageTitle"></asp:Label>
    </div>
    <div>
        <asp:DropDownList ID="filterDDL" runat="server" AutoPostBack="true" OnSelectedIndexChanged="filterDDL_SelectedIndexChanged" Width="180px">
            <asp:ListItem Selected="True" Value="">All</asp:ListItem>
            <asp:ListItem Value="1">With Consent</asp:ListItem>
            <asp:ListItem Value="2">Without Consent</asp:ListItem>
        </asp:DropDownList>
    </div>
    <div class="sectionDiv SearchDiv">
        <asp:TextBox ID="FilterTextBox" runat="server" PlaceHolder="Search by username" Width="180px"></asp:TextBox>
        <asp:Button ID="SearchButton" runat="server" OnClick="SearchButton_Click" Text="Search" />

        <hr />
    </div>
    <div>
        <asp:Label ID="DemoLabel" runat="server" Text="This application currently has 0 demographics."></asp:Label>
    </div>
    <asp:GridView ID="GridView1" runat="server" CssClass="GridView" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" DataKeyNames="id" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowCommand="GridView1_RowCommand" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDeleted="GridView1_RowDeleted" AllowPaging="True" AllowSorting="True" ShowFooter="True" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting">

        <AlternatingRowStyle BackColor="White" />
        <EditRowStyle BackColor="Wheat" ForeColor="White" />
        <SelectedRowStyle ForeColor="White" />
        <Columns>
            <asp:TemplateField ItemStyle-CssClass="deleteButtontd" ControlStyle-CssClass="editButton">
                <ItemTemplate>
                    <asp:Button runat="server" Text="Edit" CommandName="Edit" />
                </ItemTemplate>
                <ControlStyle CssClass="editButton"></ControlStyle>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle CssClass="deleteButtontd" HorizontalAlign="Center"></ItemStyle>
                <EditItemTemplate>
                    <asp:Button ID="btnUpdate" runat="server" CommandName="Update" Text="Update" CausesValidation="False" />
                    <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancel" CausesValidation="False" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-CssClass="deleteButtontd" ControlStyle-CssClass="editButton">
                <ItemTemplate>
                    <asp:Button runat="server" Text="Select" CommandName="Select" ToolTip="Selecting this demographic will allow you to change his/her information as well as adding a new visit under this demographic." />
                </ItemTemplate>
                <ControlStyle CssClass="editButton"></ControlStyle>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle CssClass="deleteButtontd" HorizontalAlign="Center"></ItemStyle>
            </asp:TemplateField>
            <asp:BoundField DataField="id" HeaderText="ID" SortExpression="id" ReadOnly="True">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="email" HeaderText="Username" SortExpression="email" ReadOnly="True">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="study_id" HeaderText="Study ID" SortExpression="consent_id" ReadOnly="True">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="password" HeaderText="password" SortExpression="password" ReadOnly="True" Visible="False"></asp:BoundField>
            <asp:CheckBoxField DataField="have_consent" HeaderText="Have consent?" SortExpression="have_consent" ReadOnly="True">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:CheckBoxField>
            <asp:CheckBoxField DataField="demo_added" HeaderText="Demo added?" SortExpression="demo_added" ReadOnly="True">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:CheckBoxField>
            <asp:BoundField DataField="unhashed_password" HeaderText="Password" SortExpression="unhashed_password" ReadOnly="True">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:CheckBoxField DataField="password_changed" HeaderText="Temporary password?" SortExpression="password_changed" ItemStyle-CssClass="ToBeEdited">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:CheckBoxField>
            <asp:BoundField DataField="Column1" HeaderText="Number of visits" ReadOnly="True" SortExpression="Column1">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField ItemStyle-CssClass="deleteButtontd" ControlStyle-CssClass="deleteButton">
                <ItemTemplate>
                    <asp:Button runat="server" Text="Delete" CommandName="Delete" OnClientClick="return(beforeDelete())" />
                </ItemTemplate>
                <ControlStyle CssClass="deleteButton"></ControlStyle>
                <ItemStyle CssClass="deleteButtontd" HorizontalAlign="Center"></ItemStyle>
            </asp:TemplateField>
        </Columns>
        <EditRowStyle BackColor="#e1e6ed" ForeColor="#555555" />
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
    <div style="text-align: right;">
        <asp:Label runat="server" Text="" ID="gridviewStatusLabel"></asp:Label>
    </div>


    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:LocalDatabaseConnectionString %>" SelectCommand="select Users.id, Studies.study_id as study_id,email, password, have_consent,demo_added,unhashed_password,password_changed,count(follow_up_ID) from Users left join Studies on Studies.patient_id = Users.id left join FollowUps on FollowUps.study_id = Studies.study_id left join Consent on Consent.patient_id=Users.id where Users.site_id=@site_id and role_id = (select role_id from Roles where role_name='patient') and (email like '%'+@email+'%' or Users.id like '%'+@email+'%') group by Users.id,email,password,have_consent,demo_added,unhashed_password,password_changed,Studies.study_id order by Users.id" DeleteCommand="DELETE FROM [Profile] where user_id = @id;
        DELETE FROM [Studies] where [patient_id]=@id;
        DELETE FROM [Consent] where [patient_id]=@id;
        DELETE FROM [Users] where id=@id;
        "
        FilterExpression=""
        InsertCommand="INSERT INTO [Demographic] ([id], [consent], [consent_date], [birth_date], [race_1], [race_2], [race_3], [race_4], [race_5], [race_6], [race_7], [race_8], [race_9], [race_10], [sex], [height], [weight], [bmi], [systolic], [diastolic], [diabetes], [diabetes_year], [smoker], [stop_year], [alcohol], [drinks], [drugs], [other], [other_specify]) VALUES (@id, @consent, @consent_date, @birth_date, @race_1, @race_2, @race_3, @race_4, @race_5, @race_6, @race_7, @race_8, @race_9, @race_10, @sex, @height, @weight, @bmi, @systolic, @diastolic, @diabetes, @diabetes_year, @smoker, @stop_year, @alcohol, @drinks, @drugs, @other, @other_specify)"
        UpdateCommand="UPDATE [Users] SET [password_changed] = 1 ^(@password_changed) WHERE [id] = @id">
        <DeleteParameters>
            <asp:Parameter Name="id" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="id" Type="String" />
            <asp:Parameter Name="consent" Type="Int32" />
            <asp:Parameter DbType="Date" Name="consent_date" />
            <asp:Parameter DbType="Date" Name="birth_date" />
            <asp:Parameter Name="race_1" Type="Boolean" />
            <asp:Parameter Name="race_2" Type="Boolean" />
            <asp:Parameter Name="race_3" Type="Boolean" />
            <asp:Parameter Name="race_4" Type="Boolean" />
            <asp:Parameter Name="race_5" Type="Boolean" />
            <asp:Parameter Name="race_6" Type="Boolean" />
            <asp:Parameter Name="race_7" Type="Boolean" />
            <asp:Parameter Name="race_8" Type="Boolean" />
            <asp:Parameter Name="race_9" Type="String" />
            <asp:Parameter Name="race_10" Type="Boolean" />
            <asp:Parameter Name="sex" Type="Int32" />
            <asp:Parameter Name="height" Type="Decimal" />
            <asp:Parameter Name="weight" Type="Decimal" />
            <asp:Parameter Name="bmi" Type="Decimal" />
            <asp:Parameter Name="systolic" Type="Int32" />
            <asp:Parameter Name="diastolic" Type="Int32" />
            <asp:Parameter Name="diabetes" Type="Boolean" />
            <asp:Parameter Name="diabetes_year" Type="Int32" />
            <asp:Parameter Name="smoker" Type="Boolean" />
            <asp:Parameter Name="stop_year" Type="Int32" />
            <asp:Parameter Name="alcohol" Type="Boolean" />
            <asp:Parameter Name="drinks" Type="Int32" />
            <asp:Parameter Name="drugs" Type="Boolean" />
            <asp:Parameter Name="other" Type="Boolean" />
            <asp:Parameter Name="other_specify" Type="String" />
        </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter DefaultValue="" Name="site_id" SessionField="site_id" Type="String" />
            <asp:ControlParameter ControlID="FilterTextBox" DefaultValue="%" Name="email" PropertyName="Text" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="password_changed" />
            <asp:Parameter Name="id" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:LocalDatabaseConnectionString %>" SelectCommand="SELECT [id], [consent], [consent_date], [birth_date] FROM [Demographic] WHERE ([id] = @id)">
        <SelectParameters>
            <asp:ControlParameter ControlID="GridView1" Name="id" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
