<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<SocialNetwork.Models.Account>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Accounts
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% using (Html.BeginForm()) {
    %> 
        <h2>Start typing any name or word:</h2> 
        <div class="editor-field">
                    <%= Html.TextBox("SearchString", (ViewData["CurrentFilter"] as string)) %>
        </div>   
        <input type="submit" value="Search" /> &nbsp;

    <%} %>

    <br />

<h1>Accounts</h1>
<br/>

<div style="height:450px; overflow-y:scroll">
    <table>
        <tr>
            <th></th>
            <th>
                ID
            </th>
            <th>
                Username
            </th>
            <th>
                Email
            </th>
            <th>
                Activated
            </th>
            <th>
                First Name
            </th>
            <th>
                Last Name
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <th>
                <%= Html.RouteLink("Edit", new { id = item.AccountID, controller = "UserProfile", action = "Edit" })%> |
                <%: Html.ActionLink("Delete", "DeleteAccount", new { id=item.AccountID })%> |
                <%= Html.RouteLink("Message", new { id = item.AccountID, controller = "Dialog", action = "Index" })%>
            </th>
            <td>
                <%: item.AccountID %>
            </td>
            <td>
                <%= Html.RouteLink(item.Username, new { id = item.AccountID, controller = "UserProfile", action = "Index" })%> 
            </td>
            <td>
                <%: item.Email %>
            </td>
            <td>
                <%: item.IsActivated %>
            </td>
            <td>
                <%: item.FirstName %>
            </td>
            <td>
                <%: item.LastName %>
            </td>
        </tr>
    
    <% } %>

    </table>
</div>

</asp:Content>

