<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/SelectedTestDemo.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="Application.Pharmacist.Report" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Text="Report" CssClass="pageTitle"></asp:Label>
    </div>
    <div>
        <asp:RadioButtonList ID="ViewOptionsRBL" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="ViewOptionsRBL_SelectedIndexChanged">
            <asp:ListItem Value="10" Selected="True">Past 10 visits</asp:ListItem>
            <asp:ListItem Value="20">Past 20 visits</asp:ListItem>
            <asp:ListItem Value="50">Past 50 visits</asp:ListItem>
            <asp:ListItem Value="10000">All visits</asp:ListItem>
        </asp:RadioButtonList>
    </div>
    <div>
        <asp:Chart ID="WeightChart" runat="server" Height="400px" Width="800px">
            <Titles>
                <asp:Title Text="Weight By Date">
                </asp:Title>
            </Titles>
            <Series>
                <asp:Series Name="Series0" ChartType="Point" Color="Black" MarkerSize="10"></asp:Series>
                <asp:Series Name="Series1" ChartType="Line" Color="Black"></asp:Series>
                
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1" BackGradientStyle="TopBottom" BackSecondaryColor="#B6D6EC" BorderDashStyle="Solid" BorderWidth="2">
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
    </div>
    <div>
        <asp:Chart ID="SystolicChart" runat="server" Height="400px" Width="800px">
            <Titles>
                <asp:Title Text="Systolic By Date">
                </asp:Title>
            </Titles>
            <Series>
                <asp:Series Name="Series0" ChartType="Point" Color="Black" MarkerSize="10"></asp:Series>
                <asp:Series Name="Series1" ChartType="Line" Color="Black"></asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1" BackGradientStyle="TopBottom" BackSecondaryColor="#B6D6EC" BorderDashStyle="Solid" BorderWidth="2">
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
    </div>
    <div>
        <asp:Chart ID="DiastolicChart" runat="server" Height="400px" Width="800px">
            <Titles>
                <asp:Title Text="Diastolic By Date">
                </asp:Title>
            </Titles>
            <Series>
                <asp:Series Name="Series0" ChartType="Point" Color="Black" MarkerSize="10"></asp:Series>
                <asp:Series Name="Series1" ChartType="Line" Color="Black"></asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1" BackGradientStyle="TopBottom" BackSecondaryColor="#B6D6EC" BorderDashStyle="Solid" BorderWidth="2">
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
    </div>
    <div>
        <asp:Chart ID="CholesterolChart" runat="server" Height="400px" Width="800px">
            <Titles>
                <asp:Title Text="Cholesterol By Date">
                </asp:Title>
            </Titles>
            <Series>
                <asp:Series Name="Series0" ChartType="Point" Color="Black" MarkerSize="10"></asp:Series>
                <asp:Series Name="Series1" ChartType="Line" Color="Black"></asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1" BackGradientStyle="TopBottom" BackSecondaryColor="#B6D6EC" BorderDashStyle="Solid" BorderWidth="2">
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
    </div>
    <div>
        <asp:Chart ID="HDLChart" runat="server" Height="400px" Width="800px">
            <Titles>
                <asp:Title Text="HDL By Date">
                </asp:Title>
            </Titles>
            <Series>
                <asp:Series Name="Series0" ChartType="Point" Color="Black" MarkerSize="10"></asp:Series>
                <asp:Series Name="Series1" ChartType="Line" Color="Black"></asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1" BackGradientStyle="TopBottom" BackSecondaryColor="#B6D6EC" BorderDashStyle="Solid" BorderWidth="2">
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
    </div>
    <div>
        <asp:Chart ID="LDLChart" runat="server" Height="400px" Width="800px">
            <Titles>
                <asp:Title Text="LDL By Date">
                </asp:Title>
            </Titles>
            <Series>
                <asp:Series Name="Series0" ChartType="Point" Color="Black" MarkerSize="10"></asp:Series>
                <asp:Series Name="Series1" ChartType="Line" Color="Black"></asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1" BackGradientStyle="TopBottom" BackSecondaryColor="#B6D6EC" BorderDashStyle="Solid" BorderWidth="2">
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
    </div>
</asp:Content>
