using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameSchool.Models.dbLINQ;

namespace GameSchool.Models.ViewModels
{
    public class TestCompletedViewModel
    {
        public TestModel Test { get; set; }
        public int correctAnswers { get; set; }
        public int numberOfQuestions { get; set; }
        public int Score { get; set; }
    }
}