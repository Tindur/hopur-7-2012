using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameSchool.Models.Interfaces;
using GameSchool.Models.dbLINQ;


namespace GameSchool.Models.Repositories
{
    public class LevelRepository : ILevelRepository
    {
        LevelDBDataContext m_levelDB = new LevelDBDataContext();

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

        public bool HasStudentFinishedLevel(int userID)
        {
            var result = (from student in m_levelDB.LevelCompletions
                          where student.StudentID.ToString() == userID.ToString()
                          select student).SingleOrDefault();

            if (result != null)
                return true;
            else
                return false;

        }

        public IQueryable<int> GetFinishedLevelsForStudent(int userName)  //Added by Björn
        {
            //TODO breyta LevelCompletion þannig hún hafi StudentUsername í stað StudentID
            /*var result = from levelID in m_levelDB.LevelCompletions
                         where levelID.StudentID.ToString() == userName
                         select levelID.LevelID;

            return result.ToList();*/
            return null;
        }

        public void RegisterLevelCompletion(int idLevel, Guid studentID)
        {
            LevelCompletion complete = new LevelCompletion();
            complete.StudentID = studentID;
            complete.LevelID = idLevel;
            m_levelDB.LevelCompletions.InsertOnSubmit(complete);
        }

       /* public IQueryable<LectureModel> GetLecturesForLevel(int levelID)
        {
            throw new NotImplementedException();
        }

        public IQueryable<CommentModel> GetAllCommentsForLecture(int lectureID)
        {
            throw new NotImplementedException();
        }

        public IQueryable<AssignmentModel> GetAllAssignmentForLevel(int levelID)
        {
            throw new NotImplementedException();
        }

        public void AddLecture(LectureModel lecture)
        {
            throw new NotImplementedException();
        }

        public void AddCommentOnLecture(string comment)
        {
            throw new NotImplementedException();
        }

        public void AddAssignment(AssignmentModel assignment)
        {
            throw new NotImplementedException();
        }

        public bool HasStudentFinishedAssignment(string username)
        {
            throw new NotImplementedException();
        }

        public bool HasStudentFinishedLecture(string username)
        {
            throw new NotImplementedException();
        }

        public bool HasStudentFinishedLevel(string username)
        {
            throw new NotImplementedException();
        }

        public void RegisterAssignmentCompletion(int idAssignment)
        {
            throw new NotImplementedException();
        }

        public void RegisterLectureCompletion(int idLecture)
        {
            throw new NotImplementedException();
        }

        */
    }
}