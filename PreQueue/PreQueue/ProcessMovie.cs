using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PreQueue
{
    class ProcessMovie
    {
        private static readonly Logger Logger = new Logger();
        internal static string GetNzbName(string nzbNameSab)
        {
            //Do Movie Name Fixing
            var patternYear = @"(?<Year>\d{4}(?![a-zA-Z]))";

            string movieName = null;
            string movieNameOnly = null;
            int year;
            Match yearMatch = Regex.Match(nzbNameSab, patternYear);

            if (yearMatch.Success)
            {
                var titleSplit = Regex.Split(nzbNameSab, patternYear);
                movieNameOnly = titleSplit[0].Replace('.', ' ').Replace('_', ' ').TrimEnd(' ', '(', '.', '-', '_', '[');
                Int32.TryParse(yearMatch.Groups["Year"].Value, out year);

                movieName = movieNameOnly + " (" + year + ")";
            }

            else
                movieName = nzbNameSab;

            return movieName;
        }
    }
}
