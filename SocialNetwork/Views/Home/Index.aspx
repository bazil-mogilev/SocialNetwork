<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%: ViewData["Message"] %></h1>
    <br/>
    <p>
    <%: Html.ActionLink("Logon", "LogOn", "Account") %> to used all services.
    </p>
    <p>
    <%: Html.ActionLink("Register", "Register", "Account") %> if you don't have an account.
    </p>
</asp:Content>
