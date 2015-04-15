# Introduction #

Movies is a Post-Processing script designed to be used to process Movies after being downloaded with SABnzbd.

Movies will sort your movies based on Extension, treating AVI Files as SD, MP4 files as iPod and WMV/MKV files as HD.

Movies will also join 2 AVI movies together to create single AVI file. The Movie file will be renamed & moved to contain the name and the year of the Movie (if the NZB contained that information in the title) - Newzbin downloads work great with this script.


## Config File ##
  * logDir - Where should the Movies.log file be stored
  * movieDir - Full path to your Movies Folder (.avi)
  * hdMovieDir - Full path to your HD Movies Folder (.mkv/.wmv)
  * ipodMoviesDir - Full path to your iPod Movies Folder (.mp4)

## Required Applications ##
mencoder is required for joining multiple AVI files, it can be downloaded as part of the MPlayer Suite, available here: http://www.mplayerhq.hu/design7/dload.html