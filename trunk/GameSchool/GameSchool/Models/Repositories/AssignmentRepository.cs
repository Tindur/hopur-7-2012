using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameSchool.Models.Interfaces;
using GameSchool.Models.dbLINQ;

namespace GameSchool.Models.Repositories
{
    public class AssignmentRepository : IAssignmentRepository
    {
        AssignmentDBDataContext m_assignmentDB = new AssignmentDBDataContext();

        public IQueryable<AssignmentModel> GetAllAssignmentsForLevel(int LevelID)
        {
            var result = from x in m_assignmentDB.AssignmentModels
                         where x.LevelID == LevelID
                         select x;
            return result;
        }

        public IQueryable<AssignmentModel> GetFiveLatest(int LevelID)
        {
            var result = (from x in m_assignmentDB.AssignmentModels
                          where x.LevelID == LevelID
                          orderby x.DateAdded descending
                          select x).Take(5);
            return result;
        }
        public AssignmentModel GetAssignmentById(int AssignmentID)
        {
            var result = (from x in m_assignmentDB.AssignmentModels
                         where x.ID == AssignmentID
                         select x).SingleOrDefault();
            return result;
        }
        public bool HasStudentFinishedAssignment(aspnet_User Student, AssignmentModel Assignment)
        {
            throw new NotImplementedException();
        }

        public void RegisterAssignmentCompletion(int AssignmentID, string StudentID, double Grade)
        {
            throw new NotImplementedException();
        }
        public void AddAssignmnet(AssignmentModel TheAssignment)
        {
            m_assignmentDB.AssignmentModels.InsertOnSubmit(TheAssignment);
        }
        public void Save()
        {
            m_assignmentDB.SubmitChanges();
        }
    }
}