using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameSchool.Models.dbLINQ;
using System.ComponentModel.DataAnnotations;

namespace GameSchool.Models.ViewModels
{
    public class CourseEditViewModel
    {
        [Display(Name="Áfangi")]
        public CourseModel Course { get; set; }

        [Display(Name="Kennarar")]
        public IQueryable<aspnet_User> Teachers { get; set; }

        [Display(Name="Nemendur")]
        public IQueryable<aspnet_User> Students { get; set; }

        [Display(Name="Nemendur í áfanga")]
        public IQueryable<string> StudentNameInCourse{ get; set; }

        [Display(Name="Kennarar í áfanga")]
        public IQueryable<string> TeachersNameInCourse { get; set; }
    }
}