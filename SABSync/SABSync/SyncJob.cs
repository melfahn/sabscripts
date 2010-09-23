using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Rss;

namespace SABSync
{
    public class SyncJob
    {
        public event DatabaseChangedHandler DbChanged = delegate { };
        public delegate void DatabaseChangedHandler(string dbName);

        private const string PatternS01E01E02 =
            @"[Ss](?<Season>(?:\d{1,2}))[Ee](?<EpisodeOne>(?:\d{1,2}))[Ee](?<EpisodeTwo>(?:\d{1,2}))";

        private const string PatternS01E01 = @"[Ss](?<Season>(?:\d{1,2}))[Ee](?<Episode>(?:\d{1,2}))";
        private const string Pattern1X01 = @"(?<Season>(?:\d{1,2}))[Xx](?<Episode>(?:\d{1,2}))";
        private const string PatternDaily = @"(?<Year>\d{4}).{1}(?<Month>\d{2}).{1}(?<Day>\d{2})";

        private static readonly Logger Logger = new Logger();
        private Database Database = new Database(); //Create a new Instance of Database
        public int AcceptCount;
        public int FeedItemCount;
        public int MyFeedsCount;
        public int MyShowsCount;
        public int MyShowsInFeedCount;
        public int RejectArchivedNzbCount;
        public int RejectDownloadQualityCount;
        public int RejectIgnoredSeasonCount;
        public int RejectOnDiskCount;
        public int RejectPasswordedCount;
        public int RejectSabHistoryCount;
        public int RejectSabQueuedCount;
        public int RejectShowQualityCount;

        public SyncJob()
            : this(new Config(), new SabService(), new TvDbService())
        {
        }

        public SyncJob(Config config, ISabService sabService, ITvDbService tvDbService)
        {
            Summary = new List<string>();
            Queued = new List<string>();
            Config = config;
            Sab = sabService;
            TvDb = tvDbService;
        }

        private Config Config { get; set; }
        private ISabService Sab { get; set; }
        private ITvDbService TvDb { get; set; }
        private List<string> Queued { get; set; }
        private List<string> Summary { get; set; }

        public void Start()
        {
            Log("Watching {0} shows", Config.MyShows.Count);

            foreach (FeedInfo feedInfo in Config.Feeds)
            {
                MyFeedsCount++;
                foreach (RssItem item in GetFeedItems(feedInfo))
                {
                    FeedItemCount++;
                    NzbInfo nzb = ParseNzbInfo(feedInfo, item);
                    QueueIfWanted(nzb);
                }
            }

            MyShowsCount = Config.MyShows.Count;
            LogSummary();
        }

        private IEnumerable<RssItem> GetFeedItems(FeedInfo feedInfo)
        {
            RssFeed feed = null;
            try
            {
                Log("INFO: Downloading feed {0} from {1}", feedInfo.Name, feedInfo.Url);
                feed = RssFeed.Read(feedInfo.Url);
            }
            catch (Exception e)
            {
                Logger.Log("ERROR: Could not download feed {0} from {1}", feedInfo.Name, feedInfo.Url);
                Logger.Log("ERROR: {0}", e);
            }
            if (feed == null || feed.Channels == null || feed.Channels.Count == 0)
                return Enumerable.Empty<RssItem>();
            return feed.Channels[0].Items.Cast<RssItem>();
        }

        private static NzbInfo ParseNzbInfo(FeedInfo feed, RssItem item)
        {
            NzbSite site = NzbSite.Parse(feed.Url.ToLower());
            return new NzbInfo
            {
                Id = site.ParseId(item.Link.ToString()),
                Title = item.Title,
                Site = site,
                Link = item.Link,
                Description = item.Description,
            };
        }

