<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Contact.aspx.cs" Inherits="PropertyList.WebForms.Contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Contact Us</h2>
    <p>Interested in a property? Fill out the form below and we'll get back to you.</p>

    <asp:Label ID="lblSuccess" runat="server" CssClass="alert alert-success d-none" />

    <asp:Panel ID="pnlForm" runat="server">
        <div class="mb-3">
            <label for="txtName">Name *</label>
            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                ErrorMessage="Name is required." CssClass="text-danger" Display="Dynamic" />
        </div>
        <div class="mb-3">
            <label for="txtEmail">Email *</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                ErrorMessage="Email is required." CssClass="text-danger" Display="Dynamic" />
            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"
                ErrorMessage="Please enter a valid email." CssClass="text-danger" Display="Dynamic" />
        </div>
        <div class="mb-3">
            <label for="ddlPropertyRef">Property Reference</label>
            <asp:DropDownList ID="ddlPropertyRef" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                <asp:ListItem Text="-- Select a property --" Value="" />
            </asp:DropDownList>
        </div>
        <div class="mb-3">
            <label for="txtMessage">Message *</label>
            <asp:TextBox ID="txtMessage" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" />
            <asp:RequiredFieldValidator ID="rfvMessage" runat="server" ControlToValidate="txtMessage"
                ErrorMessage="Message is required." CssClass="text-danger" Display="Dynamic" />
        </div>
        <asp:Button ID="btnSubmit" runat="server" Text="Send Message" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
    </asp:Panel>
</asp:Content>