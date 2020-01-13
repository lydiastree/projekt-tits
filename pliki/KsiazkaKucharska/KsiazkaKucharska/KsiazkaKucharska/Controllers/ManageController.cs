using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using KsiazkaKucharska.Models;
using KsiazkaKucharska.context;
using System.Collections.Generic;
using System.Net;
using System.Data.Entity;

namespace KsiazkaKucharska.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private KsiazkaContext db = new KsiazkaContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        

        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Twoje hasło zostało zmienione."
                : message == ManageMessageId.SetPasswordSuccess ? "Akcja zakończona sukcesem."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Akcja zakończona sukcesem."
                : message == ManageMessageId.Error ? "Coś poszło nie tak."
                : "";

            var userId = User.Identity.GetUserId();
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
            };
            return View(model);
        }

        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        public ActionResult SetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            var dania = db.Dania.ToList();
            var przepisy = db.Przepisy.ToList();
            var zupy = db.Zupy.ToList();

            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "Usunięcie przepisu powiodło się."
                : message == ManageMessageId.Error ? "Coś poszło nie tak."
                : "";
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var userName = User.Identity.GetUserName().ToString();
            var mojed = dania.Where(x => x.Ktododal == userName).ToList();
            var mojep = przepisy.Where(x => x.Ktododal == userName).ToList();
            var mojez = zupy.Where(x => x.Ktododal == userName).ToList();
            var ile = mojed.Count() + mojez.Count() + mojep.Count();
            return View(new ManageLoginsViewModel
            {
                Mojed = mojed,
                Mojep = mojep,
                Mojez = mojez,
            }) ;
        }

        public ActionResult Deleted(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Danieglowne danieglowne = db.Dania.Find(id);
            if (danieglowne == null)
            {
                return HttpNotFound();
            }
            return View(danieglowne);
        }
        [HttpPost]
        public ActionResult Deleted(int id, FormCollection collection)
        {
            Danieglowne danieglowne = db.Dania.Find(id);
            db.Dania.Remove(danieglowne);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Deletez(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zupa zupa = db.Zupy.Find(id);
            if (zupa == null)
            {
                return HttpNotFound();
            }
            return View(zupa);
        }

        [HttpPost]
        public ActionResult Deletez(int id, FormCollection collection)
        {
            Zupa zupa = db.Zupy.Find(id);
            db.Zupy.Remove(zupa);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        public ActionResult Deletep(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Przepis przepis = db.Przepisy.Find(id);
            if (przepis == null)
            {
                return HttpNotFound();
            }
            return View(przepis);
        }

        [HttpPost]
        public ActionResult Deletep(int id, FormCollection collection)
        {
            Przepis przepis = db.Przepisy.Find(id);
            db.Przepisy.Remove(przepis);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Editd(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Danieglowne danieglowne = db.Dania.Find(id);
            if (danieglowne == null)
            {
                return HttpNotFound();
            }
            return View(danieglowne);
        }
        [HttpPost]
        public ActionResult Editd(int id, Danieglowne danie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(danie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(danie);
        }



        public ActionResult Editp(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Przepis przepis = db.Przepisy.Find(id);
            if (przepis == null)
            {
                return HttpNotFound();
            }
            return View(przepis);
        }
        [HttpPost]
        public ActionResult Editp(int id, Przepis przepis)
        {
            if (ModelState.IsValid)
            {
                db.Entry(przepis).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(przepis);
        }



        public ActionResult Editz(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zupa zupa = db.Zupy.Find(id);
            if (zupa == null)
            {
                return HttpNotFound();
            }
            return View(zupa);
        }
        [HttpPost]
        public ActionResult Editz(int id, Zupa zupa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zupa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(zupa);
        }


        private void List<T>()
        {
            throw new NotImplementedException();
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
        }

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

#region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }


        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

#endregion
    }
}