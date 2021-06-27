using myfoodapp.Hub.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Configuration;

namespace myfoodapp.Hub.Business
{
    public class ExternalAirHumidityManager
    {
        public static void GetExternalAirHumidityValues(int productionUnitId, DateTime currentDate)
        {
            var db = new ApplicationDbContext();
            var dbLog = new ApplicationDbContext();

            NumberStyles style;
            CultureInfo culture;

            style = NumberStyles.AllowDecimalPoint;
            culture = CultureInfo.CreateSpecificCulture("en-US");

            var currentProductionUnit = db.ProductionUnits.Where(p => p.Id == productionUnitId).FirstOrDefault();

            if (currentProductionUnit == null)
                return;

            var apiKey = WebConfigurationManager.AppSettings["openWeatherMapAPIKey"];

            var apiCall = String.Format("http://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&APPID={2}", currentProductionUnit.locationLatitude, currentProductionUnit.locationLongitude, apiKey);

            var request = WebRequest.Create(apiCall) as HttpWebRequest;

            string responseContent = null;

            try
            {
                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                dbLog.Logs.Add(Log.CreateLog("Error on Open Weather Map WebRequest", Log.LogType.Warning));
                dbLog.SaveChanges();

                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
            }

            try
            {
                System.Diagnostics.Debug.WriteLine(responseContent);

                var externalAirTemperature = db.SensorTypes.Where(s => s.Id == 7).FirstOrDefault();
                var externalAirHumidity = db.SensorTypes.Where(s => s.Id == 8).FirstOrDefault();

                dynamic data = JObject.Parse(responseContent);

                var strExternalAirTemp = data.main.temp.ToString();

                var strExternalHumidity = data.main.humidity.ToString();

                decimal externalAirTemp = 0;
                if (decimal.TryParse(strExternalAirTemp, style, culture, out externalAirTemp))
                {
                    decimal convTemp = externalAirTemp - 273.16M;

                    db.Measures.Add(new Measure() { captureDate = currentDate, productionUnit = currentProductionUnit, sensor = externalAirTemperature, value = convTemp });
                    db.SaveChanges();
                }
                else
                {
                    dbLog.Logs.Add(Log.CreateLog(String.Format("Error on Convert external air temp. - {0}", strExternalAirTemp), Log.LogType.Warning));
                    dbLog.SaveChanges();
                }

                decimal externalHumidity = 0;
                if (decimal.TryParse(strExternalHumidity, style, culture, out externalHumidity))
                {
                    db.Measures.Add(new Measure() { captureDate = currentDate, productionUnit = currentProductionUnit, sensor = externalAirHumidity, value = externalHumidity });
                    db.SaveChanges();
                }
                else
                {
                    dbLog.Logs.Add(Log.CreateLog(String.Format("Error on Convert external air humidity - {0}", strExternalHumidity), Log.LogType.Warning));
                    dbLog.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                dbLog.Logs.Add(Log.CreateErrorLog("Error on Convert external air temp. & humidity value", ex));
                dbLog.SaveChanges();
            }

        }
    }
}