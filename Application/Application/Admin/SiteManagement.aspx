<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Admin.Master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeBehind="SiteManagement.aspx.cs" Inherits="Application.Admin.SiteManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="siteInputs sectionDiv">
            <asp:Label ID="Label3" runat="server" Text="Add Site" class="pageTitle"></asp:Label>
        </div>
        <div class="addSite">
            <div class="siteInputs">
                <asp:Label ID="Label7" runat="server" Text="Site Name" CssClass="required"></asp:Label>
                <br />
                <asp:TextBox ID="SiteNameTextbox" runat="server" CssClass="profileInput" Width="220px"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Site name cannot be empty." ControlToValidate="SiteNameTextbox" ForeColor="#CC3300"></asp:RequiredFieldValidator>
            </div>
            <div class="siteInputs">
                <asp:Label ID="Label2" runat="server" Text="Site Description"></asp:Label>
                <br />
                <asp:TextBox ID="SiteDescriptionTextBox" runat="server" CssClass="profileInput" Width="220px"></asp:TextBox>
            </div>
            <div class="siteInputs">
                <asp:Label ID="Label4" runat="server" Text="Choose a city" CssClass="required"></asp:Label>
                <br />
                <asp:DropDownList ID="CityDDL" runat="server" Width="220px" DataSourceID="SqlDataSource2" DataTextField="city_name" DataValueField="city_code">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:LocalDatabaseConnectionString %>" SelectCommand="SELECT * FROM [Cities]"></asp:SqlDataSource>
                <br />
                <asp:CustomValidator ID="DDLCustomValidator" runat="server" ErrorMessage="Site location cannot be empty" OnServerValidate="DDLCustomValidator_ServerValidate" ForeColor="#CC3300"></asp:CustomValidator>
            </div>
            <div class="siteInputs">
                <asp:Label ID="Label5" runat="server" Text="Address"></asp:Label>
                <br />
                <asp:TextBox ID="AddressTextbox" runat="server" CssClass="profileInput" Width="220px"></asp:TextBox>
            </div>
            <div class="siteInputs">
                <asp:Label ID="Label6" runat="server" Text="Phone number"></asp:Label>
                <br />
                <asp:TextBox ID="PhoneTextbox" runat="server" CssClass="profileInput" Width="220px"></asp:TextBox>
            </div>
            <div class="siteInputs">
                <asp:Label ID="Label8" runat="server" Text="Postal code"></asp:Label>
                <br />
                <asp:TextBox ID="PostalCodeTextbox" runat="server" CssClass="profileInput" Width="220px"></asp:TextBox>
            </div>
            <div class="siteInputs buttonClass addSiteSpanTwo addChangeBtn" style="padding-top: 0px;">
                <div>
                    <asp:Label ID="statusLabel" runat="server" ForeColor="#DB3056"></asp:Label>
                    <br />
                    <asp:Button ID="AddSiteBtn" runat="server" Style="display: initial;" Text="Add Site" OnClick="AddSiteBtn_Click" CssClass="buttons editButton addChangeBtn" />
                </div>
            </div>
        </div>
        <div class="siteInputs sectionDiv">
            <asp:Label ID="Label1" runat="server" Text="Modify Sites" class="pageTitle"></asp:Label>
        </div>
        <div class="registerNewUser">
            <div class="registerNewUserInputs">
                <asp:DropDownList ID="CityFilterDDL" runat="server" Width="220px" DataSourceID="SqlDataSource2" DataTextField="city_name" DataValueField="city_code" AutoPostBack="True" OnSelectedIndexChanged="CityFilterDDL_SelectedIndexChanged"></asp:DropDownList>
            </div>
        </div>
        <div class="registerNewUser">
            <div class="registerNewUserInputs" style="display: flex; align-items: center;">
                <asp:TextBox ID="FilterSiteTextBox" runat="server" AutoPostBack="True" Text="" OnTextChanged="FilterSiteTextBox_TextChanged" PlaceHolder="Site Search" Width="220px"></asp:TextBox>
                <button id="SearchBtn" type="button" onclick="SearchBtn_Click" class="buttons editButton" style="margin-left: 20px;" causesvalidation="false">
                <img src="../../Images/search.png" / height="16">Search</button>
            </div>

        </div>
        <div class="siteInputs">
            <asp:GridView ID="GridView1" runat="server" ShowFooter="true" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="site_id" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None" Style="width: 100%;" AllowSorting="True" HeaderStyle-CssClass="gvHeader" AllowPaging="True" OnRowDeleted="GridView1_RowDeleted" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="" ItemStyle-CssClass="deleteButtontd" ControlStyle-CssClass="editButton buttons">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" Style="width: 75px;" CommandName="Edit" CommandArgument='<%#Eval("site_id")%>' CausesValidation="False"><img src="../../Images/edit.png" />Edit</asp:LinkButton>
                        </ItemTemplate>
                        <ControlStyle CssClass="editButton buttons"></ControlStyle>
                        <HeaderStyle ForeColor="White" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                        <EditItemTemplate>
                            <asp:Button ID="btnUpdate" runat="server" CommandName="Update" Text="Update" CausesValidation="False" />
                            <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancel" CausesValidation="False" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="site_id" HeaderText="Site ID" SortExpression="site_id">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="site_name" HeaderText="Site Name" SortExpression="site_name">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="site_description" HeaderText="Site Description" SortExpression="site_description">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="address" HeaderText="Address" SortExpression="address">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="phone_number" HeaderText="Phone number" SortExpression="phone_number">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="postal_code" HeaderText="Postal code" SortExpression="postal_code">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="city" HeaderText="City" SortExpression="city" ReadOnly="true">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="" ItemStyle-CssClass="deleteButtontd">
                        <ItemTemplate>
                            <asp:LinkButton Style="width: 100px;" CssClass="deleteButton buttons" runat="server" CausesValidation="False" OnClientClick="return confirm('Are you sure you want to delete this site?')" CommandName="Delete" CommandArgument='<%#Eval("site_id")%>'><img src="../../Images/delete.png" / height="20">Delete</asp:LinkButton>
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
        </div>
        <div style="text-align: right; padding-right: 16px;">
            <asp:Label ID="gridStatusLabel" runat="server" Text="" ForeColor="#CC3300"></asp:Label>
        </div>
    </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:LocalDatabaseConnectionString %>" DeleteCommand="DELETE FROM [Site] WHERE [site_id] = @site_id" InsertCommand="INSERT INTO [Site] ([site_name], [site_description]) VALUES (@site_name, @site_description)" SelectCommand=" SELECT Site.site_id as site_id,site_name,site_description,address,phone_number,postal_code,city FROM [Site] left join Users on Users.site_id=Site.site_id WHERE ([site_name] LIKE '%' + @site_name + '%') group by Site.site_id,site_name,site_description,address,phone_number,postal_code,city;" UpdateCommand="UPDATE [Site] SET [site_name] = @site_name, [site_description] = @site_description,[address]=@address,[phone_number]=@phone_number,[postal_code]=@postal_code WHERE [site_id] = @site_id">
        <DeleteParameters>
            <asp:Parameter Name="site_id" DbType="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="site_name" Type="String" />
            <asp:Parameter Name="site_description" Type="String" />
        </InsertParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="FilterSiteTextBox" DefaultValue="%" Name="site_name" PropertyName="Text" Type="String" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="site_name" DbType="String" />
            <asp:Parameter Name="site_description"  DbType="String"/>
            <asp:Parameter Name="site_id"  DbType="String"/>
            <asp:Parameter Name="address"  DbType="String"/>
            <asp:Parameter Name="phone_number" DbType="String"/>
            <asp:Parameter Name="postal_code" DbType="String"/>
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>
