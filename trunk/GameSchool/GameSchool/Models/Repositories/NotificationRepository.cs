using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameSchool.Models.Interfaces;
using GameSchool.Models.dbLINQ;

namespace GameSchool.Models.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        NewsFeedDBDataContext m_NewsFeedDB = new NewsFeedDBDataContext();

        public IQueryable<NotificationModel> GetFiveLatest(int id)
        {
            var result = (from x in m_NewsFeedDB.NotificationModels
                         where x.CourseID == id
                         select x).Take(5);
            return result;
        }
        public void AddNotification(NotificationModel TheNotification)
        {
            m_NewsFeedDB.NotificationModels.InsertOnSubmit(TheNotification);
        }

        public string GetNameOfTeacher(int id)
        {
            var result = (from x in m_NewsFeedDB.NotificationModels
                         where x.CourseID == id
                         select x.UserName).SingleOrDefault();
            return result;
        }

        public void Save()
        {
            m_NewsFeedDB.SubmitChanges();
        }
    }
}