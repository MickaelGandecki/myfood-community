using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using myfoodapp.Hub.Business;
using myfoodapp.Hub.Models;
using myfoodapp.Hub.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace myfoodapp.Hub.Controllers
{
    public class HomeController : Controller
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

		protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            base.Initialize(requestContext);
        }

        public ActionResult Index()
        {
			var currentUser = this.User.Identity.GetUserName();
			if (currentUser == String.Empty)
			{
				return Redirect("/Account/Login");
			}
			var db = new ApplicationDbContext();

			var userId = UserManager.FindByName(currentUser).Id;
			var isAdmin = this.UserManager.IsInRole(userId, "Admin");
            ViewBag.HasFahrenheitSetting = db.ProductionUnitOwners.FirstOrDefault(o => o.user.UserName == this.User.Identity.Name).hasFahrenheitSetting;

            if (isAdmin)
			{
				return View();
			}
			else
			{
				return Redirect("/ProductionUnits");
			}
        }

		public ActionResult GetClusterMapData()
		{
			var db = new ApplicationDbContext();
			var measureService = new MeasureService(db);

			var listMarker = new List<Marker>();

			db.ProductionUnits.Include(p => p.owner.preferedMoment).ToList().ForEach(p =>
			{
				var phone = p.owner.phoneNumber == null ? "00 33 3 67 37 00 56" : p.owner.phoneNumber;
				var contactMail = p.owner.contactMail == null ? "contact@myfood.eu" : p.owner.contactMail;
				listMarker.Add(new Marker(p.locationLatitude, p.locationLongitude,
											String.Format(@"Click for view details"))
					{ shape = "redMarker" });
				
			});
		

            var map = new Models.ClusterMap()
            {
                Name = "map",
                CenterLatitude = 44.0235561,
                CenterLongitude = 0.3640063,
                Zoom = 4,
                TileUrlTemplate = "http://#= subdomain #.tile.openstreetmap.org/#= zoom #/#= x #/#= y #.png",
                TileSubdomains = new string[] { "a", "b", "c" },
                TileAttribution = "&copy; <a href='http://osm.org/copyright'>OpenStreetMap contributors</a>"
            };

            var result = new GeoClusterMapData { type = "FeatureCollection" };
            var list = new List<GeoFeature>();
            foreach (var marker in listMarker)
            {
                var feature = new GeoFeature { type = "Feature" };
                feature.properties = new GeoProperties { scalerank = 2, name = marker.name, long_x = marker.latlng[0], lat_y = marker.latlng[1] };
                feature.geometry = new Geometry { type = "Point", coordinates = new double[] { marker.latlng[1], marker.latlng[0] } };
                list.Add(feature);
            }
            result.features = list.ToArray();
            map.ClusterData = result;

            var data = new JavaScriptSerializer().Serialize(map);

            return Json(new {data}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProductionUnitMeasures(int id)
        {
            var db = new ApplicationDbContext();

            var currentProductionUnit = db.ProductionUnits.Where(p => p.picturePath != null).ToList()[id];

            var pHSensorValueSet = SensorValueManager.GetSensorValueSet(currentProductionUnit.Id, SensorTypeEnum.ph, db);
            var waterTempSensorValueSet = SensorValueManager.GetSensorValueSet(currentProductionUnit.Id, SensorTypeEnum.waterTemperature, db);
            var airTempSensorValueSet = SensorValueManager.GetSensorValueSet(currentProductionUnit.Id, SensorTypeEnum.airTemperature, db);
            var humiditySensorValueSet = SensorValueManager.GetSensorValueSet(currentProductionUnit.Id, SensorTypeEnum.humidity, db);
            if (db.ProductionUnitOwners.FirstOrDefault(o => o.user.UserName == this.User.Identity.Name).hasFahrenheitSetting == true)
            {
                if (waterTempSensorValueSet.CurrentCaptureTime != "-")
                {
                    waterTempSensorValueSet.AverageDayValue = Math.Round(waterTempSensorValueSet.AverageDayValue * 9 / 5, 1) + 32;
                    waterTempSensorValueSet.AverageHourValue = Math.Round(waterTempSensorValueSet.AverageHourValue * 9 / 5, 1) + 32;
                    waterTempSensorValueSet.CurrentValue = Math.Round(waterTempSensorValueSet.CurrentValue * 9 / 5, 1) + 32;
                }
                if (airTempSensorValueSet.CurrentCaptureTime != "-")
                {
                    airTempSensorValueSet.AverageDayValue = Math.Round(airTempSensorValueSet.AverageDayValue * 9 / 5, 1) + 32;
                    airTempSensorValueSet.AverageHourValue = Math.Round(airTempSensorValueSet.AverageHourValue * 9 / 5, 1) + 32;
                    airTempSensorValueSet.CurrentValue = Math.Round(airTempSensorValueSet.CurrentValue * 9 / 5, 1) + 32;
                }
            }

            return Json(new
            {
                CurrentPhValue = pHSensorValueSet.CurrentValue,
                CurrentPhCaptureTime = pHSensorValueSet.CurrentCaptureTime,
                AverageHourPhValue = pHSensorValueSet.AverageHourValue,
                AverageDayPhValue = pHSensorValueSet.AverageDayValue,
                LastDayPhCaptureTime = pHSensorValueSet.LastDayCaptureTime,

                CurrentWaterTempValue = waterTempSensorValueSet.CurrentValue,
                CurrentWaterTempCaptureTime = waterTempSensorValueSet.CurrentCaptureTime,
                AverageHourWaterTempValue = waterTempSensorValueSet.AverageHourValue,
                AverageDayWaterTempValue = waterTempSensorValueSet.AverageDayValue,
                LastDayWaterTempCaptureTime = waterTempSensorValueSet.LastDayCaptureTime,

                CurrentAirTempValue = airTempSensorValueSet.CurrentValue,
                CurrentAirTempCaptureTime = airTempSensorValueSet.CurrentCaptureTime,
                AverageHourAirTempValue = airTempSensorValueSet.AverageHourValue,
                AverageDayAirTempValue = airTempSensorValueSet.AverageDayValue,
                LastDayAirTempCaptureTime = airTempSensorValueSet.LastDayCaptureTime,

                CurrentHumidityValue = humiditySensorValueSet.CurrentValue,
                CurrentHumidityCaptureTime = humiditySensorValueSet.CurrentCaptureTime,
                AverageHourHumidityValue = humiditySensorValueSet.AverageHourValue,
                AverageDayHumidityValue = humiditySensorValueSet.AverageDayValue,
                LastDayHumidityCaptureTime = humiditySensorValueSet.LastDayCaptureTime,
            }, JsonRequestBehavior.AllowGet);
        }

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
                var currentProductionUnit = db.ProductionUnits.Where(p => p.picturePath != null).ToList();

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

        public ActionResult GetProductionUnitDetailList()
        {
            var db = new ApplicationDbContext();

            var prodUnitListCount = db.ProductionUnits.Where(p => p.picturePath != null).Count();

            if (prodUnitListCount == 0)
                return null;

            var currentProductionUnitList = db.ProductionUnits.Include(p => p.owner)
                                         .Include(p => p.productionUnitType)
                                         .Include(p => p.productionUnitStatus)
                                         .Where(p => p.picturePath != null).ToList();

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
                    options.ToList().ForEach(o => { optionList += o.name + " / "; });
                }

                lst.Add(new
                {

                    PioneerCitizenName = p.owner.pioneerCitizenName,
                    PioneerCitizenNumber = p.owner.pioneerCitizenNumber,
                    ProductionUnitVersion = p.version,
                    ProductionUnitStartDate = p.startDate,
                    ProductionUnitType = p.productionUnitType.name,
                    ProductionUnitStatus = p.productionUnitStatus.name,
                    PicturePath = p.picturePath,

                    LocationLatitude = p.locationLatitude,
                    LocationLongitude = p.locationLongitude,

                    ProductionUnitOptions = optionList,
                }
                );
            });

            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ProductionUnitStatus_Read([DataSourceRequest] DataSourceRequest request)
        {
            var db = new ApplicationDbContext();

            var rslt = db.ProductionUnits.Include(p => p.productionUnitStatus).ToList();

            var waitConfCount = rslt.Where(p => p.productionUnitStatus.Id == 1).Count();
            var setupPlannedCount = rslt.Where(p => p.productionUnitStatus.Id == 2).Count();
            var upRunningCount = rslt.Where(p => p.productionUnitStatus.Id == 3).Count();
            var onMaintenanceCount = rslt.Where(p => p.productionUnitStatus.Id == 4).Count();
            var stoppedCount  = rslt.Where(p => p.productionUnitStatus.Id == 5).Count();
            var offlineCount = rslt.Where(p => p.productionUnitStatus.Id == 6).Count();

            var statusList = new List<PieChartViewModel>();

            statusList.Add(new PieChartViewModel() { Category = "[[[Wait Confirm.]]]", Value = waitConfCount, Color = "#9de219" });
            statusList.Add(new PieChartViewModel() { Category = "[[[Setup Planned]]]", Value = setupPlannedCount, Color = "#90cc38" });
            statusList.Add(new PieChartViewModel() { Category = "[[[Up and Running]]]", Value = upRunningCount, Color = "#068c35" });
            statusList.Add(new PieChartViewModel() { Category = "[[[On Maintenance]]]", Value = onMaintenanceCount, Color = "#006634" });
            statusList.Add(new PieChartViewModel() { Category = "[[[Stopped]]]", Value = stoppedCount, Color = "#003F38" });
            statusList.Add(new PieChartViewModel() { Category = "[[[Offline]]]", Value = offlineCount, Color = "#004d38" });

            return Json(statusList);
        }

        [OutputCache(Duration = 3600)]
        public ActionResult GetNetworkStatsAndIncidents()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var stats = PerformanceManager.GetNetworkStatisticsAndIncidents(db);

            return Json(new
            {
                ProductionUnitNumber = stats.productionUnitNumber,
                TotalMonthlyIncident = stats.totalMonthlyIncident,
                TotalAnnuallyIncident = stats.totalAnnuallyIncident,
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetNetworkScore()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var score = PerformanceManager.GetNetworkScore(db);

            return Json(new
            {
                NetworkScore = score,
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetConnectivityStats()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var stats = PerformanceManager.GetConnectivityStatistics(db);

            return Json(new
            {
                Stats = stats
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
