<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    if (Request.IsAuthenticated)
    {
        if (Roles.IsUserInRole("Moderator"))
        { 
%>
        <li><%= Html.RouteLink("Accounts", new {action = "Accounts", controller = "Moderator"})%></li>
        <li><%= Html.RouteLink("Friends", new {action = "Friends", controller = "Moderator"})%></li>
        <li><%= Html.RouteLink("Dialogs", new {action = "Dialogs", controller = "Moderator"})%></li>
<%
    }
        else
        {
%>
        <li><%= Html.RouteLink("My Profile", new {action = "Index", controller = "UserProfile", id = Model})%></li>
        <li><%= Html.RouteLink("Find Friends", new { action = "Index", controller = "Friend", id = Model})%></li>
        <li><%= Html.RouteLink("My Dialogs", new { action = "Index", controller = "Dialog", id = Model})%></li>
<%            
    }
    }
    else
    {
%> 
        <li><%= Html.RouteLink("Home", new {action = "Index", controller = "Home"})%></li>
        <li><%= Html.RouteLink("About", new {action = "About", controller = "Home"})%></li>
<%
    }
%>
  
            