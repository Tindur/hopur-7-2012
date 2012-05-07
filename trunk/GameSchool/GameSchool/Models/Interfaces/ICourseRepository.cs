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
        IQueryable<string> GetStudentNameForCourse(int CourseId);
        IQueryable<string> GetTeachersNameForCourse(int CourseId);
        CourseModel GetCourseById(int id);
        void AddCourse(CourseModel Course);
        void AddStudentToCourse(CourseRegistration Registration);
        void AddTeacherToCourse(TeacherRegistration reg);
        void UpdateCourse(int id, CourseRegistration registration);
        void Save();
    }
}
