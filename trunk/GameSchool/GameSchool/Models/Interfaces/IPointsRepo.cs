using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameSchool.Models.Interfaces
{
    interface IPointsRepo
    {
        void AddPointsToLecture(int LectureID);
        void AddPointsToAssignment(int AssignmentID);
        void AddPointsToTest(int TestID);
        void GetPointsForLecture(int LectureID);
        void GetPointsForAssignment(int AssignmentID);
        void GetPointsForTest(int TestID);
    }
}
