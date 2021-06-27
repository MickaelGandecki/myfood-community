using Kendo.Mvc.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using myfoodapp.Hub.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace myfoodapp.Hub.Controllers
{
    [Authorize]
    public class MyInfoController : Controller
    {        
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
             
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

        [Authorize]
        public ActionResult Update()
        {
            var currentUser = this.User.Identity.GetUserName();
            var applicationUser = UserManager.FindByName(currentUser);

            var db = new ApplicationDbContext();

            var currentProductOwner = db.ProductionUnitOwners.Include(p => p.language).FirstOrDefault(p => p.user.UserName == currentUser);
			var currentLanguageId = 1;

			if (currentProductOwner.language != null)
              currentLanguageId = currentProductOwner.language.Id;
            return View(new UserViewModel()
            {
                Login = applicationUser.UserName,
                NotificationEmail = currentProductOwner.contactMail,
                IsMailNotificationActivated = currentProductOwner.isMailNotificationActivated,
                Language = currentLanguageId,
                HasFahrenheitSetting = currentProductOwner.hasFahrenheitSetting
            });
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(UserViewModel model, string returnUrl)
        {
            var db = new ApplicationDbContext();
            var currentUser = this.User.Identity.GetUserName();

            var currentProductionUnitOwner = db.ProductionUnitOwners.Include(p => p.language)
                                               .Include(p => p.user).FirstOrDefault(p => p.user.UserName == currentUser);

            currentProductionUnitOwner.language = db.Languages.FirstOrDefault(l => l.Id == model.Language);
            currentProductionUnitOwner.isMailNotificationActivated = model.IsMailNotificationActivated;
            currentProductionUnitOwner.contactMail = model.NotificationEmail;
            currentProductionUnitOwner.hasFahrenheitSetting = model.HasFahrenheitSetting;

            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
            }
            
            if (ModelState.IsValid)
            {
                var applicationUser = UserManager.FindByName(currentUser);

				if (model.NotificationEmail != applicationUser.Email)
				{
					applicationUser.Email = model.NotificationEmail;
					applicationUser.UserName = model.Login;
					var resultMailChanged = await UserManager.UpdateAsync(applicationUser);
					if (!resultMailChanged.Succeeded)
					{
						AddErrors(resultMailChanged);
						return RedirectToAction("Update", "MyInfo", new { error = resultMailChanged.Errors.FirstOrDefault() });
					}
				}

				if (model.OldPassword != null && model.NewPassword != null)
                {
                    var resultPasswordChanged = await UserManager.ChangePasswordAsync(applicationUser.Id, model.OldPassword, model.NewPassword);
					if (resultPasswordChanged.Succeeded)
					{
						return RedirectToAction("Index", "Home");
					}
					else
					{
						AddErrors(resultPasswordChanged);
						return RedirectToAction("Update", "MyInfo", new { error = resultPasswordChanged.Errors.FirstOrDefault() });
					}

                }
            }

			return RedirectToAction("Update");

		}

        [Authorize]
        public ActionResult AddPushNotification(string id)
        {
            var currentUser = this.User.Identity.GetUserName();

            var db = new ApplicationDbContext();

            var currentProductionUnitOwner = db.ProductionUnitOwners
                                                           .Include(p => p.user)
                                                           .Include(p => p.language)
														   .FirstOrDefault(p => p.user.UserName == currentUser);
            if (currentProductionUnitOwner != null)
            {
                currentProductionUnitOwner.notificationPushKey = id;
                db.SaveChanges();

                return Json(currentProductionUnitOwner, JsonRequestBehavior.AllowGet);
            }

            return null;
        }

        [Authorize]
        public ActionResult Languages_Read([DataSourceRequest] DataSourceRequest request)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var rslt = db.Languages;

            return Json(rslt, JsonRequestBehavior.AllowGet);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        [Authorize]
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}