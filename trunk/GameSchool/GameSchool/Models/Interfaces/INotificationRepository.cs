using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameSchool.Models.dbLINQ;

namespace GameSchool.Models.Interfaces
{
    interface INotificationRepository
    {
        IQueryable<NotificationModel> GetFiveLatest(int id);
        void AddNotification(NotificationModel TheNotification);
        void Save();
    }
}
