using myfoodapp.Hub.Models;
using myfoodapp.Hub.Models.OpenData;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace myfoodapp.Hub.Services.OpenData
{
    public class OpenDataService : IDisposable
    {
        private ApplicationDbContext entities;

        public OpenDataService(ApplicationDbContext entities)
        {
            this.entities = entities;
        }

        public IList<OpenProductionUnitViewModel> GetAll()
        {
            IList<OpenProductionUnitViewModel> result = new List<OpenProductionUnitViewModel>();

            result = entities.ProductionUnits.OrderBy(m => m.startDate).Select(pu => new OpenProductionUnitViewModel
            {
                Id = pu.Id,
                startDate = pu.startDate,
                locationLatitude = pu.locationLatitude,
                locationLongitude = pu.locationLongitude,
                version = pu.version,
                info = pu.info,
                //options = pu.options,
                lastMeasureReceived = pu.lastMeasureReceived,
                productionUnitType = pu.productionUnitType.name,
                hydroponicType = pu.hydroponicType.name,
                productionUnitStatus = pu.productionUnitStatus.name,
                pioneerCitizen = pu.owner.pioneerCitizenName,
                pioneerCitizenNumber = pu.owner.pioneerCitizenNumber
            }).ToList();

            return result;
        }

        public IList<SecureProductionUnitViewModel> GetByUser(string userMail)
        {
            IList<SecureProductionUnitViewModel> result = new List<SecureProductionUnitViewModel>();

            result = entities.ProductionUnits
                .OrderBy(m => m.startDate)
                .Where(pu => pu.owner.office365Account.ToLower() == userMail.ToLower()).Select(pu => new SecureProductionUnitViewModel
                {
                    Id = pu.Id,
                    startDate = pu.startDate,
                    locationLatitude = pu.locationLatitude,
                    locationLongitude = pu.locationLongitude,
                    version = pu.version,
                    info = pu.info,
                    lastMeasureReceived = pu.lastMeasureReceived,
                    productionUnitType = pu.productionUnitType.name,
                    hydroponicType = pu.hydroponicType.name,
                    productionUnitStatus = pu.productionUnitStatus.name,
                    pionnerCitizenOffice365Account = pu.owner.office365Account,
                    pioneerCitizen = pu.owner.pioneerCitizenName,
                    pioneerCitizenNumber = pu.owner.pioneerCitizenNumber,
                    picturePath = pu.picturePath
                }).ToList();

            foreach (var res in result)
            {
                var option = entities.OptionLists.Include(o => o.productionUnit).Include(o => o.option)
                    .Where(o => o.productionUnit.Id == res.Id)
                    .Select(o => o.option).ToList();

                res.options = option;
            }

            return result;
        }



        public IList<OpenProductionUnitViewModel> One(int Id)
        {
            IList<OpenProductionUnitViewModel> result = new List<OpenProductionUnitViewModel>();

            result = entities.ProductionUnits.Where(m => m.Id == Id).Select(pu => new OpenProductionUnitViewModel
            {
                Id = pu.Id,
                startDate = pu.startDate,
                locationLatitude = pu.locationLatitude,
                locationLongitude = pu.locationLongitude,
                version = pu.version,
                info = pu.info,
                //options = pu.options,
                lastMeasureReceived = pu.lastMeasureReceived,
                productionUnitType = pu.productionUnitType.name,
                hydroponicType = pu.productionUnitType.name,
                productionUnitStatus = pu.productionUnitStatus.name,
                pioneerCitizen = pu.owner.pioneerCitizenName,
                pioneerCitizenNumber = pu.owner.pioneerCitizenNumber
            }).ToList();

            return result;
        }

        public IList<OpenMeasureViewModel> GetMeasures(int Id)
        {
            IList<OpenMeasureViewModel> result = new List<OpenMeasureViewModel>();

            result = entities.Measures.Where(m => m.productionUnit.Id == Id).OrderByDescending(m => m.captureDate).Take(1000).Select(m => new OpenMeasureViewModel
            {
                Id = m.Id,
                captureDate = m.captureDate,
                value = m.value,
                sensor = m.sensor.name
            }).ToList();

            return result;
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}