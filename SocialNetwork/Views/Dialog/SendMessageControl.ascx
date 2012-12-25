<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<SocialNetwork.Models.MessageModel>" %>

<style type="text/css">
   TEXTAREA {
    width: 50%; /* Ширина в процентах */
    height: 100px; /* Высота в пикселах */
   }
  </style>

    <% using (Html.BeginForm()) {%>

            <div class="editor-label">
                <%: Html.LabelFor(model => model.Subject) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Subject) %>
                <%: Html.ValidationMessageFor(model => model.Subject) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.MessageText) %>
            </div>
            <div class="editor-field">
                <%= Html.TextAreaFor(model => model.MessageText) %>
                <%: Html.ValidationMessageFor(model => model.MessageText) %>
            </div>
 
            <br />

            <p>
                <input type="submit" value="Send message""/>
            </p>
    <% } %>