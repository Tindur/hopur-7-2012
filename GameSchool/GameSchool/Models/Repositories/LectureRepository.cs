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

        public IQueryable<LectureModel> GetLecturesForCourse(int CourseID)
        {
            var result = from x in m_lectureDB.LectureModels
                         where x.LevelID == CourseID
                         orderby x.DateAdded descending
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

        public LectureModel GetLectureByID(int id)
        {
            var result = (from lecture in m_lectureDB.LectureModels
                          where lecture.ID == id
                          select lecture).SingleOrDefault();

            return result;
        }

        public void AddLecture(LectureModel TheLecture)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}