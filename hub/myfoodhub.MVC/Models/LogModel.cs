using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace myfoodapp.Hub.Models
{
    public class Log
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime date { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public string stackCall { get; set; }

        private Log() { }

        public enum LogType
        {
            Information,
            Warning,
            System,
            Error,
            Message
        }

        public static Log CreateLog(string _description, LogType _logType)
        {
            string logTypeDesc = string.Empty;

            switch (_logType)
            {
                case LogType.Information:
                    logTypeDesc = "INFO";
                    break;
                case LogType.Warning:
                    logTypeDesc = "WARNING";
                    break;
                case LogType.System:
                    logTypeDesc = "SYSTEM";
                    break;
                case LogType.Error:
                    logTypeDesc = "ERROR";
                    break;
                case LogType.Message:
                    logTypeDesc = "MESSAGE";
                    break;
                default:
                    break;
            }

            return new Log()
            {
                date = DateTime.Now,
                description = _description,
                type = logTypeDesc
            };
        }

        public static Log CreateErrorLog(string _description, Exception exception)
        {
            string logTypeDesc = string.Empty;

            return new Log()
            {
                date = DateTime.Now,
                description = String.Format("Exception {0} - {1} - {2}", _description, exception.Message, exception.InnerException),
                stackCall = exception.StackTrace,
                type = "ERROR"
            };
        }

    }
}