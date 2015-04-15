# Introduction #

SABSync will check your TV directory for the shows you want, downloading only shows that are missing. It supports ignoring seasons for shows that you do not want (you choose the season and that season + all previous seasons are ignored). SABSync will also check the queue for episodes pending to be download (for both NZBs SAB has retrieved the info for and ones still 'fetching') in addition SABSync will check your imported NZB folder to see if that NZB has already been imported (great for times when SAB fails to unpack or rename the download correctly).

Support for most if not all naming conventions that SABnzbd supports (simply copy and paste you TV Renaming string into SABSync's configuration), support for Daily Shows as well.

SABSync is developed in C# Using version 2.0 of the .Net Framework and is limited to Windows (mono on Linux may also work, but this is untested). Thanks to Koonfused (Kay.One) for the assistance in developing this great application.

SABnzbd 0.5+ is required to use SABSync, due to the way the Queue Check is being done as well as the support for priority on individual NZBs and Daily show naming conventions.


## Config File ##
  * tvRoot - The full path to your TV Shows, multiple paths can be set, separated by semi-colons.
  * tvTemplate - This is taken from the Series Sorting section of SABnzbd's Sorting Configuration (Copy and Paste the Sort String)
  * tvDailyTemplate - This is from the Date Sorting section of SABnzbd's Sorting Configuration (Again Copy and Paste the Sort String)
  * videoExt - The extensions that SABSync should consider to be video files (when checking on disk for needed episodes)
  * ignoreSeasons - The shows and seasons you want to ignore (this will ignore all seasons below the value entered)
  * nzbDir - The full path to SABnzbd's Imported NZB folder
  * sabnzbdInfo - Hostname/IP Address & Port for SABnzbd
  * username - Username for SABnzbd
  * password - Password for for SABnzbd
  * apiKey - API Key for SABnzbd
  * priority - The Priority that NZB's added through SABSync should have (Low =-1, Normal =0, High=1)
  * sabReplaceChars - True or False value, set to true if SABnzbd replaces Illegal characters or false if SABnzbd removes them instead
  * downloadQuality - Quality used for TVNzb.com, as SD/HD content is in the same feed
  * rss - the name of the file that holds your RSS Feeds (Full or Relative path)
  * alias - Name of the file that contains aliases for shows (to match TheTVDB/Folders on Disk)
  * quality - Name of the file that contains Shows and the quality you want those shows in (if using different feeds with SD/HD content)
  * verboseLogging - Enables additional logging for better diagnosis
  * deleteLogs - Deletes logs older than X days (Setting to 0 (zero) will not delete them.

## RSS.Config ##
Feed Name|Feed URL - Name/URL, separated by a '|' (Pipe)

## Alias.Config ##
Show Name from RSS Feed|Show Name on TheTVDB.com/On Disk

## Quality.Config ##
Show Name|Download Quality - Show Name/Download Quality (xvid or 720p), separated by a '|' (Pipe)