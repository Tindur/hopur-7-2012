using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameSchool.Models.dbLINQ;

namespace GameSchool.Models.ViewModels
{
    public class LectureViewModel
    {
        public ImageModel UsersImage { get; set; }
        public IEnumerable<LectureModel> lecMod { get; set; }
    }
}