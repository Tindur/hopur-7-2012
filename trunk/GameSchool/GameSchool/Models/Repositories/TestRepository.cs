using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameSchool.Models.Interfaces;
using GameSchool.Models.dbLINQ;

namespace GameSchool.Models.Repositories
{
    public class TestRepository : ITestRepository
    {
        TestDBDataContext m_testDB = new TestDBDataContext();

        public IQueryable<TestModel> GetAllTestsForLevel(int TheLevelID)
        {
            var result = from x in m_testDB.TestModels
                         where x.LevelID == TheLevelID
                         select x;
            return result;
        }

        public IQueryable<TestModel> GetTestsForCourse(int CourseID)
        {
            var result = from x in m_testDB.TestModels
                         where x.CourseID == CourseID
                         select x;
            return result;


        }
        public IQueryable<QuestionModel> GetAllQuestionsForTest(int TheTestID)
        {
            var result = from x in m_testDB.QuestionModels
                         where x.TestID == TheTestID
                         select x;
            return result;
        }

        public IQueryable<AnswerModel> GetAllAnswersForQuestion(int TheQuestionID)
        {
            var result = from x in m_testDB.AnswerModels
                         where x.QuestionID == TheQuestionID
                         select x;
            return result;
        }

        public string getTestNameByID(int id)
        {
            string name = (from test in m_testDB.TestModels
                       where test.ID == id
                       select test.Name).SingleOrDefault();
            return name;
        }

        public TestModel GetTestByID(int testID)
        {
            var test = (from x in m_testDB.TestModels
                       where x.ID == testID
                       select x).SingleOrDefault();

            return test;
        }

        public void AddTest(TestModel test)
        {
            m_testDB.TestModels.InsertOnSubmit(test);
            m_testDB.SubmitChanges();
        }
    }
}