using myfoodapp.Hub.Models;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Configuration;
using System.Web.Script.Serialization;

namespace myfoodapp.Hub.Common
{
    public static class NotificationPushManager
    {
        private static string OneSignalAPIKey = WebConfigurationManager.AppSettings["oneSignalAPIKey"];
        private static string OneSignalAPIId = WebConfigurationManager.AppSettings["oneSignalAPIId"];
        private static string WebAppUrl = "https://hub.myfood.eu/";

        public static void PioneerUnitEventMessage(ProductionUnit currentProductionUnit)
        {
            var dbLog = new ApplicationDbContext();
            var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            request.Headers.Add("authorization", String.Format("Basic {0}", OneSignalAPIKey));

            var serializer = new JavaScriptSerializer();

            var obj = new object();

            if (currentProductionUnit.owner != null && currentProductionUnit.owner.language != null)
            {
                switch (currentProductionUnit.owner.language.description)
                {
                    case "fr":
                        obj = new
                        {
                            app_id = OneSignalAPIId,
                            headings = new { en = String.Format("Evénements reçu de votre Unité de Prod. {0}", currentProductionUnit.info) },
                            contents = new { en = "voir les recommandations sur le Hub" },
                            include_player_ids = new string[] { currentProductionUnit.owner.notificationPushKey },
                            url = String.Format(WebAppUrl + @"ProductionUnits/Details/{0}", currentProductionUnit.Id),
                            chrome_web_icon = WebAppUrl + "Content/favicon.ico"
                        };
                        break;
                    default:
                        obj = new
                        {
                            app_id = OneSignalAPIId,
                            headings = new { en = String.Format("Events received from your Production Unit {0}", currentProductionUnit.info) },
                            contents = new { en = "see the recommandations on the Hub" },
                            include_player_ids = new string[] { currentProductionUnit.owner.notificationPushKey },
                            url = String.Format(WebAppUrl + @"ProductionUnits/Details/{0}", currentProductionUnit.Id),
                            chrome_web_icon = WebAppUrl + "Content/favicon.ico"
                        };
                        break;
                }
            }

            var param = serializer.Serialize(obj);
            byte[] byteArray = Encoding.UTF8.GetBytes(param);

            string responseContent = null;

            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

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
                dbLog.Logs.Add(Log.CreateErrorLog(String.Format("Error with Onesignal Push Notification"), ex));
                dbLog.SaveChanges();
            }
        }

