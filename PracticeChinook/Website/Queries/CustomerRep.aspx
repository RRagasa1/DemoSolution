<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CustomerRep.aspx.cs" Inherits="Queries_CustomerRep" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>Customer of Representitive</h1>
    <asp:Label ID="Label1" runat="server" Text="Select an employee"></asp:Label>&nbsp;&nbsp;
    <asp:DropDownList ID="EmployeeNameList" runat="server" 
        DataSourceID="EmployeeNameODS" DataTextField="Name" 
        DataValueField="EmployeeId">
    </asp:DropDownList>&nbsp;&nbsp;
    <asp:Button ID="Search" runat="server" Text="Search" />
    <br />
    <asp:GridView ID="CustomerForRepList" runat="server" AutoGenerateColumns="False" DataSourceID="CustomerForRepODS" AllowPaging="True">
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name"></asp:BoundField>
            <asp:BoundField DataField="City" HeaderText="City" SortExpression="City"></asp:BoundField>
            <asp:BoundField DataField="State" HeaderText="State" SortExpression="State"></asp:BoundField>
            <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone"></asp:BoundField>
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email"></asp:BoundField>
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="CustomerForRepODS" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="RepresentitiveCustomers_Get" 
        TypeName="ChinookSystem.BLL.CustomerController">
        <SelectParameters>
            <asp:ControlParameter ControlID="EmployeeNameList" PropertyName="SelectedValue" Name="employeeid" Type="Int32"></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="EmployeeNameODS" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="EmployeeName_Get" 
        TypeName="ChinookSystem.BLL.EmployeeController">
    </asp:ObjectDataSource>
</asp:Content>

