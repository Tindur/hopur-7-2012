﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameSchool.Models.dbLINQ;

namespace GameSchool.Models.Interfaces
{
    public interface IUsersRepository
    {
        IQueryable<aspnet_User> GetAllStudents();
        aspnet_User GetUserById(string id);
        aspnet_Membership GetMembershipById(string id);
        aspnet_UsersInRole GetUserRoleById(string id);
        IQueryable<aspnet_Role> GetRoles();
        void AddUser(aspnet_User User);
        void Save();
        IQueryable<aspnet_User> GetAllTeachers();
    }
}