        private void QueueIfWanted(NzbInfo nzb)
        {
            Log(string.Empty);
            Log("----------------------------------------------------------------");
            Log("Verifying '{0}'", nzb.Title);
            try
            {
                if (nzb.IsPassworded())
                {
                    RejectPasswordedCount++;
                    Log("Skipping Passworded Report {0}", nzb.Title);
                    return;
                }

                var feedItem = new FeedItem { NzbId = nzb.Id, Title = nzb.Title, Description = nzb.Description };
                Episode episode = ParseEpisode(feedItem);
                if (episode == null)
                {
                    Log("Unsupported Title: {0}", feedItem.Title);
                    return;
                }

                if (!IsShowWanted(episode.ShowName))
                    return;

                if (!IsEpisodeWanted(episode))
                    return;

                nzb.Title = episode.FeedItem.TitleFix;
                string queueResponse = Sab.AddByUrl(nzb);

                Database.AddToHistory(episode, nzb);
                DatabaseChangedHandler handler = DbChanged;
                if (handler != null) handler("history");
                
                // TODO: check if Queued.Add need unfixed Title (was previously)
                AcceptCount++;
                Queued.Add(string.Format("{0}: {1}", nzb.Title, queueResponse));
            }
            catch (Exception e)
            {
                Log("Unsupported Title: {0} - {1}", nzb.Title, e);
            }
        }

        private void LogSummary()
        {
            foreach (string logItem in Summary)
            {
                Log(logItem);
            }

            if (Summary.Count != 0)
                Log(Environment.NewLine);

            foreach (string item in Queued)
            {
                Log("Queued for download: " + item);
            }

            if (Queued.Count != 0)
                Log(Environment.NewLine);

            Log("Number of reports added to the queue: " + Queued.Count);
        }

        private string GetEpisodeDir(Episode episode, DirectoryInfo tvDir)
        {
            if (Config.VerboseLogging)
                Log("Building string for Episode Dir");

            string path = episode.IsDaily
                ? GetPathForDaily(episode, tvDir)
                : GetPathForSeasonEpisode(episode, tvDir);

            if (Config.VerboseLogging)
                Log(path);

            return path;
        }

        private string GetPathForSeasonEpisode(Episode episode, DirectoryInfo tvDir)
        {
            string showName = CleanString(episode.ShowName);
            string snReplace = showName;
            string sDotNReplace = showName.Replace(' ', '.');
            string sUnderNReplace = showName.Replace(' ', '_');

            string zeroSReplace = String.Format("{0:00}", episode.SeasonNumber);
            string sReplace = Convert.ToString(episode.SeasonNumber);
            string zeroEReplace = String.Format("{0:00}", episode.EpisodeNumber);
            string eReplace = Convert.ToString(episode.EpisodeNumber);

            string path = Path.GetDirectoryName(tvDir + Path.DirectorySeparatorChar.ToString() + Config.TvTemplate);

            path = path.Replace(".%ext", "");
            path = path.Replace("%sn", snReplace);
            path = path.Replace("%s.n", sDotNReplace);
            path = path.Replace("%s_n", sUnderNReplace);
            path = path.Replace("%0s", zeroSReplace);
            path = path.Replace("%s", sReplace);
            path = path.Replace("%0e", zeroEReplace);
            path = path.Replace("%e", eReplace);
            return path;
        }

        private string GetPathForDaily(Episode episode, DirectoryInfo tvDir)
        {
            string showName = CleanString(episode.ShowName);
            string tReplace = showName;
            string dotTReplace = showName.Replace(' ', '.');
            string underTReplace = showName.Replace(' ', '_');
            string yearReplace = Convert.ToString(episode.AirDate.Year);
            string zeroMReplace = String.Format("{0:00}", episode.AirDate.Month);
            string mReplace = Convert.ToString(episode.AirDate.Month);
            string zeroDReplace = String.Format("{0:00}", episode.AirDate.Day);
            string dReplace = Convert.ToString(episode.AirDate.Day);

            string path = Path.GetDirectoryName(tvDir + Path.DirectorySeparatorChar.ToString() + Config.TvDailyTemplate);

            path = path.Replace(".%ext", "");
            path = path.Replace("%t", tReplace);
            path = path.Replace("%.t", dotTReplace);
            path = path.Replace("%_t", underTReplace);
            path = path.Replace("%y", yearReplace);
            path = path.Replace("%0m", zeroMReplace);
            path = path.Replace("%m", mReplace);
            path = path.Replace("%0d", zeroDReplace);
            path = path.Replace("%d", dReplace);
            return path;
        }

        private string GetEpisodeFileMask(Episode episode, DirectoryInfo tvDir)
        {
            if (Config.VerboseLogging)
                Log("Building string for Episode File Mask");

            string fileMask = episode.IsDaily
                ? GetFileMaskForDaily(episode, tvDir)
                : GetFileMaskForSeasonEpisode(episode, tvDir);

            if (Config.VerboseLogging)
                Log(fileMask);

            return fileMask;
        }

