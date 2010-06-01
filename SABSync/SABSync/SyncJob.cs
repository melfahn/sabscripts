using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Rss;

// TODO: using System.ServiceModel.Syndication;

namespace SABSync
{
    public class SyncJob
    {
        private const string PatternS01E01E02 =
            @"[Ss](?<Season>(?:\d{1,2}))[Ee](?<EpisodeOne>(?:\d{1,2}))[Ee](?<EpisodeTwo>(?:\d{1,2}))";

        private const string PatternS01E01 = @"[Ss](?<Season>(?:\d{1,2}))[Ee](?<Episode>(?:\d{1,2}))";
        private const string Pattern1X01 = @"(?<Season>(?:\d{1,2}))[Xx](?<Episode>(?:\d{1,2}))";
        private const string PatternDaily = @"(?<Year>\d{4}).{1}(?<Month>\d{2}).{1}(?<Day>\d{2})";

        private static readonly Logger Logger = new Logger();
        public int AcceptCount;
        public int FeedItemCount;
        public int MyFeedsCount;
        public int MyShowsCount;
        public int MyShowsInFeedCount;
        public int RejectDownloadQualityCount;
        public int RejectIgnoredSeasonCount;
        public int RejectInNzbArchive;
        public int RejectOnDiskCount;
        public int RejectPasswordedCount;
        public int RejectShowQualityCount;

        public SyncJob() : this(new Config(), new SabService(), new TvDbService())
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
            Log("IgnoreSeasons: {0}", Config.IgnoreSeasons);

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
            if (nzb.IsPassworded())
            {
                RejectPasswordedCount++;
                Log("Skipping Passworded Report {0}", nzb.Title);
                return;
            }

            if (!IsEpisodeWanted(nzb.Title, nzb.Id, nzb.Description))
                return;
            
            nzb.Title = GetTitleFix(nzb.Title).TrimEnd(' ', '-');
            string queueResponse = Sab.AddByUrl(nzb);

            // TODO: check if Queued.Add need unfixed Title (was previously)
            AcceptCount++;
            Queued.Add(string.Format("{0}: {1}", nzb.Title, queueResponse));
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

        private string GetEpisodeDir(string showName, int seasonNumber, int episodeNumber, DirectoryInfo tvDir)
        {
            if (Config.VerboseLogging)
                Log("Building string for Episode Dir");

            showName = CleanString(showName);

            string snReplace = showName;
            string sDotNReplace = showName.Replace(' ', '.');
            string sUnderNReplace = showName.Replace(' ', '_');

            string zeroSReplace = String.Format("{0:00}", seasonNumber);
            string sReplace = Convert.ToString(seasonNumber);
            string zeroEReplace = String.Format("{0:00}", episodeNumber);
            string eReplace = Convert.ToString(episodeNumber);

            string path = Path.GetDirectoryName(tvDir + Path.DirectorySeparatorChar.ToString() + Config.TvTemplate);

            path = path.Replace(".%ext", "");
            path = path.Replace("%sn", snReplace);
            path = path.Replace("%s.n", sDotNReplace);
            path = path.Replace("%s_n", sUnderNReplace);
            path = path.Replace("%0s", zeroSReplace);
            path = path.Replace("%s", sReplace);
            path = path.Replace("%0e", zeroEReplace);
            path = path.Replace("%e", eReplace);

            if (Config.VerboseLogging)
                Log(path);

            return path;
        }

        private string GetEpisodeFileMask(int seasonNumber, int episodeNumber, DirectoryInfo tvDir)
        {
            if (Config.VerboseLogging)
                Log("Building string for Episode File Mask");

            string zeroSReplace = String.Format("{0:00}", seasonNumber);
            string sReplace = Convert.ToString(seasonNumber);
            string zeroEReplace = String.Format("{0:00}", episodeNumber);
            string eReplace = Convert.ToString(episodeNumber);

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

            if (Config.VerboseLogging)
                Log(fileMask);

            return fileMask;
        }

