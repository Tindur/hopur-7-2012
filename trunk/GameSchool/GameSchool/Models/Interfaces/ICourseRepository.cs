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
        IQueryable<CourseModel> GetCoursesForTeacher(string TeacherID);
        IQueryable<CourseRegistration> GetStudentsForCourse(int CourseId);
        IQueryable<TeacherRegistration> GetTeachersForCourse(int CourseId);
        CourseModel GetCourseById(int id);
        void AddCourse(CourseModel Course);
        void UpdateCourse(int id, CourseRegistration registration);
        void Save();
    }
}
