using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameSchool.Models.Interfaces;
using GameSchool.Models.dbLINQ;

namespace GameSchool.Models.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        CoursesDBDataContext m_courseDB = new CoursesDBDataContext();

        public IQueryable<CourseModel> GetAllCourses()
        {
            return m_courseDB.CourseModels;
        }

        public IQueryable<CourseModel> GetCoursesForStudent(string studentUsername)
        {
            var result = from cr in m_courseDB.CourseRegistrations
                         join cm in m_courseDB.CourseModels on cr.CourseID equals cm.ID
                         where cr.StudentUsername == studentUsername
                         select cm;

            return result;
        }

        public CourseModel GetCourseById(int id)
        {
            var result = (from x in m_courseDB.CourseModels
                              where x.ID == id
                              select x).SingleOrDefault();
                return result;
        }

        public void AddCourse(CourseModel Course)
        {
            m_courseDB.CourseModels.InsertOnSubmit(Course);
        }

        public void UpdateCourse(int id, CourseRegistration registration)
        {
            
        }
        public void Save()
        {
            m_courseDB.SubmitChanges();
        }
        public IQueryable<TeacherRegistration> GetTeachersForCourse(int CourseId)
        {
            var result = from x in m_courseDB.TeacherRegistrations
                         where x.CourseID == CourseId
                         select x;

            return result;
        }
        public void AddTeachersToCourse(TeacherRegistration reg)
        {
            m_courseDB.TeacherRegistrations.InsertOnSubmit(reg);
        }
    }
}