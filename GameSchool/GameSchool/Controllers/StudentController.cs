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
    [Authorize(Roles = "Student")]  
    public class StudentController : Controller
    {
        CourseRepository m_CourseRepo = new CourseRepository();

        public ActionResult StudentIndex()
        {
            string user = User.Identity.Name;
            var courses = m_CourseRepo.GetCoursesForStudent(user);
            List<CourseModel> model = new List<CourseModel>();
            foreach (var item in courses)
            {
                model.Add(m_CourseRepo.GetCourseById(item.CourseID));
            }
            return View(model);
        }

        public ActionResult Navigation()
        {
            return PartialView("Navigation", m_CourseRepo.GetAllCourses().ToList());
        }

        public ActionResult GetCourse(int? id)
        {
            if (id.HasValue)
            {
                var model = m_CourseRepo.GetCourseById(id.Value);
                return View(model);
            }
            else
            {
                return RedirectToAction("StudentIndex");
            }
        }
    }
}
