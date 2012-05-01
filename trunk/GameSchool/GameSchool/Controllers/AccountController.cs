using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using GameSchool.Models;

namespace GameSchool.Controllers
{
    public class AccountController : Controller
    {

        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
            return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Notendanafn er nú þegar til. Vinsamlegast veldu nýtt nafn.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "Notendanafn með þetta netfang er nú þegar til. Vinsamlegast veldu annað netfang.";

                case MembershipCreateStatus.InvalidPassword:
                    return "Lykilorðið sem þú valdir er ógilt. Vinsamlegast veldu gilt lykilorð.";

                case MembershipCreateStatus.InvalidEmail:
                    return "Netfangið sem þú valdir er ekki gilt. Vinsamlegastathugið hvort rétt netfang sé skráð.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "Svar ekki rétt. Vinsamlegast reynið aftur.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "Spurning ekki rétt. Athugið gildi og reynið aftur.";

                case MembershipCreateStatus.InvalidUserName:
                    return "Notendanafn sem var gefið upp er ógilt. Vinsamlegast athugið hvort rétt nafn var gefið upp og reynið aftur.";

                case MembershipCreateStatus.ProviderError:
                    return "Staðfestingarvilla kom upp. Athugið að allar upplýsingar séu rétt upp gefnar. Ef þetta gerist aftur hafið samband við umsjónarmann kerfisins.";

                case MembershipCreateStatus.UserRejected:
                    return "Upp kom villa við nýskráningu. Athugið að allar upplýsingar séu rétt upp gefnar. Ef þetta gerist aftur hafið samband við umsjónarmann kerfisins.";

                default:
                    return "Óþekkt villa kom upp. Athugið að allar upplýsingar séu rétt upp gefnar. Ef þetta gerist aftur hafið samband við umsjónarmann kerfisins.";
            }
        }
        #endregion
    }
}
