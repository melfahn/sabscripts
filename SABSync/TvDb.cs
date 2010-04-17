using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SABSync
{
    class TvDb
    {

        private static readonly string _tvDbApiKey = "5D2D188E86E07F4F";
        internal static string CheckTvDb(string showName, int seasonNumber, int episodeNumber)
        {
            try
            {
                string episodeName = "unknown";
                string seriesId = GetSeriesId(showName);

                if (seriesId != null)
                    episodeName = GetEpisodeName(seriesId, seasonNumber, episodeNumber);

                return episodeName;
            }
            catch (Exception)
            {

                throw;
            }
        }

        internal static string CheckTvDb(string showName, int year, int month, int day)
        {
            try
            {
                string episodeName = "unknown";
                string seriesId = GetSeriesId(showName);

                if (seriesId != null)
                    episodeName = GetEpisodeName(seriesId, year, month, day);

                return episodeName;
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal static string GetSeriesId(string seriesName)
        {
            string seriesId = null;

            try
            {
                string url = "http://thetvdb.com/api/GetSeries.php?seriesname=" + seriesName;

                XmlTextReader tvDbRssReader = new XmlTextReader(url);
                XmlDocument tvDbRssDoc = new XmlDocument();
                tvDbRssDoc.Load(tvDbRssReader);

                var data = tvDbRssDoc.GetElementsByTagName(@"Data");

                if (data.Count != 0)
                {
                    var series = ((XmlElement)data[0]).GetElementsByTagName("Series");

                    if (series.Count == 0)
                    {
                        Program.Log("No Series Found");
                        return seriesId;
                    }

                    foreach (var s in series)
                    {
                        XmlElement tvDbElement = (XmlElement)s;

                        string tvDbShowName = tvDbElement.GetElementsByTagName("SeriesName")[0].InnerText.ToLower();

                        if (tvDbShowName.ToLower() == seriesName.ToLower())
                        {
                            seriesId = tvDbElement.GetElementsByTagName("seriesid")[0].InnerText.ToLower();
                            return seriesId;
                        }

                        else
                            continue;
                    }
                    XmlElement tvDbElementLast = (XmlElement)series.Item(0);
                    seriesId = tvDbElementLast.GetElementsByTagName("seriesid")[0].InnerText.ToLower();
                    return seriesId;
                }
            }
            catch (Exception ex)
            {
                Program.Log("An Error has occurred while get the Series ID: " + ex);
            }
            return null;
        }

        internal static string GetEpisodeName(string seriesId, int seasonNumber, int episodeNumber)
        {
            try
            {
                string url = "http://thetvdb.com/api/" + _tvDbApiKey + "/series/" + seriesId + "/default/" + seasonNumber + "/" + episodeNumber;

                XmlTextReader tvDbRssReader = new XmlTextReader(url);
                XmlDocument tvDbRssDoc = new XmlDocument();
                tvDbRssDoc.Load(tvDbRssReader);


                var data = tvDbRssDoc.GetElementsByTagName(@"Data");

                if (data.Count != 0)
                {
                    var episode = ((XmlElement)data[0]).GetElementsByTagName("Episode");

                    if (episode.Count == 0)
                    {
                        Program.Log("Episode Not Found");
                        return null;
                    }

                    XmlElement tvDbElement = (XmlElement)episode.Item(0);
                    string episodeName = tvDbElement.GetElementsByTagName("EpisodeName")[0].InnerText;
                    Program.Log("Episode Name is: " + episodeName);
                    return episodeName;
                }
            }
            catch (Exception ex)
            {
               Program.Log("An Error has occurred while get the Series ID: " + ex);
            }

            return null;
        }

        private static string GetEpisodeName(string seriesId, int year, int month, int day)
        {
            try
            {
                string url = "http://thetvdb.com/api/" + _tvDbApiKey + "/series/" + seriesId + "/all/";

                XmlTextReader tvDbRssReader = new XmlTextReader(url);
                XmlDocument tvDbRssDoc = new XmlDocument();
                tvDbRssDoc.Load(tvDbRssReader);

                string rssAirDate = year.ToString("D4") + "-" + month.ToString("D2") + "-" + day.ToString("D2");
                string episodeName = null;

                var data = tvDbRssDoc.GetElementsByTagName(@"Data");

                if (data.Count != 0)
                {
                    var episodes = ((XmlElement)data[0]).GetElementsByTagName("Episode");

                    if (episodes.Count == 0)
                    {
                        Program.Log("Episode Not Found");
                        return null;
                    }

                    foreach (var e in episodes)
                    {
                        XmlElement tvDbElement = (XmlElement)e;
                        string tvDbAirDate = tvDbElement.GetElementsByTagName("FirstAired")[0].InnerText;
                        //Console.WriteLine(tvDbAirDate);

                        if (tvDbAirDate == rssAirDate)
                        {
                            episodeName = tvDbElement.GetElementsByTagName("EpisodeName")[0].InnerText;
                            return episodeName;
                        }

                        else
                            continue;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.Log("An Error has occurred while get the Series ID: " + ex);
            }

            return "unknown";
        }
    }
}
