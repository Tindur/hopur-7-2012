using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameSchool.Models.dbLINQ;


namespace GameSchool.Models.ViewModels
{
    public class NavigationViewModel
    {
        public UserViewModel TheUser { get; set; }
        public ImageModel TheImage { get; set; }
        public IEnumerable<ImageModel> TeachersImages { get; set; }
        public  IQueryable<CourseModel> TheCourses {  get; set; }
        public int? TheLevel { get; set; }
        public int? TheXP { get; set; }
    }
}