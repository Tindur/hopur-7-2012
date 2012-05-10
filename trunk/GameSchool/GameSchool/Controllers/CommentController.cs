using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameSchool.Models;
using GameSchool.Models.Repositories;
using GameSchool.Models.dbLINQ;
using GameSchool.Models.ViewModels;
using System.Web.Routing;
using System.Web.Security;
using System.IO;

namespace GameSchool.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        #region Repos
        CourseRepository m_CourseRepo = new CourseRepository();
        LevelRepository m_lvlRepo = new LevelRepository();
        LectureRepository m_LectureRepo = new LectureRepository();
        TestRepository m_TestRepo = new TestRepository();
        CommentRepository m_CommentRepo = new CommentRepository();
        AssignmentRepository m_AssignmentRepo = new AssignmentRepository();
        UsersRepository m_UserRepo = new UsersRepository();
        NotificationRepository m_NotificationRepo = new NotificationRepository();
        CourseXPRepository m_CourseXPRepo = new CourseXPRepository();
        #endregion

        #region Comments
        [HttpGet]
        public ActionResult GetCommentsByID(int? id)
        {
            if (id.HasValue)
            {
                var model = m_CommentRepo.GetCommentForLecture(id.Value);
                var newResult = (from k in model
                                 select new
                                 {
                                     ShortCommentDate = k.CommentDate.ToShortTimeString(),
                                     LongCommentDate = k.CommentDate.ToShortDateString(),
                                     ID = k.ID,
                                     CommentText = k.CommentText,
                                     Name = m_UserRepo.GetUserByName(k.UserName).Name,
                                     UserImage = m_UserRepo.GetImageForName(m_UserRepo.GetUserByName(k.UserName).Name)
                                 });
                return Json(newResult, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //TODO?
                return RedirectToAction("StudentIndex");
            }
        }

        [HttpPost]
        public ActionResult CreateCommentForLecture(string CommentText, int id)
        {
            CommentModel model = new CommentModel();
            model.CommentText = CommentText;
            model.UserName = m_UserRepo.GetUserByName(User.Identity.Name).UserName/*.Name*/;

            m_CommentRepo.AddComment(model);
            m_CommentRepo.ConnectCommentToLecture(model, id);

            var result = m_CommentRepo.GetCommentForLecture(id);
            var newResult = (from k in result
                             select new
                             {
                                 LongCommentDate = k.CommentDate.ToShortDateString(),
                                 ShortCommentDate = k.CommentDate.ToShortTimeString(),
                                 ID = k.ID,
                                 CommentText = k.CommentText,
                                 UserImage = m_UserRepo.GetImageForName(m_UserRepo.GetUserByName(k.UserName).Name),
                                 Name = m_UserRepo.GetUserByName(k.UserName).Name
                             });
            return Json(newResult);
        }
        #endregion

        #region Likes
        [HttpGet]
        public ActionResult GetLikesForLecture(int LectureID)
        {
            var model = m_CommentRepo.GetLikesForLecture(LectureID);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreateLikeForComment(int CommentID)
        {
            string theLiker = m_UserRepo.GetUserByName(User.Identity.Name).Name;
            string theChecker = m_CommentRepo.GetNameForComment(CommentID);
            //Kanna hvort notandinn hafi like'að commentið áður
            var check = m_CommentRepo.GetLikesForComment(CommentID);
            foreach (var item in check)
            {
                if (item.UserName == theLiker)
                    return Json(null);
            }
            //Púsla like'inu saman
            LikeModel newLike = new LikeModel();
            newLike.UserName = theLiker;
            newLike.CommentID = CommentID;
            //Bæti like'inu í gagnagrunninn
            m_CommentRepo.AddLike(newLike);
            //TODO Redda því að checka á hvort commentari sé að likea commentið sitt.
            if (theLiker != theChecker)
            {
                var shit = m_UserRepo.GetUserByName(theChecker);
                shit.XP += 10;
            }

            m_UserRepo.Save();
            //Sæki nýjasta like'ið fyrir Json
            var latest = (from x in m_CommentRepo.GetLikesForComment(CommentID)
                          select x).ToList().Last();
            return Json(latest);
        }
        #endregion
    }
}