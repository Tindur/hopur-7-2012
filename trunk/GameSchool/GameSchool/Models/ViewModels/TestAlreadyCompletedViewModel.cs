using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameSchool.Models.dbLINQ;

namespace GameSchool.Models.ViewModels
{
    public class TestAlreadyCompletedViewModel
    {
        public TestModel Test { get; set; }
        public int StudentScore { get; set; }
    }
}