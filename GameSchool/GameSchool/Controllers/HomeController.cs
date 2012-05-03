using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameSchool.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to G4M35CH001";
            if (User.IsInRole("Student"))
                return Redirect("Student/StudentIndex/");
            else if (User.IsInRole("Teacher"))
                return Redirect("Teacher/TeacherIndex/");
            else
                return Redirect("Admin/AdminIndex/");   
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Glosur()
        {
            return View();
        }

        public ActionResult Fyrirlestrar()
        {
            return View();
        }
    }
}
