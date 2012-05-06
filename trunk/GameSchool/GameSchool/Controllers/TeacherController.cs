using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using  GameSchool.Models;
using GameSchool.Models.Repositories;
using GameSchool.Models.dbLINQ;
using GameSchool.Models.ViewModels;
using GameSchool.Models.ViewModels.TeacherViewModels;

namespace GameSchool.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class TeacherController : Controller
    {
        //
        // GET: /Teacher
        CourseRepository m_CourseRepo = new CourseRepository();
        LevelRepository m_lvlRepo = new LevelRepository();
        LectureRepository m_LectureRepo = new LectureRepository();
        TestRepository m_TestRepo = new TestRepository();
        CommentRepository m_CommentRepo = new CommentRepository();
        UsersRepository m_UserRepo = new UsersRepository();

        public ActionResult TeacherIndex()
        {
            return View();
        }

        public ActionResult Navigation()
        {
            aspnet_User TheTeacher = m_UserRepo.GetUserByName(User.Identity.Name);
            return PartialView("Navigation", m_CourseRepo.GetCoursesForTeacher(TheTeacher.UserId.ToString()));
        }

        public ActionResult GetCourse(int? id)
        {
            if (id.HasValue)
            {
                /*IEnumerable<LevelModel> levels = m_lvlRepo.GetAllLevelsForCourse(id.Value);

                return View("Course", new CourseView
                {
                    m_theCourse = m_CourseRepo.GetCourseById(id.Value),
                    m_theLevels = levels.ToList(),
                    m_finishedLvlID = m_lvlRepo.GetFinishedLevelsForStudent(User.Identity.Name).ToList(),
                    m_theLectures = m_LectureRepo.GetLecturesForCourse(id.Value).ToList()
                });*/

                var model = m_CourseRepo.GetCourseById(id.Value);
                return View("Course", model);
            }
            else
            {
                return RedirectToAction("TeacherIndex");
            }
        }

        public ActionResult CourseLectures(int? courseID)
        {
            if (courseID.HasValue)
            {
                CourseLectureViewModel model = new CourseLectureViewModel
                {
                    Lectures = m_LectureRepo.GetLecturesForCourse(courseID.Value).ToList(),
                    Levels = m_lvlRepo.GetAllLevelsForCourse(courseID.Value).ToList()
                };
                return PartialView("CourseLectures", model);
            }
            else
                return View("Error");
        }

        public ActionResult LevelsForCourse(int? courseID)
        {
            if (courseID.HasValue)
            {
                TLevelViewModel model = new TLevelViewModel
                {
                    Levels = m_lvlRepo.GetAllLevelsForCourse(courseID.Value).ToList(),
                    Lectures = m_LectureRepo.GetLecturesForCourse(courseID.Value).ToList(),
                    Tests = new List<TestModel>()
                };
                
                //if(model.Levels
                    foreach (var level in model.Levels)
	                {
                        model.Tests.AddRange(m_TestRepo.GetAllTestsForLevel(level.ID));
	                }
                
               
                return PartialView("CourseLevels", model);
            }
            else
                return View("Error");
        }

        /*
        public ActionResult ViewTestsForCourse(int? CourseID)
        {
            if (CourseID.HasValue)
            {
                
            }
        }

        public ActionResult CreateTest()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateTest(FormCollection formdata)
        {

            return RedirectToAction("
        }
        */
    }
}
