using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameSchool.Models.Interfaces;
using GameSchool.Models.dbLINQ;
using System.Diagnostics;
using System.Web.Security;

namespace GameSchool.Models.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        UsersDBDataContext m_usersDB = new UsersDBDataContext();
        CoursesDBDataContext m_courseDB = new CoursesDBDataContext();

        public IQueryable<aspnet_User> GetAllStudents()
        {
            var Students = from aspnet_User in m_usersDB.aspnet_Users
                           join userinroles in m_usersDB.aspnet_UsersInRoles on aspnet_User.UserId equals userinroles.UserId
                           where userinroles.aspnet_Role.RoleName == "Student"
                           select aspnet_User;

            return Students;
        }

        public aspnet_User GetUserById(string id)
        {
            var result = (from x in m_usersDB.aspnet_Users
                          where x.UserId.ToString() == id
                          select x).SingleOrDefault();

            return result;
        }

        public aspnet_User GetUserIDByUserName(string username)
        {
            var result = (from x in m_usersDB.aspnet_Users
                          where x.UserName == username
                          select x).SingleOrDefault();

            return result;
        }

        public void AddUser(aspnet_User Student)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            m_usersDB.SubmitChanges();
        }
        public IQueryable<aspnet_User> GetAllTeachers()
        {
            var Teachers = from aspnet_User in m_usersDB.aspnet_Users
                           join userinroles in m_usersDB.aspnet_UsersInRoles on aspnet_User.UserId equals userinroles.UserId
                           where userinroles.aspnet_Role.RoleName == "Teacher"
                           select aspnet_User;

            return Teachers;
        }


        public aspnet_Membership GetMembershipById(string id)
        {
            var result = (from x in m_usersDB.aspnet_Memberships
                          where x.UserId.ToString() == id
                          select x).SingleOrDefault();
            return result;
        }

        public aspnet_UsersInRole GetUserRoleById(string id)
        {
            var result = (from x in m_usersDB.aspnet_UsersInRoles
                          where x.UserId.ToString() == id
                          select x).SingleOrDefault();
            return result;
        }

        public IQueryable<aspnet_Role> GetRoles()
        {
            var result = from x in m_usersDB.aspnet_Roles
                         select x;
            return result;
        }
        public aspnet_User GetUserByName(string name)
        {
            var result = (from x in m_usersDB.aspnet_Users
                          where x.UserName == name
                          select x).SingleOrDefault();
            return result;
        }

        public aspnet_User GetUserByNamez(string fullName)
        {
            var result = (from x in m_usersDB.aspnet_Users
                          where x.Name == fullName
                          select x).SingleOrDefault();
            return result;
        }

        public ImageModel GetImageForUser(string UserId)
        {
            var result = (from x in m_usersDB.ImageModels
                          where x.UserID.ToString() == UserId
                          select x).SingleOrDefault();
            return result;
        }

        public string GetImageForName(string name)
        {
                var NameID = GetUserByNamez(name).UserId;
                var result = (from x in m_usersDB.ImageModels
                              where x.UserID == NameID
                              select x.Source).SingleOrDefault();
                return result;
        }

        public class TeacherForStudent
        {
            public string TeacherName { get; set; }
            public string ImageSource { get; set; }
        }

        public IQueryable<ImageModel> GetImageForTeachersUser(string UserId)
        {
            
            var UserName = GetUserById(UserId).UserName;

            var result = from x in m_courseDB.CourseRegistrations
                         where x.StudentUsername == UserName
                         select x.CourseID;

            var anotherResult = from x in m_courseDB.TeacherRegistrations
                                where result.ToList().Contains(x.CourseID)
                                select x.TeacherUsername;

            List<String> tStud = new List<String>();
            foreach (var teacher in anotherResult)
	        {
                var teacherUserID = GetUserIDByUserName(teacher).UserId.ToString();
		        tStud.Add(teacherUserID);
	        }

            var finalResult = from x in m_usersDB.ImageModels
                              where tStud.Contains(x.UserID.ToString())
                              select x;
            return finalResult;

            

            




            /*var result = from cr in m_courseDB.CourseRegistrations
                         join cm in m_courseDB.CourseModels on cr.CourseID equals cm.ID
                         where cr.StudentUsername == studentUsername
                         select cm;*/
        }
        public void AddUserImage(ImageModel Image)
        {
            m_usersDB.ImageModels.InsertOnSubmit(Image);
        }

        public void RemoveUserFromRole(aspnet_UsersInRole usr)
        {
            m_usersDB.aspnet_UsersInRoles.DeleteOnSubmit(usr);
        }

        public void SetUserToRole(aspnet_UsersInRole usr)
        {
            m_usersDB.aspnet_UsersInRoles.InsertOnSubmit(usr);
        }
        public IQueryable<int> GetXPForUsers()
        {
            var result = from x in GetAllStudents()
                                  orderby x.XP.Value descending
                                  select x.XP.Value;

            return result;           
        }
        public int GetXPForCurrentUser(string CurrentID)
        {
            var result = (from x in  GetAllStudents()
                                where x.UserId.ToString() == CurrentID.ToString()
                                select x.XP.Value).SingleOrDefault();

            return result;
        }
    }
}


