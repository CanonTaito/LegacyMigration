<%@ Page Title="Search" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Search.aspx.cs" Inherits="PropertyList.WebForms.Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Search Properties</h2>
    <div class="row mb-3">
        <div class="col-md-6">
            <label for="txtKeyword">Keyword</label>
            <asp:TextBox ID="txtKeyword" runat="server" CssClass="form-control" Placeholder="e.g. pool, family, views" />
        </div>
        <div class="col-md-4">
            <label for="ddlType">Property Type</label>
            <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control">
                <asp:ListItem Text="All Types" Value="" />
                <asp:ListItem Text="House" Value="House" />
                <asp:ListItem Text="Apartment" Value="Apartment" />
            </asp:DropDownList>
        </div>
        <div class="col-md-2 d-flex align-items-end">
            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary w-100" OnClick="btnSearch_Click" />
        </div>
    </div>

    <asp:Label ID="lblError" runat="server" CssClass="text-danger" Visible="false" />
    <asp:Label ID="lblTips" runat="server" CssClass="text-info" Visible="false" />

    <asp:GridView ID="SearchResults" runat="server" AutoGenerateColumns="False"
        CssClass="table table-striped" EmptyDataText="No properties match your search.">
        <Columns>
            <asp:BoundField DataField="Address" HeaderText="Address" />
            <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:C}" />
            <asp:BoundField DataField="Bedrooms" HeaderText="Beds" />
            <asp:BoundField DataField="PropertyType" HeaderText="Type" />
        </Columns>
    </asp:GridView>
</asp:Content>