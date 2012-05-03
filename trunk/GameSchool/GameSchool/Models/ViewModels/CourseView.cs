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
        CourseRepository m_db = new CourseRepository();
        public List<CourseModel> Courses { get; set; }
        public CourseModel Course { get; set; }

        CourseView()
        {
            //
        }
    }
}