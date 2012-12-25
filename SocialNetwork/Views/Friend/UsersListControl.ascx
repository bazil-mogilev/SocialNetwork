<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<SocialNetwork.Models.UserProfileModel>>" %>
<div style="height:450px; overflow-y:scroll">
    <table>
        <tr>
            <th></th>

            <th>
                Username
            </th>
            <th>
                Email
            </th>
            <th>
                FirstName
            </th>
            <th>
                LastName
            </th>
            <th>
                Country
            </th>
            <th>
                City
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <th>
                <%: Html.ActionLink("Add", "SendRequest", new { id = item.UserInfo.AccountID })%> |
                <%= Html.RouteLink("View", new { id = item.UserInfo.AccountID, controller = "UserProfile", action = "Index" })%>
            </th>
            <td>
                <%: item.UserInfo.Username %>
            </td>
            <td>
                <%: item.UserInfo.Email%>
            </td>
            <td>
                <%: item.UserInfo.FirstName%>
            </td>
            <td>
                <%: item.UserInfo.LastName%>
            </td>
            <td>
                <%: item.UserInfo.Country%>
            </td>
            <td>
                <%: item.UserInfo.City%>
            </td>
        </tr>
    
    <% } %>

    </table>
    </div>