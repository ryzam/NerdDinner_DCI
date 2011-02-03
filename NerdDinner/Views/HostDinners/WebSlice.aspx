<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<NerdDinner.Models.Domains.HostDinner>>" ContentType="text/html" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/html401/strict.dtd">

<html>
<head>
    <title><%: ViewData["Title"] %></title>
</head>
<body style="margin: 0">
    <div class="hslice" id="webslice" style="width: 320px">
        <div class="entry-content">
            <center><h2 class="entry-title"><%: ViewData["Title"] %></h2></center>
            <div>
                <ul>
                    <% foreach (var dinner in Model) { %>
                    <li style="list-style-type: none;">
                        <%: Html.ActionLink(dinner.DinnerDetail.Title, "Details", "HostDinners", new { id=dinner.Id }) %>
                        on <strong>
                            <%: dinner.DinnerDetail.EventDate.ToString("yyyy-MMM-dd")%>
                            <%: dinner.DinnerDetail.EventDate.ToString("HH:mm tt")%></strong> at
                        <%: dinner.DinnerDetail.Address.Address1 + " " + dinner.DinnerDetail.Address.Country %>
                    </li>
                    <% } %>
                </ul>
            </div>
        </div>
        <a rel="Bookmark" href="http://www.nerddinner.com" style="display:none;"></a>
    </div>
</body>
</html>
