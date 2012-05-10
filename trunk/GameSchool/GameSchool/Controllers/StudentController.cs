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
using System.IO;

namespace GameSchool.Controllers
{
    [Authorize(Roles = "Student")]  
    public class StudentController : Controller
    {
        public ActionResult StudentIndex()
        {
            return View();
        }
        
        #region Repos
        CourseRepository m_CourseRepo = new CourseRepository();
        LevelRepository m_lvlRepo = new LevelRepository();
        LectureRepository m_LectureRepo = new LectureRepository();
        TestRepository m_TestRepo = new TestRepository();
        CommentRepository m_CommentRepo = new CommentRepository();
        AssignmentRepository m_AssignmentRepo = new AssignmentRepository();
        UsersRepository m_UserRepo = new UsersRepository();
        NotificationRepository m_NotificationRepo = new NotificationRepository();
        CourseXPRepository m_CourseXPRepo = new CourseXPRepository();
        #endregion
      
        #region Navigation
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
        #endregion

        #region Courses
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
                string theUser = m_UserRepo.GetUserByName(User.Identity.Name).UserName;
                
                
           
                return View("Course", new CourseView
                                    {
                                        m_theCourse = m_CourseRepo.GetCourseById(id.Value),
                                        m_theLevels = levels.ToList(),
                                        m_CurrentLevel = m_lvlRepo.GetCurrentLevelForStudent(User.Identity.Name, id.Value),
                                        m_theLectures = m_LectureRepo.GetLecturesForCourse(id.Value),
                                        m_theAssignments = m_AssignmentRepo.GetAssignmentsForCourse(id.Value),
                                        m_theTests = m_TestRepo.GetTestsForCourse(id.Value),
                                        m_CompletedLevels = m_lvlRepo.GetAmountOfCompletedLevels(theUser, id.Value),
                                        m_finishedLvlID = null,
                                        m_FinishedAssignments = m_AssignmentRepo.GetFinishedAssignmentsForUser(User.Identity.Name, id.Value),
                                        m_FinishedTestsID = m_TestRepo.GetFinishedTestsInCourse(theUser, id.Value),
                                        m_FinishedLecturesID = m_LectureRepo.GetFinishedLecturesInCourse(theUser, id.Value)
                                    });
            }
            else
            {
                return RedirectToAction("StudentIndex");
            }
        }
        #endregion

        #region Levels
        //TODO: útfæra
        public bool checkForLevelCompletion(int? LevelID, string StudentName)
        {
            if (LevelID.HasValue)
            {
                //TODO: Athuga hvort nemandi sé búinn með:
                //Öll próf
                List<TestModel> Tests = m_TestRepo.GetAllTestsForLevel(LevelID.Value).ToList();
                foreach (var test in Tests)
                {
                    //Ef nemandi hefur ekki lokið einherju prófi
                    if (!m_TestRepo.UserHasFinishedTest(StudentName, test.ID))
                        return false;
                }

                //Öll verkefni
                List<AssignmentModel> Assignments = m_AssignmentRepo.GetAllAssignmentsForLevel(LevelID.Value).ToList();
                foreach (var assignment in Assignments)
                {
                    if (!m_AssignmentRepo.HasStudentFinishedAssignment(StudentName, assignment.ID))
                        return false;
                }

                //Allir fyrirlestrar
                List<LectureModel> Lectures = m_LectureRepo.GetLecturesForLevel(LevelID.Value).ToList();
                foreach (var lecture in Lectures)
                {
                    if (!m_LectureRepo.HasStudentFinishedLecture(lecture.ID, StudentName))
                        return false;
                }

                //Ef við komumst hingað hefur nemandi lokið borðinu
                return true;
            }
            //Ef annað hvort nafn nemanda eða LevelID eru null skilum við false
            else
                return false;
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

        #endregion

        #region Lectures
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
            if(id.HasValue)
            {
                LectureModel model = m_LectureRepo.GetLectureByID(id.Value); //Find the lecture
                //Check if student has already watched the lecture
                if(!m_LectureRepo.HasStudentFinishedLecture(id.Value, User.Identity.Name))
                {
                    //Marking Lecture as completed
                    int cID = m_LectureRepo.GetCourseIDByLectureID(id.Value);
                    LectureCompletion Completion = new LectureCompletion();
                    Completion.LectureID = id.Value;
                    Completion.UserName = User.Identity.Name;
                    Completion.CourseID = cID;
                    m_LectureRepo.RegisterLectureCompletion(Completion);
                    m_LectureRepo.Save();

                    //Updating CourseXP
                
                    CourseXP TheUserCourseXP = m_CourseXPRepo.GetCourseXPByUserName(User.Identity.Name);
                    if (TheUserCourseXP == null)
                    {
                        TheUserCourseXP = m_CourseXPRepo.CreateNewXPForUserName(User.Identity.Name, model.CourseID.Value);
                        m_CourseXPRepo.RegisterXPForCourse(TheUserCourseXP);
                    }

                    TheUserCourseXP.XP += model.Points.Value;
                    m_CourseXPRepo.Save();

                    //Updating UserXP
                    aspnet_User TheUser = m_UserRepo.GetUserByName(User.Identity.Name);

                    TheUser.XP += model.Points.Value;
                    m_UserRepo.Save();
                }

                if (checkForLevelCompletion(model.LevelID, User.Identity.Name))
                {
                    m_lvlRepo.RegisterLevelCompletion(model.LevelID, User.Identity.Name);
                }

                //Returning the video to the user!
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //ATH TODO!! bjarkfi
                return RedirectToAction("StudentIndex");
            }
        }

        #endregion

        #region Comments
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
        #endregion

        #region Likes
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
            string theChecker = m_CommentRepo.GetNameForComment(CommentID);
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
            if (theLiker != theChecker)
            {
                var shit = m_UserRepo.GetUserByName(theChecker);
                shit.XP += 10;
            }

            m_UserRepo.Save();
            //Sæki nýjasta like'ið fyrir Json
            var latest = (from x in m_CommentRepo.GetLikesForComment(CommentID)
                          select x).ToList().Last();
            return Json(latest);
        }
        #endregion

        #region Tests
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

        public ActionResult TakeTest(int? testID)
        {
            if (testID.HasValue)
            {
                if (!m_TestRepo.UserHasFinishedTest(User.Identity.Name, testID.Value))
                {
                    TakeTestViewModel model = new TakeTestViewModel();
                    model.Test = m_TestRepo.GetTestByID(testID.Value);
                    model.Questions = m_TestRepo.GetAllQuestionsForTest(model.Test.ID).ToList();
                    model.Answers = new List<AnswerModel>();

                    foreach (var question in model.Questions)
                    {
                        model.Answers.AddRange(m_TestRepo.GetAllAnswersForQuestion(question.ID));
                    }
                    model.StudentName = User.Identity.Name;

                    return View(model);
                }
                else
                {
                    TestModel test = m_TestRepo.GetTestByID(testID.Value);
                    return View("TestAlreadyCompleted", new TestAlreadyCompletedViewModel
                    {
                        Test = test,
                        StudentScore = m_TestRepo.GetStudentScoreInTest(test.ID, User.Identity.Name)
                    });
                }
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
                    }
                }

                //TODO: Skrá í gagnagrunn
                //Að nemandi hafi klárað prófið
                m_TestRepo.RegisterTestCompletion(new TestCompletion
                {
                    LevelID = model.Test.LevelID,
                    StudentName = formdata[0],
                    TestID = model.Test.ID,
                    Points = model.Score
                });
                //Bætum XP við nemanda
                //Updating CourseXP
                CourseXP TheUserCourseXP = m_CourseXPRepo.GetCourseXPByUserName(User.Identity.Name);
                if (TheUserCourseXP == null)
                {
                    TheUserCourseXP = m_CourseXPRepo.CreateNewXPForUserName(User.Identity.Name, m_lvlRepo.GetLevelCourse(model.Test.LevelID));
                    m_CourseXPRepo.RegisterXPForCourse(TheUserCourseXP);
                }

                TheUserCourseXP.XP += model.Score;
                m_CourseXPRepo.Save();

                //Updating UserXP
                aspnet_User TheUser = m_UserRepo.GetUserByName(User.Identity.Name);

                TheUser.XP += model.Score;
                m_UserRepo.Save();

                if (checkForLevelCompletion(model.Test.LevelID, User.Identity.Name))
                {
                    m_lvlRepo.RegisterLevelCompletion(model.Test.LevelID, User.Identity.Name);
                }

                return View("TestCompleted", model);
            }
            else
                return View("Error");
        }
        #endregion

        #region Assignments
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
                if (!m_AssignmentRepo.HasStudentFinishedAssignment(User.Identity.Name, id.Value))
                {
                    AssignmentModel model = m_AssignmentRepo.GetAssignmentById(id.Value);
                    return View(model);
                }
                else
                    return View("AssignmentAlreadyCompleted");
            }
            return View("Error");
        }

        [HttpPost]
        public ActionResult GetAssignment(AssignmentModel model /*, HttpPostedFileBase file*/)
        {
            /*if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/App_Data/Uploads"), fileName);
                file.SaveAs(path);
            }*/

            if (model != null)
            {
                //Marking assignment as completed
                AssignmentCompletion Completion = new AssignmentCompletion();
                Completion.AssignmentID = model.ID;
                Completion.UserName = User.Identity.Name;
                Completion.CourseID = model.CourseID;

                m_AssignmentRepo.RegisterAssignmentCompletion(Completion);
                m_AssignmentRepo.Save();

                //Updating CourseXP
                CourseXP TheUserCourseXP = m_CourseXPRepo.GetCourseXPByUserName(User.Identity.Name);
                if (TheUserCourseXP == null)
                {
                    TheUserCourseXP = m_CourseXPRepo.CreateNewXPForUserName(User.Identity.Name, model.CourseID.Value);
                    m_CourseXPRepo.RegisterXPForCourse(TheUserCourseXP);
                }

                TheUserCourseXP.XP += model.Points.Value;
                m_CourseXPRepo.Save();

                //Updating UserXP
                aspnet_User TheUser = m_UserRepo.GetUserByName(User.Identity.Name);

                TheUser.XP += model.Points.Value;
                m_UserRepo.Save();

                if (checkForLevelCompletion(model.LevelID, User.Identity.Name))
                {
                    m_lvlRepo.RegisterLevelCompletion(model.LevelID, User.Identity.Name);
                }


                return RedirectToAction("GetCourse", "Student", model.CourseID.Value);
            }
            else
                return View("Error");

        }
        #endregion

        #region NewsFeed
        public ActionResult NewsFeed(int? id)
        {
            if(id.HasValue)
            {
                IQueryable<LectureModel> TheLectures = m_LectureRepo.GetFiveLatest(id.Value);
                IQueryable<AssignmentModel> TheAssignments = m_AssignmentRepo.GetFiveLatest(id.Value);
                IQueryable<NotificationModel> TheNotifications = m_NotificationRepo.GetFiveLatest(id.Value);
                IQueryable<TestModel> TheTests = m_TestRepo.GetFiveLatest(id.Value);
                if (TheNotifications.Any())
                {
                    string SourceTeacherImage = m_UserRepo.GetImageForName(m_NotificationRepo.GetNameOfTeacher(id.Value));
                    NewsFeedViewModel model = new NewsFeedViewModel();
                    model.Lectures = TheLectures;
                    model.Assignments = TheAssignments;
                    model.Tests = TheTests;
                    model.Notifications = TheNotifications;
                    model.SourceTeacherImage = SourceTeacherImage;
                    return PartialView(model);
                }
                else
                {
                    NewsFeedViewModel model = new NewsFeedViewModel();
                    model.Lectures = TheLectures;
                    model.Assignments = TheAssignments;
                    model.Tests = TheTests;
                    model.Notifications = null;
                    model.SourceTeacherImage = null;
                    return PartialView(model);
                }


                

                

            }
            return View("Error");
        }
        #endregion

        #region Profile Management
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
        #endregion

        #region XP
        [HttpGet]
        public ActionResult XPList()
        {
            aspnet_User TheUser = m_UserRepo.GetUserByName(User.Identity.Name);
            IQueryable<int>UserXP = m_UserRepo.GetXPForUsers();
            int CurrentXP = m_UserRepo.GetXPForCurrentUser(TheUser.UserId.ToString());
            int Counter = m_UserRepo.GetAllStudents().Count();
            UserViewModel model = new UserViewModel();
            model.m_XP = UserXP;
            model.m_CurrentXP = CurrentXP;
            model.m_Counter = Counter;
            return PartialView("XPListView", model);
        }
        

        [HttpGet]
        public ActionResult XPCourseList()
        {
            aspnet_User TheUser = m_UserRepo.GetUserByName(User.Identity.Name);
            IQueryable<int> AllXP = m_CourseXPRepo.GetXPForStudentsInCourse();
            int CurrentXP = m_CourseXPRepo.GetXPForCurrentUserInCourse(TheUser.UserName.ToString());
            int Counter = m_CourseXPRepo.GetXPForStudentsInCourse().Count();
            UserViewModel model = new UserViewModel();
            model.m_XP = AllXP;
            model.m_CurrentXP = CurrentXP;
            model.m_Counter = Counter;
            return PartialView("XPCourseList", model);
        }
        #endregion
    }
}
