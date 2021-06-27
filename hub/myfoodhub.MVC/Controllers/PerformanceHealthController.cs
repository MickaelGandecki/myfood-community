using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using myfoodapp.Hub.Models;
using myfoodapp.Hub.Services;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace myfoodapp.Hub.Controllers
{
    public class PerformanceHealthController : Controller
    {
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
            PopulateProductionLevel();
            PopulateHealthLevel();

            return View();
        }

        [Authorize]
        public ActionResult Editing_Read([DataSourceRequest] DataSourceRequest request)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            RecognitionRecordService recognitionRecordService = new RecognitionRecordService(db);

            var currentUser = this.User.Identity.GetUserName();

            var currentProductionUnits = db.ProductionUnits.Include(p => p.owner.user)
                                                           .Where(p => p.owner.user.UserName == "mickael@myfood.eu").ToList();
            if (currentProductionUnits != null)
            {
                return Json(recognitionRecordService.GetAll(currentProductionUnits.FirstOrDefault().Id).ToDataSourceResult(request));
            }

            return null;
        }

        [Authorize]
        public ActionResult Details(int id)
        {
            ViewBag.DisplayManagementBtn = "None";

            var currentUser = this.User.Identity.GetUserName();
            var userId = UserManager.FindByName(currentUser).Id;
            var isAdmin = this.UserManager.IsInRole(userId, "Admin");

            if (isAdmin)
                ViewBag.DisplayManagementBtn = "All";
            else
            {
                ApplicationDbContext db = new ApplicationDbContext();
                var currentProductionUnit = db.ProductionUnits.Include("owner.user").Where(p => p.Id == id).FirstOrDefault();
                if(currentProductionUnit != null && currentProductionUnit.owner.user.UserName == currentUser)
                {
                    ViewBag.DisplayManagementBtn = "All";
                }
            }

            ViewBag.Title = "Performance & Health Detail Page";

            return View();
        }
     
        private void PopulateProductionLevel()
        {
             ApplicationDbContext db = new ApplicationDbContext();

             var productionLevels = db.ProductionLevels
                        .Select(m => new ProductionLevelViewModel
                        {
                            Id = m.Id,
                            name = m.name
                        })
                        .OrderBy(e => e.name);

            ViewData["ProductionLevels"] = productionLevels;
        }

        private void PopulateHealthLevel()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var healthLevel = db.HealthLevels
                       .Select(m => new HealthLevelViewModel
                       {
                           Id = m.Id,
                           name = m.name
                       })
                       .OrderBy(e => e.name);

            ViewData["HealthLevels"] = healthLevel;
        }

        protected override void Dispose(bool disposing)
        {
                base.Dispose(disposing);
        }
    }
}
