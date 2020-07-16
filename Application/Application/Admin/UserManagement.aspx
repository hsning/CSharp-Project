<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Admin.Master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeBehind="UserManagement.aspx.cs" Inherits="Application.Admin.UserManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="registerNewUser">
            <div class="registerNewUserInputs registerNewUserSpanTwo sectionDiv" style="margin-bottom: 20px;">
                <asp:Label ID="Label2" runat="server" Text="Register New User" class="pageTitle"></asp:Label>
            </div>
            <div class="registerNewUserInputs" style="padding-bottom: 0px;">
                <asp:Label ID="Label3" runat="server" Text="Email" CssClass="required" Font-Size="16px"></asp:Label>
                <br />
                <asp:TextBox ID="emailTextbox" runat="server" class="registerInput" Width="300px"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Email should not be empty" ControlToValidate="emailTextbox" ForeColor="red"></asp:RequiredFieldValidator>
            </div>
            <div class="registerNewUserInputs" style="padding-bottom: 0px;">
                <asp:Label ID="Label5" runat="server" Text="Site" CssClass="required" Font-Size="16px"></asp:Label>
                <br />
                <asp:DropDownList ID="SiteDDL" runat="server" DataSourceID="SqlDataSource3" DataTextField="site_name" DataValueField="site_id" Width="300px"></asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:LocalDatabaseConnectionString %>" SelectCommand="SELECT * FROM [Site]"></asp:SqlDataSource>
                <br />
                <asp:CustomValidator ID="SiteDDLCustomValidator" runat="server" ErrorMessage="Site field cannot be empty"  OnServerValidate="SiteDDLCustomValidator_ServerValidate" ForeColor="red"></asp:CustomValidator>
            </div>
            <div class="registerNewUserInputs">
                <asp:Label ID="Label4" runat="server" Text="First Name" Font-Size="16px"></asp:Label>
                <br />
                <asp:TextBox ID="FirstNameTextBox" runat="server" class="registerInput"></asp:TextBox>
            </div>
            <div class="registerNewUserInputs">
                <asp:Label ID="Label6" runat="server" Text="Last Name" Font-Size="16px"></asp:Label>
                <br />
                <asp:TextBox ID="LastNameTextBox" runat="server" class="registerInput"></asp:TextBox>
            </div>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:LocalDatabaseConnectionString %>" SelectCommand="SELECT [site_id], [site_name] FROM [Site]"></asp:SqlDataSource>
            <div class="registerNewUserSpanTwo registerNewUserInputs buttonClass addChangeBtn">
                <div>
                    <asp:Label ID="statusLabel" runat="server" Text="" ForeColor="Red"></asp:Label>
                    <br />
                    <asp:Button ID="registerButton" Style="display: initial;" runat="server" OnClick="registerButton_Click" Text="Register" CssClass="buttons editButton" />
                </div>
            </div>

        </div>
        <div class="registerNewUserInputs sectionDiv">
            <asp:Label ID="Label1" runat="server" Text="Modify Users" class="pageTitle"></asp:Label>
        </div>
        <div class="registerNewUser">
            <div class="registerNewUserInputs">
                <asp:DropDownList ID="SiteDDL2" runat="server" DataSourceID="SqlDataSource3" DataTextField="site_name" DataValueField="site_id" AutoPostBack="True" OnSelectedIndexChanged="SiteDDL2_SelectedIndexChanged"></asp:DropDownList>
            </div>
        </div>
        <div class="registerNewUser">
            <div class="registerNewUserInputs" style="display: flex; align-items: center;">
                <asp:TextBox ID="FilterNameTextBox" Style="width: 190px;" runat="server" AutoPostBack="True" PlaceHolder="User Search" OnTextChanged="FilterNameTextBox_TextChanged"></asp:TextBox>
                <button id="SearchBtn" type="button" onclick="SearchBtn_Click1" class="buttons editButton" style="margin-left: 20px;">
                    <img src="../../Images/search.png"/ height="16" style="width: 16px">Search</button>
            </div>
        </div>
        <div class="registerNewUserInputs">
            <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource1" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand" DataKeyNames="id" Style="width: 100%;" ForeColor="#333333" GridLines="None" HeaderStyle-CssClass="gvHeader" AllowPaging="True" OnRowUpdating="GridView1_RowUpdating" OnRowDeleting="GridView1_RowDeleting" ShowFooter="true">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="" ItemStyle-CssClass="deleteButtontd">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" Style="width: 75px;" CausesValidation="false" CommandName="Edit" class="editButton buttons" CommandArgument='<%#Eval("id")%>'><img src="../../Images/edit.png" />Edit</asp:LinkButton>
                        </ItemTemplate>
                        <ControlStyle CssClass="editButton buttons"></ControlStyle>
                        <HeaderStyle ForeColor="White" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                        <EditItemTemplate>
                            <asp:Button ID="btnUpdate" runat="server" CausesValidation="false" CommandName="Update" Text="Update" />
                            <asp:Button ID="btnCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="email" HeaderText="Email" SortExpression="email" ReadOnly="True">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="first_name" HeaderText="First Name" SortExpression="first_name">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="last_name" HeaderText="Last Name" SortExpression="last_name">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:CheckBoxField DataField="activated" HeaderText="Activated?" SortExpression="activated" ReadOnly="True">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:CheckBoxField>
                    <asp:CheckBoxField DataField="suspended" HeaderText="Suspended?" SortExpression="suspended">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:CheckBoxField>
                    <asp:BoundField DataField="create_date" HeaderText="Datetime Created" SortExpression="create_date" ReadOnly="True">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="update_date" HeaderText="Datetime last updated" SortExpression="update_date" ReadOnly="True">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="role_name" HeaderText="Role" SortExpression="role_name" ReadOnly="True">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="site_name" HeaderText="Site" SortExpression="site_name">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Action" ItemStyle-CssClass="resendButton">
                        <ItemTemplate>
                            <asp:Button runat="server" CommandName="Resend" Text="Resend confirmation email" CommandArgument='<%#Eval("email")%>'></asp:Button>
                        </ItemTemplate>
                        <HeaderStyle ForeColor="White" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" ItemStyle-CssClass="deleteButtontd">
                        <ItemTemplate>
                            <asp:LinkButton Style="width: 100px;" class="deleteButton buttons" CausesValidation="false" Text="Delete" runat="server" OnClientClick="return confirm('Are you sure you want to delete this user?')" CommandName="Delete" CommandArgument='<%#Eval("id")%>'><img src="../../Images/delete.png" / height="20">Delete</asp:LinkButton>
                        </ItemTemplate>
                        <ControlStyle CssClass="deleteButton buttons"></ControlStyle>
                        <HeaderStyle ForeColor="White" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#e1e6ed" ForeColor="#555555" />
                <FooterStyle BackColor="#00bfff" ForeColor="White" Font-Bold="True" />
                <HeaderStyle BackColor="#00bfff" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
        </div>
    </div>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:LocalDatabaseConnectionString %>" SelectCommand="SELECT [id],[email], [activated],[suspended], [create_date], [update_date], [role_name],[first_name],[last_name],[site_name],Users.[site_id] FROM Users join Roles on Users.role_id=Roles.role_id join Profile on Profile.user_id=Users.id left join Site on Site.site_id=Users.site_id where ([email] like '%'+@email+'%' or [first_name] like '%'+@email+'%' or [last_name] like '%'+@email+'%' or [site_name] like '%'+@email+'%') and role_name='pharmacist'"
        FilterExpression="site_id = '{0}'"
        DeleteCommand="DELETE Profile WHERE user_id=@id; DELETE Users WHERE id=@id;"
        UpdateCommand="update Profile set first_name=@first_name,last_name=@last_name where user_id=@id;
        update Users set suspended=@suspended,suspended_date=@suspended_date,update_date=@update_date where id=@id;">
        <DeleteParameters>
            <asp:Parameter Name="id" />
        </DeleteParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="FilterNameTextBox" DefaultValue="%" Name="email" PropertyName="Text" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="first_name" />
            <asp:Parameter Name="last_name" />
            <asp:Parameter Name="id" />
            <asp:Parameter Name="suspended" />
            <asp:Parameter Name="suspended_date" />
            <asp:Parameter Name="update_date" />
        </UpdateParameters>
        <FilterParameters>
            <asp:ControlParameter ControlID="SiteDDL2" DbType="String" Name="site_id" PropertyName="SelectedValue" />
        </FilterParameters>
    </asp:SqlDataSource>
</asp:Content>
