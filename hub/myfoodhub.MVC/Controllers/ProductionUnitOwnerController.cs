using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using myfoodapp.Hub.Models;
using myfoodapp.Hub.Services;
using System;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace myfoodapp.Hub.Controllers
{
    public class ProductionUnitOwnerController : Controller
    {
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            base.Initialize(requestContext);
        }

        private ApplicationUserManager _userManager;
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
        public ActionResult Index()
        {
            var currentUser = this.User.Identity.GetUserName();
            var db = new ApplicationDbContext();

            var userId = UserManager.FindByName(currentUser).Id;
            var isAdmin = this.UserManager.IsInRole(userId, "Admin");

            if (isAdmin)
            {
                PopulateLanguage();
                PopulatePreferedMoment();
                PopulateUser();

                return View();
            }
            else
            {
                var currentProductionUnits = db.ProductionUnits.Include(p => p.owner.user)
                                               .Where(p => p.owner.user.UserName == currentUser).ToList();
                if (currentProductionUnits != null)
                {
                    return RedirectToAction("Details", "ProductionUnits", new { Id = currentProductionUnits.FirstOrDefault().Id });
                }
                else
                    return View("Home");
            }
        }

        private void PopulateLanguage()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var languages = db.Languages
                       .Select(m => new LanguageViewModel
                       {
                           Id = m.Id,
                           name = m.name
                       })
                       .OrderBy(e => e.name);

            ViewData["Languages"] = languages;
        }

        private void PopulatePreferedMoment()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var preferedMoments = db.PreferedMoments
                       .Select(m => new PreferedMomentViewModel
                       {
                           Id = m.Id,
                           name = m.name
                       })
                       .OrderBy(e => e.name);

            ViewData["PreferedMoments"] = preferedMoments;
        }

        private void PopulateUser()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var users = db.Users
                       .Select(m => new ApplicationUserViewModel
                       {
                           Id = m.Id,
                           UserName = m.UserName
                       })
                       .OrderBy(e => e.UserName);

            ViewData["Users"] = users;
        }

        public ActionResult Editing_Read([DataSourceRequest] DataSourceRequest request)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ProductionUnitOwnerService ownerService = new ProductionUnitOwnerService(db);

            var rslt = ownerService.GetAll().OrderByDescending(ev => ev.pioneerCitizenName);

            return Json(rslt.ToDataSourceResult(request));
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Editing_Create([DataSourceRequest] DataSourceRequest request, ProductionUnitOwnerViewModel currentOwner)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ProductionUnitOwnerService productionUnitOwnerService = new ProductionUnitOwnerService(db);

            if (ModelState.IsValid)
            {
                productionUnitOwnerService.Create(currentOwner);
            }

            return Json(new[] { currentOwner }.ToDataSourceResult(request, ModelState));
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Editing_Update([DataSourceRequest] DataSourceRequest request, ProductionUnitOwnerViewModel currentOwner)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ProductionUnitOwnerService ownerService = new ProductionUnitOwnerService(db);

            if (currentOwner != null)
            {
                ownerService.Update(currentOwner);
            }

            return Json(new[] { currentOwner }.ToDataSourceResult(request, ModelState));
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Editing_Destroy([DataSourceRequest] DataSourceRequest request, ProductionUnitOwnerViewModel currentOwner)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ProductionUnitOwnerService ownerService = new ProductionUnitOwnerService(db);

            if (currentOwner != null)
            {
                if (currentOwner.user != null)
                    ModelState.AddModelError("inUse", new Exception("[[[Production Unit Owner already in use]]]"));

                ownerService.Destroy(currentOwner);
            }

            return Json(new[] { currentOwner }.ToDataSourceResult(request, ModelState));
        }

        protected override void Dispose(bool disposing)
        {
                base.Dispose(disposing);
        }
    }
}
