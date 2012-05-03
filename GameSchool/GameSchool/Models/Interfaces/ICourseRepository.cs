using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameSchool.Models.dbLINQ;

namespace GameSchool.Models.Interfaces
{
    interface ICourseRepository
    {
        IQueryable<CourseModel> GetAllCourses();
        IQueryable<CourseRegistration> GetCoursesForStudent(string studentUsername);
        CourseModel GetCourseById(int id);
        void AddCourse(CourseModel course);
        void UpdateCourse(CourseRegistration registration);
    }
}
