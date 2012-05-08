using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameSchool.Models.dbLINQ;

namespace GameSchool.Models.ViewModels.TeacherViewModels
{
    public class CreateTestViewModel
    {
        public int CourseID { get; set; }
        public TestModel Test { get; set; }
    }
}