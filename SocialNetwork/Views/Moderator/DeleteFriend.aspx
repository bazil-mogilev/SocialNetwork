<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SocialNetwork.Models.FriendshipListModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Delete Friend
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Delete friendship</h1>

    <h3>Are you sure you want to delete this?</h3>
    <fieldset>
    <table> 
    <tr>
        <td>
            <div class="display-label">First ID</div>
        </td>
        <td>
            <div class="display-field"><%: Model.FirstID %></div>
        </td>
    </tr><tr>
        <td>
            <div class="display-label">First Username</div>
        </td>
        <td>
            <div class="display-field"><%: Model.FirstUsername %></div>
        </td>
    </tr><tr>
        <td>
            <div class="display-label">Second ID</div>
        </td>
        <td>
            <div class="display-field"><%: Model.SecondID %></div>
        </td>
    </tr><tr>
        <td>
            <div class="display-label">Second Username</div>
        </td>
        <td>
            <div class="display-field"><%: Model.SecondUsername %></div>
        </td>
    </tr><tr>
        <td>
            <div class="display-label">Is Friend</div>
        </td>
        <td>
            <div class="display-field"><%: Model.IsFriend %></div>
        </td>
    </tr><tr>
        <td>
            <div class="display-label">Request From</div>
        </td>
        <td>
            <div class="display-field"><%: Model.RequestFrom %></div>
        </td>
    </tr><tr>
    </table>
    </fieldset>
    <% using (Html.BeginForm()) { %>
        <p>
		    <input type="submit" value="Delete" /> |
		    <%: Html.ActionLink("Back to List", "Index") %>
        </p>
    <% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="RightColumnContent" runat="server">
</asp:Content>

