using System;
using System.Xml;

namespace SABSync
{
    internal class TvRage
    {
        private static readonly Logger Logger = new Logger();

        internal static string CheckTvRage(string showName, int seasonNumber, int episodeNumber)
        {
            string episodeName = null;
            string seriesId = GetShowId(showName);

            if (seriesId != null)
                episodeName = GetEpisodeName(seriesId, seasonNumber, episodeNumber);

            return episodeName;
        }

        internal static string CheckTvRage(string showName, int year, int month, int day)
        {
            string episodeName = null;
            string seriesId = GetShowId(showName);

            if (seriesId != null)
                episodeName = GetEpisodeName(seriesId, year, month, day);

            return episodeName;
        }

        internal static string GetShowId(string showName)
        {
            string showId = null;

            try
            {
                string url = "http://services.tvrage.com/feeds/search.php?show=" + showName;

                var tvRageRssReader = new XmlTextReader(url);
                var tvRageRssDoc = new XmlDocument();
                tvRageRssDoc.Load(tvRageRssReader);

                XmlNodeList data = tvRageRssDoc.GetElementsByTagName(@"Results");

                if (data.Count != 0)
                {
                    XmlNodeList show = ((XmlElement) data[0]).GetElementsByTagName("show");

                    if (show.Count == 0)
                    {
                        Logger.Log("No Series Found");
                        return showId;
                    }

                    foreach (object s in show)
                    {
                        var tvRageElement = (XmlElement) s;

                        string tvRageShowName = tvRageElement.GetElementsByTagName("SeriesName")[0].InnerText.ToLower();

                        if (tvRageShowName.ToLower() == showName.ToLower())
                        {
                            showId = tvRageElement.GetElementsByTagName("showid")[0].InnerText.ToLower();
                            return showId;
                        }
                    }
                    var tvRageElementLast = (XmlElement) show.Item(0);
                    showId = tvRageElementLast.GetElementsByTagName("showid")[0].InnerText.ToLower();
                    return showId;
                }
            }
            catch (Exception ex)
            {
                Logger.Log("An Error has occurred while get the Series ID: " + ex);
            }
            return null;
        }

        internal static string GetEpisodeName(string showId, int seasonNumber, int episodeNumber)
        {
            return null;
        }

        internal static string GetEpisodeName(string showId, int year, int month, int day)
        {
            return null;
        }
    }
}