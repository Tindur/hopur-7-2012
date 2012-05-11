using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameSchool.Models.dbLINQ;

namespace GameSchool.Models.ViewModels
{
    public class AssignmentViewModel
    {
        public AssignmentModel Assignment { get; set; }
        public HttpPostedFileBase File { get; set; }
    }
}