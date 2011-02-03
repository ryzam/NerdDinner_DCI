<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NerdDinner.Models.Domains.HostDinner>" %>

<% if (Model.IsHostedBy(Context.User.Identity.Name)) { %>

    <%: Html.ActionLink("Edit Dinner", "Edit", new { id=Model.Id })%>
    |
    <%: Html.ActionLink("Delete Dinner", "Delete", new { id = Model.Id })%>    

<% } %>