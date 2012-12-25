<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SocialNetwork.Models.UserProfileModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	My profile
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1><%: Model.UserInfo.FirstName %>  <%: Model.UserInfo.LastName%> </h1>
        
    <fieldset>
                
                <table>
        <tr>
            <td>
                <div class="display-label" visible="false">AccountID</div>
            </td>
            <td>
                <div class="display-label" visible="false"><%: Model.UserInfo.AccountID%></div>
            </td>            
        </tr>
        <tr>
            <td>
                <div class="display-label">Username</div>
            </td>
            <td>
                <div class="display-label"><%: Model.UserInfo.Username%></div>
            </td>
        </tr>
                <tr>
            <td>
                <div class="display-label">First Name</div>
            </td>
            <td>
                <div class="display-label"><%: Model.UserInfo.FirstName%></div>
            </td>
        </tr>
        <tr>
            <td>          
                <div class="display-label">Last Name</div>
            </td>
            <td>
                <div class="display-label"><%: Model.UserInfo.LastName%></div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="display-label">Email</div>
            </td>
            <td>
                <div class="display-label"><%: Model.UserInfo.Email%></div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="display-label">Country</div>
            </td>
            <td>
                <div class="display-label"><%: Model.UserInfo.Country%></div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="display-label">City</div>
            </td>
            <td>
                <div class="display-label"><%: Model.UserInfo.City%></div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="display-label">Date Birthday</div>
            </td>
            <td>
                <div class="display-label"><%: String.Format("{0:d}", Model.UserInfo.DateBirthday)%></div>
            </td>
        </tr>
        </table>
    
    <br />
    
    <% if ((Model.UserInfo.AccountID == (ViewData["AccountID"] as int?)) || (Page.User.IsInRole("Moderator")))
       { %>
    <%= Html.RouteLink("Edit profile", new { action = "Edit", controller = "UserProfile", id = Model.UserInfo.AccountID })%>
    <%} %>

    </fieldset>
</asp:Content>    
    
<asp:Content ID="Content3" ContentPlaceHolderID="RightColumnContent" runat="server">
    <h2> Friends </h2>
    
    <% Html.RenderPartial("FriendsControl", Model.Friendship.Friends ); %>


    <% if (Model.UserInfo.AccountID == (ViewData["AccountID"] as int?))
       { %>
    
        <br />
        <h2> Requests on friendship </h2>
        <% Html.RenderPartial("FriendsControl", Model.Friendship.RequestsFor); %>

        <br />
        <h2> My requests on friendship</h2>
        <% Html.RenderPartial("FriendsControl", Model.Friendship.RequestsFrom); %>

     <%} %>

</asp:Content>

