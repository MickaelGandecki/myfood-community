using myfoodapp.Hub.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace myfoodapp.Hub.Services
{
    public class RecognitionRecordService : IDisposable
    {
        private ApplicationDbContext entities;

        public RecognitionRecordService(ApplicationDbContext entities)
        {
            this.entities = entities;
        }

        public IList<RecognitionRecordViewModel> GetAll(int productionUnitId)
        {
            IList<RecognitionRecordViewModel> result = new List<RecognitionRecordViewModel>();

            result = entities.RecognitionRecords.OrderBy(m => m.date).Select(rec => new RecognitionRecordViewModel
            {
                Id = rec.Id,
                date = rec.date,
                token = rec.token,
                dailyProduction = rec.dailyProduction,
                imagePath = rec.imagePath,

                productionLevelId = rec.productionLevel.Id,
                productionLevel = new ProductionLevelViewModel()
                {
                    Id = rec.productionLevel.Id,
                    name = rec.productionLevel.name
                },

                healthLevelId = rec.healthLevel.Id,
                healthLevel = new HealthLevelViewModel()
                {
                    Id = rec.healthLevel.Id,
                    name = rec.healthLevel.name
                },

                productionUnitId = rec.productionUnit.Id,
                productionUnit = new ProductionUnitViewModel()
                {
                    Id = rec.productionUnit.Id,
                    reference = rec.productionUnit.reference
                },

            }).ToList();

            return result;
        }


        public RecognitionRecordViewModel One(int productionUnitId)
        {
            IList<RecognitionRecordViewModel> result = new List<RecognitionRecordViewModel>();

            result = entities.RecognitionRecords.Include(m => m.productionUnit)
                                             .Include(m => m.healthLevel)
                                             .Include(m => m.productionLevel)
                                             .Where(p => p.Id == productionUnitId).Select(rec => new RecognitionRecordViewModel
                                             {
                                                 Id = rec.Id,
                                                 date = rec.date,
                                                 token = rec.token,
                                                 dailyProduction = rec.dailyProduction,
                                                 imagePath = rec.imagePath,

                                                 productionLevelId = rec.productionLevel.Id,
                                                 productionLevel = new ProductionLevelViewModel()
                                                 {
                                                     Id = rec.productionLevel.Id,
                                                     name = rec.productionLevel.name
                                                 },

                                                 healthLevelId = rec.healthLevel.Id,
                                                 healthLevel = new HealthLevelViewModel()
                                                 {
                                                     Id = rec.healthLevel.Id,
                                                     name = rec.healthLevel.name
                                                 },

                                                 productionUnitId = rec.productionUnit.Id,
                                                 productionUnit = new ProductionUnitViewModel()
                                                 {
                                                     Id = rec.productionUnit.Id,
                                                     reference = rec.productionUnit.reference
                                                 },

                                             }).ToList();

            return result.FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}