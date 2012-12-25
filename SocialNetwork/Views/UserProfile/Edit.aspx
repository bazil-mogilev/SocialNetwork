<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SocialNetwork.Models.UserInfoModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit profile
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1><%: Model.FirstName %>  <%: Model.LastName %> </h1>
    <br />

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true, "") %>
        <fieldset>
        
                <div class="editor-label" hidden="true">
                    <%: Html.LabelFor(model => model.AccountID) %>
                </div>  

                <div class="editor-field" hidden="true">
                    <%: Html.TextBoxFor(model => model.AccountID) %>
                    <%: Html.ValidationMessageFor(model => model.AccountID) %>
                </div>

        <table>
        <tr>
            <td>
                <div class="display-label">
                    <%: Html.LabelFor(model => model.Username) %>
                </div>
            </td>
            <td>
                <div class="display-label">
                    <div class="display-label"><%: Model.Username%></div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="editor-label">
                    <%: Html.LabelFor(model => model.FirstName) %>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.TextBoxFor(model => model.FirstName) %>
                    <%: Html.ValidationMessageFor(model => model.FirstName) %>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="editor-label">
                    <%: Html.LabelFor(model => model.LastName) %>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.TextBoxFor(model => model.LastName) %>
                    <%: Html.ValidationMessageFor(model => model.LastName) %>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="editor-label">
                    <%: Html.LabelFor(model => model.Email) %>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.TextBoxFor(model => model.Email) %>
                    <%: Html.ValidationMessageFor(model => model.Email) %>
                </div>
            </td>
        </tr>
        <tr>     
            <td>                      
                <div class="editor-label">
                    <%: Html.LabelFor(model => model.Country) %>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.TextBoxFor(model => model.Country) %>
                    <%: Html.ValidationMessageFor(model => model.Country) %>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="editor-label">
                    <%: Html.LabelFor(model => model.City) %>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.TextBoxFor(model => model.City) %>
                    <%: Html.ValidationMessageFor(model => model.City) %>
                </div>
            </td>
        </tr>
        <tr>
            <td> 
                <div class="editor-label">
                    <%: Html.LabelFor(model => model.DateBirthday) %>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.TextBoxFor(model => model.DateBirthday, String.Format("{0:d}", Model.DateBirthday)) %>
                    <%: Html.ValidationMessageFor(model => model.DateBirthday) %>
                </div>
            </td>
        </tr>
        </table>
        
            <br />

            <p>
                <input type="submit" value="Save""/>
            </p>
        </fieldset>

    <% } %>

    
        <% if (!User.IsInRole("Moderator"))
           {%>
           <div>
                <%= Html.RouteLink("Change password", new { action = "ChangePassword", controller = "Account" })%>
           </div>
         <%} %>
    

    <br />

    <div>
        <%= Html.RouteLink("Return to profile", new { action = "Index", controller = "UserProfile", Model.AccountID })%>
    </div>



</asp:Content>

