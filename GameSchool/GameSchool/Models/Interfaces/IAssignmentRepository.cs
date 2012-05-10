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
        void RegisterAssignmentCompletion(AssignmentCompletion TheCompletion);
        bool HasStudentFinishedAssignment(aspnet_User Student, AssignmentModel Assignment);//ekki verið að nota
        void AddAssignmnet(AssignmentModel TheAssignment);//ekki verið að nota
        void Save();
        IQueryable<AssignmentCompletion> GetFinishedAssignmentsForUser(string UserName, int CourseID);
    }
}
