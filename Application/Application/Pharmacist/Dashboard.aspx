<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/UnselectedTestDemo.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Application.Pharmacist.Dashboard" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:Label ID="TitleLabel" runat="server" Text="Dashboard" CssClass="pageTitle"></asp:Label>
    </div>
    <div>
        <div>
            <asp:Chart ID="GenderChart" runat="server" Width="600px" Height="400px">
                <Series>
                    <asp:Series Name="Series1"></asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1" BackGradientStyle="TopBottom" BackSecondaryColor="#B6D6EC" BorderDashStyle="Solid" BorderWidth="2">
                    </asp:ChartArea>                   
                </ChartAreas>
            </asp:Chart>
        </div>
        <div>
            <asp:RadioButtonList ID="ViewOptionsRBL" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="ViewOptionsRBL_SelectedIndexChanged">
                <asp:ListItem Value="30">Past 1 month</asp:ListItem>
                <asp:ListItem Value="90">Past 3 months</asp:ListItem>
                <asp:ListItem Value="180">Past 6 months</asp:ListItem>
                <asp:ListItem Value="365">Past 12 months</asp:ListItem>
                <asp:ListItem Value="730">Past 2 years</asp:ListItem>
                <asp:ListItem Selected="True" Value="0">All</asp:ListItem>
            </asp:RadioButtonList>
        </div>
        <div>
            <asp:Chart ID="PatientsChart" runat="server" Width="600px" Height="400px">
                <Series>
                    <asp:Series Name="Series1" Color="Black"></asp:Series>
                </Series>
                <Titles>
                    <asp:Title Text="Number of Patients from beginning"></asp:Title>
                </Titles>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1" BackGradientStyle="TopBottom" BackSecondaryColor="#B6D6EC" BorderDashStyle="Solid" BorderWidth="2">
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
        </div>
    </div>
</asp:Content>
