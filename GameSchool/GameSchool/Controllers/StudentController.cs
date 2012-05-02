using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameSchool.Models;
namespace GameSchool.Controllers
{
    public class StudentController : Controller
    {
        //
        // GET: /Student/
        [Authorize(Roles="Student")]  
        public ActionResult StudentIndex()
        {
            return View();
        }

    }
}
