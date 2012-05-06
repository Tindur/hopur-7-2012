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
        LectureRepository m_LectureRepo = new LectureRepository();
        TestRepository m_TestRepo = new TestRepository();
        CommentRepository m_CommentRepo = new CommentRepository();
        AssignmentRepository m_AssignmentRepo = new AssignmentRepository();

        public ActionResult StudentIndex()
        {
            ViewBag.Message = "Welcome to GameSchool 0.2";

            return View();
        }

        public ActionResult Navigation()
        {
            return PartialView("Navigation", m_CourseRepo.GetCoursesForStudent(User.Identity.Name));
        }

        public ActionResult CourseTabs(int id)
        {
            var model = m_CourseRepo.GetCourseById(id);
            return PartialView("CourseTabs", model);
        }

        public ActionResult GetCourse(int? id)
        {
            if (id.HasValue)
            {
                IEnumerable<LevelModel> levels = m_lvlRepo.GetAllLevelsForCourse(id.Value);

                return View("Course", new CourseView
                {
                    m_theCourse = m_CourseRepo.GetCourseById(id.Value),
                    m_theLevels = levels.ToList(),
                    m_finishedLvlID = m_lvlRepo.GetFinishedLevelsForStudent(User.Identity.Name).ToList(),
                    m_theLectures = m_LectureRepo.GetLecturesForCourse(id.Value).ToList()
                });
            }
            else
            {
                return RedirectToAction("StudentIndex");
            }
        }

        public ActionResult GetLevel(int? id)
        {
            if (id.HasValue)
            {
                var level = m_lvlRepo.GetLevelByID(id.Value);
                return View(level);
            }
            else
                return RedirectToAction("StudentIndex");
        }

        public ActionResult GetLecturesForLevel(int? id)
        {
            if (id.HasValue)
            {
                var model = m_LectureRepo.GetLecturesForCourse(id.Value).ToList();

                return PartialView("_LecturesPartial", model);
            }
            return View("Error");
        }

        [HttpGet]
        public ActionResult GetLectureByID(int? id)
        {
            //Todo: útfæra þetta fall og svipað fall í LectureRepository // fuck you that's done! kv bjafki
            if(id.HasValue)
            {
                var model = m_LectureRepo.GetLectureByID(id.Value);
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //ATH TODO!! bjarkfi
                return RedirectToAction("StudentIndex");
            }
        }

        [HttpGet]
        public ActionResult GetCommentsByID(int? id)
        {
            if (id.HasValue)
            {
                var model = m_CommentRepo.GetCommentForLecture(id.Value);
                var newResult = (from k in model 
                                 select new 
                                 { 
                                     CommentDate = k.CommentDate.ToShortDateString(), 
                                     ID = k.ID, 
                                     CommentText = k.CommentText,
                                     UserName = k.UserName
                                 });
              //  return Json(newResult);
                return Json(newResult, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //ATH TODO!! bjarkfiance
                return RedirectToAction("StudentIndex");
            }
        }

        [HttpPost]
        public ActionResult CreateCommentForLecture(string CommentText, int id)
        {
                CommentModel model = new CommentModel();
                model.CommentText = CommentText;
                model.UserName = User.Identity.Name;
            
                m_CommentRepo.AddComment(model);
                m_CommentRepo.ConnectCommentToLecture(model, id);

                var result = m_CommentRepo.GetCommentForLecture(id);
                var newResult = (from k in result select new { CommentDate = k.CommentDate.ToShortDateString(), ID = k.ID, CommentText = k.CommentText, UserName = k.UserName });
                return Json(newResult);
        }

        public ActionResult GetTestsForLevel(int? id)
        {
            if (id.HasValue)
            {
                var model = m_TestRepo.GetAllTestsForLevel(id.Value);
                return PartialView(model);
            }
            return View("Error");
        }
        
        public ActionResult GetTest(int? id)
        {
            if (id.HasValue)
            {
                string TestName = m_TestRepo.getTestNameByID(id.Value);
                var Questions = m_TestRepo.GetAllQuestionsForTest(id.Value);

                List<List<AnswerModel>> Answers = new List<List<AnswerModel>>();
                foreach (var item in Questions)
                {
                    Answers.Add(m_TestRepo.GetAllAnswersForQuestion(item.ID).ToList());
                }
                TestViewModel model = new TestViewModel
                {
                    m_Questions = Questions,
                    m_Answers = Answers,
                    testName = TestName
                };
                
                return View(model);
            }
            return View("Error");
        }

        public ActionResult GetAssignmentsForLevel(int? id)
        {
            if (id.HasValue)
            {
                IQueryable<AssignmentModel> model = m_AssignmentRepo.GetAllAssignmentsForLevel(id.Value);
                return PartialView(model);
            }
            return View("Error");
        }
        public ActionResult GetAssignment(int? id)
        {
            if (id.HasValue)
            {
                var model = m_AssignmentRepo.GetAssignmentById(id.Value);
                return View(model);
            }
            return View("Error");
        }

        public ActionResult NewsFeed(int? id)
        {
            if(id.HasValue)
            {
                IQueryable<LectureModel> TheLectures = m_LectureRepo.GetFiveLatest(id.Value);
                IQueryable<AssignmentModel> TheAssignments = m_AssignmentRepo.GetFiveLatest(id.Value);


                NewsFeedViewModel model = new NewsFeedViewModel();
                model.Lectures = TheLectures;
                model.Assignments = TheAssignments;

                return PartialView(model);

            }
            return View("Error");
        }
    }
}
