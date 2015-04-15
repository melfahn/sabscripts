
---

## SABSync 0.7.2 Released ##
UPDATE: 0.7.2 - Fixed a bug with renaming (for shows with illegal characters). Added support for Quality per show, see Quality.config for an example, config file has also been updated to have the quality.config file name there. TVnzb.com will still use the downloadQuality config option (saves you from having to add all your shows to the quality.config file if you use a TVnzb.com feed).

UPDATE: 0.7.1 - Renaming now occurs as part of adding the NZB to the queue, speeding up the overall process.

SABSync has been updated! It now supports RSS Feeds from NZBMatrix, NewzBin, NZBs.org and TVnzb.com, other feeds may also be supported, but are untested.

Episode Names are grabbed from TheTVDB.com (if it is not found the episode name will be "unknown"). Queue Items are renamed to "Show Name - 1x05 - Episode Name" to support downloads from NewzBin as well as using multiple sites that may have different naming conventions (Show.Name vs Show Name).

The config file has been updated to include downloadQuality (used for TVnzb.com feeds, since all shows are listed in one feed) and alias (name of the Alias.config file if changed from Default).
These new change has not been added to SABSyncGUI yet.

---

## TVConvert 0.2.1 Released ##
Added the ability to choose the HandBrakeCLI Preset to use, setup for iPhone & iPod Touch by default. Make sure you use "AND" (all caps) instead of "&" in the config (without quotes) as "&" is used in XML files (such as the config file). Notes on this are in the config file as well.

---

## XBox360 0.3.2 Released ##
Fixed an issue where I didn't remove some code from when I was testing, causing it to terminate abnormally (since you couldn't press a button to continue when SAB ran XBox360 after a download, sorry!

---

## XBox360 0.3.1 Released ##
I finally got around to fixing the code that I broke, everything should be working now, still only supports up to 3 ISOs for extraction, but it can verify and extract or any combo of the two, it does NOT have to verify with abgx360 to extract since it all gets stripped in the extract anyways.

---

## Movies 0.2.4 Released ##
Updated to include allow moving between volumes and an option to use the folder name as the Movie Name (as opposed to what is passed from SABnzbd), this should allow you to rename the final folder SAB extracts to using their renamer and have a cleaner name for the Video File.

Option: useFolderName true or false

---

## XBox360 0.3.0 Issues ##
Issues with Multi-disk games and both verifying with abgx and trying to extract - I'm looking into it. For now I have removed the download and restored the link for 0.2.0... since it works.

---

## XBox360 0.3.0 Released ##
This update adds the ability to auto-extract ISO's for use with JTAG'd XBox 360's. It uses exiso (included), major changes to the configuration file were made, make sure you update your changes, also a lot of code was re-written, I have done some testing, but please let me know if there are any issues. Currently extracting supports games with 1-3 DVD's, but for multiple DVD games it will not combine the folders, you will need to do this automatically, this is due to exiso not overwriting pre-existing folders, and .Net not supporting overwriting/merging of files/folders with the move operation (and me wanting to avoid having to copy files when a simple move operation would work... IMO).

---

## XBox360 0.2.0 Released ##
With the release of iXtreme LT and the recommendation for SSv2, I have added a new config setting "requireSS" which if set to true will consider the ISO failed if either it really fails (same as before) or does not have SSv2.

---

## Movies 0.2.3 Released ##
Fixed a bug when keeping the folder after combining two AVI's they would not be deleted and use up 2x the disk space.

---

## Movies 0.2.2 Released ##
Movies 0.2.2 adds the option (deleteFolder in the Config File) to either keep the Final Folder SABnzbd extracts to, or to delete it (previously it would always delete it). It's been tested for AVi files (one or two), but it should be fully functional for HD formats as well.

---

## SABSync 0.5.3 Fix Released ##
SABSync 0.5.3 Fix is properly packaging release 0.5.3, as the wrong executable was in the archive. Log files will now be generated for each run (one per minute), instead of one growing log file.

