<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<SocialNetwork.Models.DialogModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Dialogs
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h1>Dialogs</h1>
<br/>

<div style="height:450px; overflow-y:scroll">
    <table>
        <tr>
            <th></th>
            <th>
                ID
            </th>
            <th>
                Sender
            </th>
            <th>
                Recepient
            </th>
            <th>
                Subject
            </th>
            <th>
                MessageText
            </th>
            <th>
                DateTime
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <th>
                <%: Html.ActionLink("Delete", "DeleteMessage", new { id=item.MessageID  })%>
            </th>
            <td>
                <%: item.MessageID %> 
            </td>
            <td>
                <%= Html.RouteLink(item.SenderUsername, new { id = item.SenderID, controller = "UserProfile", action = "Index" })%> 
            </td>
            <td>
                <%= Html.RouteLink(item.RecepientUsername, new { id = item.RecepientID, controller = "UserProfile", action = "Index" })%>
            </td>
            <td>
                <%: item.Subject %>
            </td>
            <td>
                <%: item.MessageText %>
            </td>
            <td>
                <%: String.Format("{0:g}", item.DateTime) %>
            </td>
        </tr>
    
    <% } %>

    </table>
</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="RightColumnContent" runat="server">
</asp:Content>

