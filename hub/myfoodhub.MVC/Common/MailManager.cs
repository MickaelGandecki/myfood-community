using myfoodapp.Hub.Models;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Web.Configuration;
using System.Linq;

namespace myfoodapp.Hub.Common
{
    public class MailManager
    {
        private static string MailSendGridAPIKey = WebConfigurationManager.AppSettings["mailSendGridAPIKey"];

        private static string TemplateGreehouseOnlineFR = WebConfigurationManager.AppSettings["templateGreehouseOnlineFR"];
        private static string TemplateGreehouseOnlineEN = WebConfigurationManager.AppSettings["templateGreehouseOnlineEN"];
        private static string TemplateGreehouseOnlineDE = WebConfigurationManager.AppSettings["templateGreehouseOnlineDE"];

        private static string TemplateGreehouseOfflineFR = WebConfigurationManager.AppSettings["templateGreehouseOfflineFR"];
        private static string TemplateGreehouseOfflineEN = WebConfigurationManager.AppSettings["templateGreehouseOfflineEN"];
        private static string TemplateGreehouseOfflineDE = WebConfigurationManager.AppSettings["templateGreehouseOfflineDE"];

        private static string TemplatePasswordRecoveryFR = WebConfigurationManager.AppSettings["templatePasswordRecoveryFR"];
        private static string TemplatePasswordRecoveryEN = WebConfigurationManager.AppSettings["templatePasswordRecoveryEN"];
        private static string TemplatePasswordRecoveryDE = WebConfigurationManager.AppSettings["templatePasswordRecoveryDE"];

        private static string TemplateWeeklyMessageFR = WebConfigurationManager.AppSettings["templateWeeklyMessageFR"];
        private static string TemplateWeeklyMessageEN = WebConfigurationManager.AppSettings["templateWeeklyMessageEN"];
        private static string TemplateWeeklyMessageDE = WebConfigurationManager.AppSettings["templateWeeklyMessageDE"];

        public static void PioneerUnitOfflineMessage(ProductionUnit currentProductionUnit)
        {
            var dbLog = new ApplicationDbContext();

            try
            {
                var client = new SendGridClient(MailSendGridAPIKey);
                var from = new EmailAddress("hub@myfood.eu", "Myfood Hub Bot");

                MailTemplateObject obj = new MailTemplateObject();
                var currentTemplate = string.Empty;

                if (currentProductionUnit.owner == null || currentProductionUnit.owner.contactMail == null || currentProductionUnit.owner.contactMail == string.Empty)
                {
                    dbLog.Logs.Add(Log.CreateLog(String.Format("Error with Mail Notification - Mail is missing for {0}", currentProductionUnit.info), Log.LogType.Information));
                    dbLog.SaveChanges();

                    return;
                }

                var pioneerName = string.Format("{0} #{1}", currentProductionUnit.owner.pioneerCitizenName, currentProductionUnit.owner.pioneerCitizenNumber);

                obj.firstName = currentProductionUnit.owner.pioneerCitizenName;
                obj.pioneerNumber = currentProductionUnit.owner.pioneerCitizenNumber.ToString();
                obj.info = currentProductionUnit.info;

                if (currentProductionUnit.productionUnitType.name.ToString().Contains("Family"))
                    obj.isFamily = true;
                else if (currentProductionUnit.productionUnitType.name.ToString().Contains("City"))
                    obj.isFamily = false;
                else
                    obj.isFamily = true;

                List<EmailAddress> tos = new List<EmailAddress>
                {
                  new EmailAddress(currentProductionUnit.owner.contactMail, pioneerName)
                };

                var subject = string.Empty;
                var htmlContent = string.Empty;

                if (currentProductionUnit.owner != null && currentProductionUnit.owner.language != null)
                {
                    switch (currentProductionUnit.owner.language.description)
                    {
                        case "fr":
                            currentTemplate = TemplateGreehouseOfflineFR;
                            break;
                        case "en":
                            currentTemplate = TemplateGreehouseOfflineEN;
                            break;
                        case "de":
                            currentTemplate = TemplateGreehouseOfflineDE;
                            break;
                        default:
                            currentTemplate = TemplateGreehouseOfflineEN;
                            break;
                    }
                }
                else
                {
                    currentTemplate = TemplateGreehouseOfflineEN;
                }

                var msg = MailHelper.CreateSingleTemplateEmailToMultipleRecipients(from, tos, currentTemplate, obj);
                //msg.AddCc("agro@myfood.eu");
                var response = client.SendEmailAsync(msg);
            }
            catch (Exception ex)
            {
                dbLog.Logs.Add(Log.CreateErrorLog(String.Format("Error with Mail Notification"), ex));
                dbLog.SaveChanges();
            }

        }

