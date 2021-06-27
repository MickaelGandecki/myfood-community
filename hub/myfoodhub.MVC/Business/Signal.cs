using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myfoodapp.Hub.Business
{
    public class Signal
    {
        public static string GetSignalStrenghtName(string rawName)
        {
            string icoName = "no-signal";

            switch (rawName)
            {
                case "Excellent":
                    icoName = "good";
                    break;
                case "Good":
                    icoName = "good";
                    break;
                case "Average":
                    icoName = "average";
                    break;
                case "Limit":
                    icoName = "limit";
                    break;
                case "Wifi":
                    icoName = "good";
                    break;
                default:
                    break;
            }

            return String.Format("/Content/Pictures/Signals/strenght-{0}.png", icoName);
        }
    }
}