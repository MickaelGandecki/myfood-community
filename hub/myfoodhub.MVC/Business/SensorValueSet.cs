using myfoodapp.Hub.Models;
using System;
using System.Linq;

namespace myfoodapp.Hub.Business
{
    public class SensorValueSet
    {
        public decimal CurrentValue { get; set; }
        public string CurrentCaptureTime { get; set; }
        public decimal AverageHourValue { get; set; }
        public decimal AverageDayValue { get; set; }
        public string LastDayCaptureTime { get; set; }
    }

    public static class SensorValueManager
    {
        public static SensorValueSet GetSensorValueSet(int currentProductionUnitId, SensorTypeEnum sensor, ApplicationDbContext db)
       {
        var currentValue = 0M;
        var averageHourValue = 0M;
        var averageDayValue = 0M;

        var currentCaptureTime = String.Empty;
        var lastDayCaptureTime = String.Empty;

        var lastValue = db.Measures.Where(m => m.productionUnit.Id == currentProductionUnitId
                                          && m.sensor.Id == (int)sensor)
                                   .OrderByDescending(m => m.captureDate).FirstOrDefault();
        if (lastValue != null)
        {
            TimeZoneInfo rst = TimeZoneInfo.FindSystemTimeZoneById("Romance Standard Time");
            currentCaptureTime = TimeZoneInfo.ConvertTimeFromUtc(lastValue.captureDate, rst).ToShortTimeString();

            currentValue = lastValue.value;

            var lastHour = lastValue.captureDate.AddHours(-1);

            var averageHourValueRslt = db.Measures.Where(m => m.productionUnit.Id == currentProductionUnitId
                                                  && m.sensor.Id == (int)sensor
                                                  && m.captureDate >= lastHour)
                                                 .OrderByDescending(m => m.captureDate);

            averageHourValue = Math.Round(averageHourValueRslt.Average(m => m.value), 1);

            var lastDay = lastValue.captureDate.AddDays(-1);
            lastDayCaptureTime = lastDay.ToShortDateString();

            var averageDayValueRslt = db.Measures.Where(m => m.productionUnit.Id == currentProductionUnitId
                                                  && m.sensor.Id == (int)sensor
                                                  && m.captureDate >= lastDay)
                                                 .OrderByDescending(m => m.captureDate);

            averageDayValue = Math.Round(averageDayValueRslt.Average(m => m.value), 1);

            if(Math.Abs((lastDay - DateTime.Now).TotalDays) > 3)
              return new SensorValueSet()
              {
                  CurrentValue = currentValue,
                  CurrentCaptureTime = "-",
                  AverageHourValue = averageHourValue,
                  AverageDayValue = averageDayValue,
                  LastDayCaptureTime = "-"
              };

            return new SensorValueSet()
            {
                CurrentValue = currentValue,
                CurrentCaptureTime = currentCaptureTime,
                AverageHourValue = averageHourValue,
                AverageDayValue = averageDayValue,
                LastDayCaptureTime = lastDayCaptureTime
            };
        }

        return new SensorValueSet()
        {
            CurrentValue = 0,
            CurrentCaptureTime = "-",
            AverageHourValue = 0,
            AverageDayValue = 0,
            LastDayCaptureTime = "-"
        };
    }
}
}