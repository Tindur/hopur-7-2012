using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameSchool.Models.dbLINQ;
using GameSchool.Models.Interfaces;
using GameSchool.Models;

namespace GameSchool.Models.Repositories
{
	public class CommentRepository : ICommentRepository
	{
        CommentDBDataContext m_commentsDB = new CommentDBDataContext();

        public IQueryable<LikeModel> GetLikesForLecture(int lectureID)
        {
            var result = from comment in m_commentsDB.LectureComments
                         join like in m_commentsDB.LikeModels on comment.CommentID equals like.CommentID
                         where comment.LectureID == lectureID
                         select like;
            return result;
        }

        public void AddLike(LikeModel c)
        {
            m_commentsDB.LikeModels.InsertOnSubmit(c);
            m_commentsDB.SubmitChanges();
        }

        public IQueryable<LikeModel> GetLikesForComment(int commentID)
        {
            var result = from x in m_commentsDB.LikeModels
                         where x.CommentID == commentID
                         select x;
            return result;
        }

		public IEnumerable<CommentModel> GetComments( )
		{
			var result = from c in m_commentsDB.CommentModels
						 orderby c.CommentDate ascending
						 select c;

			return result;
		}

        public IQueryable<CommentModel> GetMoreCommentForLecture(int id)
        {
            var result = (from lc in m_commentsDB.LectureComments
                         join cm in m_commentsDB.CommentModels on lc.CommentID equals cm.ID
                         where lc.LectureID == id
                         orderby cm.CommentDate descending
                         select cm).Skip(3);
            return result;
        }
        /// <summary>
        /// Fyrir lecture_partial View svo listinn yfir öll kommentin verður ekki sjúklega langur nema
        /// notandinn vilji það
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQueryable<CommentModel> GetCommentForLecture(int id)
        {
            var result = from lc in m_commentsDB.LectureComments
                         join cm in m_commentsDB.CommentModels on lc.CommentID equals cm.ID
                         where lc.LectureID == id
                         orderby cm.CommentDate descending
                         select cm;
            return result;
        }

		public void AddComment(CommentModel c)
		{
            c.CommentDate = DateTime.Now;
			m_commentsDB.CommentModels.InsertOnSubmit( c );
			m_commentsDB.SubmitChanges( );
		}

        public void ConnectCommentToLecture(CommentModel com, int LecID)
        {
            LectureComment lecCom = new LectureComment
            {
                CommentID = com.ID,
                LectureID = LecID
            };

            m_commentsDB.LectureComments.InsertOnSubmit(lecCom);
            m_commentsDB.SubmitChanges();
        }
    }
}