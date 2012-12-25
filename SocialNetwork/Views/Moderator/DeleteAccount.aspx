<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SocialNetwork.Models.Account>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Delete
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Delete user</h1>

    Will be deleting all information about current user (user profile, friendship, dialogs).
    
    <h3>Are you sure want to delete this?</h3>
    <fieldset>
    <table>
    <tr>
        <td>   
            <div class="display-label">AccountID</div>
        </td>
        <td>
            <div class="display-field"><%: Model.AccountID %></div>
        </td>
    <tr></tr>
        <td>
            <div class="display-label">Username</div>
        </td>
        <td>
            <div class="display-field"><%: Model.Username %></div>
        </td>
    <tr></tr>
        <td>
            <div class="display-label">Email</div>
        </td>
        <td>
            <div class="display-field"><%: Model.Email %></div>
        </td>
    <tr></tr>
        <td>
            <div class="display-label">IsActivated</div>
        </td>
        <td>
            <div class="display-field"><%: Model.IsActivated %></div>
        </td>
    <tr></tr>
        <td>
            <div class="display-label">FirstName</div>
        </td>
        <td>
            <div class="display-field"><%: Model.FirstName %></div>
        </td>
    <tr></tr>
        <td>
            <div class="display-label">LastName</div>
        </td>
        <td>
            <div class="display-field"><%: Model.LastName %></div>
        </td>
    <tr></tr>
        <td>
            <div class="display-label">Country</div>
        </td>
        <td>
            <div class="display-field"><%: Model.Country %></div>
        </td>
    <tr></tr>
        <td>
            <div class="display-label">City</div>
        </td>
        <td>
            <div class="display-field"><%: Model.City %></div>
        </td>
    <tr></tr>
        <td>
            <div class="display-label">DateBirthday</div>
        </td>
        <td>
            <div class="display-field"><%: String.Format("{0:d}", Model.DateBirthday) %></div>
        </td>
    </tr>
    </table>

    </fieldset>
    <% using (Html.BeginForm()) { %>
        <p>
		    <input type="submit" value="Delete" /> |
		    <%: Html.ActionLink("Back to List", "Accounts") %>
        </p>
    <% } %>

</asp:Content>

