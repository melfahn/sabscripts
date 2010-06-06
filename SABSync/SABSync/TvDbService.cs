using System;
using System.Xml;

namespace SABSync
{
    public interface ITvDbService
    {
        void GetEpisodeName(Episode episode);
    }

    public class TvDbService : ITvDbService
    {
        private const string TvDbApiKey = "5D2D188E86E07F4F";
        private static readonly Logger Logger = new Logger();
        private Episode _last;

        #region ITvDbService Members

        public void GetEpisodeName(Episode episode)
        {
            if (!string.IsNullOrWhiteSpace(episode.Name))
                return;

            bool isSameEpisode = _last != null && episode.ShowName == _last.ShowName &&
                (episode.IsDaily
                    ? episode.FirstAired == _last.FirstAired
                    : episode.SeasonNumber == _last.SeasonNumber &&
                        episode.EpisodeNumber == _last.EpisodeNumber &&
                            episode.EpisodeNumber == _last.EpisodeNumber2);
            if (isSameEpisode)
            {
                episode.Name = _last.Name;
                episode.Name2 = _last.Name2;
                return;
            }

            string seriesId = GetSeriesId(episode.ShowName);
            if (seriesId == null)
                return;

            episode.Name = episode.IsDaily
                ? GetEpisodeName(seriesId, episode.FirstAired)
                : GetEpisodeName(seriesId, episode.SeasonNumber, episode.EpisodeNumber);

            if (episode.EpisodeNumber2 != 0)
                episode.Name2 = GetEpisodeName(seriesId, episode.SeasonNumber, episode.EpisodeNumber2);

            _last = episode;
        }

        #endregion

        private static string GetSeriesId(string seriesName)
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
                    XmlNodeList series = ((XmlElement) data[0]).GetElementsByTagName("Series");

                    if (series.Count == 0)
                    {
                        Logger.Log("No Series Found");
                        return null;
                    }

                    foreach (object s in series)
                    {
                        var tvDbElement = (XmlElement) s;

                        string tvDbShowName =
                            tvDbElement.GetElementsByTagName("SeriesName")[0].InnerText.ToLower();

                        if (tvDbShowName.ToLower() == seriesName.ToLower())
                        {
                            return
                                tvDbElement.GetElementsByTagName("seriesid")[0].InnerText.ToLower();
                        }
                    }
                    var tvDbElementLast = (XmlElement) series.Item(0);
                    return tvDbElementLast.GetElementsByTagName("seriesid")[0].InnerText.ToLower();
                }
            }
            catch (Exception ex)
            {
                Logger.Log("An Error has occurred while get the Series ID: " + ex);
            }
            return null;
        }

        private static string GetEpisodeName(string seriesId, int seasonNumber, int episodeNumber)
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
                    XmlNodeList episode = ((XmlElement) data[0]).GetElementsByTagName("Episode");

                    if (episode.Count == 0)
                    {
                        Logger.Log("Episode Not Found");
                        return null;
                    }

                    var tvDbElement = (XmlElement) episode.Item(0);
                    string episodeName = tvDbElement.GetElementsByTagName("EpisodeName")[0].InnerText;
                    Logger.Log("Episode Name is: " + episodeName);
                    return episodeName;
                }
            }
            catch (Exception ex)
            {
                Logger.Log("An Error has occurred while get the Series ID: " + ex);
            }

            return null;
        }

        private static string GetEpisodeName(string seriesId, DateTime firstAired)
        {
            try
            {
                string url = string.Format("http://thetvdb.com/api/{0}/series/{1}/all/",
                    TvDbApiKey, seriesId);

                var tvDbRssReader = new XmlTextReader(url);
                var tvDbRssDoc = new XmlDocument();
                tvDbRssDoc.Load(tvDbRssReader);

                XmlNodeList data = tvDbRssDoc.GetElementsByTagName(@"Data");

                if (data.Count != 0)
                {
                    XmlNodeList episodes = ((XmlElement) data[0]).GetElementsByTagName("Episode");

                    if (episodes.Count == 0)
                    {
                        Logger.Log("Episode Not Found");
                        return null;
                    }

                    string myFirstAired = firstAired.ToString("yyyy-MM-dd");

                    foreach (XmlElement e in episodes)
                    {
                        string dbFirstAired = e.GetElementsByTagName("FirstAired")[0].InnerText;

                        if (dbFirstAired == myFirstAired)
                            return e.GetElementsByTagName("EpisodeName")[0].InnerText;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log("An Error has occurred while get the Series ID: " + ex);
            }

            return null;
        }
    }
}