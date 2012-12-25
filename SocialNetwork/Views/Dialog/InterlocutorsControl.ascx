<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<SocialNetwork.Models.InterlocutorModel>>" %>

    <table>
        <tr>
            <th>Friends</th>
        </tr>

<% using (Html.BeginForm())
   { %>
    <% foreach (var item in Model)
       { %>
    
        <tr>
            <td>
                <%= Html.RouteLink(item.Name, new { id = item.accountID, controller = "Dialog", action = "Index" })%>
            </td>
        </tr>
    <%
    }
   }
    %>
    </table>