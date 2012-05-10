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

        public IQueryable<LectureModel> m_theLectures { get; set; }

        public IQueryable<AssignmentModel> m_theAssignments { get; set; }

        public IQueryable<TestModel> m_theTests { get; set; }

        public int m_CompletedLevels { get; set; }

        public int m_CurrentLevel { get; set; }

        public IQueryable<AssignmentCompletion> m_FinishedAssignments { get; set; }

        public IQueryable<TestCompletion> m_FinishedTests { get; set; }

        //TODO LECTURECOMPLETION!!!!

        


        //TODO:  Setja í CourseViewModel Próf, Verkefni, Tilkynningar, glósur, kennara? e.t.v. fl.  Kv Björn

        //public List<TeacherRegistration> m_theTeachersID { get; set; }

        //public IQueryable<aspnet_User> m_Users { get; set; }
    }
}