        public static void PioneerUnitOnlineMessage(ProductionUnit currentProductionUnit)
        {
            var dbLog = new ApplicationDbContext();

            try
            {
                var client = new SendGridClient(MailSendGridAPIKey);
                var from = new EmailAddress("hub@myfood.eu", "Myfood Hub Bot");

                MailTemplateObject obj = new MailTemplateObject();
                var currentTemplate = string.Empty;

                if (currentProductionUnit.owner == null || currentProductionUnit.owner.contactMail == null || currentProductionUnit.owner.contactMail == string.Empty)
                {
                    dbLog.Logs.Add(Log.CreateLog(String.Format("Error with Mail Notification - Mail is missing for {0}", currentProductionUnit.info), Log.LogType.Information));
                    dbLog.SaveChanges();

                    return;
                }

                var pioneerName = string.Format("{0} #{1}", currentProductionUnit.owner.pioneerCitizenName, currentProductionUnit.owner.pioneerCitizenNumber);

                obj.firstName = currentProductionUnit.owner.pioneerCitizenName;
                obj.pioneerNumber = currentProductionUnit.owner.pioneerCitizenNumber.ToString();
                obj.info = currentProductionUnit.info;

                if (currentProductionUnit.productionUnitType.name.ToString().Contains("Family"))
                    obj.isFamily = true;
                else if (currentProductionUnit.productionUnitType.name.ToString().Contains("City"))
                    obj.isFamily = false;
                else
                    obj.isFamily = true;

                List<EmailAddress> tos = new List<EmailAddress>
                {
                  new EmailAddress(currentProductionUnit.owner.contactMail, pioneerName)
                };

                if (currentProductionUnit.owner != null && currentProductionUnit.owner.language != null)
                {
                    switch (currentProductionUnit.owner.language.description)
                    {
                        case "fr":
                            currentTemplate = TemplateGreehouseOnlineFR;
                            break;
                        case "en":
                            currentTemplate = TemplateGreehouseOnlineEN;
                            break;
                        case "de":
                            currentTemplate = TemplateGreehouseOnlineDE;
                            break;
                        default:
                            currentTemplate = TemplateGreehouseOnlineEN;
                            break;
                    }
                }
                else
                {
                    currentTemplate = TemplateGreehouseOnlineEN;
                }

                var msg = MailHelper.CreateSingleTemplateEmailToMultipleRecipients(from, tos, currentTemplate, obj);
                //msg.AddCc("agro@myfood.eu");
                var response = client.SendEmailAsync(msg);
            }
            catch (Exception ex)
            {
                dbLog.Logs.Add(Log.CreateErrorLog(String.Format("Error with Mail Notification"), ex));
                dbLog.SaveChanges();
            }
        }

