# Introduction #

XBox360 can be used to automatically verify XBox 360 downloads to ensure they properly stealthed before being burnt and played. Support for non-XBox 360 games by filesize, support for multi-ISO releases, all output is stored for review in an HTML file, games that do not pass have a simple batch file created to re-run the script at a later date and are moved to a different directory to reduce the chance of burning it accidentally.


## Config File ##

  * logDir - Folder where XBox360.log should be stored
  * failedDir - Folder to move games that do not verify properly
  * wiiDir - Folder to move games that are under sized (likely not a 360 game)
  * passDir - Folder to move games to that verified properly (moving a previously failed game after it passes)
  * abgxOptions - Options to pass to abgx360 (the defaults should be fine for most users)

## Required Applications ##
abgx360 is required to check all games, available here: http://abgx360.net/