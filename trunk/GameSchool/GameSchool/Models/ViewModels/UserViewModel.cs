using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameSchool.Models.dbLINQ;

namespace GameSchool.Models.ViewModels
{
    public class UserViewModel
    {
        public aspnet_User m_User { get; set; }
        public aspnet_Membership m_Membership { get; set; }
        public IQueryable<aspnet_Role> m_Role { get; set; }
        public aspnet_UsersInRole m_TheUsersRole { get; set; }
        public IQueryable<int> m_XP { get; set; }
        public int m_CurrentXP { get; set; }
        public UserViewModel()
        {
        }

        public UserViewModel(aspnet_User TheUser, aspnet_Membership TheMembership, IQueryable<aspnet_Role> TheRoles, aspnet_UsersInRole TheUsersRole)
        {
            m_User = TheUser;
            m_Membership = TheMembership;
            m_Role = TheRoles;
            m_TheUsersRole = TheUsersRole;
        }
    }
}