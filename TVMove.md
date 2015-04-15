# Introduction #
TVMove will copy shows (only the ones you want) to another directory for later Conversion to the iPhone format.


## Config File ##

  * logDir - Folder where TVMove.log file will be created
  * tempDir - Folder to copy TV Shows to for conversion with TVConvert
  * shows - A semi-colon separated list of shows you want to copy (Name must match the name on disk)
  * videoExt - A semi-colon separated list of extensions that should be considered Video files
  * filenameTemplate - The Series Sorting string from SABnzbd's Config,only containing the filename portion (after the final Slash).
  * updateXBMC - True/False value if you want to update your XBMC Library before the script terminates (if disabled, notify and clean options are ignored)
  * notifyXbmc - True/False value if you want a pop-u to be sent to XBMC
  * cleanLibrary - True/False if you want the Video Library for XBMC to be cleaned after updating
  * downloadTvPath - Local Path to your TV Download Folder
  * xbmcTvPath - Path (usually an SMB share) that XBMC uses for it's TV Show library
  * xbmcHost - Hostname/IP Address for XBMC
  * xbmcPort - Port for XBMC's EventServer (default is 9777)
  * xbmcOsWindows - True/False value, if OS is Windows set to true, else set to false (XBMC running on XBMC or OSx)