        private string GetEpisodeDir(string showName, DateTime firstAired, DirectoryInfo tvDir)
        {
            if (Config.VerboseLogging)
                Log("Building string for Episode Dir");
            //int year, month, day;

            string path = Path.GetDirectoryName(tvDir + Path.DirectorySeparatorChar.ToString() + Config.TvDailyTemplate);

            showName = CleanString(showName);

            string tReplace = showName;
            string dotTReplace = showName.Replace(' ', '.');
            string underTReplace = showName.Replace(' ', '_');
            string yearReplace = Convert.ToString(firstAired.Year);
            string zeroMReplace = String.Format("{0:00}", firstAired.Month);
            string mReplace = Convert.ToString(firstAired.Month);
            string zeroDReplace = String.Format("{0:00}", firstAired.Day);
            string dReplace = Convert.ToString(firstAired.Day);

            path = path.Replace(".%ext", "");
            path = path.Replace("%t", tReplace);
            path = path.Replace("%.t", dotTReplace);
            path = path.Replace("%_t", underTReplace);
            path = path.Replace("%y", yearReplace);
            path = path.Replace("%0m", zeroMReplace);
            path = path.Replace("%m", mReplace);
            path = path.Replace("%0d", zeroDReplace);
            path = path.Replace("%d", dReplace);

            if (Config.VerboseLogging)
                Log(path);

            return path;
        }

        //Ends GetDailyShowNamingScheme

        private string GetEpisodeFileMask(DateTime firstAired, DirectoryInfo tvDir)
        {
            if (Config.VerboseLogging)
                Log("Building string for Episode File Mask");

            string fileMask = Path.GetFileName(tvDir + Path.DirectorySeparatorChar.ToString() + Config.TvDailyTemplate);

            string yearReplace = Convert.ToString(firstAired.Year);
            string zeroMReplace = String.Format("{0:00}", firstAired.Month);
            string mReplace = Convert.ToString(firstAired.Month);
            string zeroDReplace = String.Format("{0:00}", firstAired.Day);
            string dReplace = Convert.ToString(firstAired.Day);

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

            if (Config.VerboseLogging)
                Log(fileMask);

            return fileMask;
        }

        //Ends GetDailyShowNamingScheme

        private string CleanString(string name)
        {
            string result = name;
            string[] badCharacters = {"\\", "/", "<", ">", "?", "*", ":", "|", "\""};
            string[] goodCharacters = {"+", "+", "{", "}", "!", "@", "-", "#", "`"};

            for (int i = 0; i < badCharacters.Length; i++)
            {
                result = result.Replace(badCharacters[i], Config.SabReplaceChars ? goodCharacters[i] : "");
            }

            return result.Trim();
        }

        private bool IsShowWanted(string showName)
        {
            foreach (string di in Config.MyShows)
            {
                if (string.Equals(di, CleanString(showName),
                    StringComparison.InvariantCultureIgnoreCase))
                {
                    MyShowsInFeedCount++;
                    Log("'{0}' is being watched.", showName);
                    return true;
                }
            }
            Log("'{0}' is not being watched.", showName);
            return false;
        }

        //Ends IsShowWanted

        private bool IsEpWantedS01E01E02(Match match, string title, string nzbId)
        {
            string[] titleSplitMulti = Regex.Split(title, PatternS01E01E02);
            string showName = titleSplitMulti[0].Replace('.', ' ');
            showName = showName.TrimEnd();
            showName = ShowAlias(showName);

            int seasonNumber;
            int episodeNumberOne;
            int episodeNumberTwo;

            Int32.TryParse(match.Groups["Season"].Value, out seasonNumber);
            Int32.TryParse(match.Groups["EpisodeOne"].Value, out episodeNumberOne);
            Int32.TryParse(match.Groups["EpisodeTwo"].Value, out episodeNumberTwo);

            if (!IsShowWanted(showName))
                return false;

            if (IsSeasonIgnored(showName, seasonNumber))
                return false;

            if (!IsQualityWanted(showName, title))
                return false;

            string episodeOneName = TvDb.CheckTvDb(showName, seasonNumber, episodeNumberOne);
            string episodeTwoName = TvDb.CheckTvDb(showName, seasonNumber, episodeNumberTwo);
            string titleFix = showName + " - " + seasonNumber + "x" + episodeNumberOne.ToString("D2") + "-" +
                seasonNumber + "x" + episodeNumberTwo.ToString("D2") + " - " + episodeOneName + " & " + episodeTwoName;

            titleFix = titleFix.TrimEnd(' ', '-');

            bool needProper = false;

            if (Config.DownloadPropers && title.Contains("PROPER"))
            {
                if (!Sab.IsInQueue(title, titleFix, nzbId) && !InNzbArchive(title, titleFix))
                    needProper = true;
            }


            foreach (DirectoryInfo tvDir in Config.TvRootFolders)
            {
                string dir = GetEpisodeDir(showName, seasonNumber, episodeNumberOne, tvDir);
                string fileMask = GetEpisodeFileMask(seasonNumber, episodeNumberOne, tvDir);

                if (needProper)
                    DeleteForProper(dir, fileMask);

                if (IsOnDisk(dir, fileMask))
                    return false;

                if (IsOnDisk(dir, seasonNumber, episodeNumberOne))
                    return false;
            }

            if (Sab.IsInQueue(title, titleFix, nzbId))
                return false;

            if (InNzbArchive(title, titleFix))
                return false;

            if (IsQueued(titleFix))
                return false;

            return true;
        }

