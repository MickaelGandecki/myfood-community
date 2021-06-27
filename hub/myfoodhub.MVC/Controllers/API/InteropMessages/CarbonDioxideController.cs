using myfoodapp.Hub.Business;
using myfoodapp.Hub.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Data.Entity;
using System.Web.Http;
using System.Globalization;
using System.Threading;
using myfoodapp.Hub.Common;

namespace myfoodapp.Hub.Controllers.Api.InteropMessages
{
    public class CarbonDioxideController : ApiController
    {
        //[Authorize]
        public void Post([FromBody]JObject data)
        {
            var content = data["content"].ToObject<string>();
            var device = data["device"].ToObject<string>();

            var db = new ApplicationDbContext();
            var dbLog = new ApplicationDbContext();

            CultureInfo culture;
            culture = CultureInfo.CreateSpecificCulture("en-US");

            var date = DateTime.Now;

            content = content.Replace("a", "A");

            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");

            try
            {
                var measureType = db.MessageTypes.Where(m => m.Id == 1).FirstOrDefault();

                db.Messages.Add(new Message() { content = content, device = device, date = date, messageType = measureType });
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                dbLog.Logs.Add(Log.CreateErrorLog("Error on Message from Sigfox", ex));
                dbLog.SaveChanges();
            }

            try
            {
                var currentProductionUnit = db.ProductionUnits.Include(p => p.owner.user)
                                                              .Include(p => p.owner.language)
                                                              .Include(p => p.hydroponicType)
                                                              .Include(p => p.productionUnitStatus)
                                                              .Include(p => p.productionUnitType)
                                                              .Where(p => p.reference == device).FirstOrDefault();
                
                if (currentProductionUnit == null)
                {
                    db.Logs.Add(Log.CreateLog(String.Format("Production Unit not found - {0}", device), Log.LogType.Warning));
                    db.SaveChanges();
                }

                //var infoEventType = db.EventTypes.Where(p => p.Id == 8).FirstOrDefault();

                //var upRunningStatus = db.ProductionUnitStatus.Where(p => p.Id == 3).FirstOrDefault();
                //var offlineStatus = db.ProductionUnitStatus.Where(p => p.Id == 6).FirstOrDefault();

                //if(currentProductionUnit.productionUnitStatus.Id == offlineStatus.Id)
                //{
                //    if (currentProductionUnit.owner != null && currentProductionUnit.owner.language != null)
                //    {
                //        switch (currentProductionUnit.owner.language.description)
                //        {
                //            case "fr":
                //                db.Events.Add(new Event() { date = DateTime.Now, description = "Serre connectée", isOpen = false, productionUnit = currentProductionUnit, eventType = infoEventType, createdBy = "MyFood Bot" });
                //                break;
                //            default:
                //                db.Events.Add(new Event() { date = DateTime.Now, description = "Smart Greenhouse Online", isOpen = false, productionUnit = currentProductionUnit, eventType = infoEventType, createdBy = "MyFood Bot" });
                //                break;
                //        }

                //        db.SaveChanges();
                //    }

                //    if (currentProductionUnit.owner.notificationPushKey != null)
                //    {
                //        NotificationPushManager.PioneerUnitOnlineMessage(currentProductionUnit);
                //    }
                //}

                //currentProductionUnit.lastMeasureReceived = date;
                //currentProductionUnit.productionUnitStatus = upRunningStatus;

                //var currentMeasures = new GroupedMeasure();
                //currentMeasures.hydroponicTypeName = currentProductionUnit.hydroponicType.name;

                //var phSensor = db.SensorTypes.Where(s => s.Id == 1).FirstOrDefault();
                //var waterTemperatureSensor = db.SensorTypes.Where(s => s.Id == 2).FirstOrDefault();
                //var dissolvedOxySensor = db.SensorTypes.Where(s => s.Id == 3).FirstOrDefault();
                //var ORPSensor = db.SensorTypes.Where(s => s.Id == 4).FirstOrDefault();
                //var airTemperatureSensor = db.SensorTypes.Where(s => s.Id == 5).FirstOrDefault();
                //var airHumidity = db.SensorTypes.Where(s => s.Id == 6).FirstOrDefault();

                //var phContent = content.Substring(0, 4).Insert(3,".");
                //var waterTempContent = content.Substring(4, 4).Insert(3, ".");
                //var dissolvedOxyContent  = content.Substring(8, 4).Insert(3, ".");
                //var orpContent = content.Substring(12, 4).Insert(3, ".");
                //var airTempContent = content.Substring(16, 4).Insert(3, ".");
                //var airHumidityContent = content.Substring(20, 4).Insert(3, ".");

                //if(!(phContent.Contains("a") || phContent.Contains("A")))
                //{
                //    decimal pHvalue = 0;
                //    if(decimal.TryParse(phContent, style, culture, out pHvalue))
                //    {
                //        currentMeasures.pHvalue = pHvalue;

                //        db.Measures.Add(new Measure() { captureDate = date, productionUnit = currentProductionUnit, sensor = phSensor, value = pHvalue });
                //        db.SaveChanges();
                //    }
                //    else
                //    {
                //        dbLog.Logs.Add(Log.CreateLog("Error on Convert pH value", Log.LogType.Warning));
                //        dbLog.SaveChanges();
                //    }     
                //}

                //if (!(waterTempContent.Contains("a") || waterTempContent.Contains("A")))
                //{
                //    decimal waterTempvalue = 0;
                //    if (decimal.TryParse(waterTempContent, style, culture, out waterTempvalue))
                //    {
                //        currentMeasures.waterTempvalue = waterTempvalue;

                //        db.Measures.Add(new Measure() { captureDate = date, productionUnit = currentProductionUnit, sensor = waterTemperatureSensor, value = waterTempvalue });
                //        db.SaveChanges();
                //    }
                //    else
                //    {
                //        dbLog.Logs.Add(Log.CreateLog("Error on Convert water temp. value", Log.LogType.Warning));
                //        dbLog.SaveChanges();
                //    }
                //}

                //if (!(dissolvedOxyContent.Contains("a") || dissolvedOxyContent.Contains("A")))
                //{
                //    decimal DOvalue = 0;
                //    if (decimal.TryParse(dissolvedOxyContent, style, culture, out DOvalue))
                //    {
                //        currentMeasures.DOvalue = DOvalue;

                //        db.Measures.Add(new Measure() { captureDate = date, productionUnit = currentProductionUnit, sensor = dissolvedOxySensor, value = DOvalue });
                //        db.SaveChanges();
                //    }
                //    else
                //    {
                //        dbLog.Logs.Add(Log.CreateLog("Error on Convert dissolved oxy. value", Log.LogType.Warning));
                //        dbLog.SaveChanges();
                //    }
                //}

                //if (!(orpContent.Contains("a") || orpContent.Contains("A")))
                //{
                //    decimal ORPvalue = 0;
                //    if (decimal.TryParse(orpContent, style, culture, out ORPvalue))
                //    {
                //        currentMeasures.ORPvalue = ORPvalue;

                //        db.Measures.Add(new Measure() { captureDate = date, productionUnit = currentProductionUnit, sensor = ORPSensor, value = ORPvalue });
                //        db.SaveChanges();
                //    }
                //    else
                //    {
                //        dbLog.Logs.Add(Log.CreateLog("Error on Convert ORP value", Log.LogType.Warning));
                //        dbLog.SaveChanges();
                //    }
                //}

                //if (!(airTempContent.Contains("a") || airTempContent.Contains("A")))
                //{
                //    decimal airTempvalue = 0;
                //    if (decimal.TryParse(airTempContent, style, culture, out airTempvalue))
                //    {
                //        currentMeasures.airTempvalue = airTempvalue;

                //        db.Measures.Add(new Measure() { captureDate = date, productionUnit = currentProductionUnit, sensor = airTemperatureSensor, value = airTempvalue });
                //        db.SaveChanges();
                //    }
                //    else
                //    {
                //        dbLog.Logs.Add(Log.CreateLog("Error on Convert air temp. value", Log.LogType.Warning));
                //        dbLog.SaveChanges();
                //    }
                //}

                //if (!(airHumidityContent.Contains("a") || airHumidityContent.Contains("A")))
                //{
                //    decimal humidityvalue = 0;
                //    if (decimal.TryParse(airHumidityContent, style, culture, out humidityvalue))
                //    {
                //        currentMeasures.humidityvalue = humidityvalue;

                //        db.Measures.Add(new Measure() { captureDate = date, productionUnit = currentProductionUnit, sensor = airHumidity, value = humidityvalue });
                //        db.SaveChanges();
                //    }
                //    else
                //    {
                //        dbLog.Logs.Add(Log.CreateLog("Error on Convert humidity value", Log.LogType.Warning));
                //        dbLog.SaveChanges();
                //    }
                //}

            }
            catch (Exception ex)
            {
                dbLog.Logs.Add(Log.CreateErrorLog("Error on Convert Message into Measure", ex));
                dbLog.SaveChanges();
            }
        }
    }
}