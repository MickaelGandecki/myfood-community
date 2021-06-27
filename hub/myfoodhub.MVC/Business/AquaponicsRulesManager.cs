using myfoodapp.Hub.Common;
using myfoodapp.Hub.Models;
using Newtonsoft.Json;
using SimpleExpressionEvaluator;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Hosting;

namespace myfoodapp.Hub.Business
{
    public class AquaponicsRulesManager
    {
        public static List<RecommandationTemplaceObject> ValidateRules(GroupedMeasure currentMeasures, int productionUnitId)
        {
            Evaluator evaluator = new Evaluator();

            ApplicationDbContext db = new ApplicationDbContext();
            ApplicationDbContext dbLog = new ApplicationDbContext();

            var data = File.ReadAllText(HostingEnvironment.MapPath("~/Content/SmartGreenhouseRules.json"));
            var rulesList = JsonConvert.DeserializeObject<List<Rule>>(data);

            var currentProductionUnit = db.ProductionUnits.Include(p => p.owner.language).Where(p => p.Id == productionUnitId).FirstOrDefault();

            var issueEventType = db.EventTypes.Where(p => p.Id == 2).FirstOrDefault();
            var warningEventType = db.EventTypes.Where(p => p.Id == 1).FirstOrDefault();
            var infoEventType = db.EventTypes.Where(p => p.Id == 8).FirstOrDefault();

            var currentProductionUnitOwner = currentProductionUnit.owner;

            List<RecommandationTemplaceObject> reco = new List<RecommandationTemplaceObject>();

            foreach (var rule in rulesList)
            {
                var strTitle = String.Empty;
                var strDescription = String.Empty;
                var strUrl = String.Empty;

                var eventType = new EventType();

                bool rslt = false;

                try
                {
                    try
                    {
                        rslt = evaluator.Evaluate(rule.ruleEvaluator, currentMeasures);

                        strTitle = rule.title;
                        strDescription = rule.description;
                        strUrl = rule.url;

                        switch (rule.impactLevel)
                        {
                            case 0:
                                eventType = issueEventType;
                                break;
                            case 1:
                                eventType = warningEventType;
                                break;
                            case 2:
                                eventType = infoEventType;
                                break;
                            default:
                                eventType = infoEventType;
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        dbLog.Logs.Add(Log.CreateErrorLog(String.Format("Error with Rule Manager Evaluator - {0}", rule.ruleEvaluator), ex));
                        dbLog.SaveChanges();
                    }

                    if (currentProductionUnitOwner != null && currentProductionUnitOwner.language != null)
                    {
                        switch (currentProductionUnitOwner.language.description)
                        {
                            case "fr":
                                strTitle = rule.titleFR;
                                strDescription = rule.descriptionFR;
                                break;
                            case "de":
                                strTitle = rule.titleDE;
                                strDescription = rule.descriptionDE;
                                strUrl = String.Format("{0}?ljs=de", rule.url);
                                break;
                            default:
                                strTitle = rule.title;
                                strDescription = rule.description;
                                strUrl = String.Format("{0}?ljs=en", rule.url);
                                break;
                        }
                    }

                    if (rslt)
                    {
                        var bindingValue = currentMeasures.GetType().GetProperty(rule.bindingPropertyValue).GetValue(currentMeasures, null);
                        var strFormatTitle = String.Format(strTitle, bindingValue);

                        if (currentProductionUnit != null)
                            {
                                reco.Add(new RecommandationTemplaceObject() { title = strFormatTitle, description = strDescription, url = strUrl, order = rule.impactLevel }) ;

                                var content = String.Format(@"<b>{0}</b> </br> {1} </br> <a style=""color:#515A5A;"" href=""{2}"">Link</a>", strFormatTitle, strDescription, strUrl);

                                db.Events.Add(new Event() { date = DateTime.Now, description = content, isOpen = false, productionUnit = currentProductionUnit, eventType = eventType, createdBy = "MyFood Bot" });
                                db.SaveChanges();
                            }
                    }
                }
                catch (Exception ex)
                {
                    dbLog.Logs.Add(Log.CreateErrorLog(String.Format("Error with Rule Manager Evaluator - {0}", rule.ruleEvaluator), ex));
                    dbLog.SaveChanges();
                }
            }

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                dbLog.Logs.Add(Log.CreateErrorLog(String.Format("Error with Rule Manager - Save Events"), ex));
                dbLog.SaveChanges();
            }

            return reco;
        }

