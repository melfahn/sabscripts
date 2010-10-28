using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SABSync.Web.Models;

namespace SABSync.Web.Controllers
{
    public class SABSyncController : Controller
    {
        //
        // GET: /Shows/

        public ActionResult Index()
        {
            using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
            {
                var shows = from s in sabSyncEntities.shows
                            select s;
                return View(shows.ToList());
            }
        }

        public ActionResult History()
        {
            using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
            {
                var histories = from h in sabSyncEntities.histories select
                                  new HistoryModel
                                      {
                                          Id = h.id,
                                          ShowName = h.shows.show_name,
                                          SeasonNumber = h.episodes.season_number,
                                          EpisodeNumber = h.episodes.episode_number,
                                          EpisodeName = h.episodes.episode_name,
                                          FeedTitle = h.feed_title,
                                          Quality = h.quality,
                                          ProperLong = h.proper,
                                          Provider = h.provider,
                                          DateString = h.date
                                      };
                return View(histories.ToList());
            }

            
        }

        public ActionResult Upcoming()
        {
            using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
            {
                DateTime dateToday = DateTime.Now.Date;
                DateTime dateWeek = DateTime.Now.Date.AddDays(7);

                var upcoming = from s in sabSyncEntities.episodes.AsEnumerable()
                           where s.air_date != "" && Convert.ToDateTime(s.air_date) >= dateToday
                           && Convert.ToDateTime(s.air_date) < dateWeek
                           select new UpcomingModel
                           {
                               ShowName = s.shows.show_name,
                               SeasonNumber = s.season_number,
                               EpisodeNumber = s.episode_number,
                               EpisodeName = s.episode_name,
                               AirDate = s.air_date,
                               AirTime = s.shows.air_time,
                               Overview = s.overview
                           };

                //upcoming.OrderByDescending(upcoming);

                return View(upcoming.ToList());
            }
        }
    }
}
