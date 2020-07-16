<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Admin.Master" AutoEventWireup="true" CodeBehind="CityManagement.aspx.cs" Inherits="Application.Admin.CityManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="siteInputs sectionDiv">
        <asp:Label ID="Label3" runat="server" Text="Add City" class="pageTitle"></asp:Label>
    </div>
    <div class="addSite">
        <div class="siteInputs">
            <asp:Label ID="Label7" runat="server" Text="City Code" CssClass="required"></asp:Label>
            <br />
            <asp:TextBox ID="CityCodeTextbox" runat="server" CssClass="profileInput"></asp:TextBox>
            <br />
            <asp:CustomValidator ID="CityCodeCustomValidator" runat="server" ErrorMessage="" OnServerValidate="CityCodeCustomValidator_ServerValidate" ForeColor="Red" ControlToValidate="CityCodeTextbox"></asp:CustomValidator>
        </div>
        <div class="siteInputs">
            <asp:Label ID="Label2" runat="server" Text="City Name" CssClass="required"></asp:Label>
            <br />
            <asp:TextBox ID="CityNameTextBox" runat="server" CssClass="profileInput"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="CityNameRequiredFieldValidator" runat="server" ControlToValidate="CityNameTextBox" ForeColor="Red" ErrorMessage="This field should not be empty"></asp:RequiredFieldValidator>
        </div>
        <div class="siteInputs buttonClass addSiteSpanTwo addChangeBtn" style="padding-top: 0px;">
            <asp:Label ID="statusLabel" runat="server" ForeColor="#DB3056"></asp:Label>
            <br />
            <asp:Button ID="AddCityBtn" runat="server" Style="display: initial;" Text="Add Site" OnClick="AddCityBtn_Click" CssClass="buttons editButton addChangeBtn" CausesValidation="true" />
        </div>
    </div>
    <div class="siteInputs sectionDiv">
        <asp:Label ID="Label1" runat="server" Text="Modify Cities" class="pageTitle"></asp:Label>
    </div>
    <div class="siteInputs" style="display: flex;align-items: center;">
                <asp:TextBox ID="FilterCityTextBox" runat="server" AutoPostBack="True" Text="" OnTextChanged="FilterCityTextBox_TextChanged" PlaceHolder="City search"></asp:TextBox>
            <button ID="SearchBtn" type="button" OnClick="SearchBtn_Click" class="buttons editButton" style="margin-left:20px;" CausesValidation="false">
                <img src="../../Images/search.png" / height="16">Search</button>
    </div>
    <div class="siteInputs">

        <asp:GridView ID="GridView1" runat="server" ShowFooter="true" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None" Style="width: 100%;" DataKeyNames="city_code" OnRowDataBound="GridView1_RowDataBound">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="" ItemStyle-CssClass="deleteButtontd" ControlStyle-CssClass="editButton buttons">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" Style="width: 75px;" CommandName="Edit" CommandArgument='<%#Eval("city_code")%>' CausesValidation="False"><img src="../../Images/edit.png" />Edit</asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle CssClass="editButton buttons"></ControlStyle>
                    <HeaderStyle ForeColor="White" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                    <EditItemTemplate>
                        <asp:Button ID="btnUpdate" runat="server" CommandName="Update" Text="Update" CausesValidation="False" />
                        <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancel" CausesValidation="False" />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="city_code" HeaderText="City Code" SortExpression="CityCode" ReadOnly="true">
                    <HeaderStyle HorizontalAlign="Center" ForeColor="White" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="city_name" HeaderText="City Name" SortExpression="CityName">
                    <HeaderStyle HorizontalAlign="Center" ForeColor="White" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="" ItemStyle-CssClass="deleteButtontd">
                    <ItemTemplate>
                        <asp:LinkButton Style="width: 100px;" CssClass="deleteButton buttons" runat="server" CausesValidation="False" OnClientClick="return confirm('Are you sure you want to delete this city?')" CommandName="Delete" CommandArgument='<%#Eval("city_code")%>'><img src="../../Images/delete.png" / height="20">Delete</asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle CssClass="deleteButton buttons"></ControlStyle>
                    <HeaderStyle ForeColor="White" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#e1e6ed" ForeColor="#555555" />
            <FooterStyle BackColor="#00bfff" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#00bfff" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:LocalDatabaseConnectionString %>" DeleteCommand="delete from Cities where city_code=@city_code" SelectCommand="SELECT * FROM [Cities] WHERE ([city_name] LIKE '%' + @city_name + '%' or [city_code] LIKE '%' + @city_name + '%')" UpdateCommand="update cities set [city_name]=@city_name where [city_code]=@city_code">
            <DeleteParameters>
                <asp:Parameter Name="city_code" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="city_name" />
                <asp:Parameter Name="city_code" />
            </UpdateParameters>
                    <SelectParameters>
            <asp:ControlParameter ControlID="FilterCityTextBox" DefaultValue="%" Name="city_name" PropertyName="Text" Type="String" />
        </SelectParameters>
        </asp:SqlDataSource>
    </div>
</asp:Content>
