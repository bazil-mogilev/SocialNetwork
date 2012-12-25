<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Accept Friend
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Are you sure want to include this user to your friends?</h2>
    <br/>

    <fieldset>
                
        <table>

        <tr>
            <td>
                <div class="display-label">Username</div>
            </td>
            <td>
                <div class="display-label"><%: Model.Username %></div>
            </td>
        </tr>
                <tr>
            <td>
                <div class="display-label">First Name</div>
            </td>
            <td>
                <div class="display-label"><%: Model.FirstName %></div>
            </td>
        </tr>
        <tr>
            <td>          
                <div class="display-label">Last Name</div>
            </td>
            <td>
                <div class="display-label"><%: Model.LastName %></div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="display-label">Country</div>
            </td>
            <td>
                <div class="display-label"><%: Model.Country %></div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="display-label">City</div>
            </td>
            <td>
                <div class="display-label"><%: Model.City %></div>
            </td>
        </tr>
        </table>
    
    <br />

    </fieldset>
    <% using (Html.BeginForm()) { %>
        <p>
		    <input type="submit" value="Accept" /> |
		    <%: Html.ActionLink("Back to List", "Index", new { id = ViewData["AccountID"]}) %>
        </p>
    <% } %>

</asp:Content>

