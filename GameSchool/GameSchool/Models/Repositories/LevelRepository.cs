using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameSchool.Models.Interfaces;
using GameSchool.Models.dbLINQ;
using System.Web.Mvc;


namespace GameSchool.Models.Repositories
{
    public class LevelRepository : ILevelRepository
    {
        LevelDBDataContext m_levelDB = new LevelDBDataContext();
        LecturesDBDataContext m_lectureDB = new LecturesDBDataContext();
        AssignmentDBDataContext m_assignmentDB = new AssignmentDBDataContext();
        CommentDBDataContext m_commentDB = new CommentDBDataContext();

        public IQueryable<dbLINQ.LevelModel> GetAllLevelsForCourse(int courseID)
        {
            var result = from x in m_levelDB.LevelModels
                         where x.CourseID == courseID
                         orderby x.NumberInCourse ascending
                         select x;
            return result;
        }

        public void AddLevel(LevelModel level)
        {
            m_levelDB.LevelModels.InsertOnSubmit(level);
            m_levelDB.SubmitChanges();
        }

        public bool HasStudentFinishedLevel(string userName)
        {
            var result = (from student in m_levelDB.LevelCompletions
                          where student.StudentName == userName
                          select student).SingleOrDefault();

            if (result != null)
                return true;
            else
                return false;

        }

        public int GetCurrentLevelForStudent(string userName, int courseID)  //Added by Björn
        {
            var result = (from x in m_levelDB.LevelAmountCompletions
                         where x.CourseID == courseID
                         select x.LevelsCompleted.Value).SingleOrDefault();

            var result2 = (from x in m_levelDB.LevelModels
                          where x.CourseID == courseID
                          select x.ID).Min();
            
            var result3 = result2 + result;

            return result3;
        }

        public void RegisterLevelCompletion(int idLevel, string studentName)
        {
            LevelCompletion complete = new LevelCompletion();
            complete.StudentName = studentName;
            complete.LevelID = idLevel;
            m_levelDB.LevelCompletions.InsertOnSubmit(complete);
        }

        public LevelModel GetLevelByID(int levelID)
        {
            var result = (from x in m_levelDB.LevelModels
                         where x.ID == levelID
                         select x).SingleOrDefault();
            return result;
        }

        public IQueryable<LectureModel> GetLecturesForLevel(int levelID)
        {
            var result = from x in m_lectureDB.LectureModels
                         where x.LevelID == levelID
                         orderby x.DateAdded descending
                         select x;
            return result;
            //throw new NotImplementedException();
        }

        public IQueryable<CommentModel> GetAllCommentsForLecture(int lectureID)
        {
            var result = from x in m_commentDB.CommentModels
                         where x.ID == lectureID
                         select x;

            return result;
            //throw new NotImplementedException();
        }

        public IQueryable<AssignmentModel> GetAllAssignmentForLevel(int levelID)
        {
            var result = from x in m_assignmentDB.AssignmentModels
                         where x.LevelID == levelID
                         select x;
            return result;
            //throw new NotImplementedException();
        }

        public void AddLecture(LectureModel lecture)
        {
            m_lectureDB.LectureModels.InsertOnSubmit(lecture);
            m_lectureDB.SubmitChanges();
            //throw new NotImplementedException();
        }

      /*  public void AddCommentOnLecture(string comment)
        {
            throw new NotImplementedException();
        }*/

        public void AddAssignment(AssignmentModel assignment)
        {
            m_assignmentDB.AssignmentModels.InsertOnSubmit(assignment);
            m_assignmentDB.SubmitChanges();
            //throw new NotImplementedException();
        }

        public int GetAmountOfCompletedLevels(string theUser, int courseID)
        {
            var result = (from x in m_levelDB.LevelAmountCompletions
                          where x.StudentName == theUser && x.CourseID == courseID
                          select x.LevelsCompleted.Value).SingleOrDefault();
            return result;
        }

       /* public bool HasStudentFinishedAssignment(string username)
        {
            throw new NotImplementedException();
        }

        public bool HasStudentFinishedLecture(string username)
        {
            throw new NotImplementedException();
        }*/
/*
       /* public bool HasStudentFinishedLevel(string username)
        {
            var result = from x in m_levelDB.LevelCompletions
                         where x.StudentName == username
                         select x;

            if (result != null)
                return true;
            else
                return false;
            //throw new NotImplementedException();
        }*/

        public void RegisterAssignmentCompletion(int idAssignment)
        {
            throw new NotImplementedException();
        }

        public void RegisterLectureCompletion(int idLecture)
        {
            throw new NotImplementedException();
        }
    }
}