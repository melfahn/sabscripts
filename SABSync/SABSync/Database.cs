using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace SABSync
{
    public class Database
    {
        public event DatabaseChangedHandler DbChanged = delegate { };
        public delegate void DatabaseChangedHandler(string dbName);

        public event ProcessingShowHandler ProcessingShow = delegate { };
        public delegate void ProcessingShowHandler(string message);

        //Class to Hold Commands to Update the Database etc

        private ITvDbService TvDb { get; set; }
        private Logger Logger = new Logger();
        public Dictionary<int, string> QualityTable = new Dictionary<int, string>(); //Used to store the quality table
        private Config Config = new Config();
        private List<ShowAlias> Aliases = new List<ShowAlias>();

        public Database()
            : this(new TvDbService())
        {
            
        }

        public Database(ITvDbService tvDbService)
        {
            TvDb = tvDbService;
            //Add records to QualityTable
            QualityTable.Add(1, "xvid");
            QualityTable.Add(2, "720p");

            Aliases.Add(new ShowAlias("CSI", "CSI: Crime Scene Investigation"));
            Aliases.Add(new ShowAlias("CSI Miami", "CSI: Miami"));
            Aliases.Add(new ShowAlias("CSI New Nork", "CSI: NY"));
            Aliases.Add(new ShowAlias("CSI NY", "CSI: NY"));
            Aliases.Add(new ShowAlias("The Office", "The Office (US)"));
            Aliases.Add(new ShowAlias("Law and Order", "Law & Order"));
            Aliases.Add(new ShowAlias("Law and Order CI", "Law & Order: Criminal Intent"));
            Aliases.Add(new ShowAlias("Law and Order Criminal Intent", "Law & Order: Criminal Intent"));
            Aliases.Add(new ShowAlias("Law and Order SVU", "Law & Order: Special Victims Unit"));
            Aliases.Add(new ShowAlias("Law and Order Special Victims Unit", "Law & Order: Special Victims Unit"));
            Aliases.Add(new ShowAlias("David Letterman", "Late Show with David Letterman"));
            Aliases.Add(new ShowAlias("Dancing With the Stars US", "Dancing With the Stars"));
            Aliases.Add(new ShowAlias("Pure Pwnage TV", "Pure Pwnage"));
            Aliases.Add(new ShowAlias("The City", "The City (2008)"));
            Aliases.Add(new ShowAlias("Rob Dyrdeks Fantasy Factory", "Rob Dyrdek's Fantasy Factory"));
            Aliases.Add(new ShowAlias("Its Always Sunny in Philadelphia", "It's Always Sunny in Philadelphia"));
            Aliases.Add(new ShowAlias("Hawaii Five 0", "Hawaii Five-0 (2010)"));
            Aliases.Add(new ShowAlias("Hawaii Five 0 2010", "Hawaii Five-0 (2010)"));
            Aliases.Add(new ShowAlias("Kitchen Nightmares US", "Kitchen Nightmares"));
        }

        public void ShowsOnDiskToDatabase()
        {
            GetTvDbServerTime(); //Get TvDB server time first!
            foreach (var show in Config.MyShows)
            {
                ProcessingShow("Processing: " + show);
                AddNewShowWithEpisodes(show);
            }
            ProcessingShow("All Shows on Disk Processed: " + Config.MyShows.Count);
        }

        private void AddNewShowWithEpisodes(string showName)
        {
            //TODO: Find a way to migrate aliases to Database... or not, not a big deal IMO
            //Update the shows table, get TVDB ID and Name, possible get TVRage ID and Name

            Logger.Log("Checking if {0} is in Database: ", showName);

            try
            {
                using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
                {
                    if (sabSyncEntities.shows.Any(s => s.show_name == showName)) //If show was already added...skip it (Use local disk name)
                        return;

                    //Get TVDB ID & Name
                    TvDbShowInfo info = new TvDbShowInfo();
                    info = TvDb.GetShowData(showName);

                    if (info == null)
                        return;

                    //string aliases = GetAliasForDb(showName, info.SeriesName);
                    int downloadQuality = GetQualityForDb();

                    //Add to shows Database
                    shows newItem = new shows
                                        {
                                            id = new int(),
                                            show_name = showName,
                                            tvdb_id = Convert.ToInt32(info.SeriesId),
                                            tvdb_name = info.SeriesName,
                                            ignore_season = 0,
                                            air_day = info.AirDay,
                                            air_time = info.AirTime,
                                            run_time = Convert.ToInt32(info.RunTime),
                                            status = info.Status,
                                            poster_url = info.PosterUrl,
                                            banner_url = info.BannerUrl,
                                            imdb_id = info.ImdbId,
                                            genre = info.Genre.Trim('|'),
                                            overview = info.Overview,
                                            quality = downloadQuality,
                                            aliases = GetAliasForDb(showName)
                    };
                    Logger.Log("Adding {0} to database.", showName);

                    sabSyncEntities.AddToshows(newItem);
                    sabSyncEntities.SaveChanges(); //Save show to Database after each show
                    var newShow = (from s in sabSyncEntities.shows where s.tvdb_id == info.SeriesId select s).FirstOrDefault(); //Get the PK for the show just added (so we can get the banner)
                    TvDb.GetBanner(info.BannerUrl, newShow.id); //Get the banner and save to disk
                    AddEpisodes(info.Episodes, Convert.ToInt32(info.SeriesId)); //Add all episodes for this show
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
        }

        public void GetTvDbServerTime()
        {
            int time = TvDb.GetServerTime();
            try
            {
                using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
                {
                    if ((from i in sabSyncEntities.info select i).Count() != 0) //Get the count of items in info, if not 0 then return (Time was already added).
                        return;

                    info newTime = new info()
                    {
                        id = new long(),
                        last_tvdb = time
                    };
                    sabSyncEntities.AddToinfo(newTime);
                    sabSyncEntities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
                throw;
            }
        }

        public void GetTvDbUpdates()
        {
            //Get all updates to Series and Episodes since last check, get updates for watched shows and episodes

            try
            {
                using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
                {
                    var oldTime = (from t in sabSyncEntities.info select t).FirstOrDefault(); //Get the time from the DB

                    if (oldTime == null)
                        return;

                    TvDbUpdates updates = TvDb.GetUpdates(Convert.ToInt32(oldTime.last_tvdb.Value)); //Get the Updates since oldTime

                    oldTime.last_tvdb = updates.Time;

                    List<long?> seriesToUpdate = new List<long?>(); //Used to store the list of shows that need to be updated

                    var shows = from s in sabSyncEntities.shows
                                select s;

                    foreach (var seriesId in updates.Series)
                    {
                        if (shows.Any(s => s.tvdb_id == seriesId)) //If we're watching this show add to the list
                            seriesToUpdate.Add(seriesId);
                    }

                    UpdateFromTvDb(seriesToUpdate); //Update the list of shows we are watching

                    sabSyncEntities.info.ApplyCurrentValues(oldTime);
                    sabSyncEntities.SaveChanges(); //Save the new time to the info table
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
        }

        public void UpdateFromTvDb (List<long?> seriesIdList)
        {
            using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
            {
                var shows = from s in sabSyncEntities.shows
                            select s;

                //Update the shows (and add new episodes)
                foreach (var seriesId in seriesIdList)
                {
                    var show = (from s in shows where s.tvdb_id == seriesId select s).FirstOrDefault();
                        //set show to the first (of one) that is found (Should be one, if not something else is FUBAR)

                    //Get the updated series/new episode data for this seriesId
                    var updatedShowInfo = TvDb.GetShowUpdates(seriesId);

                    show.air_day = updatedShowInfo.AirDay;
                    show.air_time = updatedShowInfo.AirTime;
                    show.run_time = updatedShowInfo.RunTime;
                    show.genre = updatedShowInfo.Genre.Trim('|');
                    show.tvdb_name = updatedShowInfo.SeriesName;
                    show.overview = updatedShowInfo.Overview;
                    show.status = updatedShowInfo.Status;
                    show.poster_url = updatedShowInfo.PosterUrl;
                    show.banner_url = updatedShowInfo.BannerUrl;

                    sabSyncEntities.shows.ApplyCurrentValues(show); //Apply the current values
                    sabSyncEntities.SaveChanges(); //Save them to the server

                    AddEpisodes(updatedShowInfo.Episodes, seriesId); //Add the new episodes/update the old ones
                }
            }
        }

        public void AddToHistory(Episode episode, NzbInfo nzb)
        {
            using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
            {
                var test = from e in sabSyncEntities.episodes.AsEnumerable()
                           where e.shows.show_name.Equals(episode.ShowName, StringComparison.InvariantCultureIgnoreCase)
                           select e;

                var data = from e in sabSyncEntities.episodes.AsEnumerable()
                           where
                               e.shows.show_name.Equals(episode.ShowName, StringComparison.InvariantCultureIgnoreCase) && e.episode_number == episode.EpisodeNumber &&
                               e.season_number == episode.SeasonNumber
                           select new
                           {
                               ShowId = e.shows.id,
                               EpisodeId = e.id
                           };

                histories newItem = new histories
                {
                    id = new long(),
                    show_id = data.FirstOrDefault().ShowId,
                    episode_id = data.FirstOrDefault().EpisodeId,
                    feed_title = episode.FeedItem.Title,
                    quality = episode.Quality,
                    proper = Convert.ToInt32(episode.IsProper),
                    provider = nzb.Site.Name,
                    date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
                };

                Logger.Log("Episode added to History Database: {0} - S{1}E{2}", episode.ShowName, episode.SeasonNumber.ToString("00"), episode.EpisodeNumber.ToString("00"));
                sabSyncEntities.AddTohistories(newItem);
                sabSyncEntities.SaveChanges();
            }
        }

        public bool IsQualityWanted(Episode episode)
        {
            string description = (episode.FeedItem.Description ?? string.Empty).ToLower();
            string title = episode.FeedItem.Title.ToLower();

            //Get show quality from DB
            //TODO Clean this up, must be a better way to do it...

            using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
            {
                var qualityNumber = (from q in sabSyncEntities.shows.AsEnumerable()
                                     where q.show_name.Equals(episode.ShowName, StringComparison.InvariantCultureIgnoreCase)
                                     select new { q.quality }).FirstOrDefault();

                if (qualityNumber == null)
                {;
                    Logger.Log("Quality is not wanted");
                    return false;
                }

                if (qualityNumber.quality == 0) //If quality is 0 get it!
                {
                    //Figure out what the episode Quality for this episode is...
                    foreach (var q in QualityTable)
                    {
                        if (title.Contains(q.Value) || description.Contains(q.Value))
                        {
                            episode.Quality = q.Key;
                            Logger.Log("Quality -{0}- is wanted for: {1}.", q.Value, episode.ShowName);
                            return true;
                        }
                    }
                    return false; //In the unlikely (Web DL) event that the quality is not shown in the title, return false since it will not be wanted
                }

                var qualityString =
                    (from q in QualityTable where q.Key == qualityNumber.quality select q.Value).FirstOrDefault();

                Logger.Log("Title is: {0}", title);

                bool titleContainsQuality = title.Contains(qualityString);
                bool descriptionContainsQuality = description.Contains(qualityString);
                if (titleContainsQuality || descriptionContainsQuality)
                {
                    episode.Quality = Convert.ToInt32(qualityNumber.quality);
                    Logger.Log("Quality -{0}- is wanted for: {1}.", qualityString, episode.ShowName);
                    return true;
                }
            }

            Logger.Log("Quality is not wanted");
            return false;
        }

        public bool IsSeasonIgnored(Episode episode)
        {
            //Is this season ignored per the Database??

            using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
            {
                var ignored = (from i in sabSyncEntities.shows
                               where i.show_name.Equals(episode.ShowName, StringComparison.InvariantCultureIgnoreCase)
                               select i.ignore_season).FirstOrDefault();

                if (ignored >= episode.SeasonNumber)
                    return true;
            }
            return false;
        }

        private void AddEpisodes(List<TvDbEpisodeInfo> episodeList, long? seriesId)
        {
            //Check if Episode is in table, if not, add it!
            try
            {
                using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
                {
                    var shows = from s in sabSyncEntities.shows
                                where s.tvdb_id == seriesId
                                select new
                                {
                                    s.id
                                };

                    foreach (var episode in episodeList) //Get all Episodes for show
                    {
                        if (sabSyncEntities.episodes.Any(e => e.tvdb_id == episode.EpisodeId)) //If episode was already added...update it
                        {
                            //Update all existing episodes
                            var episodeToUpdate = (from e in sabSyncEntities.episodes where e.tvdb_id == episode.EpisodeId select e).FirstOrDefault(); //Select the first episode ID matching the TvDB Episode ID

                            episodeToUpdate.season_number = episode.SeasonNumber;
                            episodeToUpdate.episode_number = episode.EpisodeNumber;
                            episodeToUpdate.episode_name = episode.EpisodeName;
                            episodeToUpdate.air_date = episode.FirstAired;
                            episodeToUpdate.overview = episode.Overview;

                            sabSyncEntities.episodes.ApplyCurrentValues(episodeToUpdate);
                            continue;
                        }

                        episodes newItem = new episodes
                        {
                            id = new long(),
                            show_id = shows.FirstOrDefault().id,
                            season_number = episode.SeasonNumber,
                            episode_number = episode.EpisodeNumber,
                            episode_name = episode.EpisodeName,
                            air_date = episode.FirstAired,
                            tvdb_id = episode.EpisodeId,
                            overview = episode.Overview
                        };
                        sabSyncEntities.AddToepisodes(newItem);
                    }
                    sabSyncEntities.SaveChanges(); //Insert into Database after processing each series
                }
            }

            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
        }

        private int GetQualityForDb()
        {
            return Config.DownloadQualities;
        }

        public void GetEpisodeName(Episode episode)
        {
            //Check if Episode is a Daily
            episode.EpisodeName = episode.IsDaily
                ? GetEpisodeName(episode.ShowName, episode.AirDate)
                : GetEpisodeName(episode.ShowName, episode.SeasonNumber, episode.EpisodeNumber);

            if (episode.EpisodeNumber2 != 0)
                episode.EpisodeName2 = GetEpisodeName(episode.ShowName, episode.SeasonNumber, episode.EpisodeNumber2);

            return;
        }

        private string GetEpisodeName(string showName, int seasonNumber, int episodeNumber)
        {
            using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
            {
                //Return the first matching EpisodeName for this showName, seasonNumber and episodeNumber combo
                return (from e in sabSyncEntities.episodes
                            where
                                e.shows.show_name.Equals(showName, StringComparison.InvariantCultureIgnoreCase) &&
                                e.season_number == seasonNumber && e.episode_number == episodeNumber
                            select e.episode_name).FirstOrDefault();
            }
        }

        private string GetEpisodeName(string showName, DateTime airDate)
        {
            string myFirstAired = airDate.ToString("yyyy-MM-dd");
            using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
            {
                //Return the first matching EpisodeName for this showName, seasonNumber and episodeNumber combo
                return (from e in sabSyncEntities.episodes
                        where
                            e.shows.show_name.Equals(showName, StringComparison.InvariantCultureIgnoreCase) &&
                            e.air_date == myFirstAired
                        select e.episode_name).FirstOrDefault();
            }
        }

        private string GetAliasForDb(string showName)
        {
            string aliases = string.Empty;
            foreach (ShowAlias name in Aliases)
            {
                if (showName.Equals(CleanString(name.Alias), StringComparison.InvariantCultureIgnoreCase))
                    aliases += String.Format("{0};", name.BadName);
            }

            return aliases.Trim(';');
        }

        public void GetBanners()
        {
            try
            {
                using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
                {
                    var shows = from s in sabSyncEntities.shows
                                where String.IsNullOrEmpty(s.banner_url)
                                select s;

                    foreach (var show in shows)
                    {
                        //Get Banner URL for DB then download Banner
                        show.banner_url = TvDb.GetBannerUrl((long)show.tvdb_id);
                        sabSyncEntities.shows.ApplyCurrentValues(show);

                        Logger.Log("Attempting to get banner for: {0}", show.show_name);

                        //If banner comes back null or empty go onto the next
                        if (String.IsNullOrEmpty(show.banner_url))
                            continue;

                        TvDb.GetBanner(show.banner_url, show.id);
                    }
                    sabSyncEntities.SaveChanges(); //Save the Banner URLs
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
        }

        public void GetBanner(long showId)
        {
            try
            {
                using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
                {
                    var show = (from s in sabSyncEntities.shows
                                where s.id == showId
                                select s).FirstOrDefault();
                    
                    //Get Banner URL for DB then download Banner
                    show.banner_url = TvDb.GetBannerUrl((long)show.tvdb_id);
                    sabSyncEntities.shows.ApplyCurrentValues(show);

                    Logger.Log("Attempting to get banner for: {0}", show.show_name);

                    //If banner comes back null or empty return
                    if (String.IsNullOrEmpty(show.banner_url))
                        return;

                    TvDb.GetBanner(show.banner_url, show.id);
                    sabSyncEntities.SaveChanges(); //Save the Banner URLs
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
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
