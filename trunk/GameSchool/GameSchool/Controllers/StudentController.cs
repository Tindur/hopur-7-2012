using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameSchool.Models;
using GameSchool.Models.Repositories;
using GameSchool.Models.dbLINQ;
using GameSchool.Models.ViewModels;
using System.Web.Routing;
using System.Web.Security;

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
        UsersRepository m_UserRepo = new UsersRepository();
        NotificationRepository m_NotificationRepo = new NotificationRepository();


        public ActionResult StudentIndex()
        {
            return View();
        }

        public ActionResult Navigation()
        {
            aspnet_User TheUser = m_UserRepo.GetUserByName(User.Identity.Name);
            aspnet_Membership TheMembership = m_UserRepo.GetMembershipById(TheUser.UserId.ToString());
            UserViewModel TheUserModel = new UserViewModel(TheUser, TheMembership, null, null);
            ImageModel TheImage = m_UserRepo.GetImageForUser(TheUser.UserId.ToString());
            IQueryable<ImageModel> TeachersImages = m_UserRepo.GetImageForTeachersUser(TheUser.UserId.ToString());
            IQueryable<CourseModel> TheCourses = m_CourseRepo.GetCoursesForStudent(TheUser.UserName);
            
            NavigationViewModel model = new NavigationViewModel();

            model.TheCourses = TheCourses;
            model.TheImage = TheImage;
            model.TheUser = TheUserModel;
            model.TeachersImages = TeachersImages;

            model.TheLevel = TheUser.XP / 100;

            model.TheXP = TheUser.XP % 100;
    
            return PartialView("Navigation", model);
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
                                        m_theLectures = m_LectureRepo.GetLecturesForCourse(id.Value),
                                        m_theAssignments = m_AssignmentRepo.GetAssignmentsForCourse(id.Value),
                                        m_theTests = m_TestRepo.GetTestsForCourse(id.Value)
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
                LevelModel Level = m_lvlRepo.GetLevelByID(id.Value);
                IQueryable<LectureModel> Lectures = m_LectureRepo.GetLecturesForLevel(id.Value);
                IQueryable<AssignmentModel> Assignments = m_AssignmentRepo.GetAllAssignmentsForLevel(id.Value);
                IQueryable<TestModel> Tests = m_TestRepo.GetAllTestsForLevel(id.Value);

                LevelViewModel model = new LevelViewModel();
                model.TheLevel = Level;
                model.Lectures = Lectures;
                model.Assignments = Assignments;
                model.Tests = Tests;
                return View(model);
            }
            else
                return RedirectToAction("StudentIndex");
        }

        public ActionResult GetLecturesForLevel(int? id)
        {
            if (id.HasValue)
            {
                var model = m_LectureRepo.GetLecturesForLevel(id.Value).ToList();

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
                                     ShortCommentDate = k.CommentDate.ToShortTimeString(),
                                     LongCommentDate = k.CommentDate.ToShortDateString(),
                                     ID = k.ID,
                                     CommentText = k.CommentText,
                                     Name = m_UserRepo.GetUserByName(k.UserName).Name,
                                     UserImage = m_UserRepo.GetImageForName(m_UserRepo.GetUserByName(k.UserName).Name)
                                 });
                return Json(newResult, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //TODO?
                return RedirectToAction("StudentIndex");
            }
        }

        [HttpPost]
        public ActionResult CreateCommentForLecture(string CommentText, int id)
        {
                CommentModel model = new CommentModel();
                model.CommentText = CommentText;
                model.UserName = m_UserRepo.GetUserByName(User.Identity.Name).UserName/*.Name*/;
            
                m_CommentRepo.AddComment(model);
                m_CommentRepo.ConnectCommentToLecture(model, id);

                var result = m_CommentRepo.GetCommentForLecture(id);
                var newResult = (from k in result select new { LongCommentDate = k.CommentDate.ToShortDateString(), 
                                                               ShortCommentDate = k.CommentDate.ToShortTimeString(), 
                                                               ID = k.ID, 
                                                               CommentText = k.CommentText,
                                                               UserImage = m_UserRepo.GetImageForName(m_UserRepo.GetUserByName(k.UserName).Name),
                                                               Name = m_UserRepo.GetUserByName(k.UserName).Name
                                                               });
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
                IQueryable<NotificationModel> TheNotifications = m_NotificationRepo.GetFiveLatest(id.Value);
                if (TheNotifications.Any())
                {
                    string SourceTeacherImage = m_UserRepo.GetImageForName(m_NotificationRepo.GetNameOfTeacher(id.Value));
                    NewsFeedViewModel model = new NewsFeedViewModel();
                    model.Lectures = TheLectures;
                    model.Assignments = TheAssignments;
                    model.Notifications = TheNotifications;
                    model.SourceTeacherImage = SourceTeacherImage;
                    return PartialView(model);
                }
                else
                {
                    NewsFeedViewModel model = new NewsFeedViewModel();
                    model.Lectures = TheLectures;
                    model.Assignments = TheAssignments;
                    model.Notifications = null;
                    model.SourceTeacherImage = null;
                    return PartialView(model);
                }


                

                

            }
            return View("Error");
        }

        public ActionResult ProfileManagement()
        {
            string TheUserName = User.Identity.Name;
            aspnet_User TheUser = m_UserRepo.GetUserByName(TheUserName);
            ImageModel UserImage = m_UserRepo.GetImageForUser(TheUser.UserId.ToString());
            aspnet_Membership Membership = m_UserRepo.GetMembershipById(TheUser.UserId.ToString());

            ProfileManagementView model = new ProfileManagementView();

            model.ImageSource = UserImage.Source;
            model.Email = Membership.Email;
            model.NewPassword = null;
            model.OldPassword = null;
            model.ConfirmPassword = null;

            return View(model);

        }
        [HttpPost]
        public ActionResult ProfileManagement(ProfileManagementView model)
        {
            string TheUserName = User.Identity.Name;
            aspnet_User TheUser = m_UserRepo.GetUserByName(TheUserName);
            ImageModel UserImage = m_UserRepo.GetImageForUser(TheUser.UserId.ToString());
            aspnet_Membership TheMembership = m_UserRepo.GetMembershipById(TheUser.UserId.ToString());
           
            TheMembership.Email = model.Email;
            UserImage.Source = model.ImageSource;
            m_UserRepo.Save();
            if (model.NewPassword != null && model.OldPassword != null)
            {
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(TheUserName, true /*Notandi er online */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }
                if (changePasswordSucceeded)
                {
                    return RedirectToAction("StudentIndex");
                }
                else
                {
                    return View("Error");
                }
            }
            else
            {
                return RedirectToAction("StudentIndex");
            }
        }

        [HttpGet]
        public ActionResult GetLikesForLecture(int LectureID)
        {
            var model = m_CommentRepo.GetLikesForLecture(LectureID);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreateLikeForComment(int CommentID)
        {
            string theLiker = m_UserRepo.GetUserByName(User.Identity.Name).Name;
            //Kanna hvort notandinn hafi like'að commentið áður
            var check = m_CommentRepo.GetLikesForComment(CommentID);
            foreach (var item in check)
            {
                if (item.UserName == theLiker)
                    return Json(null);
            }
            //Púsla like'inu saman
            LikeModel newLike = new LikeModel();
            newLike.UserName = theLiker;
            newLike.CommentID = CommentID;
            //Bæti like'inu í gagnagrunninn
            m_CommentRepo.AddLike(newLike);
            //TODO Redda því að checka á hvort commentari sé að likea commentið sitt.
                var shit = m_UserRepo.GetUserByName(User.Identity.Name);
                shit.XP += 10;
            
            m_UserRepo.Save();
            //Sæki nýjasta like'ið fyrir Json
            var latest = (from x in m_CommentRepo.GetLikesForComment(CommentID)
                          select x).ToList().Last();
            return Json(latest);
        }
        [HttpGet]
        public ActionResult XPList()
        {
            aspnet_User TheUser = m_UserRepo.GetUserByName(User.Identity.Name);
            IQueryable<int>UserXP = m_UserRepo.GetXPForUsers();
            int CurrentXP = m_UserRepo.GetXPForCurrentUser(TheUser.UserId.ToString());
            UserViewModel model = new UserViewModel();
            model.m_XP = UserXP;
            model.m_CurrentXP = CurrentXP;
            return PartialView("XPListView", model);
        }

        public ActionResult TakeTest(int? testID)
        {
            var studentID = m_UserRepo.GetUserByName(User.Identity.Name).UserId;
            if (testID.HasValue)
            {
                if (!m_TestRepo.UserHasNotFinishedTest(studentID, testID.Value))
                {
                    TakeTestViewModel model = new TakeTestViewModel();
                    model.Test = m_TestRepo.GetTestByID(testID.Value);
                    model.Questions = m_TestRepo.GetAllQuestionsForTest(model.Test.ID).ToList();
                    model.Answers = new List<AnswerModel>();

                    foreach (var question in model.Questions)
                    {
                        model.Answers.AddRange(m_TestRepo.GetAllAnswersForQuestion(question.ID));
                    }
                    model.StudentID = m_UserRepo.GetUserByName(User.Identity.Name).UserId;

                    return View(model);
                }
                else
                    return View("TestAlreadyCompleted");
            }
            else
                return View("Error");
        }

        [HttpPost]
        public ActionResult TakeTest(FormCollection formdata)
        {
            if (formdata.Count != 0)
            {
                //Búum til nýtt módel til að birta niðurstöðurnar
                TestCompletedViewModel model = new TestCompletedViewModel 
                { 
                    correctAnswers = 0,
                    numberOfQuestions = 0,
                    Score = 0,
                    Test = m_TestRepo.GetTestByID(Convert.ToInt32(formdata[1]))
                };

                //Sækjum svörin sem nemandi svaraði
                List<AnswerModel> answers = new List<AnswerModel>();
                for (int i = 2; i < formdata.Count; i++)
                {
                    answers.Add(m_TestRepo.GetAnswerByID(Convert.ToInt32(formdata[i])));
                }
                //Förum yfir prófið
                foreach (var answer in answers)
                {
                    model.numberOfQuestions++;
                    if (answer.Correct == true)
                    {
                        model.Score += m_TestRepo.GetQuestionByID(answer.QuestionID).Points.Value;
                        model.correctAnswers++;
                        //TODO: Hækkka XP
                    }
                }

                //TODO: Skrá í gagnagrunn
                //Að nemandi hafi klárað prófið
                m_TestRepo.RegisterTestCompletion(new TestCompletion
                {
                    LevelID = model.Test.LevelID,
                    StudentID = new Guid(formdata[0]),
                    TestID = model.Test.ID
                });
                //Bætum XP við nemanda


                return View("TestCompleted", model);
            }
            else
                return View("Error");
        }
    }
}
