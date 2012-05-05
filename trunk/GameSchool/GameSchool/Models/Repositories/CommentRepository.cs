using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameSchool.Models.dbLINQ;
using GameSchool.Models.Repositories;

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
                         join cm in m_commentsDB.CommentModels on lc.LectureID equals cm.ID
                         where lc.LectureID == id
                         select cm;
            return result;

            /*var result = from cr in m_courseDB.CourseRegistrations
                         join cm in m_courseDB.CourseModels on cr.CourseID equals cm.ID
                         where cr.StudentUsername == studentUsername
                         select cm;*/
        }

		public void AddComment(CommentModel c)
		{
            c.CommentDate = DateTime.Now;
			m_commentsDB.CommentModels.InsertOnSubmit( c );
			m_commentsDB.SubmitChanges( );
		}
	}
}