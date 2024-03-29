﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameSchool.Models.dbLINQ;


namespace GameSchool.Models.Interfaces
{
    public interface ILevelRepository
    {
        IQueryable<dbLINQ.LevelModel> GetAllLevelsForCourse(int courseID);
        void AddLevel(LevelModel level);
        bool HasStudentFinishedLevel(string userName, int levelID);
        int GetCurrentLevelForStudent(string userName, int courseID);  //Added by Björn
        void RegisterLevelCompletion(int idLevel, string studentName);
        LevelModel GetLevelByID(int levelID);
        int GetAmountOfCompletedLevels(string theUser, int CourseID);
      /*
        IQueryable<LectureModel> GetLecturesForLevel(int levelID);
   
             
        IQueryable<CommentModel> GetAllCommentsForLecture(int lectureID);
     

        IQueryable<AssignmentModel> GetAllAssignmentForLevel(int levelID);
        

        void AddLecture(LectureModel lecture);
       

        void AddCommentOnLecture(string comment);
       

        void AddAssignment(AssignmentModel assignment);

        bool HasStudentFinishedAssignment(string username);


        bool HasStudentFinishedLecture(string username);
  
        bool HasStudentFinishedLevel(string username);
  

        void RegisterAssignmentCompletion(int idAssignment);
    

        void RegisterLectureCompletion(int idLecture);*/
    }
}