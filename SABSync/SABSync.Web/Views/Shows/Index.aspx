<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	MyShows
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>MyShows</h2>

    <table border="1" width ="100%">
        <tr>
            <td>
                <strong>ID</strong>
            </td>
            <td>
                <strong>Show Name</strong>
            </td>
            <td>
                <strong>TVDB ID</strong>
            </td>
            <td>
                <strong>TVDB Name</strong>
            </td>
            <td>
                <strong>Quality</strong>
            </td>
            <td>
                <strong>Ignore Season</strong>
            </td>
            <td>
                <strong>Aliases</strong>
            </td>
            <td>
                <strong>Air Day</strong>
            </td>
            <td>
                <strong>Air Time</strong>
            </td>
            <td>
                <strong>Status</strong>
            </td>
<%--            <td>
                <strong>Genre</strong>
            </td>  --%>          
        </tr>
        <% foreach (var item in ViewData.Model)

       { %>
        <tr>
            <td>
                <%= item.id %>
            </td>
            <td>
                <%= item.show_name %>
            </td>
            <td>
                <%=item.tvdb_id %>
            </td>
            <td>
                <%=item.tvdb_name %>
            </td>
            <td>
                <%=item.quality.ToString().Replace("0", "Best Possible").Replace("1", "xvid").Replace("2", "720p") %>
            </td>
            <td>
                <%=item.ignore_season %>
            </td>
            <td nowrap="nowrap"> <%--Don't wrap lines--%>
                <%=item.aliases.Replace(";", "<BR>") %>
            </td>
            <td>
                <%=item.air_day %>
            </td>
            <td>
                <%=item.air_time %>
            </td>
            <td>
                <%=item.status %>
            </td>
<%--            <td>
                <%=item.genre %>
            </td>--%>
        </tr>
        <% } %>
    </table>

</asp:Content>
