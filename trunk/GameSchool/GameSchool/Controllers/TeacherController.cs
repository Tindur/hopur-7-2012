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
                    m_theLectures = m_LectureRepo.GetLecturesForCourse(id.Value).ToList()
                });
            }
            else
            {
                return RedirectToAction("TeacherIndex");
            }
        }

        public ActionResult CourseLectures(int? courseID)
        {
           /* if (courseID.HasValue)
            {
                CourseLectureViewModel model = new CourseLectureViewModel
                {
                    Lectures = m_LectureRepo.GetLecturesForCourse(courseID.Value).ToList(),
                    Levels = m_lvlRepo.GetAllLevelsForCourse(courseID.Value).ToList()
                };
                return PartialView("CourseLectures", model);
            }
            else*/
                return View("Error");
        }

        public ActionResult CreateLecture()
        {
            return View(new LectureModel());
        }

        [HttpPost]
        public ActionResult CreateLecture(LectureModel model/*, string ReturnUrl*/)
        {
            if (model != null)
            {
                model.DateAdded = DateTime.Now;

                m_LectureRepo.AddLecture(model);
                m_LectureRepo.Save();
                /*if (Url.IsLocalUrl(ReturnUrl) && ReturnUrl.Length > 1 && ReturnUrl.StartsWith("/")
                        && !ReturnUrl.StartsWith("//") && !ReturnUrl.StartsWith("/\\"))
                {
                    return Redirect(ReturnUrl);
                }
                else*/
                    return RedirectToAction("TeacherIndex");
            }
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

        /*public ActionResult ViewTestsForCourse(int? CourseID)
        {
            if (CourseID.HasValue)
            {
                
            }
        }*/

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
            //Virkar ekki því prófin eru aldrei tengd við borðið (þ.e. levelID == 0, alltaf)
            if (model != null)
            {
                m_TestRepo.AddTest(model.Test);

                return RedirectToAction("GetCourse", "Teacher", m_CourseRepo.GetCourseById(model.CourseID));
            }
            else
                return View("Error");
        }
        public ActionResult CreateAssignment()
        {
            return View(new AssignmentModel());
        }

        [HttpPost]
        public ActionResult CreateAssignment(AssignmentModel model)
        {
            if (model != null)
            {
                model.DateAdded = DateTime.Now;

                m_AssignmentRepo.AddAssignmnet(model);
                m_AssignmentRepo.Save();

                return View("TeacherIndex");
            }
            else
            {
                return View("Error");
            }
        }

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
                model.UserName = User.Identity.Name;

                m_NewsFeedRepo.AddNotification(model);
                m_NewsFeedRepo.Save();

                return View("TeacherIndex");
            }
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
                    Answers = new List<AnswerModel>()
                };

                foreach (var question in model.Questions)
                {
                    model.Answers.AddRange(m_TestRepo.GetAllAnswersForQuestion(question.ID));
                }

                return View(model);
            }
            else
                return View("Error");
        }

            
    }
}