        public static void PioneerUnitIssueMessage(ProductionUnit currentProductionUnit, string note, string details)
        {
            var dbLog = new ApplicationDbContext();

            try
            {
                var client = new SendGridClient(MailSendGridAPIKey);
                var from = new EmailAddress("hub@myfood.eu", "Myfood Hub Bot");

                var pioneerName = string.Format("{0} #{1}", currentProductionUnit.owner.pioneerCitizenName, currentProductionUnit.owner.pioneerCitizenNumber);

                List<EmailAddress> tos = new List<EmailAddress>
                {
                      new EmailAddress("agro@myfood.eu", pioneerName)
                };

                var subject = string.Empty;
                var htmlContent = string.Empty;

                if (currentProductionUnit.owner != null && currentProductionUnit.owner.language != null)
                {
                    switch (currentProductionUnit.owner.language.description)
                    {
                        case "fr":
                            subject = string.Format("[myfood] Incident enregistré chez {0} #{1}", currentProductionUnit.owner.pioneerCitizenName, currentProductionUnit.owner.pioneerCitizenNumber);
                            htmlContent = string.Format("Bonjour, </br></br>" +
                                                        "La serre {0} #{1} vient d'enregistrer un incident critique.</br>" +
                                                        "Detail : {2} {3}</br></br>" +
                                                        "Bien à vous,", currentProductionUnit.owner.pioneerCitizenName, currentProductionUnit.owner.pioneerCitizenNumber, note, details);
                            break;
                        default:
                            subject = string.Format("[myfood] Issue recorded at {0} #{1}", currentProductionUnit.owner.pioneerCitizenName, currentProductionUnit.owner.pioneerCitizenNumber);
                            htmlContent = string.Format("Hi {0}, </br></br>" +
                                                        "The greenhouse {0} #{1} has met a critical issue.</br>" +
                                                        "Detail : {2} {3}</br></br>" +
                                                        "Have a nice day,", currentProductionUnit.owner.pioneerCitizenName, currentProductionUnit.owner.pioneerCitizenNumber, note, details);
                            break;
                    }
                }
                else
                {
                    subject = string.Format("[myfood] Issue recorded at {0} #{1}", currentProductionUnit.owner.pioneerCitizenName, currentProductionUnit.owner.pioneerCitizenNumber);
                    htmlContent = string.Format("Hi {0}, </br></br>" +
                                                "The greenhouse {0} #{1} has met a critical issue.</br>" +
                                                "Detail : {2} {3}</br></br>" +
                                                "Have a nice day,", currentProductionUnit.owner.pioneerCitizenName, currentProductionUnit.owner.pioneerCitizenNumber, note, details);
                }

                var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, tos, subject, "", htmlContent, false);
                msg.AddCc("agro@myfood.eu");

                var response = client.SendEmailAsync(msg);
            }
            catch (Exception ex)
            {
                dbLog.Logs.Add(Log.CreateErrorLog(String.Format("Error with Mail Notification"), ex));
                dbLog.SaveChanges();
            }

        }

        public static void PioneerUnitWeeklyMessage(ProductionUnit currentProductionUnit, List<RecommandationTemplaceObject> reco)
        {
            var db = new ApplicationDbContext();
            var dbLog = new ApplicationDbContext();

            var infoEventType = db.EventTypes.Where(p => p.Id == 8).FirstOrDefault();

            try
            {
                var client = new SendGridClient(MailSendGridAPIKey);
                var from = new EmailAddress("hub@myfood.eu", "Myfood Hub Bot");

                MailTemplateObject obj = new MailTemplateObject();
                var currentTemplate = string.Empty;

                if (currentProductionUnit.owner == null || currentProductionUnit.owner.contactMail == null || currentProductionUnit.owner.contactMail == string.Empty)
                {
                    dbLog.Logs.Add(Log.CreateLog(String.Format("Error with Mail Notification - Mail is missing for {0}", currentProductionUnit.info), Log.LogType.Information));
                    dbLog.SaveChanges();

                    return;
                }

                var pioneerName = string.Format("{0} #{1}", currentProductionUnit.owner.pioneerCitizenName, currentProductionUnit.owner.pioneerCitizenNumber);

                obj.firstName = currentProductionUnit.owner.pioneerCitizenName;
                obj.pioneerNumber = currentProductionUnit.owner.pioneerCitizenNumber.ToString();
                obj.info = currentProductionUnit.info;

                if (currentProductionUnit.productionUnitType.name.ToString().Contains("Family"))
                    obj.isFamily = true;
                else
                    obj.isFamily = false;

                obj.recommandations = reco.OrderBy(r => r.order).ToList();

                if (obj.recommandations.Count > 0)
                    obj.hasRecommandation = true;
                else
                {
                    obj.hasRecommandation = false;

                    //var content = String.Format("",null);

                    //db.Events.Add(new Event() { date = DateTime.Now, description = content, isOpen = false, productionUnit = currentProductionUnit, eventType = infoEventType, createdBy = "MyFood Bot" });
                    //db.SaveChanges();
                }
                    

                List<EmailAddress> tos = new List<EmailAddress>
                {
                  new EmailAddress(currentProductionUnit.owner.contactMail, pioneerName)
                };

                var subject = string.Empty;
                var htmlContent = string.Empty;

                if (currentProductionUnit.owner != null && currentProductionUnit.owner.language != null)
                {
                    switch (currentProductionUnit.owner.language.description)
                    {
                        case "fr":
                            currentTemplate = TemplateWeeklyMessageFR;
                            break;
                        case "en":
                            currentTemplate = TemplateWeeklyMessageEN;
                            break;
                        case "de":
                            currentTemplate = TemplateWeeklyMessageDE;
                            break;
                        default:
                            currentTemplate = TemplateWeeklyMessageEN;
                            break;
                    }
                }
                else
                {
                    currentTemplate = TemplateGreehouseOfflineEN;
                }

                var msg = MailHelper.CreateSingleTemplateEmailToMultipleRecipients(from, tos, currentTemplate, obj);
                //msg.AddCc("agro@myfood.eu");
                var response = client.SendEmailAsync(msg);
            }
            catch (Exception ex)
            {
                dbLog.Logs.Add(Log.CreateErrorLog(String.Format("Error with Mail Notification"), ex));
                dbLog.SaveChanges();
            }

        }

