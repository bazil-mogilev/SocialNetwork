<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	PasswordResetFinal
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Password Reset</h1>
    <p>A new password has been created and e-mailed to you.</p>
    <p>Click <%= Html.ActionLink("here", "LogOn", "Account") %> to login with your new password.</p>

</asp:Content>
