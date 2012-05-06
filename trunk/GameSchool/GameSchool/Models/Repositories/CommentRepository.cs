using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameSchool.Models.dbLINQ;
using GameSchool.Models.Interfaces;

namespace GameSchool.Models.Repositories
{
	public class CommentRepository : ICommentRepository
	{
        CommentDBDataContext m_commentsDB = new CommentDBDataContext();
       

		public IEnumerable<CommentModel> GetComments( )
		{
			var result = from c in m_commentsDB.CommentModels
						 orderby c.CommentDate ascending
						 select c;

			return result;
		}

        public IQueryable<CommentModel> GetCommentForLecture(int id)
        {
            var result = from lc in m_commentsDB.LectureComments
                         join cm in m_commentsDB.CommentModels on lc.CommentID equals cm.ID
                         where lc.LectureID == id
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