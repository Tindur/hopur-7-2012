﻿using System;
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

        public void AddStudent(aspnet_User Student)
        {
            throw new NotImplementedException();
        }

        public void UpdateStudent(aspnet_User Student)
        {
            throw new NotImplementedException();
        }
        public IQueryable<aspnet_User> GetAllTeachers()
        {
            var Teachers = from aspnet_User in m_usersDB.aspnet_Users
                            join userinroles in m_usersDB.aspnet_UsersInRoles on aspnet_User.UserId equals userinroles.UserId
                            where userinroles.aspnet_Role.RoleName == "Teacher"
                            select aspnet_User;

            return Teachers;
        }
    }
}