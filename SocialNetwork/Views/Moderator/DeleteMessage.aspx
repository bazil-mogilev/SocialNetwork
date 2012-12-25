<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SocialNetwork.Models.DialogModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	DeleteMessage
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1>DeleteMessage</h1>

    <h3>Are you sure you want to delete this?</h3>
    <fieldset>
    <table>
    <tr>
        <td>    
            <div class="display-label">Message ID</div>
        </td>
        <td>
            <div class="display-field"><%: Model.MessageID %></div>
        </td>
    </tr><tr>
        <td>
            <div class="display-label">Sender ID</div>
        </td>
        <td>
            <div class="display-field"><%: Model.SenderID %></div>
        </td>
    </tr><tr>
        <td>
            <div class="display-label">Sender Username</div>
        </td>
        <td>
            <div class="display-field"><%: Model.SenderUsername %></div>
        </td>
    </tr><tr>
        <td>
            <div class="display-label">Recepient ID</div>
        </td>
        <td>
            <div class="display-field"><%: Model.RecepientID %></div>
        </td>
    </tr><tr>
        <td>
            <div class="display-label">Recepient Username</div>
        </td>
        <td>
            <div class="display-field"><%: Model.RecepientUsername %></div>
        </td>
    </tr><tr>
        <td>
            <div class="display-label">Subject</div>
        </td>
        <td>
            <div class="display-field"><%: Model.Subject %></div>
        </td>
    </tr><tr>
        <td>
            <div class="display-label">Message Text</div>
        </td>
        <td>
            <div class="display-field"><%: Model.MessageText %></div>
        </td>
    </tr><tr>
        <td>
            <div class="display-label">DateTime</div>
        </td>
        <td>
            <div class="display-field"><%: String.Format("{0:g}", Model.DateTime) %></div>
        </td>
    </tr>
    </table>

    </fieldset>
    <% using (Html.BeginForm()) { %>
        <p>
		    <input type="submit" value="Delete" /> |
		    <%: Html.ActionLink("Back to List", "Dialogs") %>
        </p>
    <% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="RightColumnContent" runat="server">
</asp:Content>

