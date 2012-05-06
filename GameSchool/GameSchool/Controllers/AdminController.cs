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
            List<aspnet_Role> TheRoles = m_UsersRepo.GetRoles();
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
/*            foreach(var item in TheRoles)
            {
                model.RoleName.Add(item);
            }*/


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

                TheUser.UserName = model.UserName;
                TheUser.LoweredUserName = model.UserName.ToLower();
                TheUser.Name = model.Name;
                TheUser.SSN = model.SSN;
                TheUser.Address = model.Address;
                TheUser.Phone = model.Phone;
                TheMembership.Email = model.Email;
                TheMembership.LoweredEmail = model.Email.ToLower();
                //TheUsersRole.RoleId = model.Role_ID;

                m_UsersRepo.Save();

                return RedirectToAction("AdminIndex");

            }
            return View("GetUsers");
        }
        public ActionResult CreateUser()
        {
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
                TheUser.Name = model.Name;
                TheUser.SSN = model.SSN;
                TheUser.Address = model.Address;
                TheUser.Phone = model.Phone;

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
           /* List<TeacherRegistration> registration = new List<TeacherRegistration>();
            CourseModel course = new CourseModel();
            IQueryable<aspnet_User> teachers = m_UsersRepo.GetAllTeachers();

            CourseView model = new CourseView(registration, course, teachers);

            return View(model);*/
            return View();

        }
        [HttpPost]
        public ActionResult CreateCourse(FormCollection FormData)
        {
            TeacherRegistration registration = new TeacherRegistration();
            /*
            registration.TeacherID = FormData.GetValue("UserID");
            registration.CourseID = FormData["CourseID"];
             */
            UpdateModel(registration);
            m_CourseRepo.AddTeachersToCourse(registration);

            CourseModel Course = new CourseModel();
            UpdateModel(Course);
            m_CourseRepo.AddCourse(Course);

            m_CourseRepo.Save();
            
            return RedirectToAction("GetCourses");
        }
        public ActionResult EditCourse(int id)
        {
            CourseModel TheCourse = m_CourseRepo.GetCourseById(id);
            IQueryable<aspnet_User> TheStudents = m_UsersRepo.GetAllStudents();
            IQueryable<aspnet_User> TheTeachers = m_UsersRepo.GetAllTeachers();
            IQueryable<CourseRegistration> TheStudentsRegistration = m_CourseRepo.GetStudentsForCourse(id);
            IQueryable<TeacherRegistration> TheTeachersRegistration = m_CourseRepo.GetTeachersForCourse(id);

            CourseEditViewModel model = new CourseEditViewModel();
            
            model.Course = TheCourse;
            model.Students = TheStudents;
            model.Teachers = TheTeachers;
            model.StudentsInCourse =TheStudentsRegistration;
            model.TeachersInCourse = TheTeachersRegistration;

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
    }
}
