using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameSchool.Models.dbLINQ;
using System.ComponentModel.DataAnnotations;

namespace GameSchool.Models.ViewModels
{
    public class AssignmentViewModel
    {
        public AssignmentModel Assignment { get; set; }

        [Display( Name = "Skrá")]
        [Required (ErrorMessage = "Vantar skrá")]
        public HttpPostedFileBase File { get; set; }
    }
}