using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace GameSchool.Models.ViewModels
{
    public class AdminViewModel
    {
        public MembershipUserCollection Users { get; set; }
        public string[] Roles { get; set; }
    }
}