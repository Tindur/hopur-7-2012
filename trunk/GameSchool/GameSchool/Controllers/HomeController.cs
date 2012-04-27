using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameSchool.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to G4M35CH001";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
