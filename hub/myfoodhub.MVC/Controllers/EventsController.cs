using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using myfoodapp.Hub.Models;
using myfoodapp.Hub.Models.Helpers;
using myfoodapp.Hub.Services;
using System;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace myfoodapp.Hub.Controllers
{
    public class EventsController : Controller
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
        public ActionResult Index(int id)
        {
            ViewBag.Title = "Production Unit Event Page";

            var currentUser = this.User.Identity.GetUserName();
            var userId = UserManager.FindByName(currentUser).Id;

            var db = new ApplicationDbContext();

            var isAdmin = this.UserManager.IsInRole(userId, "Admin");

            var currentProductionUnits = db.ProductionUnits.Include(p => p.owner.user)
                                                           .Where(p => p.owner.user.UserName == currentUser 
                                                                    && p.Id == id).ToList();

            var currentProductionUnit = currentProductionUnits.FirstOrDefault();

            if (currentProductionUnit != null && isAdmin == false && currentProductionUnit.Id != id)
            {
                return Redirect("/ProductionUnits/Details/" + currentProductionUnit.Id);
            }

            PopulateEventType();

            return View();
        }

        [Authorize]
        public ActionResult Manage()
        {
            ViewBag.Title = "Production Units Event Page";

            PopulateEventType();

            return View();
        }

        private void PopulateEventType()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var eventTypes = db.EventTypes
                       .Select(m => new EventTypeViewModel
                       {
                           Id = m.Id,
                           name = m.name
                       })
                       .OrderBy(e => e.name);

            ViewData["EventTypes"] = eventTypes;
        }

		[Authorize]
		public ActionResult Events_Read_All([DataSourceRequest] DataSourceRequest request)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            EventService eventService = new EventService(db);
 
            var rslt = eventService.GetAll().OrderByDescending(ev => ev.date).OrderByDescending(ev => ev.Id);
 
            return Json(rslt.ToDataSourceResult(request));
        }

        [Authorize]
        public ActionResult Events_Read([DataSourceRequest] DataSourceRequest request, int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            EventService eventService = new EventService(db);

            var rslt = eventService.GetAll(id).OrderByDescending(ev => ev.date).OrderByDescending(ev => ev.Id);

            return Json(rslt.ToDataSourceResult(request));
        }

        [Authorize]
        public ActionResult Event_Read([DataSourceRequest] DataSourceRequest request, int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            EventService eventService = new EventService(db);

            var rslt = eventService.One(e=>e.Id==id);

            return Json(rslt, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Event_Update([DataSourceRequest] DataSourceRequest request, UpdateEvent currentEvent)
		{
			ApplicationDbContext db = new ApplicationDbContext();
			EventService eventService = new EventService(db);

			var currentUser = this.User.Identity.GetUserName();

			var userId = UserManager.FindByName(currentUser).Id;
			var isAdmin = this.UserManager.IsInRole(userId, "Admin");


			if (currentEvent != null)
			{
				if (isAdmin) 
					eventService.Update(currentEvent);
				else
				{

					var eventToUpdate = eventService.One(p => p.Id == Convert.ToInt64(currentEvent.Id));

					var currentOwner = db.ProductionUnitOwners.Include(p => p.user)
									   .Where(p => p.user.UserName == currentUser).FirstOrDefault();

					if (currentOwner != null && currentOwner.pioneerCitizenName == eventToUpdate.createdBy)
					eventService.Update(currentEvent);
				}
			}

			return Json(new[] { currentEvent }.ToDataSourceResult(request, ModelState));
		}

		[Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Event_Destroy([DataSourceRequest] DataSourceRequest request, string id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            EventService eventService = new EventService(db);

            var currentUser = this.User.Identity.GetUserName();

			var currentEvent = eventService.One(p => p.Id == Convert.ToInt64(id));

			var userId = UserManager.FindByName(currentUser).Id;
            var isAdmin = this.UserManager.IsInRole(userId, "Admin");

            if (currentEvent != null)
            {
                if(isAdmin)
                    eventService.Destroy(currentEvent);
                else
                {
                    var currentOwner = db.ProductionUnitOwners.Include(p => p.user)
                                       .Where(p => p.user.UserName == currentUser).FirstOrDefault();

                    if(currentOwner != null && currentOwner.pioneerCitizenName == currentEvent.createdBy)
                        eventService.Destroy(currentEvent);
                }
            }

            return Json(new[] { currentEvent }.ToDataSourceResult(request, ModelState));
        }

        [Authorize]
        public FileContentResult DownloadCSV()
        {
            var dbLog = new ApplicationDbContext();

            try
            {
                ApplicationDbContext db = new ApplicationDbContext();

                string fileName = String.Format("Events_All_[{0}].csv", DateTime.Now.ToShortDateString());

                StringBuilder csv = new StringBuilder();

                var events = db.Events.Include(p => p.productionUnit.owner).Include(p => p.eventType).Take(15000);

                events.OrderBy(m => m.date).ToList().ForEach(m =>
                {
                    csv.Append(m.Id + "; ");
                    csv.Append(m.date + "; ");
                    csv.Append(m.description + "; ");
                    csv.Append(m.isOpen + "; ");
                    csv.Append(m.createdBy + "; ");
                    csv.Append(m.productionUnit.info + "; ");
                    csv.Append(m.productionUnit.owner.pioneerCitizenName + "; ");
                    //csv.Append(StringToCSVCell(m.details) + "; ");

                    csv.Remove(csv.Length - 2, 1);
                    csv.Append("\r\n");
                }
                );
                
                return File(Encoding.Unicode.GetBytes(csv.ToString()), "text/csv", fileName);
            } 
            catch (Exception ex)
            {
                dbLog.Logs.Add(Log.CreateErrorLog("Error on File Generation", ex));
                dbLog.SaveChanges();
            }

            return null;

        }

        private string StringToCSVCell(string str)
        {
            if (str == null)
                return String.Empty;

            bool mustQuote = (str.Contains(",") || str.Contains("\"") || str.Contains("\r") || str.Contains("\n"));
            if (mustQuote)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("\"");
                foreach (char nextChar in str)
                {
                    sb.Append(nextChar);
                    if (nextChar == '"')
                        sb.Append("\"");
                }
                sb.Append("\"");
                return sb.ToString();
            }

            return str;
        }

        protected override void Dispose(bool disposing)
        {
                base.Dispose(disposing);
        }
    }
}
