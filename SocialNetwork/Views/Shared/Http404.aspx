<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	404 Not Found
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Error HTTP 404</h1>
    <br />
    
    <p>Sorry, the page you have requested is not available or was deleted from server.</p>
    
    <p>
    Return to <%= @Html.ActionLink("home page", "Index", "Home")%>
    </p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="RightColumnContent" runat="server">
</asp:Content>