        public static void ResetPasswordMessage(ProductionUnitOwner currentProductionUnitOwner, string to, string callbackUrl)
        {
            var client = new SendGridClient(MailSendGridAPIKey);
            var from = new EmailAddress("hub@myfood.eu", "Myfood Hub Bot");

            MailTemplateObject obj = new MailTemplateObject();
            var currentTemplate = string.Empty;

            var pioneerName = string.Format("{0} #{1}", currentProductionUnitOwner.pioneerCitizenName, currentProductionUnitOwner.pioneerCitizenNumber);

            obj.firstName = currentProductionUnitOwner.pioneerCitizenName;
            obj.pioneerNumber = currentProductionUnitOwner.pioneerCitizenNumber.ToString();
            obj.callbackurl = callbackUrl;

            List<EmailAddress> tos = new List<EmailAddress>
                {
                  new EmailAddress(currentProductionUnitOwner.contactMail, pioneerName)
                };

            if (currentProductionUnitOwner != null && currentProductionUnitOwner.language != null)
            {
                switch (currentProductionUnitOwner.language.description)
                {
                    case "fr":
                        currentTemplate = TemplatePasswordRecoveryFR;
                        break;
                    case "en":
                        currentTemplate = TemplatePasswordRecoveryEN;
                        break;
                    case "de":
                        currentTemplate = TemplatePasswordRecoveryDE;
                        break;
                    default:
                        currentTemplate = TemplatePasswordRecoveryEN;
                        break;
                }
            }
            else
            {
                currentTemplate = TemplatePasswordRecoveryEN;
            }

            var msg = MailHelper.CreateSingleTemplateEmailToMultipleRecipients(from, tos, currentTemplate, obj);
            //msg.AddCc("agro@myfood.eu");
            var response = client.SendEmailAsync(msg);
        }

    }

    public class MailTemplateObject
    {
        [JsonProperty("firstName")]
        public string firstName { get; set; }

        [JsonProperty("pioneerNumber")]
        public string pioneerNumber;

        [JsonProperty("info")]
        public string info;

        [JsonProperty("isFamily")]
        public bool isFamily;

        [JsonProperty("callbackurl")]
        public string callbackurl;

        [JsonProperty("hasRecommandation")]
        public bool hasRecommandation;

        [JsonProperty("recommandations")]
        public List<RecommandationTemplaceObject> recommandations;
    }

    public class RecommandationTemplaceObject
    {
        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("description")]
        public string description;

        [JsonProperty("url")]
        public string url;

        public int order;
    }

}