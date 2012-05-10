using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameSchool.Models.dbLINQ;

namespace GameSchool.Models.Interfaces
{
    interface ICourseXPRepository
    {
        CourseXP GetCourseXPByUserName(string UserName);
        CourseXP CreateNewXPForUserName(string UserName, int CourseID);
        void RegisterXPForCourse(CourseXP TheXP);
        void Save();
    }
}
