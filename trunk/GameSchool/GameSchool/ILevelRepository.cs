using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameSchool.Models.dbLINQ;

namespace GameSchool.Models.Interfaces
{
    /*interface ILevelRepository
    {
       // #region Getters
        IQueryable<LevelModel> GetAllLevelsForCourse(int courseID);
        IQueryable<int> GetFinishedLevelsForStudent(int userID);  //Added by Björn
        /*IQueryable<LectureModel> GetLecturesForLevel(int levelID);
        IQueryable<CommentModel> GetAllCommentsForLecture(int lectureID);
        IQueryable<AssignmentModel> GetAllAssignmentForLevel(int levelID);
        #endregion

        #region Adders*/
        void AddLevel(LevelModel level);
        /*void AddLecture(LectureModel lecture);
        void AddCommentOnLecture(string comment);
        void AddAssignment(AssignmentModel assignment);
        #endregion

        #region Boolers
        bool HasStudentFinishedAssignment(string username);
        bool HasStudentFinishedLecture(string username);*/
        bool HasStudentFinishedLevel(int userID);
        /*bool HasStudentFinishedLevel(string user);
        #endregion

        #region Registers
        void RegisterAssignmentCompletion(int idAssignment);
       void RegisterLectureCompletion(int idLecture);*/
        void RegisterLevelCompletion(int idLevel, Guid studentID);
        //#endregion*/
    }*/
}
