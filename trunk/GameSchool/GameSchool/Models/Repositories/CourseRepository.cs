﻿using System;
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

        public IQueryable<CourseRegistration> GetCoursesForStudent(string studentUsername)
        {
            var result = from x in m_courseDB.CourseRegistrations
                         where x.StudentUsername == studentUsername
                         select x;
            return result;
        }

        public CourseModel GetCourseById(int id)
        {
            var result = (from x in m_courseDB.CourseModels
                              where x.ID == id
                              select x).SingleOrDefault();
                return result;
        }

        public void AddCourse(CourseModel course)
        {
            //TODO
        }

        public void UpdateCourse(CourseRegistration registration)
        {
            //TODO
        }
    }
}