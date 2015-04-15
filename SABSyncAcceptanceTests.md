_Work in progress_

**My Shows**
  * Folder names in TV Root folders (excluding system and hidden folders) are included as My Shows.

**My Episodes**
  * Files in My Shows folders (excluding system and hidden files) with extensions in Video Extensions are included as My Episodes.

**My Feeds**
  * Feeds in RSS config are included as My Feeds.

**Feed Shows**
  * My Feeds items with titles containing "(passworded)" are excluded.
  * My Feeds items not matching the following patterns for TV shows are excluded:
    * Show name S01E05
    * Show name 1x05
    * Show name 2010-04-30

**Queued Shows**
  * Feed Shows not in My Shows (matched by show name) are excluded.
  * Feed Shows in Ignore Seasons with seasons less than or equal to configured season are excluded.
  * Feed Shows in My Episodes (matched by show name/season/episode) are excluded.
  * Feed Shows in SAB .nzb Backup folder are excluded.
  * Feed Shows in SAB History are excluded.
  * Feed Shows in SAB Queue are excluded.
  * Feed Shows not excluded are added to SAB Queue.