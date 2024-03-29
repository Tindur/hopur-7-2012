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
        aspnet_User GetUserByName(string name);
        aspnet_Membership GetMembershipById(string id);
        aspnet_UsersInRole GetUserRoleById(string id);
        void RemoveUserFromRole(aspnet_UsersInRole usr);
        void SetUserToRole(aspnet_UsersInRole usr);
        IQueryable<aspnet_Role> GetRoles();
        void AddUser(aspnet_User User);
        void Save();
        IQueryable<aspnet_User> GetAllTeachers();
        ImageModel GetImageForUser(string UserId);
        void AddUserImage(ImageModel Image);
        IQueryable<int> GetXPForUsers();
    }
}
