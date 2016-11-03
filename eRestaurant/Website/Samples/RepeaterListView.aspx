<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="RepeaterListView.aspx.cs" Inherits="Samples_RepeaterListView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>Nested Repeater using Html markup</h1>
    <asp:Repeater ID="Repeater1" runat="server" DataSourceID="MenuCategoryFoodItemsODS">
        <ItemTemplate>
            <h2><%# Eval("MenuCategoryDescription") %></h2>
            <!-- we need to deal with a collection inside the current
                row that is being display from the dataset (DTO) -->
            <asp:Repeater ID="NestedRepeater" runat="server" DataSource='<%# Eval("FoodItems") %>'>
                <ItemTemplate>
                    <%# Eval("ItemID") %>&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="label1" runat="server" 
                         Text='<%# Eval("FoodDescription") %>'></asp:Label>&nbsp;&nbsp;&nbsp;
                    <%# string.Format("{0:0.00}",Eval("CurrentPrice")) %>&nbsp;&nbsp;&nbsp;
                    <span class="badge"><%# Eval("TimesServed") %> Servings</span><br />
                </ItemTemplate>
            </asp:Repeater>
        </ItemTemplate>
    </asp:Repeater>
    <h1>Repeater using a ListView</h1>
     <asp:Repeater ID="Repeater2" runat="server" DataSourceID="MenuCategoryFoodItemsODS">
        <ItemTemplate>
            <h2><%# Eval("MenuCategoryDescription") %></h2>
             <asp:ListView ID="ListView1" runat="server" DataSource='<%# Eval("FoodItems") %>'>
        <AlternatingItemTemplate>
            <tr style="background-color: #FFFFFF; color: #284775;">
               
                <td>
                    <asp:Label Text='<%# Eval("ItemID") %>' runat="server" ID="ItemIDLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("FoodDescription") %>' runat="server" ID="FoodDescriptionLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("CurrentPrice") %>' runat="server" ID="CurrentPriceLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("TimesServed") %>' runat="server" ID="TimesServedLabel" /></td>
            </tr>
        </AlternatingItemTemplate>
       
        <EmptyDataTemplate>
            <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                <tr>
                    <td>No data was returned.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
       
        <ItemTemplate>
            <tr style="background-color: #E0FFFF; color: #333333;">
               
                <td>
                    <asp:Label Text='<%# Eval("ItemID") %>' runat="server" ID="ItemIDLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("FoodDescription") %>' runat="server" ID="FoodDescriptionLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("CurrentPrice") %>' runat="server" ID="CurrentPriceLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("TimesServed") %>' runat="server" ID="TimesServedLabel" /></td>
            </tr>
        </ItemTemplate>
        <LayoutTemplate>
            <table runat="server">
                <tr runat="server">
                    <td runat="server">
                        <table runat="server" id="itemPlaceholderContainer" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;" border="1">
                            <tr runat="server" style="background-color: #E0FFFF; color: #333333;">
                                
                                <th runat="server">ID</th>
                                <th runat="server">Item</th>
                                <th runat="server">$</th>
                                <th runat="server">Servings</th>
                            </tr>
                            <tr runat="server" id="itemPlaceholder"></tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server">
                    <td runat="server" style="text-align: center; background-color: #5D7B9D; font-family: Verdana, Arial, Helvetica, sans-serif; color: #FFFFFF">
                        <asp:DataPager runat="server" ID="DataPager1">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowLastPageButton="True"></asp:NextPreviousPagerField>
                            </Fields>
                        </asp:DataPager>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
       
    </asp:ListView>
        </ItemTemplate>
    </asp:Repeater>

    <asp:ObjectDataSource ID="MenuCategoryFoodItemsODS" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="MenuCategoryFoodItemsDTO_Get" 
        TypeName="eRestaurantSystem.BLL.ItemsController">
    </asp:ObjectDataSource>
</asp:Content>

