using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SABSync
{
    class TvRage
    {
        internal static string CheckTvRage(string showName, int seasonNumber, int episodeNumber)
        {
            try
            {
                string episodeName = "unknown";
                string seriesId = GetShowId(showName);

                if (seriesId != null)
                    episodeName = GetEpisodeName(seriesId, seasonNumber, episodeNumber);

                return episodeName;
            }
            catch (Exception)
            {

                throw;
            }
        }

        internal static string CheckTvRage(string showName, int year, int month, int day)
        {
            try
            {
                string episodeName = "unknown";
                string seriesId = GetShowId(showName);

                if (seriesId != null)
                    episodeName = GetEpisodeName(seriesId, year, month, day);

                return episodeName;
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal static string GetShowId(string showName)
        {
            string showId = null;

            try
            {
                string url = "http://services.tvrage.com/feeds/search.php?show=" + showName;

                XmlTextReader tvRageRssReader = new XmlTextReader(url);
                XmlDocument tvRageRssDoc = new XmlDocument();
                tvRageRssDoc.Load(tvRageRssReader);

                var data = tvRageRssDoc.GetElementsByTagName(@"Results");

                if (data.Count != 0)
                {
                    var show = ((XmlElement)data[0]).GetElementsByTagName("show");

                    if (show.Count == 0)
                    {
                        Program.Log("No Series Found");
                        return showId;
                    }

                    foreach (var s in show)
                    {
                        XmlElement tvRageElement = (XmlElement)s;

                        string tvRageShowName = tvRageElement.GetElementsByTagName("SeriesName")[0].InnerText.ToLower();

                        if (tvRageShowName.ToLower() == showName.ToLower())
                        {
                            showId = tvRageElement.GetElementsByTagName("showid")[0].InnerText.ToLower();
                            return showId;
                        }

                        else
                            continue;
                    }
                    XmlElement tvRageElementLast = (XmlElement)show.Item(0);
                    showId = tvRageElementLast.GetElementsByTagName("showid")[0].InnerText.ToLower();
                    return showId;
                }
            }
            catch (Exception ex)
            {
                Program.Log("An Error has occurred while get the Series ID: " + ex);
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