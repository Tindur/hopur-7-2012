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
        //Comments 
        CommentRepository rep = new CommentRepository();
        [HttpGet]
        public ActionResult Comments()
        {
            var model = rep.GetComments();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Create(string CommentText)
        {

            if (!String.IsNullOrEmpty(CommentText))
            {
                CommentModel c = new CommentModel();

                c.CommentText = CommentText;
                String strUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                if (!String.IsNullOrEmpty(strUser))
                {
                    int slashPos = strUser.IndexOf("\\");
                    if (slashPos != -1)
                    {
                        strUser = strUser.Substring(slashPos + 1);
                    }
                    c.UserName = strUser;

                    rep.AddComment(c);
                }
                else
                {
                    c.UserName = "Unknown user";
                }
                var model = rep.GetComments();
                return Json(model);
            }
            else
            {
                ModelState.AddModelError("CommentText", "Kjánaprump. Ætlarðu að setja inn tóma athugasemd?");
                return Comments();
            }
        }

    }
}