        private string GetFileMaskForSeasonEpisode(Episode episode, DirectoryInfo tvDir)
        {
            string zeroSReplace = String.Format("{0:00}", episode.SeasonNumber);
            string sReplace = Convert.ToString(episode.SeasonNumber);
            string zeroEReplace = String.Format("{0:00}", episode.EpisodeNumber);
            string eReplace = Convert.ToString(episode.EpisodeNumber);

            string fileMask = Path.GetFileName(tvDir + Path.DirectorySeparatorChar.ToString() + Config.TvTemplate);

            fileMask = fileMask.Replace(".%ext", "");
            fileMask = fileMask.Replace("%en", "*");
            fileMask = fileMask.Replace("%e.n", "*");
            fileMask = fileMask.Replace("%e_n", "*");
            fileMask = fileMask.Replace("%sn", "*");
            fileMask = fileMask.Replace("%s.n", "*");
            fileMask = fileMask.Replace("%s_n", "*");
            fileMask = fileMask.Replace("%0s", zeroSReplace);
            fileMask = fileMask.Replace("%s", sReplace);
            fileMask = fileMask.Replace("%0e", zeroEReplace);
            fileMask = fileMask.Replace("%e", eReplace);

            //Trim fileMask down to just season and episode file mask (for shows that do not have episode name) ie. [*S01E01*] instead of [* - S01E01 - *]
            fileMask = fileMask.TrimEnd(' ', '*', '.', '-', '_');
            fileMask = fileMask.TrimStart(' ', '*', '.', '-', '_');
            fileMask = "*" + fileMask + "*";
            return fileMask;
        }