        public static GroupedMeasure MeasuresProcessor(int productionUnitId)
        {
            var db = new ApplicationDbContext();
            var dbLog = new ApplicationDbContext();

            ProductionUnit currentProductionUnit = db.ProductionUnits.Include(p => p.hydroponicType)
                                                                     .Where(p => p.Id == productionUnitId).FirstOrDefault();

            var phSensor = db.SensorTypes.Where(s => s.Id == 1).FirstOrDefault();
            var waterTemperatureSensor = db.SensorTypes.Where(s => s.Id == 2).FirstOrDefault();
            var dissolvedOxySensor = db.SensorTypes.Where(s => s.Id == 3).FirstOrDefault();
            var ORPSensor = db.SensorTypes.Where(s => s.Id == 4).FirstOrDefault();
            var airTemperatureSensor = db.SensorTypes.Where(s => s.Id == 5).FirstOrDefault();
            var airHumidity = db.SensorTypes.Where(s => s.Id == 6).FirstOrDefault();

            DateTime thisDay = DateTime.Now;
            DateTime lastDay = thisDay.AddDays(-1);
            DateTime twoDaysAgo = thisDay.AddDays(-2);
            DateTime threeDaysAgo = thisDay.AddDays(-3);
            DateTime fourDaysAgo = thisDay.AddDays(-4);
            DateTime fiveDaysAgo = thisDay.AddDays(-5);
            DateTime aWeekAgo = thisDay.AddDays(-7);

            GroupedMeasure currentMeasures = new GroupedMeasure();
            currentMeasures.hydroponicTypeName = currentProductionUnit.hydroponicType.name;

            try
            {
                var currentLastDayMaxPHValue = ((decimal?)db.Measures.Where(m => m.captureDate > lastDay &&
                                   m.productionUnit.Id == currentProductionUnit.Id &&
                                   m.sensor.Id == phSensor.Id).OrderBy(m => m.Id).Max(t => (decimal?)t.value)).GetValueOrDefault();

                var currentLastDayMinPHValue = ((decimal?)db.Measures.Where(m => m.captureDate > lastDay &&
                                   m.productionUnit.Id == currentProductionUnit.Id &&
                                   m.sensor.Id == phSensor.Id).OrderBy(m => m.Id).Min(t => (decimal?)t.value)).GetValueOrDefault();

                var currentTwoDaysMaxPHValue = ((decimal?)db.Measures.Where(m => m.captureDate > twoDaysAgo && m.captureDate < lastDay &&
                                   m.productionUnit.Id == currentProductionUnit.Id &&
                                   m.sensor.Id == phSensor.Id).OrderBy(m => m.Id).Max(t => (decimal?)t.value)).GetValueOrDefault();

                var currentTwoDaysMinPHValue = ((decimal?)db.Measures.Where(m => m.captureDate > twoDaysAgo && m.captureDate < lastDay &&
                                   m.productionUnit.Id == currentProductionUnit.Id &&
                                   m.sensor.Id == phSensor.Id).OrderBy(m => m.Id).Min(t => (decimal?)t.value)).GetValueOrDefault();

                var currentThreeDaysMaxPHValue = ((decimal?)db.Measures.Where(m => m.captureDate > threeDaysAgo && m.captureDate < twoDaysAgo &&
                                   m.productionUnit.Id == currentProductionUnit.Id &&
                                   m.sensor.Id == phSensor.Id).OrderBy(m => m.Id).Max(t => (decimal?)t.value)).GetValueOrDefault();

                var currentThreeDaysMinPHValue = ((decimal?)db.Measures.Where(m => m.captureDate > threeDaysAgo && m.captureDate < twoDaysAgo &&
                                   m.productionUnit.Id == currentProductionUnit.Id &&
                                   m.sensor.Id == phSensor.Id).OrderBy(m => m.Id).Min(t => (decimal?)t.value)).GetValueOrDefault();

                var currentFourDaysMaxPHValue = ((decimal?)db.Measures.Where(m => m.captureDate > fourDaysAgo && m.captureDate < threeDaysAgo &&
                                   m.productionUnit.Id == currentProductionUnit.Id &&
                                   m.sensor.Id == phSensor.Id).OrderBy(m => m.Id).Max(t => (decimal?)t.value)).GetValueOrDefault();

                var currentFourDaysMinPHValue = ((decimal?)db.Measures.Where(m => m.captureDate > fourDaysAgo && m.captureDate < threeDaysAgo &&
                                   m.productionUnit.Id == currentProductionUnit.Id &&
                                   m.sensor.Id == phSensor.Id).OrderBy(m => m.Id).Min(t => (decimal?)t.value)).GetValueOrDefault();

                var currentFiveDaysMaxPHValue = ((decimal?)db.Measures.Where(m => m.captureDate > fiveDaysAgo && m.captureDate < fourDaysAgo &&
                                   m.productionUnit.Id == currentProductionUnit.Id &&
                                   m.sensor.Id == phSensor.Id).OrderBy(m => m.Id).Max(t => (decimal?)t.value)).GetValueOrDefault();

                var currentFiveDaysMinPHValue = ((decimal?)db.Measures.Where(m => m.captureDate > fiveDaysAgo && m.captureDate < fourDaysAgo &&
                                   m.productionUnit.Id == currentProductionUnit.Id &&
                                   m.sensor.Id == phSensor.Id).OrderBy(m => m.Id).Min(t => (decimal?)t.value)).GetValueOrDefault();

                var currentLastWeekMaxPHValue = ((decimal?)db.Measures.Where(m => m.captureDate > aWeekAgo &&
                                   m.productionUnit.Id == currentProductionUnit.Id &&
                                   m.sensor.Id == phSensor.Id).OrderBy(m => m.Id).Max(t => (decimal?)t.value)).GetValueOrDefault();

                var currentLastWeekMinPHValue = ((decimal?)db.Measures.Where(m => m.captureDate > aWeekAgo &&
                                   m.productionUnit.Id == currentProductionUnit.Id &&
                                   m.sensor.Id == phSensor.Id).OrderBy(m => m.Id).Min(t => (decimal?)t.value)).GetValueOrDefault();

                var currentLastWeekMaxPHDate = ((DateTime?)db.Measures.Where(m => m.captureDate > aWeekAgo &&
                                   m.productionUnit.Id == currentProductionUnit.Id &&
                                   m.sensor.Id == phSensor.Id).OrderBy(m => m.Id).Max(t => (DateTime?)t.captureDate)).GetValueOrDefault();

                var currentLastWeekMinPHDate = ((DateTime?)db.Measures.Where(m => m.captureDate > aWeekAgo &&
                                   m.productionUnit.Id == currentProductionUnit.Id &&
                                   m.sensor.Id == phSensor.Id).OrderBy(m => m.Id).Min(t => (DateTime?)t.captureDate)).GetValueOrDefault();

                currentMeasures.lastWeekMaxPHValue = currentLastWeekMaxPHValue;
                currentMeasures.lastWeekMinPHValue = currentLastWeekMinPHValue;

                if (currentLastWeekMinPHDate < currentLastWeekMaxPHDate)
                    currentMeasures.lastWeekPHRise = true;
                else
                    currentMeasures.lastWeekPHFall = false;

                if (currentLastWeekMinPHDate > currentLastWeekMaxPHDate)
                    currentMeasures.lastWeekPHFall = true;
                else
                    currentMeasures.lastWeekPHRise = false;

                var currentLastWeekAveragePHValue = ((decimal?)db.Measures.Where(m => m.captureDate > aWeekAgo &&
                                   m.productionUnit.Id == currentProductionUnit.Id &&
                                   m.sensor.Id == phSensor.Id).OrderBy(m => m.Id).Average(t => t.value)).GetValueOrDefault();

                currentMeasures.lastWeekPHVariation = Math.Round(Math.Abs(currentLastWeekMaxPHValue - currentLastWeekMinPHValue), 1);
                currentMeasures.fiveLastDayPHVariation = Math.Round((Math.Abs(currentLastDayMaxPHValue - currentLastDayMinPHValue) + Math.Abs(currentTwoDaysMaxPHValue - currentTwoDaysMinPHValue) + Math.Abs(currentThreeDaysMaxPHValue - currentThreeDaysMinPHValue) + Math.Abs(currentFourDaysMaxPHValue - currentFourDaysMinPHValue) + Math.Abs(currentFiveDaysMaxPHValue - currentFiveDaysMinPHValue)) / 5, 1);
                currentMeasures.lastWeekAveragePHValue = Math.Round(currentLastWeekAveragePHValue, 1);

                var currentLastWeekMaxAirTempValue = ((decimal?)db.Measures.Where(m => m.captureDate > aWeekAgo &&
                                   m.productionUnit.Id == currentProductionUnit.Id &&
                                   m.sensor.Id == airTemperatureSensor.Id).OrderBy(m => m.Id).Max(t => (decimal?)t.value)).GetValueOrDefault();

                var currentLastWeekMinAirTempValue = ((decimal?)db.Measures.Where(m => m.captureDate > aWeekAgo &&
                                   m.productionUnit.Id == currentProductionUnit.Id &&
                                   m.sensor.Id == airTemperatureSensor.Id).OrderBy(m => m.Id).Min(t => (decimal?)t.value)).GetValueOrDefault();

                var currentLastWeekAverageAirTempValue = ((decimal?)db.Measures.Where(m => m.captureDate > aWeekAgo &&
                                   m.productionUnit.Id == currentProductionUnit.Id &&
                                   m.sensor.Id == airTemperatureSensor.Id).OrderBy(m => m.Id).Sum(t => (decimal?)t.value) / (6 * 24)).GetValueOrDefault();

                currentMeasures.lastWeekMaxAirTempValue = Math.Round(currentLastWeekMaxAirTempValue, 1);
                currentMeasures.lastWeekMinAirTempValue = Math.Round(currentLastWeekMinAirTempValue, 1);
                currentMeasures.lastWeekAverageAirTempValue = Math.Round(currentLastWeekAverageAirTempValue, 1);

                var currentLastWeekMaxWaterTempValue = ((decimal?)db.Measures.Where(m => m.captureDate > aWeekAgo &&
                                   m.productionUnit.Id == currentProductionUnit.Id &&
                                   m.sensor.Id == waterTemperatureSensor.Id).OrderBy(m => m.Id).Max(t => (decimal?)t.value)).GetValueOrDefault();

                var currentLastWeekMinWaterTempValue = ((decimal?)db.Measures.Where(m => m.captureDate > aWeekAgo &&
                                   m.productionUnit.Id == currentProductionUnit.Id &&
                                   m.sensor.Id == waterTemperatureSensor.Id).OrderBy(m => m.Id).Min(t => (decimal?)t.value)).GetValueOrDefault();

                currentMeasures.lastWeekMaxWaterTempValue = Math.Round(currentLastWeekMaxWaterTempValue, 1);
                currentMeasures.lastWeekMinWaterTempValue = Math.Round(currentLastWeekMinWaterTempValue, 1);

                var currentLastWeekAverageHumidityValue = ((decimal?)db.Measures.Where(m => m.captureDate > aWeekAgo &&
                                   m.productionUnit.Id == currentProductionUnit.Id &&
                                   m.sensor.Id == airHumidity.Id).OrderBy(m => m.Id).Average(t => (decimal?)t.value)).GetValueOrDefault();

                currentMeasures.lastWeekAverageHumidityValue = Math.Round(currentLastWeekAverageHumidityValue, 1);

                var warningEventType = db.EventTypes.Where(p => p.Id == 1).FirstOrDefault();

                var lostSignal = db.Events.Include(ev=> ev.eventType).Where(ev => ev.date > aWeekAgo && ev.eventType.Id == warningEventType.Id && ev.createdBy == "MyFood Bot" &&
                                                   ev.productionUnit.Id == currentProductionUnit.Id && 
                                                  (ev.description.Contains("Déconnectée") || ev.description.Contains("Offine"))).Count();

                currentMeasures.lastWeekLostSignal = lostSignal;

                if (currentProductionUnit.lastMeasureReceived != null)
                    currentMeasures.daysSinceLastSignal = (DateTime.Now - currentProductionUnit.lastMeasureReceived).Value.Days;

                currentMeasures.lastSignalStrenghtReceived = currentProductionUnit.lastSignalStrenghtReceived;
            }
            catch (Exception ex)
            {
                dbLog.Logs.Add(Log.CreateErrorLog("Error on Measures Processing", ex));
                dbLog.SaveChanges();
            }     

            return currentMeasures;
        }
    }

