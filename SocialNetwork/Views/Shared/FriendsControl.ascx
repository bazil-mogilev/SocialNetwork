<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<SocialNetwork.Models.FriendsListModel>>" %>

    <table>
        <tr>
            <th>Friend name</th>
            <th>
                Action
            </th>
        </tr>


    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.RouteLink(item.FirstName + " " +item.LastName, new { id = item.FriendID, controller = "UserProfile", action = "Index" })%>
            </td>
            <td> 
                <% if ((item.AccountID == (ViewData["AccountID"] as int?)))
                   {
                        if (item.IsFriend == true) 
                        {%>
                            | <%= Html.RouteLink("Delete", new { id = item.FriendID, controller = "Friend", action = "DeleteFriend" })%>
                        <%}
                        else
                        {
                              if (item.IsMyRequest == true)
                              {%>
                                    | <%= Html.RouteLink("Denied", new { id = item.FriendID, controller = "Friend", action = "DeleteFriend" })%>
                              <%}
                              else
                              {%>
                                    | <%= Html.RouteLink("Accept", new { id = item.FriendID, controller = "Friend", action = "AcceptFriend" })%>
                                    | <%= Html.RouteLink("Denied", new { id = item.FriendID, controller = "Friend", action = "DeleteFriend" })%>                                   
                              <%}
                        }
                   } %>
            </td>
        </tr>
    <%
    }
    %>
    </table>