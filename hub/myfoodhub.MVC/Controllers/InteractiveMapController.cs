using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using myfoodapp.Hub.Business;
using myfoodapp.Hub.Models;
using myfoodapp.Hub.Viewmodels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.Mvc;

namespace myfoodapp.Hub.Controllers
{
    public class InteractiveMapController : Controller
    {
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            base.Initialize(requestContext);
        }

        public ActionResult Index(string lang)
        {
            ViewBag.Title = "Interactive Map Page";

            if (lang != String.Empty)
                System.Web.HttpContext.Current.Session["UserLang"] = lang;

            return View();
        }

        public ActionResult ClusterMap()
        {
            ViewBag.Title = "Interactive Map Page";

            return View();
        }

        [OutputCache(Duration = 43200)]
        public ActionResult GetProductionUnitIndex(string SelectedProductionUnitCoord)
        {
            var db = new ApplicationDbContext();

            NumberStyles style;
            CultureInfo culture;

            style = NumberStyles.AllowDecimalPoint;
            culture = CultureInfo.CreateSpecificCulture("en-US");

            var strLat = SelectedProductionUnitCoord.Split('|')[0];
            var strLong = SelectedProductionUnitCoord.Split('|')[1];

            double latitude = 0;
            double longitude = 0;

            if (double.TryParse(strLat, style, culture, out latitude) && double.TryParse(strLong, style, culture, out longitude))
            {
                var currentProductionUnit = db.ProductionUnits.Where(p => p.productionUnitStatus.Id == 3).ToList();

                var currentProductionUnitIndex = currentProductionUnit.FindIndex(p => p.locationLatitude == latitude &&
                                                                                 p.locationLongitude == longitude);

                return Json(new
                {
                    CurrentIndex = currentProductionUnitIndex
                }, JsonRequestBehavior.AllowGet);
            }
            else
                return null;
        }

        [OutputCache(Duration = 43200)]
        public ActionResult GetProductionUnitDetail(string SelectedProductionUnitCoord)
        {
            var db = new ApplicationDbContext();

            var strLat = Double.Parse(SelectedProductionUnitCoord.Split('|')[0]);
            var strLong = Double.Parse(SelectedProductionUnitCoord.Split('|')[1]);

            var responseData = db.ProductionUnits.Where(p => p.locationLatitude == strLat && p.locationLongitude == strLong)
                                         .Include(p => p.owner.preferedMoment)
                                         .Include(p => p.productionUnitType)
                                         .Include(p => p.productionUnitStatus).ToList()[0];


            var options = db.OptionLists.Include(o => o.productionUnit)
                .Include(o => o.option)
                .Where(o => o.productionUnit.Id == responseData.Id)
                .Select(o => o.option);

            var optionList = string.Empty;

            if (options.Count() > 0)
            {
                options.ToList().ForEach(o => { optionList += o.name + "/"; });
            }

            var waterTempSensorValueSet = SensorValueManager.GetSensorValueSet(responseData.Id, SensorTypeEnum.waterTemperature, db);
            var pHSensorValueSet = SensorValueManager.GetSensorValueSet(responseData.Id, SensorTypeEnum.ph, db);
            if (db.ProductionUnitOwners.FirstOrDefault(o => o.user.UserName == this.User.Identity.Name).hasFahrenheitSetting == true && waterTempSensorValueSet.CurrentCaptureTime != "-")
            {
                waterTempSensorValueSet.AverageDayValue = Math.Round(waterTempSensorValueSet.AverageDayValue * 9 / 5, 1) + 32;
                waterTempSensorValueSet.AverageHourValue = Math.Round(waterTempSensorValueSet.AverageHourValue * 9 / 5, 1) + 32;
                waterTempSensorValueSet.CurrentValue = Math.Round(waterTempSensorValueSet.CurrentValue * 9 / 5, 1) + 32;
            }
            var lst = new object();
            lst = new
            {

                CurrentWaterTempValue = waterTempSensorValueSet.CurrentValue,
                CurrentWaterTempCaptureTime = waterTempSensorValueSet.CurrentCaptureTime,
                AverageHourWaterTempValue = waterTempSensorValueSet.AverageHourValue,
                AverageDayWaterTempValue = waterTempSensorValueSet.AverageDayValue,
                LastDayWaterTempCaptureTime = waterTempSensorValueSet.LastDayCaptureTime,

                CurrentpHValue = pHSensorValueSet.CurrentValue,
                CurrentpHCaptureTime = pHSensorValueSet.CurrentCaptureTime,
                AverageHourpHValue = pHSensorValueSet.AverageHourValue,
                AverageDaypHValue = pHSensorValueSet.AverageDayValue,
                LastDaypHCaptureTime = pHSensorValueSet.LastDayCaptureTime,

                PioneerCitizenName = responseData.owner.pioneerCitizenName,
                PioneerCitizenNumber = responseData.owner.pioneerCitizenNumber,
                ProductionUnitStartDate = responseData.startDate,
                ProductionUnitInfo = responseData.info,
                ProductionUnitType = responseData.productionUnitType.name,
                ProductionUnitStatus = responseData.productionUnitStatus.name,

                PhoneNumber = responseData.owner.phoneNumber == null ? "00 33 3 67 37 00 56" : responseData.owner.phoneNumber,
                ContactMail = responseData.owner.contactMail == null ? "contact@myfood.eu" : responseData.owner.contactMail,
                PicturePath = responseData.picturePath == null ? "NoImage.png" : responseData.picturePath,
                
                PreferedMoment = responseData.owner.preferedMoment == null ? "" : responseData.owner.preferedMoment.name,
                Location = responseData.owner.location == null ? "" : responseData.owner.location,

                ProductionUnitOptions = optionList,
            };

            return Json(lst);
        }

