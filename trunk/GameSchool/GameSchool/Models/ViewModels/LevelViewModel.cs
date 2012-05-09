using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameSchool.Models.dbLINQ;

namespace GameSchool.Models.ViewModels
{
    public class LevelViewModel
    {
        public IQueryable<LectureModel> Lectures { get; set; }
        public IQueryable<AssignmentModel> Assignments { get; set; }
        public IQueryable<TestModel> Tests { get; set; }
        public LevelModel TheLevel { get; set; }
    }
}