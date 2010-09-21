using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace SABSync
{
    public interface ITvDbService
    {
        void GetEpisodeName(Episode episode);
        TvDbShowInfo GetShowData(string seriesName);
        TvDbEpisodeInfo GetEpisodeData(int episodeId);
        int GetServerTime();
        TvDbUpdates GetUpdates(int time);
        TvDbShowInfo GetShowUpdates(int seriesId);
    }

    public class TvDbService : ITvDbService
    {
        private const string TvDbApiKey = "5D2D188E86E07F4F";
        private static readonly Logger Logger = new Logger();
        private Episode _last;

        #region ITvDbService Members

        public void GetEpisodeName(Episode episode)
        {
            if (!string.IsNullOrWhiteSpace(episode.EpisodeName))
                return;

            bool isSameEpisode = _last != null && episode.ShowName == _last.ShowName &&
                (episode.IsDaily
                    ? episode.AirDate == _last.AirDate
                    : episode.SeasonNumber == _last.SeasonNumber &&
                        episode.EpisodeNumber == _last.EpisodeNumber &&
                            episode.EpisodeNumber == _last.EpisodeNumber2);
            if (isSameEpisode)
            {
                episode.EpisodeName = _last.EpisodeName;
                episode.EpisodeName2 = _last.EpisodeName2;
                return;
            }

            string seriesId = GetSeriesId(episode.ShowName);
            if (seriesId == null)
                return;

            episode.EpisodeName = episode.IsDaily
                ? GetEpisodeName(seriesId, episode.AirDate)
                : GetEpisodeName(seriesId, episode.SeasonNumber, episode.EpisodeNumber);

            if (episode.EpisodeNumber2 != 0)
                episode.EpisodeName2 = GetEpisodeName(seriesId, episode.SeasonNumber, episode.EpisodeNumber2);

            _last = episode;
        }

        public TvDbShowInfo GetShowData(string seriesName)
        {
            string seriesId = GetSeriesId(seriesName);

            if (seriesId == null)
                return null;

            return GetShowInfo(seriesId);
        }

        public TvDbShowInfo GetShowUpdates(int seriesId)
        {
            return GetShowInfo(seriesId.ToString());
        }

        public TvDbEpisodeInfo GetEpisodeData(int episodeId)
        {
            return GetEpisodeInfo(episodeId);
        }

        public TvDbUpdates GetUpdates(int time)
        {
            return ProcessGetUpdates(time);
        }

        public int GetServerTime()
        {
            return ProcessGetServerTime();
        }

        #endregion

        private Config Config = new Config();

        private string GetSeriesId(string seriesName)
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

                        if (CleanString(tvDbShowName.ToLower()) == seriesName.ToLower())
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
                Logger.Log("An Error has occurred while getting the Series ID: " + ex);
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

        private static TvDbShowInfo GetShowInfo(string seriesId)
        {
            try
            {
                string url = string.Format("http://www.thetvdb.com/data/series/{0}/all",
                                           seriesId);

                Logger.Log("Fetching Series and Episode info from: {0}", url);
                XDocument xDoc = XDocument.Load(url);

                var series = from s in xDoc.Descendants("Series")
                             select new TvDbShowInfo()
                             {
                                 SeriesId = Convert.ToInt32(s.Element("id").Value),
                                 SeriesName = s.Element("SeriesName").Value,
                                 AirDay = s.Element("Airs_DayOfWeek").Value,
                                 AirTime = s.Element("Airs_Time").Value,
                                 RunTime = Convert.ToInt32(s.Element("Runtime").Value),
                                 Status = s.Element("Status").Value,
                                 PosterUrl = s.Element("poster").Value,
                                 ImdbId = s.Element("IMDB_ID").Value,
                                 Genre = s.Element("Genre").Value,
                                 Overview = s.Element("Overview").Value
                             };

                var episodes = from e in xDoc.Descendants("Episode")
                               select new TvDbEpisodeInfo
                               {
                                   EpisodeId = Convert.ToInt32(e.Element("id").Value),
                                   SeasonNumber = Convert.ToInt32(e.Element("SeasonNumber").Value),
                                   EpisodeNumber = Convert.ToInt32(e.Element("EpisodeNumber").Value),
                                   EpisodeName = e.Element("EpisodeName").Value,
                                   FirstAired = e.Element("FirstAired").Value,
                                   Overview = e.Element("Overview").Value
                               };

                TvDbShowInfo info = series.FirstOrDefault(); //Store the first Series found for this ID (Only 1 will exist anyways)
                info.Episodes = episodes.ToList(); //Add the Epsiodes to info
                
                return info; 
            }

            catch (Exception ex)
            {
                Logger.Log("An Error has occurred while getting series/episode information for Series ID: " + ex);
            }
            return null;
        }

        private static TvDbEpisodeInfo GetEpisodeInfo(int episodeId)
        {
            try
            {
                string url = string.Format("http://www.thetvdb.com/api/5D2D188E86E07F4F/episodes/{0}/en.xml",
                                           episodeId);

                Logger.Log("Fetching Episode info from: {0}", url);
                XDocument xDoc = XDocument.Load(url);

                var episode = from e in xDoc.Descendants("Episode")
                               select new TvDbEpisodeInfo
                               {
                                   EpisodeId = Convert.ToInt32(e.Element("id").Value),
                                   SeasonNumber = Convert.ToInt32(e.Element("SeasonNumber").Value),
                                   EpisodeNumber = Convert.ToInt32(e.Element("EpisodeNumber").Value),
                                   EpisodeName = e.Element("EpisodeName").Value,
                                   FirstAired = e.Element("FirstAired").Value,
                                   Overview = e.Element("Overview").Value
                               };

                return episode.FirstOrDefault(); //Return the list of episodes, to be added to the DB
            }

            catch (Exception ex)
            {
                Logger.Log("An Error has occurred while getting show information for Series ID: " + ex);
            }
            return null;
        }

        private static int ProcessGetServerTime()
        {
            try
            {
                string url = string.Format("http://www.thetvdb.com/api/Updates.php?type=none");

                Logger.Log("Fetching TVDB Server Time: {0}", url);
                XDocument xDoc = XDocument.Load(url);

                var time = from s in xDoc.Descendants("Items")
                             select new 
                             {
                                 Time = Convert.ToInt32(s.Element("Time").Value)
                             };

                return time.FirstOrDefault().Time; //Return the first Time found (of one)
            }

            catch (Exception ex)
            {
                Logger.Log("An Error has occurred while getting show information for Series ID: " + ex);
                return 0;
            }
        }

        private static TvDbUpdates ProcessGetUpdates(int oldTime)
        {
            TvDbUpdates newUpdates = new TvDbUpdates();
            //Do the fetching to get the updates from TvDb
            try
            {
                string url = string.Format("http://www.thetvdb.com/api/Updates.php?type=all&time={0}", oldTime);

                Logger.Log("Fetching TVDB Updates since {0}: {1}", oldTime, url);
                XDocument xDoc = XDocument.Load(url);

                var updates = from s in xDoc.Descendants("Items")
                           select new
                           {
                               Time = Convert.ToInt32(s.Element("Time").Value),
                               Series = s.Elements("Series"),
                               Episodes = s.Elements("Episode")
                           };

                foreach (var update in updates)
                {
                    foreach (var series in update.Series)
                        newUpdates.Series.Add(Convert.ToInt32(series.Value));

                    foreach (var episode in update.Episodes)
                        newUpdates.Episodes.Add(Convert.ToInt32(episode.Value));
                }

                newUpdates.Time = updates.FirstOrDefault().Time; //Return the first Time found (of one)

                return newUpdates;
            }

            catch (Exception ex)
            {
                Logger.Log("An Error has occurred while getting show information for Series ID: " + ex);
                return null;
            }
        }

        private string CleanString(string name)
        {
            string result = name;
            string[] badCharacters = { "\\", "/", "<", ">", "?", "*", ":", "|", "\"" };
            string[] goodCharacters = { "+", "+", "{", "}", "!", "@", "-", "#", "`" };

            for (int i = 0; i < badCharacters.Length; i++)
            {
                result = result.Replace(badCharacters[i], Config.SabReplaceChars ? goodCharacters[i] : "");
            }

            return result.Trim();
        }
    }
}