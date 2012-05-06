using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameSchool.Models.dbLINQ;

namespace GameSchool.Models.Interfaces
{
    interface ICommentRepository
    {
        IEnumerable<CommentModel> GetComments();
        IQueryable<CommentModel> GetCommentForLecture(int lectureID);
        void AddComment(CommentModel c);
        void ConnectCommentToLecture(CommentModel com, int LectureID);
    }
}
