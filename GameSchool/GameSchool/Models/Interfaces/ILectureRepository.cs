using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameSchool.Models.dbLINQ;

namespace GameSchool.Models.Interfaces
{
    interface ILectureRepository
    {
        IQueryable<LectureModel> GetLecturesForLevel(int LevelID);
        IQueryable<LectureModel> GetLecturesForCourse(int CourseID);
        IQueryable<LectureModel> GetFiveLatest(int LevelID);
        void AddLecture(LectureModel TheLecture);
        void Save();
    }
}
