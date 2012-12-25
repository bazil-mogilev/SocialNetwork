<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	PasswordReset
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Password Reset</h1>
    Enter your account information below and click continue...
    <% using (Html.BeginForm()) { %>
        <%= Html.ValidationSummary() %>
        <div>
            <fieldset>
                <legend>Account Information</legend>
                
                <div class="editor-label">
                    User name: 
                </div>
                <div class="editor-field">
                    <%= Html.TextBox("UserName") %> 
                </div>
                                               
                <p><input type="submit" value="Continue..." /></p>
            </fieldset>
        </div>
    <% } %>


</asp:Content>