---

## Movies 0.2.1 Released ##
Movies 0.2.1 should now properly update your XBMC library for HD movies, one more config setting added from 0.2.0 to properly support the for XBMC running on a different computer.

---

## Movies 0.2.0 Flaw ##
There is a flaw causing XBMC not to update for HD movies - I'm working on a fix.

---

## SABSync 0.5.3 Released ##
SABSync 0.5.3 fixes a possible problem where the episode/season numbers cannot be found causing the script to exit pre-maturely.

---

## SABSync 0.5.2 Released ##
SABSync 0.5.2 addresses an issue with shows that contain illegal characters being sent to SAB even if the NZB was already imported, but the episode was not found on disk (likely failed to extract/rename properly).

---

## Movies 0.2.0 Released ##
Movies 0.2.0 Adds the ability to update your XBMC Library after renaming/moving the movie.

---

## TVMove 0.3.0 Released ##
TVMove 0.3.0 adds the ability to update your XBMC library (through XBMC's HTTP API), it passes the path of the TV Show to XBMC so it will only update that show and updates much quicker, it also tries to avoid the situation where multiple downloads try to update the entire library one after another which will cancel the previous attempt if it has not finished.

---

## XBox360 0.1.1 Released ##
XBox360 0.1.1 fixes a bug where closing the second abgx360 window too quickly causes XBox360 to stop responding as it cannot properly read the abgx360.html file.

---

## SABSyncGui 0.3.0 Released ##
SABSyncGUI 0.3.0 adds in a test button (successfully tests will return the current SABnzbd version in the status bar, failed request will return an error (APIKey or Authentication required), or if it is unable to connect after 10 seconds you will need to check your settings/SABnzbd is possibly down. The Reset button will reload the config file.

Download and drop SABSyncGUI in the same folder as SABSync.exe and SABSync.exe.config.

---

## TVMove and TVConvert Packaged Separately ##
With a recent bug found in TVMove (Episode Name not being stored), I have separated the two components to not force people to download both parts when one is updated. Grab TVMove 0.2.1 to fix the bug.

---

## TVShows 0.2.0 Released ##
An update for TVShows (TVMove and TVConvert) has been released. TVMove should now handle most naming conventions (when using Newzbin as a source, other sources haven't been tested). TVConvert will properly handle 1x01 or S01E01 (or any mixture containing 1 or 2 digits for Season/Episode) with Atomic Parsley support, other naming conventions should convert properly, but may not have full Atomic Parsley support.


---

# Introducing SABSync! #

SABSync will check your TV directory for the shows you want downloading only shows that are missing.
It supports ignoring seasons for shows that you do not want (you choose the season and that season + all previous seasons are ignored).
SABSync will also check the queue for episodes pending to be download (for both NZBs SAB has retrieved the info for and ones still 'fetching') in addition SABSync will check your imported NZB folder to see if that NZB has already been imported (great for times when SAB fails to unpack or rename the download correctly).

Support for most if not all naming conventions that SABnzbd supports (simply copy and paste you TV Renaming string into SABSync's configuration), support for Daily Shows as well.

SABSync is developed in C# Using version 2.0 of the .Net Framework and is limited to Windows (mono on Linux may also work, but this is untested). Thanks to Koonfused (Kay.One) for the assistance in developing this great application.

SABnzbd 0.5+ is required to use SABSync, due to the way the Queue Check is being done as well as the support for priority on individual NZBs and Daily show naming conventions.



---

**TVShows** will copy a TV Show that you want converted to iPhone/iPod Touch format, with TVMove.exe and then TVConvert.exe can be scheduled to run of the same computer or another computer (the faster computer the better) to Convert the TV Show with Handbrake and add the appropriate "atoms" with Atomic Parsley.

Both Atomic Parsley and HandBrake (CLI Only if you wish) are required.

Atomic Parsley: http://atomicparsley.sourceforge.net/

Handbrake: http://handbrake.fr/