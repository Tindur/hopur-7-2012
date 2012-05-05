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
            model.Password = TheMembership.Password;
            model.ConfirmPassword = TheMembership.Password;
            model.Role_ID = TheUsersRole.RoleId.ToString();
 /*           foreach(var item in TheRoles)
            {
                model.RoleName.Add(item.RoleName);
            }*/


            return View(model);
        }
        [HttpPost]
        public ActionResult EditUser(RegistrationViewModel model)
        {
            if (model != null)//ModelState.IsValid)
            {
                aspnet_User TheUser = m_UsersRepo.GetUserById(model.User_ID);
                aspnet_Membership TheMembership = m_UsersRepo.GetMembershipById(model.User_ID);
                aspnet_UsersInRole TheUsersRole = m_UsersRepo.GetUserRoleById(model.User_ID);

                TheUser.UserName = model.UserName;
                TheUser.Name = model.Name;
                TheUser.SSN = model.SSN;
                TheUser.Address = model.Address;
                TheUser.Phone = model.Phone;
                TheMembership.Email = model.Email;
                TheMembership.Password = model.Password;
                //TheMembership.Password = model.ConfirmPassword;
                //TheUsersRole.RoleId = model.Role_ID;

                UpdateModel(TheUser);
                UpdateModel(TheMembership);
                UpdateModel(TheUsersRole);
                m_UsersRepo.Save();


                return RedirectToAction("AdminIndex");

            }
            /*if(TheUser != null)
            {
                UpdateModel(TheUser);
                m_UsersRepo.Save();

                return RedirectToAction("GetUsers");
            }
            else*/
                return View("GetUsers");
        }
        public ActionResult CreateUser()
        {
            return View();
        }
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
            CourseModel Course = m_CourseRepo.GetCourseById(id);
            return View(Course);
        }
        [HttpPost]
        public ActionResult EditCourse(int id, FormCollection FormData)
        {
            CourseModel Course = m_CourseRepo.GetCourseById(id);
            if (Course != null)
            {
                UpdateModel(Course);
                m_CourseRepo.Save();
                return RedirectToAction("GetCourses");
            }
            return RedirectToAction("GetCourses");
        }
    }
}
