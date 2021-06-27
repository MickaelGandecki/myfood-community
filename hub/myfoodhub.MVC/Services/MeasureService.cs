using myfoodapp.Hub.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace myfoodapp.Hub.Services
{
    public class MeasureService : IDisposable
    {
        private ApplicationDbContext entities;

        public MeasureService(ApplicationDbContext entities)
        {
            this.entities = entities;
        }

        public IList<MeasureViewModel> GetAll()
        {
            IList<MeasureViewModel> result = new List<MeasureViewModel>();

            result = entities.Measures.OrderByDescending(m => m.captureDate).Take(1000).Select(meas => new MeasureViewModel
            {
                Id = meas.Id,
                captureDate = meas.captureDate,
                value = meas.value,
                sensorId = meas.sensor.Id,
                sensor = new SensorTypeViewModel()
                {
                    Id = meas.sensor.Id,
                    name = meas.sensor.name
                },
                productionUnitId = meas.productionUnit.Id,
                productionUnit = new ProductionUnitViewModel()
                {
                    Id = meas.productionUnit.Id,
                    info = meas.productionUnit.info,
                    locationLatitude = meas.productionUnit.locationLatitude,
                    locationLongitude = meas.productionUnit.locationLongitude
                },

            }).ToList();

            return result;
        }

        public IEnumerable<MeasureViewModel> Read()
        {
            return GetAll();
        }

        public IEnumerable<MeasureViewModel> Read(int currentProductionUnitId)
        {
            IList<MeasureViewModel> result = new List<MeasureViewModel>();

            result = entities.Measures.Where(m => m.productionUnit.Id == currentProductionUnitId)
                                      .OrderByDescending(p => p.Id)
                                      .Select(meas => new MeasureViewModel
                                      {
                                          Id = meas.Id,
                                          captureDate = meas.captureDate,
                                          value = meas.value,
                                          sensorId = meas.sensor.Id,
                                          sensor = new SensorTypeViewModel()
                                          {
                                              Id = meas.sensor.Id,
                                              name = meas.sensor.name
                                          },
                                          productionUnitId = meas.productionUnit.Id,
                                          productionUnit = new ProductionUnitViewModel()
                                          {
                                              Id = meas.productionUnit.Id,
                                              info = meas.productionUnit.info,
                                              locationLatitude = meas.productionUnit.locationLatitude,
                                              locationLongitude = meas.productionUnit.locationLongitude
                                          },

                                      }).ToList();

            return result;
        }

        public IEnumerable<MeasureViewModel> Read(SensorTypeEnum sensorType, double productionUnitLat, double productionUnitLong)
        {
            return GetAll().Where(m => m.sensorId == (int)sensorType && 
                                       m.productionUnit.locationLatitude == productionUnitLat &&
                                       m.productionUnit.locationLongitude == productionUnitLong).Take(24);
        }

        public IEnumerable<MeasureViewModel> Read(SensorTypeEnum sensorType, int currentProductionUnitId, string range)
        {
            IList<MeasureViewModel> result = new List<MeasureViewModel>();

            var fewDaysAgo = DateTime.Now.AddDays(-6);

            var aWeekAgo = DateTime.Now.AddDays(-14);

            var threeMonthsAgo = DateTime.Now.AddDays(-90);

            var selectedTimeFrame = new DateTime();

            if(range == "thisLastDays" || range == null || range == "")
            {
                selectedTimeFrame = fewDaysAgo;
            }

            if (range == "thisWeek")
            {
                selectedTimeFrame = aWeekAgo;
            }

            if (range == "lastThreeMonths")
            {
                selectedTimeFrame = threeMonthsAgo;
            }

            if (range == "lastThreeMonths")
            {
                result = entities.Measures.OrderByDescending(m => m.captureDate)
                .Where(m => m.sensor.Id == (int)sensorType && m.productionUnit.Id == currentProductionUnitId && m.captureDate > selectedTimeFrame && m.captureDate.Hour == 9 && m.captureDate.Minute < 15)
                .Select(meas => new MeasureViewModel
                {
                    Id = meas.Id,
                    captureDate = meas.captureDate,
                    value = meas.value,
                    sensorId = meas.sensor.Id,
                    sensor = new SensorTypeViewModel()
                    {
                        Id = meas.sensor.Id,
                        name = meas.sensor.name
                    },
                    productionUnitId = meas.productionUnit.Id,
                    productionUnit = new ProductionUnitViewModel()
                    {
                        Id = meas.productionUnit.Id,
                        info = meas.productionUnit.info,
                        locationLatitude = meas.productionUnit.locationLatitude,
                        locationLongitude = meas.productionUnit.locationLongitude
                    },

                }).ToList();
            }
            else
            {
               result = entities.Measures.OrderByDescending(m => m.captureDate)
              .Where(m => m.sensor.Id == (int)sensorType && m.productionUnit.Id == currentProductionUnitId && m.captureDate > selectedTimeFrame)
              .Select(meas => new MeasureViewModel
              {
                  Id = meas.Id,
                  captureDate = meas.captureDate,
                  value = meas.value,
                  sensorId = meas.sensor.Id,
                  sensor = new SensorTypeViewModel()
                  {
                      Id = meas.sensor.Id,
                      name = meas.sensor.name
                  },
                  productionUnitId = meas.productionUnit.Id,
                  productionUnit = new ProductionUnitViewModel()
                  {
                      Id = meas.productionUnit.Id,
                      info = meas.productionUnit.info,
                      locationLatitude = meas.productionUnit.locationLatitude,
                      locationLongitude = meas.productionUnit.locationLongitude
                  },

              }).ToList();

            }

            return result;
        }

        public void Create(MeasureViewModel measure)
        {
            var entity = new Measure();

            entity.Id = measure.Id;
            entity.captureDate = measure.captureDate;
            entity.value = measure.value;

            if (entity.sensor == null)
            {
                entity.sensor = new SensorType();
                entity.sensor.Id = measure.sensorId;
            }

            if (entity.productionUnit == null)
            {
                entity.productionUnit = new ProductionUnit();
                entity.productionUnit.Id = measure.productionUnitId;
            }

            entities.Measures.Add(entity);
            entities.SaveChanges();

            measure.Id = entity.Id;
        }

        public void Update(MeasureViewModel measure)
        {
            Measure target = new Measure();
            target = entities.Measures.Where(m => m.Id == measure.Id).Include(m => m.sensor).FirstOrDefault();

            if (target != null)
            {
                target.captureDate = measure.captureDate;
                target.value = measure.value;

                SensorType currentSensorType = new SensorType();
                currentSensorType = entities.SensorTypes.Where(m => m.Id == measure.sensorId).FirstOrDefault();

                target.sensor = currentSensorType;

                ProductionUnit currentProductionUnit = new ProductionUnit();
                currentProductionUnit = entities.ProductionUnits.Where(m => m.Id == measure.productionUnitId).FirstOrDefault();

                target.productionUnit = currentProductionUnit;
            }      

                entities.SaveChanges();  
        }

        public void Destroy(MeasureViewModel message)
        {
                var entity = new Measure();

                entity.Id = message.Id;

                entities.Measures.Attach(entity);
                entities.Measures.Remove(entity);

                entities.SaveChanges();
        }

        public MeasureViewModel One(Func<MeasureViewModel, bool> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}