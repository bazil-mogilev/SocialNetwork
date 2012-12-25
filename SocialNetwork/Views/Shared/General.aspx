<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<System.Web.Mvc.HandleErrorInfo>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Site Error
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Sorry, an error occurred while processing your request.</h1>
    <br />

    <p style="font-weight: bold;"> <%= Model.Exception.Message%></p>

    <p>
    Return to <%= @Html.ActionLink("home page", "Index", "Home")%>
    </p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="RightColumnContent" runat="server">
</asp:Content>
