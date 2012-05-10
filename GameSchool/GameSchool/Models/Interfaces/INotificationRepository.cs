using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameSchool.Models.dbLINQ;
using GameSchool.Models;
using GameSchool.Models.Repositories;

namespace GameSchool.Models.Interfaces
{
    interface INotificationRepository
    {
        IQueryable<NotificationModel> GetFiveLatest(int id);
        void AddNotification(NotificationModel TheNotification);
        void Save();
    }
}
