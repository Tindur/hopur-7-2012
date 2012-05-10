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
        AssignmentRepository m_AssignmentRepo = new AssignmentRepository();
        CourseRepository m_CourseRepo = new CourseRepository();
        LevelRepository m_lvlRepo = new LevelRepository();
        LectureRepository m_LectureRepo = new LectureRepository();
        TestRepository m_TestRepo = new TestRepository();
        CommentRepository m_CommentRepo = new CommentRepository();
        UsersRepository m_UserRepo = new UsersRepository();
        NotificationRepository m_NewsFeedRepo = new NotificationRepository();

        public ActionResult TeacherIndex()
        {
            return View();
        }

        public ActionResult Navigation()
        {
            aspnet_User TheUser = m_UserRepo.GetUserByName(User.Identity.Name);
            aspnet_Membership TheMembership = m_UserRepo.GetMembershipById(TheUser.UserId.ToString());
            UserViewModel TheUserModel = new UserViewModel(TheUser, TheMembership, null, null);
            ImageModel TheImage = m_UserRepo.GetImageForUser(TheUser.UserId.ToString());
            IQueryable<CourseModel> TheCourses = m_CourseRepo.GetCoursesForTeacher(TheUser.UserName);
            NavigationViewModel model = new NavigationViewModel();

            model.TheCourses = TheCourses;
            model.TheImage = TheImage;
            model.TheUser = TheUserModel;


            return PartialView("Navigation", model);
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
                    m_finishedLvlID = null /*m_lvlRepo.GetFinishedLevelsForStudent(User.Identity.Name).ToList()*/,
                    m_theLectures = m_LectureRepo.GetLecturesForLevel(id.Value)
                });
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

        public ActionResult CourseAssignments(int? courseID)
        {
            if (courseID.HasValue)
            {
                CourseAssignmentsViewModel model = new CourseAssignmentsViewModel
                {
                    Assignments = m_AssignmentRepo.GetAssignmentsForCourse(courseID.Value).ToList(),
                    Levels = m_lvlRepo.GetAllLevelsForCourse(courseID.Value).ToList()
                };
                return PartialView("CourseAssignments", model);
            }
            else
                return View("Error");
        }

        public ActionResult CourseTests(int? courseID)
        {
            if (courseID.HasValue)
            {
                CourseTestViewModel model = new CourseTestViewModel
                {
                    Tests = m_TestRepo.GetTestsForCourse(courseID.Value).ToList(),
                    Levels = m_lvlRepo.GetAllLevelsForCourse(courseID.Value).ToList()
                };
                return PartialView("CourseTests", model);
            }
            else
                return View("Error");
        }

        public ActionResult CreateLecture(int LevelID, int CourseID)
        {
            LectureModel model = new LectureModel();
            model.CourseID = CourseID;
            model.LevelID = LevelID;

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateLecture(LectureModel model)
        {
            if (model != null)
            {
                model.DateAdded = DateTime.Now;

                m_LectureRepo.AddLecture(model);

                m_LectureRepo.Save();
                return RedirectToAction("GetCourse", "Teacher", new { id = model.CourseID });
            }
            return View("Error");
        }

        public ActionResult LevelsForCourse(int? courseID)
        {
            if (courseID.HasValue)
            {
                TLevelViewModel model = new TLevelViewModel
                {
                    CourseID = courseID.Value,
                    Levels = m_lvlRepo.GetAllLevelsForCourse(courseID.Value).ToList(),
                    Lectures = m_LectureRepo.GetLecturesForCourse(courseID.Value).ToList(),
                    Assignments = new List<AssignmentModel>(),
                    Tests = new List<TestModel>()

                };

                foreach (var level in model.Levels)
	            {
                    model.Tests.AddRange(m_TestRepo.GetAllTestsForLevel(level.ID));
                    model.Assignments.AddRange(m_AssignmentRepo.GetAllAssignmentsForLevel(level.ID));
	            }
                
               
                return PartialView("CourseLevels", model);
            }
            else
                return View("Error");
        }

        public ActionResult CreateLevel(int? CourseID)
        {
            if (CourseID.HasValue)
            {
                LevelModel model = new LevelModel();
                model.CourseID = CourseID.Value;
                return View(model);
            }
            else
                return View ("Error");
        }

        [HttpPost]
        public ActionResult CreateLevel(LevelModel model)
        {
            if (model != null)
            {
                int number = m_lvlRepo.GetAllLevelsForCourse(model.CourseID).Count() + 1;
                model.NumberInCourse = number;
                m_lvlRepo.AddLevel(model);
                return RedirectToAction("GetCourse", new { id = model.CourseID });
            }
            else
                return View("Error");
        }

        /*public ActionResult ViewTestsForCourse(int? CourseID)
        {
            if (CourseID.HasValue)
            {
                
            }
        }*/

        #region Tests
        public ActionResult CreateTest(int? levelID, int? CourseID)
        {
            if (levelID.HasValue && CourseID.HasValue)
            {
                CreateTestViewModel model = new CreateTestViewModel 
                { 
                    Test = new TestModel { LevelID = levelID.Value },
                    CourseID = CourseID.Value
                };

                return View(model);
            }
            else
                return PartialView("ErrorPartial");
        }

        [HttpPost]
        public ActionResult CreateTest(CreateTestViewModel model)
        {
            if (model != null)
            {
                model.Test.DateAdded = DateTime.Now;
                model.Test.Points = 0;
                model.Test.CourseID = model.CourseID;
                m_TestRepo.AddTest(model.Test);

                return RedirectToAction("GetCourse", "Teacher", m_CourseRepo.GetCourseById(model.CourseID));
            }
            else
                return View("Error");
        }

        public ActionResult EditTest(int? theTestID)
        {
            if (theTestID.HasValue)
            {
                TestModel theTest = m_TestRepo.GetTestByID(theTestID.Value);

                EditTestViewModel model = new EditTestViewModel
                {
                    Test = theTest,
                    Questions = m_TestRepo.GetAllQuestionsForTest(theTest.ID).ToList(),
                    Answers = new List<AnswerModel>(),
                    newQuestion = new QuestionModel {TestID = theTestID.Value },
                    newAnswers = new List<AnswerModel>(),
                };

                for (int i = 0; i < 4; i++)
                {
                    model.newAnswers.Add(new AnswerModel());
                }

                foreach (var question in model.Questions)
                {
                    model.Answers.AddRange(m_TestRepo.GetAllAnswersForQuestion(question.ID));
                }

                return View(model);
            }
            else
                return View("Error");
        }

        [HttpPost]
        public ActionResult EditTest(FormCollection formdata)
        {
            if(formdata.Count != 0)
            {
                QuestionModel newQuestion = new QuestionModel
                {
                    Question = Convert.ToString(formdata["newQuestion.Question"]),
                    Points = Convert.ToInt32(formdata["newQuestion.Points"]),
                    TestID = Convert.ToInt32(formdata["Test.ID"])
                };
                m_TestRepo.AddPointsToTest(newQuestion.TestID, newQuestion.Points.Value);
                m_TestRepo.AddQuestion(newQuestion);

                int questID = m_TestRepo.FindTestID(newQuestion);

                //Fyrsta svar alltaf sett sem rétt == ekki gott.  
                //Röðum svörunum því í handahófskennda röð áður en við vistum þau í gagnagrunninn
                List<AnswerModel> list = new List<AnswerModel>();
                list.Add(new AnswerModel
                    {
                        Answer = formdata[3],
                        Correct = true,
                        QuestionID = questID
                    });
                for (int i = 4; i < 7; i++)
                {
                    list.Add(new AnswerModel
                            {
                                Answer = formdata[i],
                                Correct = false,
                                QuestionID = questID
                            });
                }

                List<AnswerModel> list2 = new List<AnswerModel>();
                Random r = new Random(DateTime.Now.Millisecond);
                AnswerModel answer;
                while (list2.Count < 4)
                {
                    r = new Random(DateTime.Now.Millisecond);
                    answer = list.ElementAt(r.Next() % 4);
                    if (!list2.Contains(answer))
                        list2.Add(answer);
                }

                m_TestRepo.AddAnswers(list2);

                return RedirectToAction("EditTest", new {theTestID = newQuestion.TestID });
            }
            else
                return View("Error");
        }

        #endregion

        #region Assignments
        public ActionResult CreateAssignment(int LevelID, int CourseID)
        {
            AssignmentModel model = new AssignmentModel();
            model.LevelID = LevelID;
            model.CourseID = CourseID;
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateAssignment(AssignmentModel model)
        {
            if (model != null)
            {
                model.DateAdded = DateTime.Now;

                m_AssignmentRepo.AddAssignmnet(model);
                m_AssignmentRepo.Save();

                return RedirectToAction("GetCourse", "Teacher", new { id = model.CourseID });
            }
            else
            {
                return View("Error");
            }
        }
        #endregion

        #region Notifications
        public ActionResult CreateNotification(int? id)
        {
            if (id.HasValue)
            {
                NotificationModel model = new NotificationModel();
                model.CourseID = id.Value;

                return View(model);
            }
            else
                return View("Error");            
        }

        [HttpPost]
        public ActionResult CreateNotification(NotificationModel model)
        {
            if (model != null)
            {
                model.DateAdded = DateTime.Now;
                model.UserName = m_UserRepo.GetUserByName(User.Identity.Name).Name;

                m_NewsFeedRepo.AddNotification(model);
                m_NewsFeedRepo.Save();

                return View("TeacherIndex");
            }
            return View("Error");
        }
        #endregion    
    }
}