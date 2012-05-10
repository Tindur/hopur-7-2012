using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameSchool.Models.Interfaces;
using GameSchool.Models.dbLINQ;

namespace GameSchool.Models.Repositories
{
    public class LectureRepository : ILectureRepository
    {
        LecturesDBDataContext m_lectureDB = new LecturesDBDataContext();

        public IQueryable<LectureModel> GetLecturesForLevel(int LevelID)
        {
            var result = from x in m_lectureDB.LectureModels
                         where x.LevelID == LevelID
                         orderby x.Name ascending
                         select x;
            return result;
        }

        public IQueryable<LectureModel> GetLecturesForCourse(int CourseID)
        {
            var result = from x in m_lectureDB.LectureModels
                         where x.CourseID == CourseID
                         orderby x.Name ascending
                         select x;
            return result;
        }
        public IQueryable<LectureModel> GetFiveLatest(int LevelID)
        {
            var result = (from x in m_lectureDB.LectureModels
                         where x.LevelID == LevelID
                         orderby x.DateAdded descending
                         select x).Take(5);
            return result;
        }
        public IQueryable<LectureModel> GetAllFiveLatest()
        {
            var result = (from x in m_lectureDB.LectureModels
                          orderby x.CourseID descending
                          select x).Take(5);
            return result;
        }

        public LectureModel GetLectureByID(int id)
        {
            var result = (from lecture in m_lectureDB.LectureModels
                          where lecture.ID == id
                          select lecture).SingleOrDefault();

            return result;
        }

        public int GetCourseIDByLectureID(int LecID)
        {
            var result = (from x in m_lectureDB.LectureModels
                          where x.ID == LecID
                          select x.CourseID).SingleOrDefault();
            return result.Value;
        }

        public void AddLecture(LectureModel TheLecture)
        {
            m_lectureDB.LectureModels.InsertOnSubmit(TheLecture);
        }

        public void RegisterLectureCompletion(LectureCompletion TheCompletion)
        {
            m_lectureDB.LectureCompletions.InsertOnSubmit(TheCompletion);
        }

        public IQueryable<int> GetFinishedLecturesInCourse(string UserName, int CourseID)
        {
            //Finn alla fyrirlestra í kúrsinum
            List<LectureModel> list = GetLecturesForCourse(CourseID).ToList();
            
            //Finn ID á fyrirlestrum sem nemandi hefur klárað
            List<int> finishedLecturesID = new List<int>();
            foreach (var lecture in list)
            {
                if(HasStudentFinishedLecture(lecture.ID , UserName))
                {
                    finishedLecturesID.Add(lecture.ID);
                }
            }

            //Finn LectureCompletion
            var result = (from x in m_lectureDB.LectureCompletions
                         where finishedLecturesID.Contains(x.LectureID) && x.UserName == UserName
                         select x.LectureID);
            return result;
        }

        public bool HasStudentFinishedLecture(int LecID, string UserName)
        {
            var result = (from x in m_lectureDB.LectureCompletions
                         where x.LectureID == LecID && x.UserName == UserName
                         select x).SingleOrDefault();
            if (result != null)
                return true;
            else
                return false;
        }

        public void Save()
        {
            m_lectureDB.SubmitChanges();
        }
    }
}