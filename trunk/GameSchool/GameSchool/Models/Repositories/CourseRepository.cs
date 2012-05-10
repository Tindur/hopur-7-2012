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
            var result = from cr in m_courseDB.CourseModels
                         orderby cr.Name ascending
                         select cr;
            return result;
        }

        public IQueryable<CourseModel> GetCoursesForStudent(string studentUsername)
        {
            var result = from cr in m_courseDB.CourseRegistrations
                         join cm in m_courseDB.CourseModels on cr.CourseID equals cm.ID
                         where cr.StudentUsername == studentUsername
                         orderby cm.Name ascending
                         select cm;

            return result;
        }

        public IQueryable<CourseModel> GetCoursesForTeacher(string TeachersUserName)
        {
           var result = from cr in m_courseDB.TeacherRegistrations
                         join cm in m_courseDB.CourseModels on cr.CourseID equals cm.ID
                         where cr.TeacherUsername == TeachersUserName
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

        public void AddStudentToCourse(CourseRegistration Registration)
        {
            m_courseDB.CourseRegistrations.InsertOnSubmit(Registration);
        }

        public void AddTeacherToCourse(TeacherRegistration Registration)
        {
            m_courseDB.TeacherRegistrations.InsertOnSubmit(Registration);
        }

        public void Save()
        {
            m_courseDB.SubmitChanges();
        }

        public IQueryable<string> GetTeachersNameForCourse(int CourseId)
        {
            var result = from x in m_courseDB.TeacherRegistrations
                         where x.CourseID == CourseId
                         select x.TeacherUsername;

            return result;
        }

        public IQueryable<string> GetStudentNameForCourse(int CourseId)
        {
            var result = from x in m_courseDB.CourseRegistrations
                         where x.CourseID == CourseId
                         select x.StudentUsername;
            return result;
        }

        public void RemoveStudentFromCourse(int courseID, string studentUserName)
        {
            CourseRegistration cReg = (from reg in m_courseDB.CourseRegistrations
                                       where courseID == reg.CourseID
                                       where studentUserName == reg.StudentUsername
                                       select reg).SingleOrDefault();

            m_courseDB.CourseRegistrations.DeleteOnSubmit(cReg);
        }

        public IQueryable<TeacherRegistration> GetTeacherRegistrations()
        {
            var result = from x in m_courseDB.TeacherRegistrations
                         select x;
            return result;
        }
    }
}