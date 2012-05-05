using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameSchool.Models.dbLINQ;

namespace GameSchool.Models.Repositories
{
    interface ILectureRepository
    {
        IQueryable<LectureModel> GetLecturesForCourse(int CourseID);
        void AddLecture(LectureModel TheLecture);
        void Save();
    }
}