    public class GroupedMeasure
    {
        public Int64 Id { get; set; }

        public decimal? pHvalue { get; set; } = 0;
        public decimal? airTempvalue { get; set; } = 0;
        public decimal? waterTempvalue { get; set; } = 0;
        public decimal? humidityvalue { get; set; } = 0;      
        public decimal? DOvalue { get; set; } = 0;
        public decimal? ORPvalue { get; set; } = 0;

        public decimal? lastWeekMinPHValue { get; set; } = 0;
        public decimal? lastWeekMaxPHValue { get; set; } = 0;
        public decimal? lastWeekPHVariation { get; set; } = 0;
        public bool? lastWeekPHRise { get; set; } = false;
        public bool? lastWeekPHFall { get; set; } = false;
        public decimal? fiveLastDayPHVariation { get; set; } = 0;
        public decimal? lastWeekAveragePHValue { get; set; } = 0;
        public decimal? waterTempValue { get; set; } = 0;
        public decimal? lastWeekMinWaterTempValue { get; set; } = 0;
        public decimal? lastWeekMaxWaterTempValue { get; set; } = 0;
        public decimal? lastWeekMinAirTempValue { get; set; } = 0;
        public decimal? lastWeekMaxAirTempValue { get; set; } = 0;
        public decimal? lastWeekAverageAirTempValue { get; set; } = 0;
        public decimal? lastWeekAverageHumidityValue { get; set; } = 0;
        public string hydroponicTypeName { get; set; } = String.Empty;
        public int? lastWeekLostSignal { get; set; } = 0;
        public string lastSignalStrenghtReceived { get; set; } = String.Empty;
        public int? daysSinceLastSignal { get; set; } = 0;
    }
}