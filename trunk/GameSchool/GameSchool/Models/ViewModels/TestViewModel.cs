using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using GameSchool.Models.dbLINQ;

namespace GameSchool.Models.ViewModels
{
    public class TestViewModel
    {
        public string testName { get; set; }
        public IQueryable<QuestionModel> m_Questions { get; set; }
        public List<List<AnswerModel>> m_Answers { get; set; }

        /*public TestViewModel(IQueryable<QuestionModel> TheQuestions, List<List<AnswerModel>> TheAnswers)
        {
            m_Questions = TheQuestions;
            m_Answers = TheAnswers;

        }*/
    }
}