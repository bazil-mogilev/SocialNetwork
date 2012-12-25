<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SocialNetwork.Models.UsersListModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	My Friends
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Find new friends</h1>
    <br/>

    <% using (Html.BeginForm()) {
    %> 
        <h2>Start typing any name or word:</h2> 
        <div class="editor-field">
                    <%= Html.TextBox("SearchString", (ViewData["CurrentFilter"] as string)) %>
        </div>   
        <input type="submit" value="Search" /> &nbsp;

    <%} %>

    <br />
    <h3>Peoples on social network:</h3>
    <% Html.RenderPartial("UsersListControl", Model.Users ); %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="RightColumnContent" runat="server">
    

    <h2> Friends </h2>
    <% Html.RenderPartial("FriendsControl", Model.Friendship.Friends ); %>

    <br />
    <h2> Requests on friendship </h2>
    <% Html.RenderPartial("FriendsControl", Model.Friendship.RequestsFor); %>

    <br />
    <h2> My requests on friendship</h2>
    <% Html.RenderPartial("FriendsControl", Model.Friendship.RequestsFrom); %>
    

</asp:Content>


