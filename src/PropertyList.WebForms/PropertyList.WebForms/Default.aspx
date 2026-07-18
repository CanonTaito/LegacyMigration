<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="Default.aspx.cs" Inherits="PropertyList.WebForms.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Available Properties</h2>

    <asp:GridView ID="PropertyGrid" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="Id"
        OnRowCommand="PropertyGrid_RowCommand" CssClass="table table-striped table-hover"
        EmptyDataText="No properties available.">
        <Columns>
            <asp:BoundField DataField="Address" HeaderText="Address" />
            <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:C}" />
            <asp:BoundField DataField="Bedrooms" HeaderText="Beds" />
            <asp:BoundField DataField="PropertyType" HeaderText="Type" />
            <asp:ButtonField CommandName="ViewDetail" Text="View" ControlStyle-CssClass="btn btn-sm btn-primary" />
        </Columns>
    </asp:GridView>
</asp:Content>