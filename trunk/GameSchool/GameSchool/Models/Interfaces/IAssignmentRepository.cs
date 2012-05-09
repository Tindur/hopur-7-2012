using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameSchool.Models.dbLINQ;

namespace GameSchool.Models.Interfaces
{
    interface IAssignmentRepository
    {
        IQueryable<AssignmentModel> GetAllAssignmentsForLevel(int LevelID);
        IQueryable<AssignmentModel> GetAssignmentsForCourse(int CourseID);
        IQueryable<AssignmentModel> GetFiveLatest(int LevelID);
        AssignmentModel GetAssignmentById(int AssignmentID);
        bool HasStudentFinishedAssignment(aspnet_User Student, AssignmentModel Assignment);
        void RegisterAssignmentCompletion(int AssignmentID, string StudentID, double Grade);
        void AddAssignmnet(AssignmentModel TheAssignment);
        void Save();
    }
}
