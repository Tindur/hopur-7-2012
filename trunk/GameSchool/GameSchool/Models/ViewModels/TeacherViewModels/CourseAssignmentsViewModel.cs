﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameSchool.Models.dbLINQ;

namespace GameSchool.Models.ViewModels.TeacherViewModels
{
    public class CourseAssignmentsViewModel
    {
        public List<LevelModel> Levels { get; set; }
        public List<AssignmentModel> Assignments { get; set; }
    }
}