        private bool IsEpWantedS01E01(Match match, string title, string nzbId, string description)
        {
            string showName = ShowAlias(Regex.Split(title, PatternS01E01)[0].Replace('.', ' ').TrimEnd(' ', '-'));
            string episodeName;
            int seasonNumber, episodeNumber;
            int.TryParse(match.Groups["Season"].Value, out seasonNumber);
            int.TryParse(match.Groups["Episode"].Value, out episodeNumber);

            if (!IsShowWanted(showName))
                return false;

            //Get Epsiode name from title (used for lilx.net feeds)
            if (Regex.Split(title, PatternS01E01)[3].StartsWith(" - "))
                episodeName = Regex.Split(title, PatternS01E01)[3].TrimStart('-', ' ');

            else
                episodeName = TvDb.CheckTvDb(showName, seasonNumber, episodeNumber);
            
            string titleFix = string.Format("{0} - {1}x{2:D2} - {3}",
                showName, seasonNumber, episodeNumber, episodeName).TrimEnd(' ', '-');

            bool needProper = false;

            if (IsSeasonIgnored(showName, seasonNumber)
                || !IsQualityWanted(showName, title, description)
                )
                return false;

            if (Config.DownloadPropers && title.Contains("PROPER"))
                if (!Sab.IsInQueue(title, titleFix, nzbId) && !InNzbArchive(title, titleFix))
                    needProper = true;

            foreach (DirectoryInfo tvDir in Config.TvRootFolders)
            {
                string dir = GetEpisodeDir(showName, seasonNumber, episodeNumber, tvDir);
                string fileMask = GetEpisodeFileMask(seasonNumber, episodeNumber, tvDir);

                if (needProper)
                    DeleteForProper(dir, fileMask);

                if (IsOnDisk(dir, fileMask)
                    || IsOnDisk(dir, seasonNumber, episodeNumber)
                    )
                    return false;
            }

            if (Sab.IsInQueue(title, titleFix, nzbId)
                || InNzbArchive(title, titleFix)
                    || IsQueued(titleFix)
                )
                return false;



            return true;
        }

        private bool IsEpWanted1X01(Match match, string title, string nzbId)
        {
            string showName = ShowAlias(Regex.Split(title, Pattern1X01)[0].Replace('.', ' ').TrimEnd());
            int seasonNumber, episodeNumber;
            int.TryParse(match.Groups["Season"].Value, out seasonNumber);
            int.TryParse(match.Groups["Episode"].Value, out episodeNumber);
            if (!IsShowWanted(showName))
                return false;

            string episodeName = TvDb.CheckTvDb(showName, seasonNumber, episodeNumber);
            string titleFix = string.Format("{0} - {1}x{2:D2} - {3}",
                showName, seasonNumber, episodeNumber, episodeName).TrimEnd(' ', '-');

            bool needProper = false;

            if (IsSeasonIgnored(showName, seasonNumber)
                || !IsQualityWanted(showName, title)
                )
                return false;

            if (Config.DownloadPropers && title.Contains("PROPER"))
                if (!Sab.IsInQueue(title, titleFix, nzbId) && !InNzbArchive(title, titleFix))
                    needProper = true;

            foreach (DirectoryInfo tvDir in Config.TvRootFolders)
            {
                string dir = GetEpisodeDir(showName, seasonNumber, episodeNumber, tvDir);
                string fileMask = GetEpisodeFileMask(seasonNumber, episodeNumber, tvDir);

                if (needProper)
                    DeleteForProper(dir, fileMask);

                if (IsOnDisk(dir, fileMask)
                    || IsOnDisk(dir, seasonNumber, episodeNumber)
                    )
                    return false;
            }

            if (Sab.IsInQueue(title, titleFix, nzbId)
                || InNzbArchive(title, titleFix)
                    || IsQueued(titleFix)
                )
                return false;

            return true;
        }

