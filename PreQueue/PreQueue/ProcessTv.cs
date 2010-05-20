using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PreQueue
{
    class ProcessTv
    {
        private static readonly Logger Logger = new Logger();
        private const string PatternS01E01E02 = @"[Ss](?<Season>(?:\d{1,2}))[Ee](?<EpisodeOne>(?:\d{1,2}))[Ee](?<EpisodeTwo>(?:\d{1,2}))";
        private const string PatternS01E01 = @"[Ss](?<Season>(?:\d{1,2}))[Ee](?<Episode>(?:\d{1,2}))";
        private const string Pattern1x01 = @"(?<Season>(?:\d{1,2}))[Xx](?<Episode>(?:\d{1,2}))";
        private const string PatternDaily = @"(?<Year>\d{4}).{1}(?<Month>\d{2}).{1}(?<Day>\d{2})";
    
        internal static string GetNzbName(string showNameSab, int seasonNumberSab, int episodeNumberSab, string episodeNameSab)
        {
            //Do TV Show Renaming
            string showName = ShowAlias(showNameSab);
            int seasonNumber = seasonNumberSab;
            int episodeNumber = episodeNumberSab;
            string episodeName = episodeNameSab;

            return String.Format("{0} - {1}x{2:D2} - {3}", showName, seasonNumber, episodeNumber, episodeName);
        }

        internal static string GetNzbName(string showNameSab, int seasonNumberSab, int episodeNumberSab)
        {
            //Do TV Show Renaming

            string showName = ShowAlias(showNameSab);
            int seasonNumber = seasonNumberSab;
            int episodeNumber = episodeNumberSab;
            string episodeName = TvDb.CheckTvDb(showName, seasonNumber, episodeNumber);
            string nzbName = null;

            if (episodeName != null)
                nzbName = String.Format("{0} - {1}x{2:D2} - {3}", showName, seasonNumber, episodeNumber, episodeName);

            else
                nzbName = String.Format("{0} - {1}x{2:D2}", showName, seasonNumber, episodeNumber);

            return nzbName;
        }

        internal static string GetNzbName(string nzbNameSab)
        {
            //Do TV Show Renaming

            string nzbName = nzbNameSab;

            //Check for S01E01E02
            Match titleMatchMulti = Regex.Match(nzbNameSab, PatternS01E01E02);
            if (titleMatchMulti.Success)
                return GetNzbNameS01E01E02(titleMatchMulti, nzbNameSab);

            //Check for S01E01
            Match titleMatch = Regex.Match(nzbNameSab, PatternS01E01);
            if (titleMatch.Success)
                return GetNzbNameS01E01(titleMatch, nzbNameSab);

            //Check for 1x01
            Match titleMatchX = Regex.Match(nzbNameSab, Pattern1x01);
            if (titleMatchX.Success)
                return GetNzbName1x01(titleMatchX, nzbNameSab);

            //Daily Show Title Check
            Match titleMatchDaily = Regex.Match(nzbNameSab, PatternDaily);
            if (titleMatchDaily.Success)
                return GetNzbNameDaily(titleMatchDaily, nzbNameSab);

            return nzbName;
        }

        private static string GetNzbNameS01E01E02(Match match, string title)
        {
            string[] titleSplitMulti = Regex.Split(title, PatternS01E01E02);
            string showName = titleSplitMulti[0].Replace('.', ' ');
            showName = showName.TrimEnd();
            showName = ShowAlias(showName);

            int seasonNumber = 0;
            int episodeNumberOne = 0;
            int episodeNumberTwo = 0;

            Int32.TryParse(match.Groups["Season"].Value, out seasonNumber);
            Int32.TryParse(match.Groups["EpisodeOne"].Value, out episodeNumberOne);
            Int32.TryParse(match.Groups["EpisodeTwo"].Value, out episodeNumberTwo);

            string episodeOneName = TvDb.CheckTvDb(showName, seasonNumber, episodeNumberOne);
            string episodeTwoName = TvDb.CheckTvDb(showName, seasonNumber, episodeNumberTwo);
            string titleFix = showName + " - " + seasonNumber + "x" + episodeNumberOne.ToString("D2") + "-" + seasonNumber + "x" + episodeNumberTwo.ToString("D2") + " - " + episodeOneName + " & " + episodeTwoName;

            return titleFix;

        }

        private static string GetNzbNameS01E01(Match match, string title)
        {
            string showName = ShowAlias(Regex.Split(title, PatternS01E01)[0].Replace('.', ' ').TrimEnd());
            int seasonNumber, episodeNumber;
            int.TryParse(match.Groups["Season"].Value, out seasonNumber);
            int.TryParse(match.Groups["Episode"].Value, out episodeNumber);

            string episodeName = TvDb.CheckTvDb(showName, seasonNumber, episodeNumber);
            string titleFix = string.Format("{0} - {1}x{2:D2} - {3}",
                showName, seasonNumber, episodeNumber, episodeName);

            return titleFix;
        }

        private static string GetNzbName1x01(Match match, string title)
        {
            string showName = ShowAlias(Regex.Split(title, Pattern1x01)[0].Replace('.', ' ').TrimEnd());
            int seasonNumber, episodeNumber;
            int.TryParse(match.Groups["Season"].Value, out seasonNumber);
            int.TryParse(match.Groups["Episode"].Value, out episodeNumber);

            string episodeName = TvDb.CheckTvDb(showName, seasonNumber, episodeNumber);
            string titleFix = string.Format("{0} - {1}x{2:D2} - {3}",
                showName, seasonNumber, episodeNumber, episodeName);

            return titleFix;
        }

        private static string GetNzbNameDaily(Match match, string title)
        {
            string showName = ShowAlias(Regex.Split(title, PatternDaily)[0].Replace('.', ' ').TrimEnd());
            int year, month, day;
            int.TryParse(match.Groups["Year"].Value, out year);
            int.TryParse(match.Groups["Month"].Value, out month);
            int.TryParse(match.Groups["Day"].Value, out day);

            string episodeName = TvDb.CheckTvDb(showName, year, month, day);
            string titleFix = showName + " - " + year.ToString("D4") + "-" + month.ToString("D2") + "-" + day.ToString("D2") + " - " + episodeName;

            return titleFix;
        }

        private static string ShowAlias(string showName)
        {
            foreach (ShowAlias alias in Config.ShowAliases)
            {
                if (showName.ToLower() == alias.BadName.ToLower())
                {
                    showName = alias.Alias;
                }
            }

            var patternYear = @"\s(?<Year>\d{4}\z)";
            var replaceYear = @" (${Year})";
            showName = Regex.Replace(showName, patternYear, replaceYear);

            var patternCountry = @"\s(?<Country>[A-Z]{2}\z)";
            var replaceCountry = @" (${Country})";
            showName = Regex.Replace(showName, patternCountry, replaceCountry);

            return showName;
        }
    }
}
