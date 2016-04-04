using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace testfan2.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "The best Euro 2016 Fantasy Football App";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Fantasy Football Euro 2016 contact page.";

            return View();
        }
    }
}