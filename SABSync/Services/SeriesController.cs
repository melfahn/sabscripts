using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using log4net;
using NLog;
using SABSync.Repository;
using SABSync.TvDb;
using SubSonic.Repository;

namespace SABSync.Services
{
    class SeriesController
    {
        private readonly ILog _logger;
        private readonly IDiskController _diskController;
        private readonly IConfigController _config;
        private readonly IRepository _sonioRepo;

        public SeriesController(ILog logger, IDiskController diskController, IConfigController configController, IRepository dataRepository)
        {
            _logger = logger;
            _diskController = diskController;
            _config = configController;
            _sonioRepo = dataRepository;
        }


        public void SyncSeriesWithDisk()
        {
            foreach (var root in _config.GetTvRoots())
            {
                foreach (var seriesFolder in _diskController.GetDirectories(root))
                {
                    var dirInfo = new DirectoryInfo(seriesFolder);
                    _sonioRepo.Single<Series>(s => s.Path == dirInfo.FullName);

                }
            }

        }

        private void AddNewShowWithEpisodes(string showName)
        {
            //TODO: Find a way to migrate aliases to Database... or not, not a big deal IMO
            //Update the shows table, get TVDB ID and Name, possible get TVRage ID and Name

            _logger.InfoFormat("Checking if {0} is in Database: ", showName);

            try
            {
                using (var sabSyncEntities = new SABSyncEntities())
                {
                    if (sabSyncEntities.shows.Any(s => s.show_name == showName)) //If show was already added...skip it (Use local disk name)
                        return;

                    //Get TVDB ID & Name
                    TvDbShowInfo info = new TvDbShowInfo();
                    //info = TvDb.GetShowData(showName);

                    if (info == null)
                        return;

                    //string aliases = GetAliasForDb(showName, info.SeriesName);

                    //Add to shows Database
                    shows newItem = new shows
                    {
                        id = new int(),
                        show_name = showName,
                        tvdb_id = Convert.ToInt32(info.SeriesId),
                        tvdb_name = info.SeriesName,
                        ignore_season = 0,
                        air_day = info.AirDay,
                        air_time = info.AirTime,
                        run_time = Convert.ToInt32(info.RunTime),
                        status = info.Status,
                        poster_url = info.PosterUrl,
                        banner_url = info.BannerUrl,
                        imdb_id = info.ImdbId,
                        genre = info.Genre.Trim('|'),
                        overview = info.Overview,
                        quality = 0, //Uses defined value in Quality.Config or uses downloadQuality in Config file (easy upgrading), can be adjusted later to remove the need for Quality.Config
                    };
                    _logger.InfoFormat("Adding {0} to database.", showName);

                    sabSyncEntities.AddToshows(newItem);
                    sabSyncEntities.SaveChanges(); //Save show to Database after each show
                    AddNewEpisodes(info.Episodes, Convert.ToInt32(info.SeriesId));//Add all episodes for this show
                }
            }
            catch (Exception ex)
            {
                _logger.Fatal("An error has occured while adding shows to DB", ex);
                throw;

            }
        }

        private void AddNewEpisodes(List<TvDbEpisodeInfo> episodeList, int seriesId)
        {
            //Check if Episode is in table, if not, add it!
            try
            {
                using (var sabSyncEntities = new SABSyncEntities())
                {
                    var shows = from s in sabSyncEntities.shows
                                where s.tvdb_id == seriesId
                                select new
                                {
                                    s.id
                                };

                    foreach (var episode in episodeList) //Get all Episodes for show
                    {
                        if (sabSyncEntities.episodes.Any(e => e.tvdb_id == episode.EpisodeId)) //If episode was already added...skip it (use the TVDB episode ID)
                            continue;

                        var newItem = new episodes
                        {
                            id = new long(),
                            show_id = shows.First().id,
                            season_number = episode.SeasonNumber,
                            episode_number = episode.EpisodeNumber,
                            episode_name = episode.EpisodeName,
                            air_date = episode.FirstAired,
                            tvdb_id = episode.EpisodeId,
                            overview = episode.Overview
                        };
                        sabSyncEntities.AddToepisodes(newItem);
                    }
                    sabSyncEntities.SaveChanges(); //Insert into Database after processing each series
                }
            }

            catch (Exception ex)
            {
                _logger.Fatal("An error has occured while adding an episode", ex);
                throw;
            }
        }


    }
}
