using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameSchool.Models;
namespace GameSchool.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        [Authorize(Roles="Admin")]
        public ActionResult AdminIndex()
        {
            return View();
        }

    }
}
