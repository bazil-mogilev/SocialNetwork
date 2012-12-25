<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<SocialNetwork.Models.FriendshipListModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Friends
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h1>Friends</h1>
<br/>

<div style="height:450px; overflow-y:scroll">
    <table>
        <tr>
            <th></th>
            <th>
                FirstID
            </th>
            <th>
                FirstUsername
            </th>
            <th>
                SecondID
            </th>
            <th>
                SecondUsername
            </th>
            <th>
                IsFriend
            </th>
            <th>
                RequestFrom
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <th>
                <%: Html.ActionLink("Delete", "DeleteFriend", new { firstID = item.FirstID, secondID = item.SecondID })%>
            </th>
            <td>
                <%: item.FirstID %>
            </td>
            <td>
                <%= Html.RouteLink(item.FirstUsername, new { id = item.FirstID, controller = "UserProfile", action = "Index" })%> 
            </td>
            <td>
                <%: item.SecondID %>
            </td>
            <td>
                <%= Html.RouteLink(item.SecondUsername, new { id = item.SecondID, controller = "UserProfile", action = "Index" })%> 
            </td>
            <td>
                <%: item.IsFriend %>
            </td>
            <td>
                <%: item.RequestFrom %>
            </td>
        </tr>
    
    <% } %>

    </table>
</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="RightColumnContent" runat="server">
</asp:Content>

