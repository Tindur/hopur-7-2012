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
        IQueryable<CourseModel> GetCoursesForStudent(string studentUsername);
        CourseModel GetCourseById(int id);
        void AddCourse(CourseModel Course);
        void UpdateCourse(int id, CourseRegistration registration);
        void Save();
        IQueryable<TeacherRegistration> GetTeachersForCourse(int CourseId);
    }
}