        private string GetFileMaskForDaily(Episode episode, DirectoryInfo tvDir)
        {
            string fileMask = Path.GetFileName(tvDir + Path.DirectorySeparatorChar.ToString() + Config.TvDailyTemplate);

            string yearReplace = Convert.ToString(episode.AirDate.Year);
            string zeroMReplace = String.Format("{0:00}", episode.AirDate.Month);
            string mReplace = Convert.ToString(episode.AirDate.Month);
            string zeroDReplace = String.Format("{0:00}", episode.AirDate.Day);
            string dReplace = Convert.ToString(episode.AirDate.Day);

            fileMask = fileMask.Replace(".%ext", "*");
            fileMask = fileMask.Replace("%desc", "*");
            fileMask = fileMask.Replace("%.desc", "*");
            fileMask = fileMask.Replace("%_desc", "*");
            fileMask = fileMask.Replace("%t", "*");
            fileMask = fileMask.Replace("%.t", "*");
            fileMask = fileMask.Replace("%_t", "*");
            fileMask = fileMask.Replace("%y", yearReplace);
            fileMask = fileMask.Replace("%0m", zeroMReplace);
            fileMask = fileMask.Replace("%m", mReplace);
            fileMask = fileMask.Replace("%0d", zeroDReplace);
            fileMask = fileMask.Replace("%d", dReplace);

            //Trim fileMask down to just year/month/day (for shows that do not have episode name) ie. [*2010-01-25*] instead of [* - 2010-01-25 - *]
            fileMask = fileMask.TrimEnd(' ', '*', '.', '-', '_');
            fileMask = fileMask.TrimStart(' ', '*', '.', '-', '_');
            fileMask = "*" + fileMask + "*";

            return fileMask;
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

        private bool IsShowWanted(string showName)
        {
            if (Config.MyShows
                .Any(myShow => myShow.Equals(CleanString(showName), StringComparison.InvariantCultureIgnoreCase)))
            {
                MyShowsInFeedCount++;
                Log("'{0}' is being watched.", showName);
                return true;
            }
            Log("'{0}' is not being watched.", showName);
            return false;
        }

        private Episode ParseEpisode(FeedItem feedItem)
        {
            return MatchSeasonEpisodeMulti(feedItem) ??
                MatchSeasonEpisode(feedItem) ??
                    MatchFirstAiredEpisode(feedItem);
        }

        private Episode MatchSeasonEpisodeMulti(FeedItem item)
        {
            Match match = Regex.Match(item.Title, PatternS01E01E02);
            if (!match.Success) return null;

            return new Episode
            {
                FeedItem = item,
                ShowName = ParseShowName(item, PatternS01E01E02),
                SeasonNumber = int.Parse(match.Groups["Season"].Value),
                EpisodeNumber = int.Parse(match.Groups["EpisodeOne"].Value),
                EpisodeNumber2 = int.Parse(match.Groups["EpisodeTwo"].Value),
            };
        }

        private Episode MatchSeasonEpisode(FeedItem item)
        {
            string episodeName = null;
            string pattern = PatternS01E01;

            Match match = Regex.Match(item.Title, pattern);
            if (!match.Success)
            {
                pattern = Pattern1X01;
                match = Regex.Match(item.Title, Pattern1X01);
                if (!match.Success)
                    return null;
            }
            else
            {
                // Get episode name from title (used for lilx.net feeds)
                string[] part = Regex.Split(item.Title, PatternS01E01);
                if (part.Length > 2 && part[3].StartsWith(" - "))
                    episodeName = part[3].TrimStart('-', ' ');
            }
            return new Episode
            {
                FeedItem = item,
                ShowName = ParseShowName(item, pattern),
                SeasonNumber = int.Parse(match.Groups["Season"].Value),
                EpisodeNumber = int.Parse(match.Groups["Episode"].Value),
                EpisodeName = episodeName,
            };
        }

        private Episode MatchFirstAiredEpisode(FeedItem item)
        {
            Match match = Regex.Match(item.Title, PatternDaily);
            if (!match.Success) return null;

            return new Episode
            {
                FeedItem = item,
                ShowName = ParseShowName(item, PatternDaily),
                AirDate = DateTime.Parse(string.Format("{0}-{1}-{2}",
                    match.Groups["Year"].Value,
                    match.Groups["Month"].Value,
                    match.Groups["Day"].Value)),
            };
        }

        private string ParseShowName(FeedItem item, string pattern)
        {
            string showName = Regex.Split(item.Title, pattern)[0].Replace('.', ' ').TrimEnd(' ', '-');
            return ShowAlias(showName);
        }

        private bool IsEpisodeWanted(Episode episode)
        {
            if (IsSeasonIgnored(episode))
                return false;

            if (!IsQualityWanted(episode))
                return false;

            if (IsInLocalHistory(episode))
                return false;
            
            Database.GetEpisodeName(episode);
            GetTitleFix(episode);

            if (Queued.Contains(episode.FeedItem.TitleFix + ": ok"))
                return false;

            if (Sab.IsInQueue(episode))
            {
                RejectSabQueuedCount++;
                return false;
            }
            if (IsArchivedNzb(episode.FeedItem))
                return false;

            if (IsOnDisk(episode))
                return false;

            //Ignore the SAB History Check - It's too slow and causes issues with Quality "Upgrading"
            //bool isProper = episode.IsProper && Config.DownloadPropers;
            //if (!isProper && Sab.IsInHistory(episode))
            //{
            //    RejectSabHistoryCount++;
            //    return false;
            //}

            return true;
        }

        private bool IsOnDisk(Episode episode)
        {
            foreach (DirectoryInfo tvDir in Config.TvRootFolders)
            {
                string dir = GetEpisodeDir(episode, tvDir);
                string fileMask = GetEpisodeFileMask(episode, tvDir);

                if (episode.IsProper && Config.DownloadPropers)
                    DeleteForProper(dir, fileMask);

                if (IsOnDisk(dir, fileMask) ||
                    IsOnDisk(dir, episode.SeasonNumber, episode.EpisodeNumber))
                    return true;
            }

            return false;
        }

        private bool IsOnDisk(string dir, string fileMask)
        {
            if (!Directory.Exists(dir))
            {
                if (Config.VerboseLogging)
                    Log("Directory does not exist: {0}", dir);
                return false;
            }

            Log("Checking directory: {0} for [{1}]", dir, fileMask);

            foreach (string ext in Config.VideoExt)
            {
                string[] matchingFiles = Directory.GetFiles(dir, fileMask + ext, SearchOption.AllDirectories);

                if (matchingFiles.Length != 0)
                {
                    RejectOnDiskCount++;
                    Log("Episode on disk. '{0}'", true, matchingFiles[0]);
                    return true;
                }
            }
            return false;
        }

        private bool IsOnDisk(string dir, int seasonNumber, int episodeNumber)
        {
            if (!Directory.Exists(dir))
            {
                if (Config.VerboseLogging)
                    Log("Directory does not exist: {0}", dir);
                return false;
            }

            //Create list for formats (less code... I hope)
            //Create Strings for addional searching for episodes and add to formats List
            var formats = new List<string>
            {
                "*" + seasonNumber + "x" + episodeNumber.ToString("D2") + "*",
                "*" + "S" + seasonNumber.ToString("D2") + "E" + episodeNumber.ToString("D2") + "*",
                "*" + seasonNumber + episodeNumber.ToString("D2") + "*"
            };

            foreach (string format in formats)
            {
                Log("Checking directory: {0} for [{1}]", dir, format);

                foreach (string ext in Config.VideoExt)
                {
                    string[] matchingFiles = Directory.GetFiles(dir, format + ext, SearchOption.AllDirectories);

                    if (matchingFiles.Length != 0)
                    {
                        RejectOnDiskCount++;
                        Log("Episode on disk. '{0}'", true, matchingFiles[0]);
                        return true;
                    }
                }
            }
            return false;
        }

        private bool IsSeasonIgnored(Episode episode)
        {
            // TODO: add ignore season support for first aired (ignore <= date?)
            if (episode.IsDaily) return false;

            if (Database.IsSeasonIgnored(episode))
            {
                RejectIgnoredSeasonCount++;
                Log("Ignoring '{0}' Season '{1}'  ", episode.ShowName, episode.SeasonNumber);
                return true;
            }

            return false;
        }

        private bool IsArchivedNzb(FeedItem feedItem)
        {
            string rssTitle = feedItem.Title;
            string rssTitleFix = feedItem.TitleFix;
            Log("Checking for Imported NZB for [{0}] or [{1}]", rssTitle, rssTitleFix);

            bool inNzbArchive = false;

            string nzbFileName = ReplaceSeparatorChars(CleanString(rssTitle.TrimEnd('.')));
            foreach (FileInfo fi in Config.NzbDir.GetFiles("*.nzb.gz"))
            {
                string archiveFileName = ReplaceSeparatorChars(fi.Name.Replace(".nzb.gz", string.Empty));
                if (nzbFileName == archiveFileName)
                {
                    inNzbArchive = true;
                    break;
                }
            }

            // TODO: would this ever be true?
            string nzbFileNameFix = CleanString(rssTitleFix.TrimEnd('.'));
            if (File.Exists(Path.Combine(Config.NzbDir.FullName, nzbFileNameFix + ".nzb.gz")))
            {
                inNzbArchive = true;
            }

            if (inNzbArchive)
            {
                RejectArchivedNzbCount++;
                Log("Episode in archive: '{0}'", true, nzbFileName + ".nzb.gz");
                return true;
            }

            return false;
        }

        private bool IsInLocalHistory(Episode episode)
        {
            //TODO: Move this to Database

            Logger.Log("Checking SABSync.db for: [{0} - S{1:00}E{2:00}]", episode.ShowName, episode.SeasonNumber, episode.EpisodeNumber);
            using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
            {
                var ep = (from e in sabSyncEntities.histories
                          where
                              e.shows.show_name.Equals(episode.ShowName) & e.episodes.season_number.Value.Equals(episode.SeasonNumber) &
                              e.episodes.episode_number.Value.Equals(episode.EpisodeNumber)
                          select new
                          {
                              e.proper,
                              e.quality
                          }); //Grab all matches containing matching show name, season number and episode number

                if (ep.Count() > 0)
                {
                    if (ep.Count(y => y.quality < episode.Quality) == ep.Count()) //If they are equal then higher quality episode has not yet been downloaded
                    {
                        Logger.Log("Episode is better quality than previously downloaded, deleting previous version");

                        foreach (DirectoryInfo tvDir in Config.TvRootFolders)
                        {
                            string dir = GetEpisodeDir(episode, tvDir);
                            string fileMask = GetEpisodeFileMask(episode, tvDir);

                            DeleteForUpgrade(dir, fileMask);
                        }

                        return false;
                    }

                    //Check if Both are Propers
                    if (episode.IsProper)
                    {
                        if (ep.Any(p => Convert.ToBoolean(p.proper.Value) && episode.Quality == p.quality))
                        {
                            Logger.Log("Found in Local History");
                            return true;
                        }

                        return false; //Episode in History is not a proper, episode is better than on previosuly downloaded
                    }

                    //Episode to be downloaded is not a proper and episode was found in local history
                    Logger.Log("Found in Local History");
                    return true;
                }
            }
            return false; //Not found, return false
        }

        private bool IsQualityWanted(Episode episode)
        {
            if(Database.IsQualityWanted(episode))
                return true;

            RejectDownloadQualityCount++;
            return false;
        }

        private static string ReplaceSeparatorChars(string s)
        {
            return s.Replace('.', ' ').Replace('-', ' ').Replace('_', ' ');
        }

        private string ShowAlias(string showName)
        {
            foreach (ShowAlias alias in Config.ShowAliases)
            {
                if (showName.ToLower() == alias.BadName.ToLower())
                {
                    showName = alias.Alias;

                    if (Config.VerboseLogging)
                        Log("DEBUG: Show Alias={0}", showName);
                    break;
                }
            }

            const string patternYear = @"\s(?<Year>\d{4}\z)";
            const string replaceYear = @" (${Year})";
            showName = Regex.Replace(showName, patternYear, replaceYear);

            const string patternCountry = @"\s(?<Country>[A-Z]{2}\z)";
            const string replaceCountry = @" (${Country})";
            showName = Regex.Replace(showName, patternCountry, replaceCountry);

            return showName;
        }

        private void GetTitleFix(Episode episode)
        {
            if (Config.VerboseLogging)
                Log("Getting Fixed Title for: " + episode.FeedItem.Title);

            string titleFix;

            if (episode.IsDaily)
                titleFix = string.Format("{0} - {1}", episode.AirDate.ToString("yyyy-MM-dd"), episode.EpisodeName);
            else if (episode.IsMulti)
                titleFix = string.Format("{0}x{1:D2}-{0}x{2:D2} - {3} & {4}",
                    episode.SeasonNumber, episode.EpisodeNumber, episode.EpisodeNumber2,
                    episode.EpisodeName, episode.EpisodeName2);
            else
                titleFix = string.Format("{0}x{1:D2} - {2}",
                    episode.SeasonNumber, episode.EpisodeNumber, episode.EpisodeName);

            episode.FeedItem.TitleFix = string.Format("{0} - {1}", episode.ShowName, titleFix).TrimEnd(' ', '-');

            if (Config.VerboseLogging)
                Log("Title Fix is: {0}", episode.FeedItem.TitleFix);
        }

        private void DeleteForProper(string dir, string fileMask)
        {
            //Delete old download to make room for proper!

            if (!Directory.Exists(dir))
                return;

            foreach (string ext in Config.VideoExt)
            {
                string[] matchingFiles = Directory.GetFiles(dir, fileMask + ext);

                if (matchingFiles.Length != 0)
                {
                    //Delete Matching File(s)
                    foreach (string m in matchingFiles)
                    {
                        Log("Deleting Episode on Disk for PROPER: " + m, true);
                        File.Delete(m);
                    }
                }
            }
        }

        private void DeleteForUpgrade(string dir, string fileMask)
        {
            //Delete old download to make room for better quality download

            if (!Directory.Exists(dir))
                return;

            foreach (string ext in Config.VideoExt)
            {
                string[] matchingFiles = Directory.GetFiles(dir, fileMask + ext);

                if (matchingFiles.Length != 0)
                {
                    //Delete Matching File(s)
                    foreach (string m in matchingFiles)
                    {
                        Log("Deleting Episode on Disk for better qualit: " + m, true);
                        File.Delete(m);
                    }
                }
            }
        }

        internal void Log(string message)
        {
            Logger.Log(message);
        }

        internal void Log(string message, params object[] para)
        {
            Logger.Log(message, para);
        }

        internal void Log(string message, bool showInSummary)
        {
            if (showInSummary) Summary.Add(message);
            Log(message);
        }

        internal void Log(string message, bool showInSummary, params object[] para)
        {
            Log(String.Format(message, para), showInSummary);
        }
    }
}