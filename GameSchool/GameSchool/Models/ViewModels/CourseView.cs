using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameSchool.Models.dbLINQ;
using GameSchool.Models.Repositories;

namespace GameSchool.Models.ViewModels
{
    public class CourseView
    {
        /*
        CourseRepository m_db = new CourseRepository();
        public List<CourseModel> Courses { get; set; }
        public CourseModel Course { get; set; }

        CourseView()
        {
            //
        }
         Kv. Björn */

        public CourseModel m_theCourse { get; set; }

        public List<LevelModel> m_theLevels { get; set; }

        public List<int> m_finishedLvlID { get; set; }

        public List<TeacherRegistration> m_theTeachersID { get; set; }

        public IQueryable<aspnet_User> m_Users { get; set; }

        public CourseView(List<TeacherRegistration> registration, CourseModel course, IQueryable<aspnet_User> teachers)
        {
            m_theTeachersID = registration;
            m_theCourse = course;
            m_Users = teachers;
        }


        public CourseView(CourseModel course, IEnumerable<LevelModel> levels, List<int> finishedLvlID)
        {
            m_theCourse = course;
            m_theLevels = levels.ToList();
            m_finishedLvlID = finishedLvlID;
        }
    }
}