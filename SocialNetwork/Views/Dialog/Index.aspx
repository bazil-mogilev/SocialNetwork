<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SocialNetwork.Models.ConversationModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Dialogs
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Dialog:</h2>
    <% using (Html.BeginForm())
	{ %>
	 
    <%= Html.Hidden("_convModel", ViewData["_convModel"]) %>
        <fieldset> 
            <legend>Dialog with [<%: Model.InterlocutorUsername %>]</legend>
       

        <% Html.RenderPartial("DialogControl", Model.Dialogs); %>
    </fieldset>

    <fieldset>
        <legend>New message:</legend>
        <% Html.RenderPartial("SendMessageControl", Model.NewMessage ); %>
        <br />
        <%: Html.ValidationSummary(true, "") %>    
    </fieldset>

    <%} %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="RightColumnContent" runat="server">

    <% Html.RenderPartial("InterlocutorsControl", Model.Interlocutors ); %>
</asp:Content>
