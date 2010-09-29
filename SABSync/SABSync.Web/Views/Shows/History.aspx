<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	History
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>History</h2>

        <table border="1" width ="100%">
        <tr>
            <td>
                <strong>Show Name</strong>
            </td>
            <td width = "55px">
                <strong>Season Number</strong>
            </td>
            <td width = "55px">
                <strong>Episode Number</strong>
            </td>
            <td>
                <strong>Episode Name</strong>
            </td>
<%--            <td>
                <strong>Feed Title</strong>
            </td>--%>
            <td>
                <strong>Quality</strong>
            </td>
            <td>
                <strong>Proper</strong>
            </td>
            <td>
                <strong>Provider</strong>
            </td>
            <td>
                <strong>Date</strong>
            </td>         
        </tr>
        <% foreach (var item in ViewData.Model)

       { %>
        <tr>
            <td>
                <%= item.ShowName %>
            </td>
            <td>
                <%=item.SeasonNumber %>
            </td>
            <td>
                <%=item.EpisodeNumber %>
            </td>
            <td>
                <%=item.EpisodeName %>
            </td>
<%--            <td style="WORD-BREAK:BREAK-ALL">
                <%=item.FeedTitle %>
            </td>--%>
            <td>
            <%=item.Quality.ToString().Replace("0", "Best Possible").Replace("1", "xvid").Replace("2", "720p")%>
            </td>
            <td>
                <%=item.Proper %>
            </td>
            <td>
                <%=item.Provider %>
            </td>
            <td>
                <%=item.Date %>
            </td>
        </tr>
        <% } %>
    </table>

</asp:Content>
