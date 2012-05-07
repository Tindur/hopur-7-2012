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

        public ImageModel GetImageForUser(string UserId)
        {
            var result = (from x in m_usersDB.ImageModels
                          where x.UserID.ToString() == UserId
                          select x).SingleOrDefault();
            return result;
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
    }
}


