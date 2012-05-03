using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameSchool.Models;
using GameSchool.Models.Repositories;
using GameSchool.Models.dbLINQ;
using GameSchool.Models.ViewModels;
namespace GameSchool.Controllers
{
    [Authorize(Roles = "Student")]  
    public class StudentController : Controller
    {
        CourseRepository m_CourseRepo = new CourseRepository();
        LevelRepository m_lvlRepo = new LevelRepository();

        public ActionResult StudentIndex()
        {
            /*string user = User.Identity.Name;
            var courses = m_CourseRepo.GetCoursesForStudent(user);
            List<CourseModel> model = new List<CourseModel>();
            foreach (var item in courses)
            {
                model.Add(m_CourseRepo.GetCourseById(item.ID));
            }*/
            List<CourseModel> model = m_CourseRepo.GetCoursesForStudent(User.Identity.Name).ToList();

            return View(model);
        }

        public ActionResult Navigation()
        {
            return PartialView("Navigation", m_CourseRepo.GetCoursesForStudent(User.Identity.Name));
        }

        public ActionResult GetCourse(int? id)
        {
            if (id.HasValue)
            {
                IEnumerable<LevelModel> levels = m_lvlRepo.GetAllLevelsForCourse(id.Value);

                /*return View(new CourseView(m_CourseRepo.GetCourseById(id.Value), 
                                            levels,
                                             m_lvlRepo.GetFinishedLevelsForStudent(User.Identity.Name)));*/
                return View();
            }
            else
            {
                return RedirectToAction("StudentIndex");
            }
        }
    }
}
