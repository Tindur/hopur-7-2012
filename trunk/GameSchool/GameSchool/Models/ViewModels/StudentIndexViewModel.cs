﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameSchool.Models.dbLINQ;

namespace GameSchool.Models.ViewModels
{
    public class StudentIndexViewModel : Controller
    {
        public IQueryable<LectureModel> Lectures { get; set; }

        public IQueryable<AssignmentModel> Assignments { get; set; }

        public IQueryable<NotificationModel> Notifications { get; set; }

        public string SourceTeacherImage { get; set; }

        public IQueryable<NotificationComment> NotifComments { get; set; }

        public IQueryable<TestModel> Tests { get; set; }

        public CourseModel Course { get; set; }
        //public int CurrentLevel { get; set; }
        //public List<LevelModel> Level { get; set; }
    }
}