        private bool IsEpWantedDaily(Match match, string title, string nzbId)
        {
            string showName = ShowAlias(Regex.Split(title, PatternDaily)[0].Replace('.', ' ').TrimEnd());

            DateTime firstAired = DateTime.Parse(
                match.Groups["Year"].Value + "-" +
                    match.Groups["Month"].Value + "-" +
                        match.Groups["Day"].Value);

            if (!IsShowWanted(showName))
                return false;

            string episodeName = TvDb.CheckTvDb(showName, firstAired);
            string titleFix = showName + " - " + firstAired.ToString("yyyy-MM-dd") + " - " + episodeName;

            titleFix = titleFix.TrimEnd(' ', '-');

            bool needProper = false;

            if (!IsQualityWanted(showName, title))
                return false;

            if (Config.DownloadPropers && title.Contains("PROPER"))
            {
                if (!Sab.IsInQueue(title, titleFix, nzbId) && !InNzbArchive(title, titleFix))
                    needProper = true;
            }

            foreach (DirectoryInfo tvDir in Config.TvRootFolders)
            {
                string dir = GetEpisodeDir(showName, firstAired, tvDir);
                string fileMask = GetEpisodeFileMask(firstAired, tvDir);

                if (needProper)
                    DeleteForProper(dir, fileMask);

                if (IsOnDisk(dir, fileMask))
                    return false;
            }

            if (Sab.IsInQueue(title, titleFix, nzbId))
                return false;

            if (InNzbArchive(title, titleFix))
                return false;

            if (IsQueued(titleFix))
                return false;

            return true;
        }

