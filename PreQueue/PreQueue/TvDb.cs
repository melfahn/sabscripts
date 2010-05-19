using System;
using System.Xml;

namespace PreQueue
{
    internal class TvDb
    {
        private const string TvDbApiKey = "5D2D188E86E07F4F";
        private static readonly Logger logger = new Logger();

        internal static string CheckTvDb(string showName, int seasonNumber, int episodeNumber)
        {
            string episodeName = null;
            string seriesId = GetSeriesId(showName);

            if (seriesId != null)
                episodeName = GetEpisodeName(seriesId, seasonNumber, episodeNumber);

            return episodeName;
        }

        internal static string CheckTvDb(string showName, int year, int month, int day)
        {
            string episodeName = null;
            string seriesId = GetSeriesId(showName);

            if (seriesId != null)
                episodeName = GetEpisodeName(seriesId, year, month, day);

            return episodeName;
        }

        internal static string GetSeriesId(string seriesName)
        {
            try
            {
                string url = string.Format("http://thetvdb.com/api/GetSeries.php?seriesname={0}", seriesName);

                var tvDbRssReader = new XmlTextReader(url);
                var tvDbRssDoc = new XmlDocument();
                tvDbRssDoc.Load(tvDbRssReader);

                XmlNodeList data = tvDbRssDoc.GetElementsByTagName(@"Data");

                if (data.Count != 0)
                {
                    XmlNodeList series = ((XmlElement)data[0]).GetElementsByTagName("Series");

                    if (series.Count == 0)
                    {
                        logger.Log("No Series Found");
                        return null;
                    }

                    foreach (object s in series)
                    {
                        var tvDbElement = (XmlElement)s;

                        string tvDbShowName =
                            tvDbElement.GetElementsByTagName("SeriesName")[0].InnerText.ToLower();

                        if (tvDbShowName.ToLower() == seriesName.ToLower())
                        {
                            return
                                tvDbElement.GetElementsByTagName("seriesid")[0].InnerText.ToLower();
                        }
                    }
                    var tvDbElementLast = (XmlElement)series.Item(0);
                    return tvDbElementLast.GetElementsByTagName("seriesid")[0].InnerText.ToLower();
                }
            }
            catch (Exception ex)
            {
                logger.Log("An Error has occurred while get the Series ID: " + ex);
            }
            return null;
        }

        internal static string GetEpisodeName(string seriesId, int seasonNumber, int episodeNumber)
        {
            try
            {
                string url = string.Format("http://thetvdb.com/api/{0}/series/{1}/default/{2}/{3}",
                                           TvDbApiKey, seriesId, seasonNumber, episodeNumber);

                var tvDbRssReader = new XmlTextReader(url);
                var tvDbRssDoc = new XmlDocument();
                tvDbRssDoc.Load(tvDbRssReader);

                XmlNodeList data = tvDbRssDoc.GetElementsByTagName(@"Data");

                if (data.Count != 0)
                {
                    XmlNodeList episode = ((XmlElement)data[0]).GetElementsByTagName("Episode");

                    if (episode.Count == 0)
                    {
                        logger.Log("Episode Not Found");
                        return null;
                    }

                    var tvDbElement = (XmlElement)episode.Item(0);
                    string episodeName = tvDbElement.GetElementsByTagName("EpisodeName")[0].InnerText;
                    logger.Log("Episode Name is: " + episodeName);
                    return episodeName;
                }
            }
            catch (Exception ex)
            {
                logger.Log("An Error has occurred while get the Series ID: " + ex);
            }

            return null;
        }

        private static string GetEpisodeName(string seriesId, int year, int month, int day)
        {
            try
            {
                string url = string.Format("http://thetvdb.com/api/{0}/series/{1}/all/",
                                           TvDbApiKey, seriesId);

                var tvDbRssReader = new XmlTextReader(url);
                var tvDbRssDoc = new XmlDocument();
                tvDbRssDoc.Load(tvDbRssReader);

                string rssAirDate = year.ToString("D4") + "-" + month.ToString("D2") + "-" +
                                    day.ToString("D2");

                XmlNodeList data = tvDbRssDoc.GetElementsByTagName(@"Data");

                if (data.Count != 0)
                {
                    XmlNodeList episodes = ((XmlElement)data[0]).GetElementsByTagName("Episode");

                    if (episodes.Count == 0)
                    {
                        logger.Log("Episode Not Found");
                        return null;
                    }

                    foreach (object e in episodes)
                    {
                        var tvDbElement = (XmlElement)e;
                        string tvDbAirDate = tvDbElement.GetElementsByTagName("FirstAired")[0].InnerText;

                        if (tvDbAirDate == rssAirDate)
                        {
                            return tvDbElement.GetElementsByTagName("EpisodeName")[0].InnerText;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Log("An Error has occurred while get the Series ID: " + ex);
            }

            return "unknown";
        }
    }
}