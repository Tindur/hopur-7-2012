﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameSchool.Models.dbLINQ;

namespace GameSchool.Models.Interfaces
{
    interface ITestRepository
    {
        IQueryable<TestModel> GetAllTestsForLevel(int TheLevelID);
        IQueryable<TestModel> GetTestsForCourse(int CourseID);
        IQueryable<QuestionModel> GetAllQuestionsForTest(int TheTestID);
        IQueryable<AnswerModel> GetAllAnswersForQuestion(int TheQuestionID);
        IQueryable<TestModel> GetFiveLatest(int CourseID);
        void AddTest(TestModel test);
        string getTestNameByID(int id);
        TestModel GetTestByID(int testID);

    }
}
