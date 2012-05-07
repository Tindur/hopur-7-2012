using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameSchool.Models.Interfaces;
using GameSchool.Models.dbLINQ;

namespace GameSchool.Models.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        LikeDBDataContext m_db = new LikeDBDataContext();
        CommentDBDataContext com = new CommentDBDataContext();
        public IQueryable<LikeModel> GetLikesForLecture(int lectureID)
        {
            var result = from x in m_db.LikeModels
                         join z in com.LectureComments on x.CommentID equals z.CommentID
                         where z.LectureID == lectureID
                         select x;
            return result;
        }

        public void AddLike(LikeModel c)
        {
            m_db.LikeModels.InsertOnSubmit(c);
            m_db.SubmitChanges();
        }

        public IQueryable<LikeModel> GetLikesForComment(int commentID)
        {
            var result = from x in m_db.LikeModels
                         where x.CommentID == commentID
                         select x;
            return result;
        }

    }
}