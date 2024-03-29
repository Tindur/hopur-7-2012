﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameSchool.Models.dbLINQ;
using System.ComponentModel.DataAnnotations;
using System.Collections.Specialized;

namespace GameSchool.Models.ViewModels.TeacherViewModels
{
    public class EditTestViewModel
    {
        public TestModel Test { get; set; }
        public List<QuestionModel> Questions { get; set; }
        public List<AnswerModel> Answers { get; set; }
        public QuestionModel newQuestion { get; set; }
        public List<AnswerModel> newAnswers { get; set; }
    }
}