using System;

namespace myfoodapp.Business
{
    public class Log
    {
        public int Id { get; set; }
        public DateTime date { get; set; }
        public string description { get; set; }
        public string stackCall { get; set; }
        public LogType type { get; set; }

        public Log() { }

        public enum LogType
        {
            Information,
            Warning,
            System,
            Error
        }

        public static Log CreateLog(string _description, LogType _logType)
        {
            return new Log()
            {
                description = _description,
                type = _logType
            };
        }

        public static Log CreateErrorLog(string _description, Exception exception)
        {
            return new Log()
            {
                description = String.Format("Exception {0} - {1} - {2}", _description, exception.Message, exception.InnerException),
                stackCall = exception.StackTrace,
                type = LogType.Error
            };
        }
    }
}
