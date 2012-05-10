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

        public void AddQuestion(QuestionModel newQuestion)
        {
            m_testDB.QuestionModels.InsertOnSubmit(newQuestion);
            m_testDB.SubmitChanges();
        }

        public int FindTestID(QuestionModel Question)
        {
            var result = (from x in m_testDB.QuestionModels
                         where Question.ID == x.ID
                         select x.ID).SingleOrDefault();
            return result;
        }

        public void AddAnswers(IEnumerable<AnswerModel> list)
        {
            foreach (var answer in list)
            {
                m_testDB.AnswerModels.InsertOnSubmit(answer);
                m_testDB.SubmitChanges();
            }
        }

        public AnswerModel GetAnswerByID(int p)
        {
            var result = (from x in m_testDB.AnswerModels
                          where x.ID == p
                          select x).SingleOrDefault();
            return result;
        }

        public QuestionModel GetQuestionByID(int p)
        {
            var result = (from question in m_testDB.QuestionModels
                          where question.ID == p
                          select question).SingleOrDefault();
            return result;
        }

        public void RegisterTestCompletion(TestCompletion testCompletion)
        {
            m_testDB.TestCompletions.InsertOnSubmit(testCompletion);
            m_testDB.SubmitChanges();
        }

        public bool UserHasFinishedTest(string studentName, int testID)
        {
            var result = (from x in m_testDB.TestCompletions
                          where x.StudentName == studentName && x.TestID == testID
                          select x).SingleOrDefault();
            if (result != null)
                return true;
            else
                return false;
        }

        public IQueryable<int> GetFinishedTestsInCourse(string StudentName, int CourseID)
        {
            //Finna öll próf í kúrsinum
            List<TestModel> list = GetTestsForCourse(CourseID).ToList();

            //Finnum ID á prófum sem nemandi hefur klárað
            List<int> finishedTestsID = new List<int>();
            foreach (var test in list)
            {
                if (UserHasFinishedTest(StudentName, test.ID))
                {
                    finishedTestsID.Add(test.ID);
                }
            }

            //Finnum TestCompletion
            var result = (from x in m_testDB.TestCompletions
                          where finishedTestsID.Contains(x.TestID) && x.StudentName == StudentName
                          select x.TestID);

            return result;
        }
    }
}