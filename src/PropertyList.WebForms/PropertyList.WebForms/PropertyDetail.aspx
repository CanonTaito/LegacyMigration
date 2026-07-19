<%@ Page Title="Property Detail" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="PropertyDetail.aspx.cs" Inherits="PropertyList.WebForms.PropertyDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblAddress" runat="server" CssClass="h3"></asp:Label>
    <br /><br />
    <asp:Label ID="lblError" runat="server" CssClass="text-danger" Visible="false" />
    <div class="row">
        <div class="col-md-6">
            <img src="" alt="Property" id="imgProperty" runat="server" class="img-fluid rounded" />
        </div>
        <div class="col-md-6">
            <dl class="row">
                <dt class="col-sm-4">Price</dt>
                <dd class="col-sm-8"><asp:Label ID="lblPrice" runat="server" /></dd>
                <dt class="col-sm-4">Bedrooms</dt>
                <dd class="col-sm-8"><asp:Label ID="lblBedrooms" runat="server" /></dd>
                <dt class="col-sm-4">Type</dt>
                <dd class="col-sm-8"><asp:Label ID="lblType" runat="server" /></dd>
            </dl>
            <p><asp:Label ID="lblDescription" runat="server" CssClass="text-muted" /></p>
        </div>
    </div>
    <br />
    <asp:HyperLink ID="lnkBack" runat="server" NavigateUrl="~/Default.aspx" CssClass="btn btn-secondary">Back to List</asp:HyperLink>
</asp:Content>