        [OutputCache(Duration = 43200)]
        public ActionResult GetProductionUnitDetailListSlider()
        {
            var db = new ApplicationDbContext();

			var prodUnitListCount = db.ProductionUnits.Where(p => p.productionUnitStatus.Id == 3).Count();

            if (prodUnitListCount == 0)
                return null;

            var currentProductionUnitList = db.ProductionUnits.Where(p => p.picturePath != null && p.productionUnitStatus.Id == 3)
                                         .Include(p => p.owner.preferedMoment)
                                         .Include(p => p.productionUnitType)
                                         .Include(p => p.productionUnitStatus).ToList();

            var lst = new List<object>();

            currentProductionUnitList.ForEach(p =>
            {
                var options = db.OptionLists.Include(o => o.productionUnit)
                .Include(o => o.option)
                .Where(o => o.productionUnit.Id == p.Id)
                .Select(o => o.option);

                var optionList = string.Empty;

                if (options.Count() > 0)
                {
                    options.ToList().ForEach(o => { optionList += o.name + "/"; });
                }


                var waterTempSensorValueSet = SensorValueManager.GetSensorValueSet(p.Id, SensorTypeEnum.waterTemperature, db);

                var pHSensorValueSet = SensorValueManager.GetSensorValueSet(p.Id, SensorTypeEnum.ph, db);
                if (db.ProductionUnitOwners.FirstOrDefault(o => o.user.UserName == this.User.Identity.Name).hasFahrenheitSetting == true && waterTempSensorValueSet.CurrentCaptureTime != "-")
                {
                    waterTempSensorValueSet.AverageDayValue = Math.Round(waterTempSensorValueSet.AverageDayValue * 9 / 5, 1) + 32;
                    waterTempSensorValueSet.AverageHourValue = Math.Round(waterTempSensorValueSet.AverageHourValue * 9 / 5, 1) + 32;
                    waterTempSensorValueSet.CurrentValue = Math.Round(waterTempSensorValueSet.CurrentValue * 9 / 5, 1) + 32;
                }


                lst.Add(new
                    {
                        PioneerCitizenName = p.owner.pioneerCitizenName,
                        PioneerCitizenNumber = p.owner.pioneerCitizenNumber,
                        ProductionUnitVersion = p.version,
                        ProductionUnitStartDate = p.startDate,
                        ProductionUnitType = p.productionUnitType.name,

                        ProductionUnitStatus = p.productionUnitStatus.name,

                        PhoneNumber = p.owner.phoneNumber == null ? "00 33 3 67 37 00 56" : p.owner.phoneNumber,
                        ContactMail = p.owner.contactMail == null ? "contact@myfood.eu" : p.owner.contactMail,
                        PicturePath = p.picturePath,

                        LocationLatitude = p.locationLatitude,
                        LocationLongitude = p.locationLongitude,
                        PreferedMoment = p.owner.preferedMoment == null ? "" : p.owner.preferedMoment.name,
                        Location = p.owner.location == null ? "" : p.owner.location,

                        ProductionUnitOptions = optionList,

                        CurrentWaterTempValue = waterTempSensorValueSet.CurrentValue,
                        CurrentWaterTempCaptureTime = waterTempSensorValueSet.CurrentCaptureTime,
                        AverageHourWaterTempValue = waterTempSensorValueSet.AverageHourValue,
                        AverageDayWaterTempValue = waterTempSensorValueSet.AverageDayValue,
                        LastDayWaterTempCaptureTime = waterTempSensorValueSet.LastDayCaptureTime,

                        CurrentpHValue = pHSensorValueSet.CurrentValue,
                        CurrentpHCaptureTime = pHSensorValueSet.CurrentCaptureTime,
                        AverageHourpHValue = pHSensorValueSet.AverageHourValue,
                        AverageDaypHValue = pHSensorValueSet.AverageDayValue,
                        LastDaypHCaptureTime = pHSensorValueSet.LastDayCaptureTime,
                    });
                
            });

            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 43200)]
        public ActionResult GetNetworkStats()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var stats = PerformanceManager.GetNetworkStatistics(db);

            return Json(new
            {
                ProductionUnitNumber = stats.productionUnitNumber,
                TotalMonthlyProduction = stats.totalMonthlyProduction,
                TotalMonthlySparedCO2 = stats.totalMonthlySparedCO2,
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProductionUnitStatus_Read([DataSourceRequest] DataSourceRequest request)
        {
            var db = new ApplicationDbContext();

            var rslt = db.ProductionUnits.Include("productionUnitStatus").ToList();

            var waitConfCount = rslt.Where(p => p.productionUnitStatus.Id == 1).Count();
            var setupPlannedCount = rslt.Where(p => p.productionUnitStatus.Id == 2).Count();
            var upRunningCount = rslt.Where(p => p.productionUnitStatus.Id == 3).Count();
            var onMaintenanceCount = rslt.Where(p => p.productionUnitStatus.Id == 4).Count();

            var statusList = new List<NewPieModel>();

            statusList.Add(new NewPieModel() { name = "[[[Wait Confirm.]]]", y = waitConfCount });
            statusList.Add(new NewPieModel() { name = "[[[Setup Planned]]]", y = setupPlannedCount });
            statusList.Add(new NewPieModel() { name = "[[[Up & Running]]]", y = upRunningCount });
            statusList.Add(new NewPieModel() { name = "[[[On Maintenance]]]", y = onMaintenanceCount });
            return Json(statusList);
        }
    }
}
