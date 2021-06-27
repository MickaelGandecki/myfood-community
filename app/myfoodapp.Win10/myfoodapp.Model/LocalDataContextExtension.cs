using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myfoodapp.Model
{
    public static class LocalDataContextExtension
    {
        public static void EnsureSeedData(this LocalDataContext context)
        {
            //if (context.Measures.Any())
            //{
            //    context.RemoveRange(context.Measures);
            //    context.SaveChanges();
            //}

            if (!context.SensorTypes.Any())
            {
                context.SensorTypes.Add(new SensorType() { Id = 1, name = "pH Sensor", description = "Common values between 6-7" });
                context.SensorTypes.Add(new SensorType() { Id = 2, name = "Water Temperature Sensor", description = "Common values between 15-30" });
                context.SensorTypes.Add(new SensorType() { Id = 3, name = "Dissolved Oxygen Sensor", description = "Common values between 0-10" });
                context.SensorTypes.Add(new SensorType() { Id = 4, name = "Electro-Conductivity Sensor", description = "Common values between 300-800" });
                context.SensorTypes.Add(new SensorType() { Id = 5, name = "Air Temperature Sensor", description = "Common values between 5-32" });
                context.SensorTypes.Add(new SensorType() { Id = 6, name = "Air Humidity Sensor", description = "Common values between 40-80" });
                context.SensorTypes.Add(new SensorType() { Id = 7, name = "ORP sensor", description = "Common values between 0-280" });

                context.SaveChanges();
            }

#if !ARM
            //if (!context.Measures.Any())
            //{
            //    var phSensor = context.SensorTypes.Where(s => s.Id == 1).FirstOrDefault();
            //    var waterTemperatureSensor = context.SensorTypes.Where(s => s.Id == 2).FirstOrDefault();
            //    var dissolvedOxySensor = context.SensorTypes.Where(s => s.Id == 3).FirstOrDefault();
            //    var ORPSensor = context.SensorTypes.Where(s => s.Id == 4).FirstOrDefault();
            //    var airTemperatureSensor = context.SensorTypes.Where(s => s.Id == 5).FirstOrDefault();

            //    for (int i = 0; i < 100; i++)
            //    {
            //        Random rnd = new Random();
            //        var currentDate = DateTime.Now;
            //        currentDate = currentDate.AddTicks(-(currentDate.Ticks % TimeSpan.TicksPerSecond)).AddMinutes(-10 * i);

            //        decimal phValue = Convert.ToDecimal(Math.Round(7 + Math.Sin(0.5 * i) + 0.1 * rnd.Next(-1, 1), 3));
            //        context.Measures.Add(new Measure() { captureDate = currentDate, value = phValue, sensor = phSensor });

            //        decimal waterTemperatureValue = Convert.ToDecimal(Math.Round(15 + Math.Sin(0.1 * i) + 0.5 * rnd.Next(-1, 1), 3));
            //        context.Measures.Add(new Measure() { captureDate = currentDate, value = waterTemperatureValue, sensor = waterTemperatureSensor });

            //        decimal dissolvedOxyValue = Convert.ToDecimal(Math.Round(250 + Math.Sin(0.01 * i) + 0.5 * rnd.Next(-1, 1), 3));
            //        context.Measures.Add(new Measure() { captureDate = currentDate, value = dissolvedOxyValue, sensor = dissolvedOxySensor });

            //        decimal ORPValue = Convert.ToDecimal(Math.Round(150 + Math.Sin(0.01 * i) + 0.7 * rnd.Next(-1, 1), 3));
            //        context.Measures.Add(new Measure() { captureDate = currentDate, value = ORPValue, sensor = ORPSensor });

            //        decimal airTemperatureValue = Convert.ToDecimal(Math.Round(20 + Math.Sin(0.001 * i) + 0.5 * rnd.Next(-1, 1), 3));
            //        context.Measures.Add(new Measure() { captureDate = currentDate, value = airTemperatureValue, sensor = airTemperatureSensor });
            //    };

            //    context.SaveChanges();

            //}
#endif
        }
      }
    }
