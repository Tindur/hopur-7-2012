using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameSchool.Models.Interfaces;
using GameSchool.Models.dbLINQ;

namespace GameSchool.Models.Repositories
{
    public class CourseXPRepository : ICourseXPRepository
    {
        XPDBDataContext m_XPDB = new XPDBDataContext();

        public CourseXP GetCourseXPByUserName(string UserName)
        {
            var result = (from x in m_XPDB.CourseXPs
                          where x.UserName == UserName
                          select x).SingleOrDefault();
            return result;
        }

        public CourseXP CreateNewXPForUserName(string UserName, int CourseID)
        {
            CourseXP NewXP = new CourseXP();
            NewXP.CourseID = CourseID;
            NewXP.UserName = UserName;
            NewXP.XP = 0;

            return NewXP;
        }

        public void RegisterXPForCourse(CourseXP TheXP)
        {
            m_XPDB.CourseXPs.InsertOnSubmit(TheXP);
        }

        public void Save()
        {
            m_XPDB.SubmitChanges();
        }
    }
}