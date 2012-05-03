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
        CourseRepository m_CourseRepo = new CourseRepository();
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
        [HttpGet]
        public ActionResult EditUser(string id)
        {
            aspnet_User TheUser = m_UsersRepo.GetUserById(id);
            return View(TheUser);
        }
        [HttpPost]
        public ActionResult EditUser(string id, FormCollection FormData)
        {
            aspnet_User TheUser = m_UsersRepo.GetUserById(id);

            if(TheUser != null)
            {
                UpdateModel(TheUser);
                m_UsersRepo.Save();

                return RedirectToAction("GetUsers");
            }
            else
                return View("GetUsers");
        }
        public ActionResult CreateUser()
        {
            
            return View();
        }
        public ActionResult GetCourses()
        {
            var model = m_CourseRepo.GetAllCourses();
            return View(model);
        }
        public ActionResult CreateCourse()
        {
            return View(new CourseModel());
        }
        [HttpPost]
        public ActionResult CreateCourse(FormCollection FormData)
        {
            CourseModel Course = new CourseModel();
            UpdateModel(Course);
            m_CourseRepo.AddCourse(Course);
            m_CourseRepo.Save();
            return RedirectToAction("GetCourses");
        }
        public ActionResult EditCourse(int id)
        {
            CourseModel Course = m_CourseRepo.GetCourseById(id);
            return View(Course);
        }
        [HttpPost]
        public ActionResult EditCourse(int id, FormCollection FormData)
        {
            CourseModel Course = m_CourseRepo.GetCourseById(id);
            if (Course != null)
            {
                UpdateModel(Course);
                m_CourseRepo.Save();
                return RedirectToAction("GetCourses");
            }
            return RedirectToAction("GetCourses");
        }
    }
}
