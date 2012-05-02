using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using  GameSchool.Models;

namespace GameSchool.Controllers
{
    public class TeacherController : Controller
    {
        //
        // GET: /Teacher/
        [Authorize(Roles="Teacher")]
        public ActionResult TeacherIndex()
        {
            return View();
        }

    }
}
