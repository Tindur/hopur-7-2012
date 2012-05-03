using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameSchool.Models;
using GameSchool.Models.Repositories;
using GameSchool.Models.dbLINQ;
namespace GameSchool.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdminController : Controller
    {
        UsersRepository m_UsersRepo = new UsersRepository();
        //
        // GET: /Admin/
        
        public ActionResult AdminIndex()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetStudents()
        {
            var model = m_UsersRepo.GetAllStudents();
            return View(model);
        }
        public ActionResult GetTeachers()
        {
            var model = m_UsersRepo.GetAllTeachers();
            return View(model);
        }
        public ActionResult EditUser(string id)
        {
            //if (id.HasValue)
            //{
                aspnet_User TheUser = m_UsersRepo.GetUserById(id);
               // if (TheUser != null)
                    return View(TheUser);
            
            //return View("NotFound");
        }
        /*[HttpPost]
        public ActionResult CreateUser()
        {

        }*/
    }
}
