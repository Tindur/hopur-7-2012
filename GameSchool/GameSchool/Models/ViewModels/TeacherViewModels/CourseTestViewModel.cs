using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameSchool.Models.dbLINQ;

namespace GameSchool.Models.ViewModels.TeacherViewModels
{
    public class CourseTestViewModel
    {
        public List<LevelModel> Levels { get; set; }
        public List<TestModel> Tests { get; set; }
    }
}