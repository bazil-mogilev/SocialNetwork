<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Welcome
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

 <h1>Welcome</h1>

 <p>Thank you for registering!</p>

 <p>Activation email has been sent to you. Click on the link in
 the email to activate your account!</p>

 <p>Click <%= Html.ActionLink("here", "LogOn", "Account") %> to login.</p>

</asp:Content>
