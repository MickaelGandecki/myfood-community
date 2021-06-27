using Microsoft.AspNetCore.Http;
using myfoodapp.Hub.Models;
using System;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace myfoodapp.Hub.Controllers.Api
{
    public class PushController : ApiController
    {
        // POST api/<controller>
        //[Authorize]
        public HttpResponseMessage Get(int productionUnitId, int eventTypeId, int eventTypeItemId)
        {
            var db = new ApplicationDbContext();
            var dbLog = new ApplicationDbContext();

            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");

            try
            {
                var currentProductionUnit = db.ProductionUnits.Include(p => p.owner.language).Where(p => p.Id == productionUnitId).FirstOrDefault();

                if(currentProductionUnit == null)
                {
                    dbLog.Logs.Add(Log.CreateLog("Production Unit not found from Push Message", Log.LogType.Warning));
                    dbLog.SaveChanges();

                    return null;
                }

                var currentEventType = db.EventTypes.Where(p => p.Id == eventTypeId).FirstOrDefault();

                if (currentEventType == null)
                {
                    dbLog.Logs.Add(Log.CreateLog("Event Type not found from Push Message", Log.LogType.Warning));
                    dbLog.SaveChanges();

                    return null;
                }

                var currentEventTypeItem = db.EventTypeItems.Where(p => p.Id == eventTypeItemId).FirstOrDefault();

                if (currentEventTypeItem == null)
                {
                    dbLog.Logs.Add(Log.CreateLog("Event Type Item not found from Push Message", Log.LogType.Warning));
                    dbLog.SaveChanges();

                    return null;
                }

                var currentProductionUnitOwner = currentProductionUnit.owner;

                if (currentProductionUnitOwner != null && currentProductionUnitOwner.language != null)
                {

                    i18n.HttpContextExtensions.SetPrincipalAppLanguageForRequest(
                    System.Web.HttpContext.Current,
                    i18n.LanguageHelpers.GetMatchingAppLanguage(currentProductionUnitOwner.language.description), true);
                }

                var descriptionLocalised = i18n.HttpContextExtensions.ParseAndTranslate(System.Web.HttpContext.Current, currentEventTypeItem.name);

                db.Events.Add(new Event() { date = DateTime.Now, description = descriptionLocalised, productionUnit = currentProductionUnit, eventType = currentEventType, createdBy = "Push Notif." });
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                dbLog.Logs.Add(Log.CreateErrorLog("Error on Push Message", ex));
                dbLog.SaveChanges();
            }

            var response = new HttpResponseMessage();
            response.Content = new StringContent(@"<html><script type=""text/javascript"">window.close();</script><body></body></html>");
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }

        public class JavaScriptResult : ContentResult
        {
            public JavaScriptResult(string script)
            {
                this.Content = script;
                this.ContentType = "application/javascript";
            }
        }
    }
}