## SAB Scripts: ##
A collection of Post Processing Scripts for the SABnzbd Newsgroup Daemon, running on Windows.

[![](https://www.paypal.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=AF7SYT2LWMCZU)

---

## SABSync 0.9.9.6 Released ##
Fixed an issue where the Proper name for a show was not returned properly with the show contained two upper-case letters alone (CSI: NY) was one.

---

## SABSync 0.9.9.5 Released ##
Test SAB button in Options now works
Fixed adding daily shows to history table
If a show doesn't have a runtime it will get added properly now

---

## SABSync 0.9.9.4 Released ##
New Shows view
Updates from TheTVDB work properly now

---

## SABSync 0.9.9.3 Released ##
Fixed:
File -> Exit will now work
Shows that SABnzbd renames to begin words with upper-case were not properly getting matched when looking for show quality (Skipping them even though they were wanted)

Added:
History will sort by Date Descending (Newest on top)
Log files also sorted by Date Descending (Newest on top)
Both the adds above really only apply to new installs due to GUI settings being saved between exiting and opening.

---

## SABSync 0.9.9.2 Released ##
Fixed: Unable to edit the Shows view, somehow I disabled the option, sorry!

---

## SABSync 0.9.9.1 Released ##
Fixed:
Adding shows to history with Add time always in the AM
Proper check failing to properly check the History table for matches
Built for x86 (32-bit) only due to lack of x64 libraries

Added:
Log viewer (under Tools) - Will provide a list of Logs currently in the log dir, double clicking one will open that file in your default editor for ".txt" files.

---

## SABSync 0.9.9.0 Released ##
This is a whole new release (well almost). Config is done within SABSync (no separate GUI) and is stored in the users application data folder (instead of with the app itself). The Database holds show and episode information + a history of what you have downloaded. It's been tested, though bug may appear, so please let me know if you find anything.

---

## Sab2Torrent 0.1.0 Released ##
Sab2Torrent is a Post-Processing Script that creates a torrent for the file downloaded and sends the torrent to uTorrent. TV Shows will not work properly (unless they are stored in an individual folder) or the Torrent file will contain all the shows in the directory.

---

## SABSync 0.9.8.2 Released ##
Updated to support running on Linux under mono, Icon is also included for SABSync.exe. If you have any problems with SABSync on Linux, please let me know.

---

## SABSyncGUI 0.4.2 Released ##
No notable changes except to add support for running on Linux under mono. (Creating a scheduled task under Ubuntu will not work though).

---

## SABSync 0.9.8.1 Released ##
**DotNet 4.0 Required**

Grab it from here (Full Installer):

http://www.microsoft.com/downloads/details.aspx?familyid=0A391ABD-25C1-4FC0-919F-B21F31AB88B7&displaylang=en

or here (Web Installer):

http://www.microsoft.com/downloads/details.aspx?FamilyID=9cfb2d51-5ff4-4491-b0e5-b386f32c0992&displaylang=en

SABSync 0.9.8.1 fixes issues with finding files without episode names on disk. Also if an episode name cannot be grabbed from TheTVDb.com it will be blank and the resulting Queue Item will be "Show Name - 1x01" (No more extra Dash). The imported NZB check will properly check for the full name of a NZB (not limited to 80 characters). There has also been a major reworking of the code (thanks John), but things look good, please report any issues.

---

## SABSyncGUI 0.4.1 Released ##
SABSyncGUI 0.4.1 will now use the computer name instead of localhost to add the scheduled task. It also will take a maximum of 60 minutes as the modifier for time between runs.

---

## SABSync 0.9.8 Released ##
SABSync 0.9.8 fixes a big with 1x05 as the season/episode convention.
Adds support for season/episode convention of 1x05 in non-Newzbin feeds.

---

## TVMove 0.5.0 Released ##
No need to enter in your SAB sorting string, it will now look for 1x05 or S01E05 on disk and copy to the temp folder. Better logging for tracing issues has also been added.

---

## TVConvert 0.5.0 Released ##
No real change to functionality, but better logging has been added. Supports 1x01 and S01E05 naming conventions. If the proper episode information can't be processed (Show Name, Season Number, Episode Number, Episode Name) it will convert the file, but will not run Atomic Parsley on it.

---

## SABSync 0.9.6 Released ##
Fixes a bug where SABSync will crash instead of exiting properly when run from a Scheduled Task (unable to press a button to exit gracefully as it is not running interactively).

---

## SABSyncGUI 0.4.0 Released ##
Create Scheduled Task right from the GUI, test SABSync from the GUI. For systems with UAC (Vista, 7, Server 2008), Run as Administrator

---

## SABSync 0.9.5 Released ##
-(Added) Support for NZBsRus.com
-(Added) Default Download Quality for all shows (downloadQuality in Config), download quality will be checked for a match to that specific show, then the download quality for all other shows if a show match is not found.
-(Added) Support for cleaning (deleting) of log files order than X days (config option deleteLogs) - Setting to 0 (zero) will not delete log files
I have also included the GUI as part of this release.

---

## SABSyncGUI 0.3.5 Released ##
Support for new config file options in SABSync, editing of RSS, Alias and Quality config files within the GUI. This should virtually eliminate the need to edit the config file manually. Switching tabs in the GUI will save the settings, if you change the filename of a .config file it will create a new file when you switch over to the config edit tab.

---

## SABSync 0.9.3 Released ##
SABSync 0.9.3 Adds the config option verboseLogging, when set to true additional logging is written to the screen and log file.
SABSync 0.9.2 Fixes an issue with PROPERs and multiple TVRoot folders
SABSync 0.9.1 Adds logging when an Episode is deleted in favour of a PROPER
-(Fixed) NZB File Matching, will now properly match NZB files with non-space separators to their space separated counter-parts and vice-versa, fixing issues with feeds from different sites (nzbmatrix vs nzbs.org) - Fix also will allow PROPERs to be downloaded, as this was broken in 0.8.2

-(Fixed) Replace illegal URL characters with their proper escape characters

-(Fixed) SABSync will now remove all leading and trailing periods, spaces, dashes and underscores from the fileMask to find episodes already on disk, which will make it more strict and support finding files that do not have episode names (searching for [**S01E01**] instead of [**- S01E01 -**])

-(Added) Support for naming conventions other than the one defined in your script (1x05, S01E05, 105 only)

-(Added) Queue checking will now look for the "nzbId" of the RSS Feed item before it adds the item to the queue, useful for long queues that still have items being downloaded from the various NZB sites. There will be an issue if you add a NZB from one site that has the same NZB ID as one found in the RSS feed from another site. For Example, NZBs.org ID 12345 is added (and still not loaded into SAB), an RSS Feed scanning NZBMatrix.com finds soemthing needed with ID 12345, it will not be added as SABSync can't determine which site the NZBID applies to - this is VERY VERY unlikely to happen (NZBs.org is up in 600K for nzbId's, NZBMatrix is down in the 300K range currently), but it is a possibility, after the NZB is properly loaded into SABnzbd, SABSync would then be able to add the one found in the RSS Feed, if it is still pending to be added.

-(Added) Support for multiple TV Root Directories, these should be added to SABSync.exe.config in the same location, separated by a semi-colon.

---

## TVConvert 0.2.2 Released ##
Fixes a bug that forced a user to press a key to start conversion with HandBrakeCLI.

---

## SABSync 0.8.2 Released ##
Fixed a bug where multiple copies of the same show/episode were added when SABnzbd was still fetching and it was found on a different source in the same run. Once imported the Queue check should catch dupes, but if SABSync added multiple NZBs from the first site, SABnzbd may take a bit to fetch them.

---

## SABSync 0.8.1 Released ##
Quick bug fixes for checking the imported NZB folder and for checking the internal Queued list of SABSync for items that have been added in the run of SABsync (fixes an issue where an item will be added to the queue 2x if the NZB is still being fetched by SABnzbd and it find it in another RSS feed, since it fails to find the first item in the Queue or the imported NZB folder).

---

## SABSync 0.8.0 Released ##
SABSync 0.8.0 Adds support for propers (Config option downloadPropers [to false](defaults.md)). Enabling the download of propers will delete the existing file if an matching NZB file is not found - ie the PROPER has not been downloaded. Support is not fully tested, please advise of any issues.

For episodes that did not retrieve the episode name from TheTVDb.com, the episode name will be set to unknown so it is not downloaded again (if the NZB name was different and it could not find the episode matching "Show Name - S01E05" since it would be looking "**- S01E05 -**" in disk, the second dash would be stripped by SAB as there is no episode.

NZB check should also find similar NZB files (using a period instead of a space and vice versa).

I have included a new EXE, named SABSyncHide.exe which will run SABSync.exe as a hidden window,no more console window in-case you are running this on a desktop computer where you do not want to see it pop-up every time the scheduled task runs.

Please let me know if you have any issues.