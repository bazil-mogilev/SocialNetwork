<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<SocialNetwork.Models.DialogModel>>" %>

<div style="height:250px; overflow-y:scroll">
<table width = "95%" >
    <% foreach (var item in Model) { %>
   <tr>
        <td width = "30%" rowspan = "2">
        	<% if ((item.SenderID == (ViewData["AccountID"] as int?)))
            {%>
                  <b><%: item.SenderUsername%></b>
            <%}
            else
            {%>
                  <b><%: item.SenderUsername%></b>
            <%} %>
            <br /><%: String.Format("{0:g}", item.DateTime)%>
        </td>
		<td width = "70%">	
        	<b><%: item.Subject %></b>
        </td>
    </tr>
    <tr>
        <td width = "70%">	
            <%: item.MessageText %>
        </td>
    </tr>
    <% } %>
</table>
</div>
   
    
