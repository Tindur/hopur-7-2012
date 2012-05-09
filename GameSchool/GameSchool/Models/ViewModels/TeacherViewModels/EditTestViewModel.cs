﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameSchool.Models.dbLINQ;

namespace GameSchool.Models.ViewModels.TeacherViewModels
{
    public class EditTestViewModel
    {
        public TestModel Test { get; set; }
        public List<QuestionModel> Questions { get; set; }
        public List<AnswerModel> Answers { get; set; }
    }
}