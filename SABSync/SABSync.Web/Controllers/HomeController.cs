using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SABSync.Web.Models;

namespace SABSync.Web.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //ViewData["Message"] = "My Shows";
            return RedirectToAction("Index", "SABSync"); //Using Redirect to Index of SABSyncController

            //return View(); //USe Redirect instead (No need to mess with the Home Controller)
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
