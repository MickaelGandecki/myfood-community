using Microsoft.EntityFrameworkCore;
using myfoodapp.Business;
using myfoodapp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myfoodapp.Model
{
    public class DatabaseModel
    {
        private static readonly AsyncLock asyncLock = new AsyncLock();
        private LogModel logModel = LogModel.GetInstance;

        private static DatabaseModel instance;

        public static DatabaseModel GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DatabaseModel();
                }
                return instance;
            }
        }

        private LocalDataContext db;

        private DatabaseModel()
        {
            db = new LocalDataContext();
        }

        public async Task SetLastCalibrationDate(SensorTypeEnum sensorType, DateTime currentDate)
        {
            using (await asyncLock.LockAsync())
            {
                try
                {
                    var currentSensorType = await (from m in db.SensorTypes where m.Id == (int)sensorType select m).FirstOrDefaultAsync();

                    if (currentSensorType != null)
                    {
                        currentSensorType.lastCalibration = currentDate;
                        await db.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    logModel.AppendLog(Log.CreateErrorLog("Exception on Database - Set Last Calibration Date", ex));
                }

            }
        }

        public async Task<String> GetLastCalibrationDate(SensorTypeEnum sensorType)
        {
            using (await asyncLock.LockAsync())
            {
                var currentDateTime = await (from m in db.SensorTypes where m.Id == (int)sensorType select m.lastCalibration).FirstOrDefaultAsync();

                if (currentDateTime.Value == null)
                    return "No yet calibrated";
                else
                    return currentDateTime.Value.ToString("d");
            }
        }

        public async Task<bool> DeleteAllMesures()
        {
            using (await asyncLock.LockAsync())
            {
                var task = Task.Run(async () =>
                {
                        try
                        {
                            if (db.Measures.Any())
                            {
                                db.RemoveRange(db.Measures);
                                await db.SaveChangesAsync();
                            }
                    }
                    catch (Exception ex)
                        {
                            logModel.AppendLog(Log.CreateErrorLog("Exception on Database", ex));
                        }
                });
                task.Wait();
            }
            return true;
        }

        public async Task<DateTime> GetLastMesureDateTime()
        {
            using (await asyncLock.LockAsync())
            {
                var currentDateTime = await (from m in db.Measures orderby m.Id descending select m.captureDate).FirstOrDefaultAsync();

                if (currentDateTime == null)
                    return new DateTime(2016, 8, 10, 0, 0, 0);
                else
                    return currentDateTime;
            }
        }

        public async Task<Decimal> GetLastMesure(SensorTypeEnum sensorType)
        {
            using (await asyncLock.LockAsync())
            {
                var rslt = await (from m in db.Measures.Include(m => m.sensor)
                          .Where(m => m.sensor.Id == (int)sensorType)
                          .OrderByDescending(m => m.Id)
                           select m).FirstOrDefaultAsync();

                if (rslt != null)
                {
                    if(sensorType == SensorTypeEnum.humidity)
                        return Math.Round(rslt.value, 0);
                    else
                        return Math.Round(rslt.value, 1);
                }
                else
                    return 0;
            }
        }

        public async Task<string> GetLastMesureSignature()
        {
            StringBuilder valueSignature = new StringBuilder();

            using (await asyncLock.LockAsync())
            {
                var allValues = Enum.GetValues(typeof(SensorTypeEnum));

                foreach (SensorTypeEnum sensorType in Enum.GetValues(typeof(SensorTypeEnum)))
                {
                    if(sensorType != SensorTypeEnum.orp)
                    {
                        var rslt = await (from m in db.Measures.Include(m => m.sensor)
                              .Where(m => m.sensor.Id == (int)sensorType)
                              .OrderByDescending(m => m.Id)
                                          select m).FirstOrDefaultAsync();

                        if (rslt != null)
                            valueSignature.Append(Math.Round(rslt.value, 1).ToString("000.0").Replace(".", "").Replace("-", "B"));
                        else
                            valueSignature.Append("AAAA");
                    }
                }

                return valueSignature.ToString();
            }
        }

        public async Task<Decimal> GetYesterdayMesure(SensorTypeEnum sensorType)
        {
            using (await asyncLock.LockAsync())
            {

                if (await db.Measures.CountAsync() == 0)
                return 0;

            var rslt = await (from m in db.Measures.Include(m => m.sensor)
                              .Where(m => m.sensor.Id == (int)sensorType)
                              .OrderBy(m => m.Id)
                              select m.value).Take(6 * 24).FirstOrDefaultAsync();

 
            return Math.Round(rslt, 1);

            }
        }

        public async Task<Decimal> GetLastDayMinMesure(SensorTypeEnum sensorType)
        {
            using (await asyncLock.LockAsync())
            {
                if (await db.Measures.CountAsync() == 0)
                return 0;

            var rslt = await (from m in db.Measures.Include(m => m.sensor)
                           .Where(m => m.sensor.Id == (int)sensorType)
                           .OrderByDescending(m => m.Id)
                              select m).Take(6 * 24).MinAsync(m => m.value);

                return Math.Round(rslt, 1);

            }
        }

        public async Task<Decimal> GetLastDayMaxMesure(SensorTypeEnum sensorType)
        {
            using (await asyncLock.LockAsync())
            {

                if (await db.Measures.CountAsync() == 0)
                return 0;

            var rslt = await (from m in db.Measures.Include(m => m.sensor)
                          .Where(m => m.sensor.Id == (int)sensorType)
                          .OrderByDescending(m => m.Id)
                              select m).Take(6 * 24).MaxAsync(m => m.value);

                return Math.Round(rslt, 1);
            }
        }

        public async Task<Decimal> GetLastDayAverageMesure(SensorTypeEnum sensorType)
        {
            using (await asyncLock.LockAsync())
            {
                if (await db.Measures.CountAsync() == 0)
                return 0;

            var rslt = await (from m in db.Measures.Include(m => m.sensor)
                          .Where(m => m.sensor.Id == (int)sensorType)
                          .OrderByDescending(m => m.Id)
                              select m).Take(6 * 24).AverageAsync(m => m.value);

                return Math.Round(rslt, 1);

            }
        }

        public async Task<List<Measure>> GetLastDayMesures(SensorTypeEnum sensorType)
        {
            using (await asyncLock.LockAsync())
            {
                return await (from m in db.Measures.OrderByDescending(m => m.Id).Include(m => m.sensor)
                              select m).Where(m => m.sensor.Id == (int)sensorType).Take(24 * 6).ToListAsync();
            }          
        }
        public async Task<List<Measure>> GetLastWeeksMesures(SensorTypeEnum sensorType)
        {
            using (await asyncLock.LockAsync())
            {
                return await (from m in db.Measures.OrderByDescending(m => m.Id).Include(m => m.sensor).Take(7 * 24 * 6 * 4)
                              select m).Where(m => m.sensor.Id == (int)sensorType).ToListAsync();
            }
        }

        public async Task<List<Measure>> GetLastWeeksMesures()
        {
            using (await asyncLock.LockAsync())
            {
                return await (from m in db.Measures.OrderByDescending(m => m.Id)
                              select m).Take(4 * 7 * 24 * 6 * 6).ToListAsync();
            }
        }

        public async Task<List<Measure>> GetLastTwoMonthsMesures(SensorTypeEnum sensorType)
        {
            using (await asyncLock.LockAsync())
            {
                return await (from m in db.Measures.Include(m => m.sensor).Where(m => m.sensor.Id == (int)sensorType).Take(7 * 30 * 2)
                              group m by new { m.captureDate.Year, m.captureDate.DayOfYear} into groupedDay
                              select new Measure
                              {
                                  captureDate = groupedDay.FirstOrDefault().captureDate,
                                  value = groupedDay.Average(x => x.value),
                              }).ToListAsync();

            }
        }

        public async Task<bool> AddMesure(DateTime currentDate, decimal capturedValue, SensorTypeEnum sensorType)
        {
            using (await asyncLock.LockAsync())
            {
                var task = Task.Run(async () =>
                {
                        try
                        {
                            var currentSensor = db.SensorTypes.Where(s => s.Id == (int)sensorType).FirstOrDefault();

                            db.Measures.Add(new Measure() { value = capturedValue, captureDate = currentDate, sensor = currentSensor });
                            await db.SaveChangesAsync();
                        }
                        catch (Exception ex)
                        {
                            logModel.AppendLog(Log.CreateErrorLog("Exception on Database - Add Mesure", ex));
                        }
                });
                task.Wait();
            }

            return true;
        }

    }
}