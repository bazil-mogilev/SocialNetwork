<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	QuestionAndAnswer
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Password Reset</h1>
    <p>Answer your security question below and click Reset Password</p>

    <% using (Html.BeginForm()) { %>
        <%= Html.ValidationSummary() %>
        <div>
            <fieldset>
                <legend>Password Recovery Question</legend>

                <div class="editor-label">
                User name: <%= ViewData["UserName"] %>
                </div>

                <div class="editor-label">
                Security Question: <%= ViewData["Question"] %>
                </div>

                <div class="editor-label">
                Answer: 
                </div>
                <div class="editor-field">
                    <%= Html.TextBox("Answer") %>
                </div>

                <p>
                    <input type="submit" value="Reset Password" />
                </p>
            </fieldset>
        </div>
    <% } %>

</asp:Content>
