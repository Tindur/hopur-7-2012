using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameSchool.Models;
using GameSchool.Models.Repositories;
using GameSchool.Models.dbLINQ;
using GameSchool.Models.ViewModels;
using System.Web.Security;
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
            aspnet_Membership TheMembership = m_UsersRepo.GetMembershipById(id);
            IQueryable<aspnet_Role> TheRoles = m_UsersRepo.GetRoles();
            aspnet_UsersInRole TheUsersRole = m_UsersRepo.GetUserRoleById(id);

            RegistrationViewModel model = new RegistrationViewModel();

            model.User_ID = TheUser.UserId.ToString();
            model.UserName = TheUser.UserName;
            model.Name = TheUser.Name;
            model.SSN = TheUser.SSN;
            model.Address = TheUser.Address;
            model.Phone = TheUser.Phone;
            model.Email = TheMembership.Email;
            model.Role_ID = TheUsersRole.RoleId.ToString();

            List<SelectListItem> item = new List<SelectListItem>();
            foreach(var role in TheRoles)
            {
                item.Add(new SelectListItem { Text = role.RoleName, Value = role.RoleId.ToString() });
            }
            ViewData["Roles"] = item;

            return View(model);
        }
        [HttpPost]
        public ActionResult EditUser(RegistrationViewModel model)
        {
            if (model != null)
            {
                aspnet_User TheUser = m_UsersRepo.GetUserById(model.User_ID);
                aspnet_Membership TheMembership = m_UsersRepo.GetMembershipById(model.User_ID);
                aspnet_UsersInRole TheUsersRole = m_UsersRepo.GetUserRoleById(model.User_ID);
                Guid TheUsersRoleGuid = new Guid(model.Role_ID);
                Guid TheUsersID = new Guid(model.User_ID);

                m_UsersRepo.RemoveUserFromRole(TheUsersRole);

                TheUser.UserName = model.UserName;
                TheUser.LoweredUserName = model.UserName.ToLower();
                TheUser.Name = model.Name;
                TheUser.SSN = model.SSN;
                TheUser.Address = model.Address;
                TheUser.Phone = model.Phone;
                TheMembership.Email = model.Email;
                TheMembership.LoweredEmail = model.Email.ToLower();

                aspnet_UsersInRole role = new aspnet_UsersInRole();
                role.RoleId = TheUsersRoleGuid;
                role.UserId = TheUsersID;


                m_UsersRepo.SetUserToRole(role);
                m_UsersRepo.Save();

                return RedirectToAction("AdminIndex");

            }
            return View("Error");
        }
        public ActionResult CreateUser()
        {
            IQueryable<aspnet_Role> TheRoles = m_UsersRepo.GetRoles();
            List<SelectListItem> item = new List<SelectListItem>();
            foreach (var role in TheRoles)
            {
                item.Add(new SelectListItem { Text = role.RoleName, Value = role.RoleId.ToString() });
            }
            ViewData["Roles"] = item;

            return View();
        }
        [HttpPost]
        public ActionResult CreateUser(RegistrationViewModel model)
        {
            if (model != null)
            {
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                aspnet_User TheUser = m_UsersRepo.GetUserByName(model.UserName);
                aspnet_UsersInRole TheUserRole = new aspnet_UsersInRole();
                Guid TheUsersRoleID = new Guid(model.Role_ID);
                //Guid TheUserID = new Guid(model.User_ID);

                TheUserRole.RoleId = TheUsersRoleID;
                TheUserRole.UserId = TheUser.UserId;
                TheUser.Name = model.Name;
                TheUser.SSN = model.SSN;
                TheUser.Address = model.Address;
                TheUser.Phone = model.Phone;
                ImageModel TheImage = new ImageModel
                {
                    Source = "../../Content/Images/Prisonmike.png",
                    UserID = TheUser.UserId
                };
                m_UsersRepo.AddUserImage(TheImage);
                //Guid TheUserId = new Guid(TheUser.UserId);

                m_UsersRepo.SetUserToRole(TheUserRole);
                m_UsersRepo.Save();


                if (createStatus == MembershipCreateStatus.Success)
                {
                    return RedirectToAction("AdminIndex", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }

            }
            return View(model);
        }
        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Notendanafn er nú þegar til. Vinsamlegast veldu nýtt nafn.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "Notendanafn með þetta netfang er nú þegar til. Vinsamlegast veldu annað netfang.";

                case MembershipCreateStatus.InvalidPassword:
                    return "Lykilorðið sem þú valdir er ógilt. Vinsamlegast veldu gilt lykilorð.";

                case MembershipCreateStatus.InvalidEmail:
                    return "Netfangið sem þú valdir er ekki gilt. Vinsamlegastathugið hvort rétt netfang sé skráð.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "Svar ekki rétt. Vinsamlegast reynið aftur.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "Spurning ekki rétt. Athugið gildi og reynið aftur.";

                case MembershipCreateStatus.InvalidUserName:
                    return "Notendanafn sem var gefið upp er ógilt. Vinsamlegast athugið hvort rétt nafn var gefið upp og reynið aftur.";

                case MembershipCreateStatus.ProviderError:
                    return "Staðfestingarvilla kom upp. Athugið að allar upplýsingar séu rétt upp gefnar. Ef þetta gerist aftur hafið samband við umsjónarmann kerfisins.";

                case MembershipCreateStatus.UserRejected:
                    return "Upp kom villa við nýskráningu. Athugið að allar upplýsingar séu rétt upp gefnar. Ef þetta gerist aftur hafið samband við umsjónarmann kerfisins.";

                default:
                    return "Óþekkt villa kom upp. Athugið að allar upplýsingar séu rétt upp gefnar. Ef þetta gerist aftur hafið samband við umsjónarmann kerfisins.";
            }
        }
        #endregion

        public ActionResult GetCourses()
        {
            var model = m_CourseRepo.GetAllCourses();
            return View(model);
        }
        public ActionResult CreateCourse()
        {
            IQueryable<aspnet_User> TheStudents = m_UsersRepo.GetAllStudents();
            IQueryable<aspnet_User> TheTeachers = m_UsersRepo.GetAllTeachers();

            CourseEditViewModel model = new CourseEditViewModel();
            model.Students = TheStudents;
            model.Teachers = TheTeachers;
            model.StudentNameInCourse = new List<String>().AsQueryable();
            model.TeachersNameInCourse = new List<String>().AsQueryable();
            model.Course = new CourseModel();

            return View(model);
        }
        [HttpPost]
        public ActionResult CreateCourse(CourseEditViewModel model)
        {
            CourseModel TheCourse = model.Course;

            m_CourseRepo.AddCourse(TheCourse);
            m_CourseRepo.Save();
            
            return RedirectToAction("GetCourses");
        }
        public ActionResult EditCourse(int id)
        {
            CourseModel TheCourse = m_CourseRepo.GetCourseById(id);
            IQueryable<aspnet_User> TheStudents = m_UsersRepo.GetAllStudents();
            IQueryable<aspnet_User> TheTeachers = m_UsersRepo.GetAllTeachers();
            IQueryable<string> TheStudentsRegistration = m_CourseRepo.GetStudentNameForCourse(id);
            IQueryable<string> TheTeachersRegistration = m_CourseRepo.GetTeachersNameForCourse(id);

            CourseEditViewModel model = new CourseEditViewModel();
            
            model.Course = TheCourse;
            model.Students = TheStudents;
            model.Teachers = TheTeachers;
            model.StudentNameInCourse = TheStudentsRegistration;
            model.TeachersNameInCourse = TheTeachersRegistration;

            return View(model);
        }
        [HttpPost]
        public ActionResult EditCourse(CourseEditViewModel model)
        {
            if (model != null)
            {
                CourseModel TheCourse = m_CourseRepo.GetCourseById(model.Course.ID);
                TheCourse.Name = model.Course.Name;
                TheCourse.Description = model.Course.Description;

                m_CourseRepo.Save();
            }
            return RedirectToAction("GetCourses");
        }

        [HttpPost]
        public ActionResult AddStudentToCourse(int CourseID, string StudentName)
        {
            CourseRegistration Registration = new CourseRegistration();
            Registration.CourseID = CourseID;
            Registration.StudentUsername = StudentName;

            m_CourseRepo.AddStudentToCourse(Registration);
            m_CourseRepo.Save();

            return Json(Registration, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddTeacherToCourse(int CourseID, string TeachersUserName)
        {
            TeacherRegistration Registration = new TeacherRegistration();
            Registration.CourseID = CourseID;
            Registration.TeacherUsername = TeachersUserName;

            m_CourseRepo.AddTeacherToCourse(Registration);
            m_CourseRepo.Save();

            return Json(Registration, JsonRequestBehavior.AllowGet);
            //return Json(null);
        }

        [HttpPost]
        public ActionResult RemoveStudentFromCourse(int courseID, string StudentName)
        {
            m_CourseRepo.RemoveStudentFromCourse(courseID, StudentName);
            m_CourseRepo.Save();

            return Json(null);
        }
    }
}