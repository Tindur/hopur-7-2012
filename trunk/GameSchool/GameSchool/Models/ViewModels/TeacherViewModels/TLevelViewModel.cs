using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameSchool.Models.dbLINQ;

namespace GameSchool.Models.ViewModels.TeacherViewModels
{
    public class TLevelViewModel
    {
        public int CourseID { get; set; }
        public List<LevelModel> Levels { get; set; }
        public List<AssignmentModel> Assignments { get; set; }
        public List<TestModel> Tests { get; set; }
        public List<LectureModel> Lectures { get; set; }
    }
}