        private bool IsEpisodeWanted(string title, string nzbId, string description)
        {
            Log("----------------------------------------------------------------");
            Log("Verifying '{0}'", title);
            try
            {
                //Check for S01E01E02
                Match titleMatchMulti = Regex.Match(title, PatternS01E01E02);
                if (titleMatchMulti.Success)
                    return IsEpWantedS01E01E02(titleMatchMulti, title, nzbId);

                //Check for S01E01
                Match titleMatch = Regex.Match(title, PatternS01E01);
                if (titleMatch.Success)
                    return IsEpWantedS01E01(titleMatch, title, nzbId, description);

                //Check for 1x01
                Match titleMatchX = Regex.Match(title, Pattern1X01);
                if (titleMatchX.Success)
                    return IsEpWanted1X01(titleMatchX, title, nzbId);

                //Daily Show Title Check
                Match titleMatchDaily = Regex.Match(title, PatternDaily);
                if (titleMatchDaily.Success)
                    return IsEpWantedDaily(titleMatchDaily, title, nzbId);
            }
            catch (Exception e)
            {
                Log("Unsupported Title: {0} - {1}", title, e);
                return false;
            }
            Log("Unsupported Title: {0}", title);
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

        private bool IsSeasonIgnored(string showName, int seasonNumber)
        {
            if (Config.IgnoreSeasons.Contains(showName))
            {
                string[] showsSeasonIgnore = Config.IgnoreSeasons.Trim(';', ' ').Split(';');
                foreach (string showSeasonIgnore in showsSeasonIgnore)
                {
                    if (Config.VerboseLogging)
                        Log("Checking Ignored Season for match: " + showSeasonIgnore);

                    string[] showNameIgnoreSplit = showSeasonIgnore.Split('=');
                    string showNameIgnore = showNameIgnoreSplit[0];
                    int seasonIgnore = Convert.ToInt32(showNameIgnoreSplit[1]);

                    if (showNameIgnore == showName)
                    {
                        if (seasonNumber <= seasonIgnore)
                        {
                            RejectIgnoredSeasonCount++;
                            Log("Ignoring '{0}' Season '{1}'  ", showName, seasonNumber);
                            return true;
                        } //End if seasonNumber Less than or Equal to seasonIgnore
                    } //Ends if showNameIgnore equals showName
                } //Ends foreach loop for showsSeasonIgnore
            } //Ends if Config.IgnoreSeasons contains showName
            return false; //If Show Name is not being ignored or that season is not ignored return false
        }

        //Ends IsSeasonIgnored

        private bool IsQualityWanted(string showName, string rssTitle)
        {
            foreach (ShowQuality q in Config.ShowQualities)
            {
                if (showName.ToLower() == q.Name.ToLower())
                {
                    if (rssTitle.ToLower().Contains(q.Quality.ToLower()))
                    {
                        Log("Quality -{0}- is wanted for: {1}.", q.Quality, showName);
                        return true;
                    }
                    RejectShowQualityCount++;
                    Log("Quality is not wanted");
                    return false;
                }
            }

            foreach (string quality in Config.DownloadQualities)
            {
                if (rssTitle.ToLower().Contains(quality.ToLower()))
                {
                    Log("Quality is wanted - Default");
                    return true;
                }
            }
            RejectDownloadQualityCount++;
            Log("Quality is not wanted");
            return false;
        }

        private bool IsQualityWanted(string showName, string rssTitle, string description)
        {
            foreach (ShowQuality q in Config.ShowQualities)
            {
                if (showName.ToLower() == q.Name.ToLower())
                {
                    if (rssTitle.ToLower().Contains(q.Quality.ToLower()) || description.ToLower().Contains(q.Quality.ToLower()))
                    {
                        Log("Quality -{0}- is wanted for: {1}.", q.Quality, showName);
                        return true;
                    }
                    RejectShowQualityCount++;
                    Log("Quality is not wanted");
                    return false;
                }
            }

            foreach (string quality in Config.DownloadQualities)
            {
                if (rssTitle.ToLower().Contains(quality.ToLower()) || description.ToLower().Contains(quality.ToLower()))
                {
                    Log("Quality is wanted - Default");
                    return true;
                }
            }
            RejectDownloadQualityCount++;
            Log("Quality is not wanted");
            return false;
        }

        private bool IsQueued(string rssTitleFix)
        {
            //Checks Queued List for "Fixed" name, resolves issue when Item is added, but not properly renamed and it is found at another source.

            if (Queued.Contains(rssTitleFix + ": ok"))
                return true;

            return false;
        }

        private bool InNzbArchive(string rssTitle, string rssTitleFix)
        {
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
                RejectInNzbArchive++;
                Log("Episode in archive: '{0}'", true, nzbFileName + ".nzb.gz");
                return true;
            }

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
                if (Config.VerboseLogging)
                    Log("Checking for alias: " + alias.BadName);

                if (showName.ToLower() == alias.BadName.ToLower())
                {
                    showName = alias.Alias;

                    if (Config.VerboseLogging)
                        Log("Alias found, new name is: " + showName);
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

        private string GetTitleFix(string title)
        {
            if (Config.VerboseLogging)
                Log("Getting Fixed Title for: " + title);

            string titleFix = null;

            const string patternMulti = @"[Ss](?<Season>(?:\d{1,2}))[Ee](?<EpisodeOne>(?:\d{1,2}))E(?<EpisodeTwo>(?:\d{1,2}))";
            const string pattern = @"[Ss](?<Season>(?:\d{1,2}))[Ee](?<Episode>(?:\d{1,2}))";
            const string patternX = @"(?<Season>(?:\d{1,2}))[Xx](?<Episode>(?:\d{1,2}))";
            const string patternDaily = @"(?<Year>\d{4}).{1}(?<Month>\d{2}).{1}(?<Day>\d{2})";

            //S01E01E02
            Match titleMatchMulti = Regex.Match(title, patternMulti);

            if (titleMatchMulti.Success)
            {
                string[] titleSplitMulti = Regex.Split(title, patternMulti);
                string showName = titleSplitMulti[0].Replace('.', ' ');
                showName = showName.TrimEnd();
                showName = ShowAlias(showName);

                int seasonNumber;
                int episodeNumberOne;
                int episodeNumberTwo;

                Int32.TryParse(titleMatchMulti.Groups["Season"].Value, out seasonNumber);
                Int32.TryParse(titleMatchMulti.Groups["EpisodeOne"].Value, out episodeNumberOne);
                Int32.TryParse(titleMatchMulti.Groups["EpisodeTwo"].Value, out episodeNumberTwo);

                string episodeOneName = TvDb.CheckTvDb(showName, seasonNumber, episodeNumberOne);
                string episodeTwoName = TvDb.CheckTvDb(showName, seasonNumber, episodeNumberTwo);
                titleFix = showName + " - " + seasonNumber + "x" + episodeNumberOne.ToString("D2") + "-" +
                    seasonNumber + "x" + episodeNumberTwo.ToString("D2") + " - " + episodeOneName +
                        " & " + episodeTwoName;
            }

            //S01E01
            Match titleMatch = Regex.Match(title, pattern);

            if (titleMatch.Success)
            {
                string[] titleSplit = Regex.Split(title, pattern);
                string showName = titleSplit[0].Replace('.', ' ').TrimEnd(' ', '-');
                showName = showName.TrimEnd();
                showName = ShowAlias(showName);

                int seasonNumber;
                int episodeNumber;
                string episodeName;

                Int32.TryParse(titleMatch.Groups["Season"].Value, out seasonNumber);
                Int32.TryParse(titleMatch.Groups["Episode"].Value, out episodeNumber);

                //Get Epsiode name from title (used for lilx.net feeds)
                if (Regex.Split(title, PatternS01E01)[3].StartsWith(" - "))
                    episodeName = Regex.Split(title, PatternS01E01)[3].TrimStart('-', ' ');

                else
                    episodeName = TvDb.CheckTvDb(showName, seasonNumber, episodeNumber);

                titleFix = showName + " - " + seasonNumber + "x" + episodeNumber.ToString("D2") + " - " + episodeName;
            }

            //1x01
            Match titleMatchX = Regex.Match(title, patternX);

            if (titleMatchX.Success)
            {
                string[] titleSplitX = Regex.Split(title, patternX);
                string showName = titleSplitX[0].Replace('.', ' ');
                showName = showName.TrimEnd();
                showName = ShowAlias(showName);

                int seasonNumber;
                int episodeNumber;

                Int32.TryParse(titleMatchX.Groups["Season"].Value, out seasonNumber);
                Int32.TryParse(titleMatchX.Groups["Episode"].Value, out episodeNumber);

                string episodeName = TvDb.CheckTvDb(showName, seasonNumber, episodeNumber);
                titleFix = showName + " - " + seasonNumber + "x" + episodeNumber.ToString("D2") + " - " + episodeName;
            }

            //Daily Show Title
            Match titleMatchDaily = Regex.Match(title, patternDaily);

            if (titleMatchDaily.Success)
            {
                string[] titleSplitDaily = Regex.Split(title, patternDaily);
                string showName = titleSplitDaily[0].Replace('.', ' ');
                showName = showName.TrimEnd();
                showName = ShowAlias(showName);

                DateTime firstAired = DateTime.Parse(
                    titleMatchDaily.Groups["Year"].Value + "-" +
                        titleMatchDaily.Groups["Month"].Value + "-" +
                            titleMatchDaily.Groups["Day"].Value);

                string episodeName = TvDb.CheckTvDb(showName, firstAired);
                titleFix = showName + " - " + firstAired.ToString("yyyy-MM-dd") + " - " + episodeName;
            }
            if (Config.VerboseLogging)
                Log("Title Fix is: " + titleFix);

            return titleFix;
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

        #region Nested type: Show

        public class Show
        {
            public string Name { get; set; }
            public int Season { get; set; }
            public int Episode { get; set; }
        }

        #endregion
    }
}