using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myfoodapp.Common
{
        [EventSource(Name = "MyfoodLogProvider")]
        sealed public class MyfoodLogProvider : EventSource
        {
            public static MyfoodLogProvider Log = new MyfoodLogProvider();

            [Event(1, Level = EventLevel.Verbose, Channel = EventChannel.Debug)]
            public void Debug(string message)
            {
                WriteEvent(1, message);
            }

            [Event(2, Level = EventLevel.Informational, Channel = EventChannel.Debug)]
            public void Info(string message)
            {
                WriteEvent(2, message);
            }

            [Event(3, Level = EventLevel.Warning, Channel = EventChannel.Debug)]
            public void Warn(string message)
            {
                WriteEvent(3, message);
            }

            [Event(4, Level = EventLevel.Error, Channel = EventChannel.Debug)]
            public void Error(string message)
            {
                WriteEvent(4, message);
            }

            [Event(5, Level = EventLevel.Critical, Channel = EventChannel.Debug)]
            public void Critical(string message)
            {
                WriteEvent(5, message);
            }
        }
}

