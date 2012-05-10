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

        public IQueryable<AssignmentModel> GetAssignmentsForCourse(int CourseID)
        {
            var result = from x in m_assignmentDB.AssignmentModels
                         where x.CourseID == CourseID
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

        public IQueryable<AssignmentCompletion> GetFinishedAssignmentsForUser(string UserName, int CourseID)
        {
            /*var result = from x in m_assignmentDB.AssignmentCompletions
                         where x.*/
            return null;
        }

        public bool HasStudentFinishedAssignment(aspnet_User Student, AssignmentModel Assignment)
        {
            throw new NotImplementedException();
        }

        public void RegisterAssignmentCompletion(AssignmentCompletion TheCompletion)
        {
            m_assignmentDB.AssignmentCompletions.InsertOnSubmit(TheCompletion);
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