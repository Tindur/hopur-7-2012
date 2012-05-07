using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameSchool.Models.dbLINQ;

namespace GameSchool.Models.Interfaces
{
    interface ILikeRepository
    {
        IQueryable<LikeModel> GetLikesForLecture(int lectureID);
        IQueryable<LikeModel> GetLikesForComment(int commentID);
        void AddLike(LikeModel c);
    }
}
