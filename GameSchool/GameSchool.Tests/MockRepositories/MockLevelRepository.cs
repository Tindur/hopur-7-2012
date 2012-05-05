using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameSchool.Models.Interfaces;
using GameSchool.Models.dbLINQ;
using GameSchool.Models;
using System.Web;


namespace GameSchool.Tests.MockRepositories
{
   /* public class MockLevelRepository: ILevelRepository
    {
        public List<LevelModel> testLevelRep;
        public List<LevelCompletion> LevelCompletions;

        public MockLevelRepository(int count)
        {
            for (int i = 0; i < count; i++)
            {
                testLevelRep.Add(new LevelModel
                {
                    ID = 1 + i,
                    CourseID = 1 + i,
                    NumberInCourse = 1 + i,
                    Name = "bubbi" + (1 + i)
                });
            }
        }

        public IQueryable<LevelModel> GetAllLevelsForCourse(int courseID)
        {
            var result = (from cours in testLevelRep
                          where cours.CourseID == courseID
                          orderby cours.NumberInCourse ascending
                          select cours);
            return result.AsQueryable();

                          //throw new NotImplementedException();
        }

        public void AddLevel(LevelModel level)
        {
            level.ID = testLevelRep.Count + 1;
            testLevelRep.Add(level);
            //throw new NotImplementedException();
        }

        public bool HasStudentFinishedLevel(string userName)
        {   
            var result = (from student in LevelCompletions
                          where student.StudentName == userName
                          select student).SingleOrDefault();
            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }


            //throw new NotImplementedException();
        }

        public IQueryable<int> GetFinishedLevelsForStudent(string userName)
        {
            var result = ( from levelID in LevelCompletions
                           where levelID.StudentName == userName
                           select levelID.LevelID);
            return result.AsQueryable();
            //throw new NotImplementedException();
        }

        public void RegisterLevelCompletion(int idLevel, string studentName)
        {
            LevelCompletion complete = new LevelCompletion();
            complete.StudentName = studentName;
            complete.LevelID = idLevel;
            LevelCompletions.Add(complete);
            //throw new NotImplementedException();
        }

    }*/
}
