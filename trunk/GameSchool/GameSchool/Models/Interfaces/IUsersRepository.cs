using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameSchool.Models.dbLINQ;

namespace GameSchool.Models.Interfaces
{
    interface IUsersRepository
    {
        IQueryable<aspnet_User> GetAllStudents();
        aspnet_User GetUserById(string id);
        void AddStudent(aspnet_User Student);
        void UpdateStudent(aspnet_User Student);
        IQueryable<aspnet_User> GetAllTeachers();
    }
}
