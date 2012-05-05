﻿using System;
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

        public ActionResult StudentIndex()
        {
            ViewBag.Message = "Welcome to GameSchool 0.1";

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
                return Json(model, JsonRequestBehavior.AllowGet);
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

            LectureComment l = new LectureComment();
            l.CommentID = model.ID.ToString();
            l.LectureID = id;
            
            m_CommentRepo.AddComment(model);
            var result = m_CommentRepo.GetComments();
            return Json(result);
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
                var Questions = m_TestRepo.GetAllQuestionsForTest(id.Value);
                List<List<AnswerModel>> Answers = new List<List<AnswerModel>>();
                foreach (var item in Questions)
                {
                    Answers.Add(m_TestRepo.GetAllAnswersForQuestion(item.ID).ToList());
                }
                TestViewModel model = new TestViewModel(Questions, Answers);
                
                return View(model);
            }
            return View("Error");
        }




    }
}
