using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using myfoodapp.Hub.Models;
using myfoodapp.Hub.Services;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace myfoodapp.Hub.Controllers
{
    public class MeasuresController : Controller
    {
        [Authorize]
        // GET: Measures
        public ActionResult Index()
        {
            PopulateSensorsTypes();
            PopulateProductionUnit();
            
            return View();
        }

        [Authorize]
        public ActionResult Editing_Read([DataSourceRequest] DataSourceRequest request)
        {
            var db = new ApplicationDbContext();
            var measureService = new MeasureService(db);

            JsonResult result = Json(measureService.Read().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = Int32.MaxValue;

            return result;
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Editing_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<MeasureViewModel> measures)
        {
            var db = new ApplicationDbContext();
            var measureService = new MeasureService(db);

            var results = new List<MeasureViewModel>();

            if (measures != null && ModelState.IsValid)
            {
                foreach (var measure in measures)
                {
                    measureService.Create(measure);
                    results.Add(measure);
                }
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Editing_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<MeasureViewModel> messages)
        {
            var db = new ApplicationDbContext();
            var measureService = new MeasureService(db);

            if (messages != null && ModelState.IsValid)
            {
                foreach (var measure in messages)
                {
                    measureService.Update(measure);
                }
            }

            return Json(messages.ToDataSourceResult(request, ModelState));
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Editing_Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<MeasureViewModel> measures)
        {
            var db = new ApplicationDbContext();
            var measureService = new MeasureService(db);

            if (measures.Any())
            {
                foreach (var measure in measures)
                {
                    measureService.Destroy(measure);
                }
            }

            return Json(measures.ToDataSourceResult(request, ModelState));
        }

        private void PopulateSensorsTypes()
        {
            var db = new ApplicationDbContext();

            var sensorTypes = db.SensorTypes
                        .Select(m => new SensorTypeViewModel
                        {
                            Id = m.Id,
                            name = m.name
                        })
                        .OrderBy(e => e.name);

            ViewData["sensorTypes"] = sensorTypes;
        }

        private void PopulateProductionUnit()
        {
            var db = new ApplicationDbContext();

            var productionUnits = db.ProductionUnits.Include(p => p.owner)
                        .Select(m => new ProductionUnitViewModel
                        {
                            Id = m.Id,
                            info = m.owner.pioneerCitizenName + " - " + m.info
                        })
                        .OrderBy(e => e.info);

            ViewData["productionUnits"] = productionUnits;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