        public static void PioneerUnitOwnerFeelingMessage(ProductionUnit currentProductionUnit)
        {
            var dbLog = new ApplicationDbContext();
            var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            request.Headers.Add("authorization", String.Format("Basic {0}", OneSignalAPIKey));

            var serializer = new JavaScriptSerializer();

            var obj = new object();

            var happyLinkActionUrl = WebAppUrl + String.Format("api/push?productionUnitId={0}&eventTypeId=7&eventTypeItemId=51", currentProductionUnit.Id);
            var needHelpActionUrl = WebAppUrl + String.Format("api/push?productionUnitId={0}&eventTypeId=7&eventTypeItemId=55", currentProductionUnit.Id);

            if (currentProductionUnit.owner != null && currentProductionUnit.owner.language != null)
            {
                switch (currentProductionUnit.owner.language.description)
                {
                    case "fr":
                        obj = new
                        {
                            app_id = OneSignalAPIId,
                            headings = new { en = "Alors, comment ça va aujourd'hui ?" },
                            contents = new { en = "envoyé depuis votre Unité de Prod. | Serre Connectée" },
                            chrome_web_icon = WebAppUrl + "Content/favicon.ico",
                            chrome_web_image = WebAppUrl + String.Format("Content/Pictures/Sites/{0}", currentProductionUnit.picturePath),
                            include_player_ids = new string[] { currentProductionUnit.owner.notificationPushKey },
                            url = WebAppUrl + "&_osp=do_not_open",
                            android_background_layout = new { image = WebAppUrl + "Content/miniLogoWhite.jpg", headings_color = "FFFF0000", contents_color = "FF00FF00" },
                            web_buttons = new[] {
                                    new { id = "happy-button", text = "Heureux", icon = WebAppUrl + "Content/Pictures/Feelings/happy.png", url = happyLinkActionUrl},
                                    new { id = "needHelp-button", text = "Besoin d'Aide", icon = WebAppUrl + "Content/Pictures/Feelings/needHelp.png", url = needHelpActionUrl}
                                }
                        };
                        break;
                    default:
                       obj = new
                        {
                            app_id = OneSignalAPIId,
                            headings = new { en = "Tell me, how do you feel today?" } ,
                            contents = new { en = "from your Production Unit | Smart Greenhouse" },
                            chrome_web_icon = WebAppUrl + "Content/favicon.ico",
                            chrome_web_image = WebAppUrl + String.Format("Content/Pictures/Sites/{0}", currentProductionUnit.picturePath),
                            include_player_ids = new string[] { currentProductionUnit.owner.notificationPushKey },
                            url = WebAppUrl + "&_osp=do_not_open",
                            android_background_layout = new { image = WebAppUrl + "Content/miniLogoWhite.jpg", headings_color = "FFFF0000", contents_color = "FF00FF00" },
                            web_buttons = new[] {
                                    new { id = "happy-button", text = "Happy", icon = WebAppUrl + "Content/Pictures/Feelings/happy.png", url = happyLinkActionUrl },
                                    new { id = "needHelp-button", text = "Need help", icon = WebAppUrl + "Content/Pictures/Feelings/needHelp.png", url = needHelpActionUrl }
                                }
                        };
                        break;
                }
            }

            var param = serializer.Serialize(obj);
            byte[] byteArray = Encoding.UTF8.GetBytes(param);

            string responseContent = null;

            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

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
                dbLog.Logs.Add(Log.CreateErrorLog(String.Format("Error with Onesignal Push Notification"), ex));
                dbLog.SaveChanges();
            }

        }

        public static void PioneerUnitOfflineMessage(ProductionUnit currentProductionUnit)
        {
            var dbLog = new ApplicationDbContext();
            var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            request.Headers.Add("authorization", String.Format("Basic {0}", OneSignalAPIKey));

            var serializer = new JavaScriptSerializer();

            var obj = new object();

            if (currentProductionUnit.owner != null && currentProductionUnit.owner.language != null)
            {
                switch (currentProductionUnit.owner.language.description)
                {
                    case "fr":
                        obj = new
                        {
                            app_id = OneSignalAPIId,
                            headings = new { en = String.Format("Aucun message reçu depuis 60 min {0}", currentProductionUnit.info) },
                            contents = new { en = "envoyé depuis votre Unité de Prod. | Serre Connectée" },
                            include_player_ids = new string[] { currentProductionUnit.owner.notificationPushKey },
                            url = WebAppUrl + "&_osp=do_not_open",
                            chrome_web_icon = WebAppUrl + "Content/favicon.ico"
                        };
                        break;
                    default:
                        obj = new
                        {
                            app_id = OneSignalAPIId,
                            headings = new { en = String.Format("No message received since 60 min {0}", currentProductionUnit.info) },
                            contents = new { en = "from your Production Unit | Smart Greenhouse" },
                            include_player_ids = new string[] { currentProductionUnit.owner.notificationPushKey },
                            url = WebAppUrl + "&_osp=do_not_open",
                            chrome_web_icon = WebAppUrl + "Content/favicon.ico"
                        };
                        break;
                }
            }

            var param = serializer.Serialize(obj);
            byte[] byteArray = Encoding.UTF8.GetBytes(param);

            string responseContent = null;

            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

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
                dbLog.Logs.Add(Log.CreateErrorLog(String.Format("Error with Onesignal Push Notification"), ex));
                dbLog.SaveChanges();
            }
        }

        public static void PioneerUnitOnlineMessage(ProductionUnit currentProductionUnit)
        {
            var dbLog = new ApplicationDbContext();
            var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            request.Headers.Add("authorization", String.Format("Basic {0}", OneSignalAPIKey));

            var serializer = new JavaScriptSerializer();

            var obj = new object();

            if (currentProductionUnit.owner != null && currentProductionUnit.owner.language != null)
            {
                switch (currentProductionUnit.owner.language.description)
                {
                    case "fr":
                        obj = new
                        {
                            app_id = OneSignalAPIId,
                            headings = new { en = String.Format("Message reçu à l'instant {0}", currentProductionUnit.info) },
                            contents = new { en = "envoyé depuis votre Unité de Prod. | Serre Connectée" },
                            include_player_ids = new string[] { currentProductionUnit.owner.notificationPushKey },
                            url = WebAppUrl + "&_osp=do_not_open",
                            chrome_web_icon = WebAppUrl + "Content/favicon.ico"
                        };
                        break;
                    default:
                        obj = new
                        {
                            app_id = OneSignalAPIId,
                            headings = new { en = String.Format("Message received just now {0}", currentProductionUnit.info) },
                            contents = new { en = "from your Production Unit | Smart Greenhouse" },
                            include_player_ids = new string[] { currentProductionUnit.owner.notificationPushKey },
                            url = WebAppUrl + "&_osp=do_not_open",
                            chrome_web_icon = WebAppUrl + "Content/favicon.ico"
                        };
                        break;
                }
            }

            var param = serializer.Serialize(obj);
            byte[] byteArray = Encoding.UTF8.GetBytes(param);

            string responseContent = null;

            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

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
                dbLog.Logs.Add(Log.CreateErrorLog(String.Format("Error with Onesignal Push Notification"), ex));
                dbLog.SaveChanges();
            }
        }

    }
}