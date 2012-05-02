using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameSchool.Models
{
    public class TestRepository
    {
        TestDBDataContext m_testDB = new TestDBDataContext();
        public List<TestModel> GetAllTestsForLevel(int? id) 
        {
            throw new NotImplementedException();
        }
        public List<QuestionModel>GetAllQuestionsForTest() 
        {
             throw new NotImplementedException();
        }
        public List<AnswerModel>GetAllAnswersForQuestions()
        {
            throw new NotImplementedException();
        }
        public void AddTest(TestModel test)
        {
            throw new NotImplementedException(); 
        }
        public void AddQuestions(QuestionModel question)
        {
            throw new NotImplementedException();
        }
        public void AddAnswer(AnswerModel answer)
        {
            throw new NotImplementedException();
        }
        public bool HasStudentFinishedTest(int? id)
        {
            throw new NotImplementedException();
        }
        public bool RegisterTestCompletion(int? id)
        {
            throw new NotImplementedException();
        }
}